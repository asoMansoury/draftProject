using DraftProject.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.CRUDSqliLite
{
    public  class Map
    {
        public static DraftModel mapToModel(DataRow item)
        {
            var result = new DraftModel();
            result.Management = Encoding.UTF8.GetString(item["Management"] as byte[]);
            result.CarTag = Encoding.UTF8.GetString(item["CarTag"] as byte[]);
            result.Driver = Encoding.UTF8.GetString(item["Driver"] as byte[]);
            result.CertificateDriver = Encoding.UTF8.GetString(item["CertificateDrive"] as byte[]);
            result.Number = Int32.Parse(item["Number"].ToString());
            result.Serial = item["Serial"].ToString();
            result.Truck = item["Truck"].ToString();
            result.Type = item["Type"].ToString();
            result.Origin = item["Origin"].ToString();
            result.Destination = item["Destination"].ToString();
            result.Value = Int32.Parse(item["Value"].ToString());
            result.UserID = Int32.Parse(item["UserID"].ToString());
            result.ID = Int32.Parse(item["ID"].ToString());
            return result;
        }
    }
}
