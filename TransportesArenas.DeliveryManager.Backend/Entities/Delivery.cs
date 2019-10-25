using System;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.Backend.Implementations
{
    public class Delivery: IDelivery
    {
        public DateTime PickUpDate { get; set; }
        public string DriverName { get; set; }
        public string DeliveryReference { get; set; }
        public string PickUpCompany { get; set; }
        public string PickUpCity { get; set; }
        public string DropOffCompany { get; set; }
        public string DropOffCity { get; set; }
        public int Weight { get; set; }
    }
}