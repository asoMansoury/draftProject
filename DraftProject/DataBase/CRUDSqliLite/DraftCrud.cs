using DraftProject.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.CRUDSqliLite
{
    public class DraftCrud
    {
        DbContext db = null;
        public DraftCrud()
        {
            db = new DbContext();
        }
        public DraftModel findUser(string userName, string password)
        {
            var result = new DraftModel();
            var executeQuery = db.LoadData(@"SELECT * from Users WHERE UserName = '" + userName + "' AND Password = '" + password + "'");
            if (executeQuery.Rows.Count > 0)
            {
                foreach (DataRow item in executeQuery.Rows)
                {
                    result = mapToModel(item);
                }
                return result;
            }

            return null;
        }

        public DraftModel findUserByID(int ID)
        {
            var result = new DraftModel();
            var executeQuery = db.LoadData(@"SELECT * from Users WHERE ID=" + ID);
            if (executeQuery.Rows.Count > 0)
            {
                foreach (DataRow item in executeQuery.Rows)
                {
                    result = mapToModel(item);
                }
                return result;
            }

            return null;
        }

        public List<DraftModel> findUsers(string username = "", string name = "", string family = "")
        {
            var result = new List<DraftModel>();
            var executeQuery = db.LoadData(@"SELECT * from Users WHERE name = '" + name + "' or UserName IS NOT NULL  or Family = '" + family + "' or Family is NOT NULL or UserName ='" + username + "' or UserName Is NOT NULL");
            if (executeQuery.Rows.Count > 0)
            {
                foreach (DataRow item in executeQuery.Rows)
                {
                    result.Add(mapToModel(item));
                }
                return result;
            }

            return null;
        }

        private DraftModel mapToModel(DataRow item)
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
