using KWDMAktywnosc.Core.Models;
using KWDMAktywnosc.Core.Models.KMeans;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using System.IO;
using System.Diagnostics;

namespace KWDMAktywnosc.Core.Services.Implementation
{
    public class KMeansClusteringService : IKMeansClusteringService
    {
        private readonly IInputReaderService inputReaderService;

        //5 clusters - standing, sitting down, sitting (still), sitting up, walk
        public KMeansClusteringService(IInputReaderService inputReaderService)
        {
            this.inputReaderService = inputReaderService;
        }

        private Features ComputeDataFeatures(List<Reading> readings, ReadingType readingType)
        {
            var singleSensorReading = readings.Where(x => x.ReadingType == readingType).ToList();

            var featuresList = new List<Features>();

            var xAxisReading = singleSensorReading.Select(x => x.X).ToList();
            var yAxisReading = singleSensorReading.Select(x => x.Y).ToList();
            var zAxisReading = singleSensorReading.Select(x => x.Z).ToList();

            var xyzReadings = xAxisReading
                .Concat(yAxisReading).Concat(zAxisReading).ToList();

            var maximum = xyzReadings.Max();
            var minimum = xyzReadings.Min();
            var mean = xyzReadings.Average();
            var std = Statistics.StandardDeviation(xyzReadings);
            var percentile20 = Statistics.Percentile(xyzReadings, 20);
            var percentile50 = Statistics.Percentile(xyzReadings, 50);
            var percentile80 = Statistics.Percentile(xyzReadings, 80);
            var xyzReadingsDouble = xyzReadings.ConvertAll(x => (double)x);
            var skewness = Statistics.Skewness(xyzReadingsDouble);
            var kurtosis = Statistics.Kurtosis(xyzReadingsDouble);

            var features = new Features() 
            {
                Maximum = maximum,
                Minimum = minimum,
                Mean = mean,
                StandardDevation = (float)std, 
                Percentile20 = percentile20,
                Percentile50 = percentile50,
                Percentile80 = percentile80,
                Skewness = (float)skewness,
                Kurtosis = (float)kurtosis,
            };
            return features;
        }

        public KMeansResult PerformKMeans(List<Reading> readings, ReadingType readingType)
        {
            var sw = new Stopwatch();
            //get features for input and all train data
            var testFeatures = ComputeDataFeatures(readings, readingType);
            //prepare files for learning
            var path = Path.Combine(Environment.CurrentDirectory, @"LearningData\");
            var files = Directory.GetFiles(path, "*.txt");
            var learningFeatures = new List<Features>();
            sw.Start();
            foreach (var file in files)
            {
                var reading = inputReaderService.ReadSensorsInput(file);
                var learningFeature = ComputeDataFeatures(reading, readingType);
                learningFeatures.Add(learningFeature);
            }
            sw.Stop();
            var time = sw.ElapsedMilliseconds;

            var ml = new MLContext();
            var trainingData = ml.Data.LoadFromEnumerable(learningFeatures);
            //train on all data
            //NOTE: default cluster number set to 5, just like we wanted
            var featuresColumnName = "Features";
            var pipeline = ml.Transforms
                .Concatenate(featuresColumnName, "Maximum", "Minimum", "Mean", "StandardDevation",
                "Percentile20", "Percentile50", "Percentile80", "Skewness", "Kurtosis")
                .Append(ml.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 5));
            var model = pipeline.Fit(trainingData);
            //test data
            var testFeaturesEnumerable = new List<Features>() { testFeatures };
            //predict
            var predictor = ml.Model.CreatePredictionEngine<Features, ClusterPrediction>(model);
            var prediction = predictor.Predict(testFeatures);

            var modelParams = model.LastTransformer.Model;

            return new KMeansResult { Predction = prediction, ModelParameters = modelParams };
        }
    }
}
