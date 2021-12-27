using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWDMAktywnosc.Core.Models;
using System.IO;

namespace KWDMAktywnosc.Core.Services.Implementation
{
    public class InputReaderService : IInputReaderService
    {
        public InputReaderService()
        {

        }

        public List<Reading> ReadSensorsInput(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var readings = new List<Reading>();

            foreach (var line in lines)
            {
                var splitted = line.Split(';');
                var enumCastResult = Enum.TryParse(splitted[0], out ReadingType readingType);

                if (!enumCastResult)
                    continue;

                var reading = new Reading();
                switch (readingType) //TODO: add try cath for format exception
                {
                    case ReadingType.TimeStamp:
                        reading.ReadingType = readingType;
                        reading.Time = new TimeSpan(0, 0, 0, 0, int.Parse(splitted[1]));
                        break;

                    default:
                        reading.Time = new TimeSpan(0, 0, 0, 0, int.Parse(splitted[1]));
                        reading.ReadingType = readingType;
                        reading.X = float.Parse(splitted[2]);
                        reading.Y = float.Parse(splitted[3]);
                        reading.Z = float.Parse(splitted[4]);
                        break;
                }
                readings.Add(reading);
            }

            return readings;
        }
    }
}
