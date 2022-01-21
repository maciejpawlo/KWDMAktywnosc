using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWDMAktywnosc.Core.Models.KMeans
{
    public class Features
    {
        public float Maximum { get; set; }
        public float Minimum { get; set; }
        public float Mean { get; set; }
        public float StandardDevation { get; set; }
        public float Percentile20 { get; set; }
        public float Percentile50 { get; set; }
        public float Percentile80 { get; set; }
        public float Skewness { get; set; }
        public float Kurtosis { get; set; }
        //public float MeanSquareError { get; set; }
        //public float AutoCorrelation { get; set; }
        //public float SignalEnergyLog { get; set; }
        //public string Label { get; set; }
    }
}
