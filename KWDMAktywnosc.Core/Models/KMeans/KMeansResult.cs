using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWDMAktywnosc.Core.Models.KMeans
{
    public class KMeansResult
    {
        public ClusterPrediction Predction { get; set; }
        public KMeansModelParameters ModelParameters { get; set; }
    }
}
