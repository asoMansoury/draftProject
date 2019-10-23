using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.CRUDSqliLite
{
    public  class DbContext
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public DataTable LoadData(string SqlQuery= "select * from Users")
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(SqlQuery, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;
        }

        public void InsertData(string TableName, Dictionary<string,string> parameters) {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();

            string fieldNames = "";
            string valueFields = "";
            foreach (var item in parameters)
            {
                valueFields += "@" + item.Key + ",";
                fieldNames += item.Key + ",";
            }
            if (valueFields.Length > 0)
                valueFields= valueFields.Remove(valueFields.Length - 1);
            if (fieldNames.Length > 0)
                fieldNames = fieldNames.Remove(fieldNames.Length - 1);

            sql_cmd.CommandText = "INSERT into "+ TableName+"("+ fieldNames + ") VALUES ("+ valueFields + ")";

            sql_cmd.Prepare();
            foreach (var item in parameters)
            {
                string key = "@" + item.Key;
                sql_cmd.Parameters.AddWithValue(key, item.Value);
            }
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
            
        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                (@"Data Source = D:\DrafProject22\draftProject\DraftProject\DataBase\sqlLiteDbDraft.db; Version = 3; FailIfMissing = True; Foreign Keys = True;", true);
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }


        public void UpdateUser(string TableName,int ID, Dictionary<string, string> parameters)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();

            string valueFields = "";
            foreach (var item in parameters)
            {
                valueFields += item.Key + " = @" + item.Key+",";
            }
            if (valueFields.Length > 0)
                valueFields = valueFields.Remove(valueFields.Length - 1);
            sql_cmd.CommandText = "UPDATE " + TableName + " SET "+ valueFields + " WHERE ID = " + ID;

            sql_cmd.Prepare();
            foreach (var item in parameters)
            {
                string key = "@" + item.Key;
                sql_cmd.Parameters.AddWithValue(key, item.Value);
            }
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

    }
}
