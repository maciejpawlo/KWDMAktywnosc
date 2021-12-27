using KWDMAktywnosc.Core.Models;
using System.Collections.Generic;

namespace KWDMAktywnosc.Core.Services.Implementation
{
    public interface IInputReaderService
    {
        List<Reading> ReadSensorsInput(string filePath);
    }
}