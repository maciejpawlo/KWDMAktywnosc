using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWDMAktywnosc.Core.Models.KMeans
{
    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }
        [ColumnName("Score")]
        public float[] Distances { get; set; }
    }
}
