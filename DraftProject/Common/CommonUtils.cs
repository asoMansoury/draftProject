using DraftProject.DataBase.CRUDSqliLite;
using DraftProject.DataBase.Models;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DraftProject.Common
{
    public class CommonUtils
    {
        public static string uniqeUserID { get; set; }
        public static StiReport ShowReport(int ID)
        {
            var stiReportResearcher = new StiReport();
            DraftCrud draftCrud = new DraftCrud();
            var findedDraft = draftCrud.findDraftByID(ID);
            stiReportResearcher = new StiReport();
            stiReportResearcher.Load(Application.StartupPath + "\\Report.mrt");

            StiText Part1 = new StiText();
            Part1 = (StiText)stiReportResearcher.GetComponentByName("Part1");
            Part1.Text = "مدیریت محترم : " + findedDraft.Origin;

            StiText txtDate = new StiText();
            txtDate = (StiText)stiReportResearcher.GetComponentByName("txtDate");
            txtDate.Text = "تاریخ : " + findedDraft.Date;

            StiText txtNumber = new StiText();
            txtNumber = (StiText)stiReportResearcher.GetComponentByName("txtNumber");
            txtNumber.Text = "شماره گزارش : " + findedDraft.Number;


            StiText Part2 = new StiText();
            Part2 = (StiText)stiReportResearcher.GetComponentByName("Part2");
            string carrTag = findedDraft.CarTag;
            string finalCarTag = CartTagFunc(carrTag);
            Part2.Text = " کامیون " + getTrucksType().Where(z => z.ID == Int32.Parse(findedDraft.TruckID)).FirstOrDefault().Name + " به شماره پلاک " + finalCarTag + " به رانندگی " + findedDraft.Driver + " شماره گواهینامه " + findedDraft.CertificateDriver + " جهت حمل " + findedDraft.TypeID + " به مقدار " + findedDraft.Value + " تن به مقصد " + findedDraft.Destination + "  حضورتان معرفی میگردد";

            
            return stiReportResearcher;
        }
        public static string CartTagFunc(string carrTag)
        {
            string firstCarTag = carrTag.Substring(3, 3);
            string twoCarTag = carrTag.Substring(0, 2);
            string wordCarTag = carrTag.Substring(2, 1);
            string lastCarTag = carrTag.Substring(7, 2);
            string finalCarTag = firstCarTag + " " + wordCarTag + twoCarTag + " ایران" + lastCarTag;
            return finalCarTag;

        }
        public class draftReportObj
        {
            public string DraftReports { get; set; }
        }

        public static StiReport ShowReportList(List<DraftModel> draftModels)
        {
            var mainReport = new StiReport();
            mainReport.Load(Application.StartupPath + "\\ReportList.mrt");
            DraftCrud draftCrud = new DraftCrud();
            List<draftReportObj> data = new List<draftReportObj>();
            int SumOfValue = 0;
            foreach (var item in draftModels)
            {
                draftReportObj itemrr = new draftReportObj();
                string finalCarTage = CartTagFunc(item.CarTag);
                itemrr.DraftReports = " کامیون " + getTrucksType().Where(z => z.ID == Int32.Parse(item.TruckID)).FirstOrDefault().Name + " به شماره پلاک " + finalCarTage +  " جهت حمل " + item.TypeID + " به مقدار " + item.Value +"تن،از مبدا "+item.Origin+ "  به مقصد " + item.Destination  +" به شماره "+item.Number+" تاریخ "+item.Date;
                data.Add(itemrr);
                SumOfValue += item.Value;
            }

            mainReport.RegBusinessObject("DraftReports", data);
            StiText Part1 = new StiText();
            Part1 = (StiText)mainReport.GetComponentByName("TxtSumValue");
            Part1.Text ="مجموع کل حواله ها برابر با "+SumOfValue+" تن است";
            return mainReport;
        }
        public static string HashingPassword(string Password)
        {
            var data =Password.ToArray().ToList();
            var resultData = "";
            foreach (char item in data)
            {
                resultData += Char.ConvertFromUtf32(System.Convert.ToInt32(item)+8);
            }
            return resultData;
            byte[] byteArray = Encoding.UTF8.GetBytes(Password);
            MemoryStream stream = new MemoryStream(byteArray);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(stream);
            string result = System.Text.Encoding.UTF8.GetString(sha1data);
            return result;
        }

        public static string generateRandumNumber()
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;
            int Hour = DateTime.Now.Hour;
            int Minute = DateTime.Now.Minute;
            int Second = DateTime.Now.Second;
            int Milisecond = DateTime.Now.Millisecond;
            string result = GenerateRandomNumber(4).ToString() + Year + Month + Day + Hour + Minute + Second + Milisecond + uniqeUserID;
            return result;
        }
        private static Int64 GenerateRandomNumber(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            string s;
            for (int i = 0; i < size; i++)
            {
                s = Convert.ToString(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(s);
            }

            return Convert.ToInt64((builder.ToString()));
        }

        public static DateTime ConvertPersianToMiladiDate(string Date)
        {
            PersianCalendar pc = new PersianCalendar();

            var persianDateSplitedParts = Date.Split('/');
            DateTime dateTime = new DateTime(int.Parse(persianDateSplitedParts[0]), int.Parse(persianDateSplitedParts[1]), int.Parse(persianDateSplitedParts[2]), pc);
            return DateTime.Parse(dateTime.ToString(CultureInfo.InvariantCulture));
        }

        public static string ConvertMiladiToPersianDate(string Date)
        {
            
            DateTime d = DateTime.Parse(Date);
            PersianCalendar pc = new PersianCalendar();
            string result =string.Format("{0}/{1}/{2}", pc.GetYear(d), pc.GetMonth(d), pc.GetDayOfMonth(d));
            return result;
        }


        public static int ConvertMiladiToPersianDateGetYear(string Date)
        {

            DateTime d = DateTime.Parse(Date);
            PersianCalendar pc = new PersianCalendar();
            
            return pc.GetYear(d);
        }


        public static string GetEnumDescription(Enum value)
        {

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static bool CheckRegex(string input,string regex)
        {
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return false;
                // does not match
            }
            return true;
        }

        public static List<ItemModel> getTrucksType()
        {
            List<ItemModel> result = new List<ItemModel>();
            ItemModel data = new ItemModel();
            data.ID = 0;
            data.Name = "تریلی";
            result.Add(data);
            data = new ItemModel();
            data.ID = 1;
            data.Name = "کشنده";
            result.Add(data);
            data = new ItemModel();
            data.ID = 2;
            data.Name = "سه چرخ";
            result.Add(data);
            data = new ItemModel();
            data.ID = 3;
            data.Name = "کفی";
            result.Add(data);
            return result;
        }

        public static List<ItemModel> getTypes()
        {
            List<ItemModel> result = new List<ItemModel>();
            ItemModel data = new ItemModel();
            data.ID = 0;
            data.Name = "گندم";
            result.Add(data);
            data = new ItemModel();
            data.ID = 1;
            data.Name = "گل";
            result.Add(data);
            data = new ItemModel();
            data.ID = 2;
            data.Name = "زغال";
            result.Add(data);
            data = new ItemModel();
            data.ID = 3;
            data.Name = "آب";
            result.Add(data);
            data = new ItemModel();
            data.ID = 4;
            data.Name = "نفت";
            result.Add(data);
            data = new ItemModel();
            data.ID = 5;
            data.Name = "بنزین";
            result.Add(data);
            data = new ItemModel();
            data.ID = 6;
            data.Name = "رول";
            result.Add(data);
            return result;
        }
    }
}
