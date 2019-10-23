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

        public DraftModel findDraftByID(int ID)
        {
            var result = new DraftModel();
            var executeQuery = db.LoadData(@"SELECT * from Draft WHERE ID=" + ID);
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

        public List<DraftModel> findDrafts(DraftModel draftModel)
        {
            var result = new List<DraftModel>();
            string query = @"SELECT * from Draft WHERE ID Is NOT NULL ";
            query += draftModel.Serial != "" ? " AND Serial = '" + draftModel.Serial + "'" : "";
            query += draftModel.Management != "" ? "AND Management ='" + draftModel.Management + "'" : "";

            query += draftModel.Date != "" ? "AND Date ='" + draftModel.Date + "'" : "";
            query += draftModel.CarTag != "" ? "AND CarTag ='" + draftModel.CarTag + "'" : "";

            query += draftModel.Driver != "" ? "AND Driver ='" + draftModel.Driver + "'" : "";
            query += draftModel.Type != "" ? "AND Type ='" + draftModel.Type + "'" : "";

            query += draftModel.Origin != "" ? "AND Origin ='" + draftModel.Origin + "'" : "";
            query += draftModel.Destination != "" ? "AND Destination ='" + draftModel.Destination + "'" : "";

            var executeQuery = db.LoadData(query);
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
            result.CertificateDriver = Encoding.UTF8.GetString(item["CertificateDriver"] as byte[]);
            result.Number = Int32.Parse(item["Number"].ToString());
            result.Serial = item["Serial"].ToString();
            result.Truck = item["Truck"].ToString();
            result.Type = item["Type"].ToString();
            result.Origin = Encoding.UTF8.GetString(item["Origin"] as byte[]);
            result.Destination = Encoding.UTF8.GetString(item["Destination"] as byte[]);
            result.Value = Int32.Parse(item["Value"].ToString());
            result.UserID = Int32.Parse(item["UserID"].ToString());
            result.ID = Int32.Parse(item["ID"].ToString());
            result.Date = item["Date"].ToString();

            return result;
        }
    }
}
