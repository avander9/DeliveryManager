using System;
using OfficeOpenXml;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class ExcelGeneratorBase : IDisposable
    {
        protected int CurrentRow { get; set; }
        protected int CurrentColumn { get; set; }

        protected ExcelPackage ExcelPackage { get; set; }

        /// <summary>
        /// Execute order <see cref="PrintHeader"/>
        /// <see cref="PrintColumnsHeader"/>
        /// <see cref="PrintContent"/>
        /// <see cref="SaveFile"/>
        /// <see cref="Dispose"/>
        /// </summary>
        protected void Generate()
        {
            this.CurrentRow = 1;
            this.CurrentColumn = 1;
            this.PrintHeader();
            this.PrintColumnsHeader();
            this.PrintContent();
            this.SaveFile();
            this.Dispose();
        }

        protected virtual void PrintHeader()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void PrintColumnsHeader()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void PrintContent()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void SaveFile()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.ExcelPackage.Dispose();
        }
    }
}