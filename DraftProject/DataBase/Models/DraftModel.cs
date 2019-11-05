using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.Models
{
    public class DraftModel
    {
        public DraftModel()
        {
            Serial = "";
            Management = "";
            Truck = "";
            CarTag = "";
            Driver = "";
            CertificateDriver = "";
            Type = "";
            Origin = "";
            Destination = "";
        }
        public int ID { get; set; }
        public string Number { get; set; }
        public string Serial { get; set; }
        public string Management { get; set; }
        public string Truck { get; set; }
        public string TruckID { get; set; }
        public string CarTag { get; set; }
        public string Driver { get; set; }
        public string CertificateDriver { get; set; }
        public string Type { get; set; }
        public string TypeID { get; set; }
        public int Value { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int UserID { get; set; }
        public string Date { get; set; }

    }
}
