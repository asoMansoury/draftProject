using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.Models
{
    public class UsersModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string userCode { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int IsActive { get; set; }
    }
}
