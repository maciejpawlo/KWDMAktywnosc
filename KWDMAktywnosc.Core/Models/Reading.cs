using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWDMAktywnosc.Core.Models
{
    public class Reading
    {
        public ReadingType ReadingType { get; set; }
        public TimeSpan Time { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
