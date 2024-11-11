using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.DAL
{

    public class LookupModel
    {
        public int Code { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public int d_order { get; set; }
        public string img { get; set; }

    }
    public class LookupMaster : BaseRepository
    {

        public LookupMaster(AssetsEntitiesNew _context) : base(_context)
        {

        }

        public List<LookupModel> GetItems(string TableName, string FilterStr, string d_order = "", string refname = "", string refvalue = "")
        {
            string query = "SELECT * ";
            query += " FROM " + TableName;
            query += " where 1=1";

            if (FilterStr != "" && FilterStr != "0")
            {
                query += " and ( TitleEn like N'%" + FilterStr + "%' or TitleAr like N'%" + FilterStr + "%')";
            }

            if (refvalue != "" && refvalue != "0")
            {
                query += " and (" + refname + "=" + refvalue + ")";
            }
            if (d_order != "")
            {
                query += " order by  d_order asc";
            }
            var result = DC.Database.SqlQuery<LookupModel>(query);
            return result.ToList<LookupModel>();
        }

        public List<LookupModel> FillLookup(string TableName)
        {
            string query = "select * from " + TableName;// " order by  TitleEn";
                                                        // return ABOBasic.ins.ExecuteDs(query);
            var result = DC.Database.SqlQuery<LookupModel>(query);
            return result.ToList<LookupModel>();
        }

        public bool checkTextExistance(string TableName, string TextToCompair)
        {
            string query = ("select * from " + TableName + " where TitleAr like N'" + TextToCompair + "'");

            var result = DC.Database.SqlQuery<LookupModel>(query).ToList();
            // result.ToList<LookupModel>();

            if (result != null && result.Count > 0)
            {
                return true;
            }

            return false;
        }

        public LookupModel GetDetails(string TableName, string code)
        {
            string query = ("select * from " + TableName + " where code=" + code);


            var result = DC.Database.SqlQuery<LookupModel>(query);
            return result.FirstOrDefault<LookupModel>();

        }

        public void Insert(string TableName, string TitleEn, string TitleAr, string D_Order = "", string refName = "", string refvalue = "", string img = "")
        {
            string q = "insert into " + TableName + "(TitleEn,TitleAr " + (D_Order != "" ? ",D_Order" : "") + (refName != "" ? "," + refName + "" : "") + (img != "" ? ",img" : "") + ")";
            q += " values(N'" + FixString(TitleEn) + "',N'" + FixString(TitleAr) + "'" + (D_Order != "" ? "," + D_Order : "") + (refvalue != "" ? "," + refvalue : "") + (img != "" ? ",'" + img + "'" : "") + ")";

            DC.Database.ExecuteSqlCommand(q);
        }

        public void Update(string TableName, string code, string TitleEn, string TitleAr, string D_Order = "", string img = "")
        {
            string q = "update " + TableName + " set TitleEn=N'" + FixString(TitleEn) + "',TitleAr=N'" + FixString(TitleAr) + "'" + (D_Order != "" ? ",D_Order=" + D_Order : "") + (img != "" ? ",img='" + img + "'" : "");
            q += " where code = " + code;

            DC.Database.ExecuteSqlCommand(q);

        }
        public void Delete(string TableName, string id)
        {
            string q = ("delete from " + TableName + " where code=" + id);

            DC.Database.ExecuteSqlCommand(q);

        }
        public void DeleteList(string TableName, string list)
        {
            string q = "delete from " + TableName + " where code in (" + list + ")";
            DC.Database.ExecuteSqlCommand(q);
        }
        private string FixString(string per)
        {
            if (per.Equals("") | per.Equals("0"))
            {
                return "0";
            }
            else
            {
                return per;
            }
        }
    }
}
