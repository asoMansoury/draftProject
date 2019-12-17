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
    public  class UsersCrud
    {
        DbContext db = null;

        public UsersCrud()
        {
            db = new DbContext();
        }



        public List<SecretKeyModel> GetAllSecretCodes()
        {
            List<SecretKeyModel> result = new List<SecretKeyModel>();
            var executeQuery = db.LoadData(@"SELECT * FROM SecretKeys");
            if (executeQuery.Rows.Count > 0)
                foreach (DataRow item in executeQuery.Rows)
                {
                    var model = mapToModelSecret(item);
                    result.Add(model);
                }

            return result;
        }

        
        public bool CheckProgramIsActive()
        {
            List<SecretKeyModel> result = new List<SecretKeyModel>();
            var executeQuery = db.LoadData(String.Format("SELECT * FROM SecretKeys"));
            if(executeQuery.Rows.Count>0)
                foreach (DataRow item in executeQuery.Rows)
                {
                    var model = mapToModelSecret(item);
                    result.Add(model);
                }
            int Year = CommonUtils.ConvertMiladiToPersianDateGetYear(DateTime.Now.ToString());
            foreach (var item in result)
            {
                if (Int32.Parse(item.FromDate) == Year)
                {
                    if (item.IsActive != 0)
                        return true;
                }
            }
            return false;
        }

        public UsersModel findUser(string userName, string password) {
            UsersModel result = new UsersModel();
            var executeQuery = db.LoadData(String.Format("SELECT * from Users WHERE UserName = '{0}' AND Password = '{1}'", userName, password));
            if (executeQuery.Rows.Count > 0) {
                foreach (DataRow item in executeQuery.Rows)
                {
                    result = mapToModel(item);
                }
                return result;
            }

            return null;
        }

        public UsersModel findUserByID(int ID)
        {
            UsersModel result = new UsersModel();
            var executeQuery = db.LoadData(String.Format("SELECT * from Users WHERE ID={0}" ,ID));
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

        public List<UsersModel> findUsers(string username="", string name="", string family="")
        {
            var result = new List<UsersModel>();
            var executeQuery = db.LoadData(String.Format("SELECT * from Users WHERE name = '{0}' or UserName IS NOT NULL  or Family = '{1}' or Family is NOT NULL or UserName ='{2}' or UserName Is NOT NULL",name,family,username));
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

        public List<UsersModel> GetUsers(string path)
        {
            List<UsersModel> result = new List<UsersModel>();
            string query = String.Format("SELECT * from Users");
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

        public bool DisableUser(int ID)
        {
            var paramValue = new Dictionary<string, string>();
            paramValue.Add(DraftConstantData.IsActive, "0");
            db.UpdateUser("Users",ID,paramValue);
            return true;
        }

        public bool saveIntoDraftTable(List<UsersModel> model)
        {
            foreach (var item in model)
            {
                db.InsertData(users.DatabaseConstantData.UsersTable, bindFields(item));
            }
            return true;
        }

        private Dictionary<string, string> bindFields(UsersModel model)
        {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(DatabaseConstantData.name, model.name);
            paramValues.Add(DatabaseConstantData.Password, model.password);
            paramValues.Add(DatabaseConstantData.Family, model.family);
            paramValues.Add(DatabaseConstantData.UserCode, model.userCode);
            paramValues.Add(DatabaseConstantData.UserName, model.userName);
            return paramValues;
        }

        private UsersModel mapToModel(DataRow item) {
            var result = new UsersModel();
            try
            {
                byte[] namebyteArray = item["Name"] as byte[];
                if (namebyteArray != null)
                    result.name = Encoding.UTF8.GetString(namebyteArray, 0, namebyteArray.Length);
                byte[] familybyteArray = item["Family"] as byte[];
                if (familybyteArray != null)
                    result.family = Encoding.UTF8.GetString(familybyteArray);
                result.userCode = item["UserCode"].ToString();
                result.password = item["Password"].ToString();
                result.userName = item["UserName"].ToString();
                result.ID = Int32.Parse(item["ID"].ToString());
                result.IsActive = Int32.Parse(item["IsActive"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private SecretKeyModel mapToModelSecret(DataRow item)
        {
            var result = new SecretKeyModel();
            result.Key = item["Key"].ToString();
            result.FromDate = item["FromDate"].ToString();
            result.ToDate = item["ToDate"].ToString();
            result.IsActive = Int32.Parse(item["IsActive"].ToString());
            return result;
        }
    }
}
