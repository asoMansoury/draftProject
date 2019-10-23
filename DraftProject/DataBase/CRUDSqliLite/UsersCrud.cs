﻿using DraftProject.DataBase.Models;
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

        public UsersModel findUser(string userName, string password) {
            UsersModel result = new UsersModel();
            var executeQuery = db.LoadData(@"SELECT * from Users WHERE UserName = '"+userName+"' AND Password = '"+password+"'");
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

        public List<UsersModel> findUsers(string username="", string name="", string family="")
        {
            var result = new List<UsersModel>();
            var executeQuery = db.LoadData(@"SELECT * from Users WHERE name = '" + name+"' or UserName IS NOT NULL  or Family = '"+family+"' or Family is NOT NULL or UserName ='"+username+"' or UserName Is NOT NULL");
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
            string query = @"SELECT * from Users";
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
            byte[] namebyteArray = item["Name"] as byte[];
            if(namebyteArray!=null)
                result.name = Encoding.UTF8.GetString(namebyteArray, 0, namebyteArray.Length);
            byte[] familybyteArray = item["Family"] as byte[];
            if(familybyteArray!=null)
                result.family = Encoding.UTF8.GetString(familybyteArray);
            result.userCode = item["UserCode"].ToString();
            result.password = item["Password"].ToString();
            result.userName = item["UserName"].ToString();
            result.ID = Int32.Parse(item["ID"].ToString());
            return result;
        }
    }
}
