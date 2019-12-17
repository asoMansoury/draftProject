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
            var executeQuery = db.LoadData(String.Format("SELECT * from Users WHERE UserName = '{0}' AND Password = '{1}'",userName,password));
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
            var executeQuery = db.LoadData(String.Format("SELECT * from Draft WHERE ID={0}" , ID));
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
            var executeQuery = db.LoadData(String.Format("SELECT * from Draft WHERE Number={0}", Number));
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
            string query =String.Format("SELECT * from Draft WHERE ID Is NOT NULL ");
            query += !string.IsNullOrEmpty(draftModel.Number) ? String.Format(" AND Number = '{0}'",draftModel.Number) : "";
            query += draftModel.Management != "" ?String.Format( "AND Management ='{0}'",draftModel.Management) : "";

            query += draftModel.CarTag != "" ?String.Format("AND CarTag ='{0}'",draftModel.CarTag) : "";

            query += draftModel.Driver != "" ?String.Format("AND Driver ='{0}'",draftModel.Driver) : "";
            query += draftModel.Type != "" ? String.Format("AND Type ='{0}'",draftModel.Type) : "";

            query += draftModel.Origin != "" ? String.Format("AND Origin ='{0}'",draftModel.Origin) : "";
            query += draftModel.Destination != "" ?String.Format( "AND Destination ='{0}'",draftModel.Destination) : "";

            if (FromDate != "" || ToDate != "")
            {
                if (FromDate != "" && ToDate != "")
                {
                    query +=String.Format( " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) between '{0}' and '{1}'", 
                        SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)), 
                        SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)));
                }else if (FromDate != "")
                {
                    query += String.Format(" AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) >= '{0}'", SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)));
                }
                else if(ToDate != "")
                {
                    query +=String.Format( " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) <= '{0}'", SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)));
                }
            }

            query +=String.Format( " LIMIT 10 OFFSET {0}",page*10);
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
            string query =String.Format("SELECT * from Draft WHERE ID Is NOT NULL ");
            query += !string.IsNullOrEmpty(draftModel.Number)  ? String.Format(" AND Number = '{0}'",draftModel.Number) : "";
            query += draftModel.Management != "" ?String.Format( "AND Management ='" + draftModel.Management + "'") : "";

            query += draftModel.CarTag != "" ?String.Format( "AND CarTag ='{0}'",draftModel.CarTag) : "";

            query += draftModel.Driver != "" ?String.Format( "AND Driver ='{0}'",draftModel.Driver) : "";
            query += draftModel.Type != "" ? String.Format("AND Type ='{0}'",draftModel.Type) : "";

            query += draftModel.Origin != "" ?String.Format( "AND Origin ='{0}'",draftModel.Origin) : "";
            query += draftModel.Destination != "" ?String.Format( "AND Destination ='{0}'",draftModel.Destination) : "";

            if (FromDate != "" || ToDate != "")
            {
                if (FromDate != "" && ToDate != "")
                {
                    query +=String.Format( " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) between '{0}' and '{1}'", 
                        SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)), 
                        SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)));
                }
                else if (FromDate != "")
                {
                    query +=String.Format( " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) >= '{0}'", SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(FromDate)));
                }
                else if (ToDate != "")
                {
                    query +=String.Format( " AND substr(Date,7)||substr(Date,1,2) ||substr(Date,4,2) <= '{0}'", SplitBackFromDate(CommonUtils.ConvertPersianToMiladiDate(ToDate)));
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
            result.Type = item["Type"].ToString();
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
            string query = String.Format("SELECT * from Draft IsBackup Is null or IsBackup = false");
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
            string query = String.Format("Update Draft Set IsBackup = true where IsBackup Is Null or IsBackup = false");
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
