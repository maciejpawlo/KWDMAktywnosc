using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using KWDMAktywnosc.Core.Services.Implementation;
using KWDMAktywnosc.Core.Models;
using OxyPlot.Axes;

namespace KWDMAktywnosc.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        #region Properties
        private PlotModel _model;
        public PlotModel Model 
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        public string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        public List<ReadingPlotType> _readingPlotTypes;
        public List<ReadingPlotType> ReadingPlotTypes
        {
            get { return _readingPlotTypes; }
            set { SetProperty(ref _readingPlotTypes, value); }
        }

        public List<Reading> _readings;
        public List<Reading> Readings
        {
            get { return _readings; }
            set { SetProperty(ref _readings, value); }
        }

        #endregion

        #region Commands
        public IMvxAsyncCommand PickFilkeCommand { get; set; }
        #endregion

        #region Services
        private readonly IInputReaderService inputReaderService;
        #endregion

        public MainViewModel(IInputReaderService inputReaderService)
        {
            this.inputReaderService = inputReaderService;
        }

        public override Task Initialize()
        {
            this.Model = new PlotModel { Title = "Plot" };
            this.ReadingPlotTypes = new List<ReadingPlotType>()
            {
                new ReadingPlotType { ReadingType = ReadingType.Accelerometer, Description = "Accelerometer" },
                new ReadingPlotType { ReadingType = ReadingType.GeomagneticRotation, Description = "Geomagnetic Rotation" },
                new ReadingPlotType { ReadingType = ReadingType.Gravity, Description = "Gravity" },
                new ReadingPlotType { ReadingType = ReadingType.Gyro, Description = "Gyro" },
                new ReadingPlotType { ReadingType = ReadingType.LinearAcceleration, Description = "Linear Acceleration" },
                new ReadingPlotType { ReadingType = ReadingType.MagneticField, Description = "Magnetic Field" },
                new ReadingPlotType { ReadingType = ReadingType.Rotation, Description = "Rotation" }
            };
            return base.Initialize();
        }

        public void HandleChosenFile(string fileName, string safeFileName)
        {
            FilePath = fileName;
            FileName = safeFileName;
            var inputFromFile = inputReaderService.ReadSensorsInput(FilePath);
            Readings = inputFromFile;
        }

        public void HandleReadingPlotTypeSelectionChanged(ReadingPlotType selectedPlotType)
        {
            this.Model = CreatePlotModelFromInput(selectedPlotType);
        }

        private PlotModel CreatePlotModelFromInput(ReadingPlotType selectedPlotType)
        {
            var plot = new PlotModel { Title = $"{selectedPlotType.Description}" };
            var data = Readings.Where(r => r.ReadingType == selectedPlotType.ReadingType);
            //create 3 series for x, y and z
            var seriesX = new LineSeries
            {
                Color = OxyColors.Red,
                StrokeThickness = 0.5,
                Title = "X"
            };
            var seriesY = new LineSeries
            {
                Color = OxyColors.Blue,
                StrokeThickness = 0.5,
                Title = "Y"
            };
            var seriesZ = new LineSeries
            {
                Color = OxyColors.Green,
                StrokeThickness = 0.5,
                Title = "Z"
            };
            //create axes
            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TitleColor = OxyColors.Black,
                AxislineColor = OxyColors.Black,
                Title = "Value",
                TicklineColor = OxyColors.Black
            });
            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                TitleColor = OxyColors.Black,
                AxislineColor = OxyColors.Black,
                TicklineColor = OxyColors.Black,
                Title = "Time [ms]"
            });
            //add points to series
            foreach (var item in data)
            {
                seriesX.Points.Add(new DataPoint(item.Time.TotalMilliseconds, item.X));
                seriesY.Points.Add(new DataPoint(item.Time.TotalMilliseconds, item.Y));
                seriesZ.Points.Add(new DataPoint(item.Time.TotalMilliseconds, item.Z));
            }

            plot.Series.Add(seriesX);
            plot.Series.Add(seriesY);
            plot.Series.Add(seriesZ);

            return plot;
        }
    }
}
