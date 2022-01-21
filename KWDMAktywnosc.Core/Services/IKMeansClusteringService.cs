using KWDMAktywnosc.Core.Models;
using KWDMAktywnosc.Core.Models.KMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWDMAktywnosc.Core.Services
{
    public interface IKMeansClusteringService
    {
        KMeansResult PerformKMeans(List<Reading> readings, ReadingType readingType);
    }
}
