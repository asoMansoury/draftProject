using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject.DataBase.CRUDSqliLite
{
    public  class DbContext
    {
        private SQLiteConnection sql_con;
        private SQLiteConnection sql_conForLoading;
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

            sql_cmd.CommandText =String.Format("INSERT into {0}({1}) VALUES ({2})",TableName,fieldNames,valueFields);

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
            string dbFilePath = Application.StartupPath+"\\sqlLiteDbDraft.db";
            sql_con = new SQLiteConnection
                (@"Data Source = "+ dbFilePath + "; Version = 3; FailIfMissing = True; Foreign Keys = True;", true);
        }

        public void SetConnection(string filePath)
        {
            sql_conForLoading = new SQLiteConnection
                (@"Data Source = " + filePath + "; Version = 3; FailIfMissing = True; Foreign Keys = True;", true);
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


        public DataTable ExecuteQueryForLoading(string txtQuery)
        {
            SetConnection();
            sql_conForLoading.Open();
            sql_cmd = sql_conForLoading.CreateCommand();
            DB = new SQLiteDataAdapter(txtQuery, sql_conForLoading);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;
        }

        public bool ActiveSecretCode(string Key)
        {
            string query =String.Format( "Update SecretKeys Set IsActive = true where Key  = '{0}'",Key);
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = query;
            sql_cmd.Prepare();
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
            return true;
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
            sql_cmd.CommandText =String.Format("UPDATE {0} SET {1} WHERE ID = {2}",TableName,valueFields,ID) ;

            sql_cmd.Prepare();
            foreach (var item in parameters)
            {
                string key = "@" + item.Key;
                sql_cmd.Parameters.AddWithValue(key, item.Value);
            }
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void UpdateGenerate(string TableName, int ID, Dictionary<string, string> parameters)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();

            string valueFields = "";
            foreach (var item in parameters)
            {
                valueFields += item.Key + " = @" + item.Key + ",";
            }
            if (valueFields.Length > 0)
                valueFields = valueFields.Remove(valueFields.Length - 1);
            sql_cmd.CommandText =String.Format("UPDATE {0} SET {1} WHERE ID = {2}",TableName,valueFields,ID) ;

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
