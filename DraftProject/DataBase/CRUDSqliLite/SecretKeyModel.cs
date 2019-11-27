using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase
{
    public class SecretKeyModel
    {
        public string Key { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int IsActive { get; set; }
    }
}
