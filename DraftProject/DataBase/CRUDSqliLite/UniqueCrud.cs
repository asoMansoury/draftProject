using DraftProject.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DraftProject.DataBase.CRUDSqliLite
{
    public class UniqueCrud
    {
        DbContext db = null;

        public UniqueCrud()
        {
            db = new DbContext();
        }

        public UniqueModel GetLastUnique()
        {
            List<UniqueModel> result = new List<UniqueModel>();
            var executeQuery = db.LoadData(String.Format("SELECT * FROM GenerateUnique"));
            if (executeQuery.Rows.Count > 0)
                foreach (DataRow item in executeQuery.Rows)
                {
                    var model = mapToModelSecret(item);
                    result.Add(model);
                }

            return result.FirstOrDefault();
        }



        private UniqueModel mapToModelSecret(DataRow item)
        {
            var result = new UniqueModel();
            result.ID = Int32.Parse(item["ID"].ToString());
            result.UniquID = Int32.Parse(item["UniquID"].ToString());
            result.Date = item["Date"].ToString();
            return result;
        }

    }
}
