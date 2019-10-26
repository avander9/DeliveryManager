﻿using System;
using System.Threading.Tasks;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IPdfWrapper: IDisposable
    {
        IPdfWrapper Build(string filePath);
        string ReadPage(int pageNumber);
        IPdfResult FindValueAsync(string value);
        void SplitAndSave(string outputFile, int pageNumber);
    }
}