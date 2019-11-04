using System;
using System.IO;
using OfficeOpenXml;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class ExcelGeneratorBase
    {
        protected int CurrentRow { get; set; }
        protected int CurrentColumn { get; set; }
        
        /// <summary>
        /// Execute order <see cref="PrintHeader"/>
        /// <see cref="PrintColumnsHeader"/>
        /// <see cref="PrintContent"/>
        /// </summary>
        protected void Generate()
        {
            this.CurrentRow = 1;
            this.CurrentColumn = 1;
            this.PrintHeader();
            this.PrintColumnsHeader();
            this.PrintContent();
        }

        protected virtual void PrintHeader()
        {
            throw new NotImplementedException();
        }

        protected virtual void PrintColumnsHeader()
        {
            throw new NotImplementedException();
        }

        protected virtual void PrintContent()
        {
            throw new NotImplementedException();
        }
    }
}