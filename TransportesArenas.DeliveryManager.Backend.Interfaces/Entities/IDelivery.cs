using System;

namespace TransportesArenas.DeliveryManager.Backend.Interfaces
{
    public interface IDelivery
    {
        DateTime PickUpDate { get; set; }
        string DriverName { get; set; }
        string DeliveryReference { get; set; }
        string PickUpCompany { get; set; }
        string PickUpCity { get; set; }
        string DropOffCompany { get; set; }
        string DropOffCity { get; set; }
        int Weight { get; set; }
    }
}