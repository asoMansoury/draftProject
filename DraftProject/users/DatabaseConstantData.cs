using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftProject.users
{
    public class DatabaseConstantData
    {
        public static string UsersTable = "Users";
        public static string DraftTable = "Draft";
        public static string GenerateTable = "GenerateUnique";
        public static string UsersFields = "name,Family,UserCode,UserName,Password";
        public static string name = "name";
        public static string Family = "Family";
        public static string UserCode = "UserCode";
        public static string UserName = "UserName";
        public static string Password = "Password";
        public static string IsBackup = "IsBackup";
        public static string InsertDate = "InsertDate";
        public static string InsertBy = "InsertBy";
        public static string UpdateDate = "UpdateDate";
        public static string UpdateBy = "UpdateBy";
        public static string RestoreDate = "RestoreDate";
        public static string RestoreBy = "RestoreBy";
    }
}
