using DraftProject.Common;
using DraftProject.DataBase.Models;
using DraftProject.Draft;
using DraftProject.users;
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

        public DraftModel findDraftByNumber(string Number)
        {
            var result = new DraftModel();
            var executeQuery = db.LoadData(@"SELECT * from Draft WHERE Number=" + Number);
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

        private string SplitBackFromDate(DateTime Date)
        {
            string Month = Date.Month < 10 ? "0" + Date.Month.ToString() : Date.Month.ToString();
            string Day = Date.Day < 10 ? "0" + Date.Day.ToString() : Date.Day.ToString();
            return Date.Year.ToString()+ Month + Day;
        }

        public List<DraftModel> findDrafts(DraftModel draftModel,int page = 0,string FromDate="",string ToDate="")
        {
            var result = new List<DraftModel>();
            string query = @"SELECT * from Draft WHERE ID Is NOT NULL ";
            query += draftModel.Serial != "" ? " AND Serial = '" + draftModel.Serial + "'" : "";
            query += draftModel.Management != "" ? "AND Management ='" + draftModel.Management + "'" : "";

            query += draftModel.CarTag != "" ? "AND CarTag ='" + draftModel.CarTag + "'" : "";

            query += draftModel.Driver != "" ? "AND Driver ='" + draftModel.Driver + "'" : "";
            query += draftModel.Type != "" ? "AND Type ='" + draftModel.Type + "'" : "";

            query += draftModel.Origin != "" ? "AND Origin ='" + draftModel.Origin + "'" : "";
            query += draftModel.Destination != "" ? "AND Destination ='" + draftModel.Destination + "'" : "";

            if (FromDate != "" || ToDate != "")
            {
                if (FromDate != "" && ToDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) between '"+SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate))+"' and '"+SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate))+"'";
                }else if (FromDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) >= '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate))+"'";
                }
                else if(ToDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) <= '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate))+"'";
                }
            }

            query += " LIMIT 10 OFFSET "+page*10;
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

        public List<DraftModel> findAllDrafts(DraftModel draftModel, int page = 0, string FromDate = "", string ToDate = "")
        {
            var result = new List<DraftModel>();
            string query = @"SELECT * from Draft WHERE ID Is NOT NULL ";
            query += draftModel.Serial != "" ? " AND Serial = '" + draftModel.Serial + "'" : "";
            query += draftModel.Management != "" ? "AND Management ='" + draftModel.Management + "'" : "";

            query += draftModel.CarTag != "" ? "AND CarTag ='" + draftModel.CarTag + "'" : "";

            query += draftModel.Driver != "" ? "AND Driver ='" + draftModel.Driver + "'" : "";
            query += draftModel.Type != "" ? "AND Type ='" + draftModel.Type + "'" : "";

            query += draftModel.Origin != "" ? "AND Origin ='" + draftModel.Origin + "'" : "";
            query += draftModel.Destination != "" ? "AND Destination ='" + draftModel.Destination + "'" : "";

            if (FromDate != "" || ToDate != "")
            {
                if (FromDate != "" && ToDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) between '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)) + "' and '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)) + "'";
                }
                else if (FromDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) >= '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)) + "'";
                }
                else if (ToDate != "")
                {
                    query += " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) <= '" + SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)) + "'";
                }
            }

            
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
            result.Number = item["Number"].ToString();
            result.Serial = item["Serial"].ToString();
            result.TruckID = item["Truck"].ToString();
            result.TypeID = item["Type"].ToString();
            result.Truck = CommonUtils.getTrucksType().Where(z => z.ID == Int32.Parse(item["Truck"].ToString())).FirstOrDefault().Name ;
            result.Type = CommonUtils.getTypes().Where(z=>z.ID== Int32.Parse(item["Type"].ToString())).FirstOrDefault().Name;
            result.Origin = Encoding.UTF8.GetString(item["Origin"] as byte[]);
            result.Destination = Encoding.UTF8.GetString(item["Destination"] as byte[]);
            result.Value = Int32.Parse(item["Value"].ToString());
            result.UserID = Int32.Parse(item["UserID"].ToString());
            result.ID = Int32.Parse(item["ID"].ToString());
            result.Date =CommonUtils.ConvertMiladiToPersianDate( item["Date"].ToString());

            return result;
        }

        public List<DraftModel> GetDrafts(string path)
        {
            List<DraftModel> result = new List<DraftModel>();
            string query = @"SELECT * from Draft IsBackup Is null or IsBackup = false";
            db.SetConnection(path);
            var executeQuery = db.ExecuteQueryForLoading(query);
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

        public void UpdateDraftTabe(string path)
        {
            string query = @"Update Draft Set IsBackup = true where IsBackup Is Null or IsBackup = false";
            db.SetConnection(path);
            var executeQuery = db.ExecuteQueryForLoading(query);
        }


        public bool saveIntoDraftTable(List<DraftModel> model)
        {
            foreach (var item in model)
            {
                db.InsertData(users.DatabaseConstantData.DraftTable, bindFields(item));
            }
            return true;
        }

        private Dictionary<string, string> bindFields(DraftModel model)
        {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(DraftConstantData.CarTag, model.CarTag);
            paramValues.Add(DraftConstantData.CertificateDriver, model.CertificateDriver);
            paramValues.Add(DraftConstantData.Date, model.Date);
            paramValues.Add(DraftConstantData.Destination, model.Destination);
            paramValues.Add(DraftConstantData.Driver, model.Driver);
            paramValues.Add(DraftConstantData.Management, model.Management);
            paramValues.Add(DraftConstantData.Number, model.Number.ToString());
            paramValues.Add(DraftConstantData.Origin, model.Origin);
            paramValues.Add(DraftConstantData.Serial, model.Serial);
            paramValues.Add(DraftConstantData.Truck, model.Truck);
            paramValues.Add(DraftConstantData.Type, model.Type);
            paramValues.Add(DraftConstantData.UserID, model.UserID.ToString());
            paramValues.Add(DraftConstantData.Value, model.Value.ToString());
            return paramValues;
        }
    }
}
