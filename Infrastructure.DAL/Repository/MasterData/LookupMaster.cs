using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
        private static readonly string[] AllowedTables = { "Categories", "D_Locations", "Assets" }; // Add allowed table names here

        public LookupMaster(AssetsEntitiesNew _context) : base(_context) { }

        private bool IsValidTable(string tableName) => AllowedTables.Contains(tableName);

        public List<LookupModel> GetItems(string tableName, string filterStr, string d_order = "", string refname = "", string refvalue = "")
        {
            ////if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");

            var parameters = new List<SqlParameter>();
            string query = $"SELECT * FROM {tableName} WHERE 1=1";

            if (!string.IsNullOrEmpty(filterStr) && filterStr != "0")
            {
                query += " AND (TitleEn LIKE @filter OR TitleAr LIKE @filter)";
                parameters.Add(new SqlParameter("@filter", "%" + filterStr + "%"));
            }

            if (!string.IsNullOrEmpty(refvalue) && refvalue != "0" && !string.IsNullOrEmpty(refname))
            {
                query += $" AND {refname} = @refvalue";
                parameters.Add(new SqlParameter("@refvalue", refvalue));
            }

            if (!string.IsNullOrEmpty(d_order))
            {
                query += " ORDER BY d_order ASC";
            }

            return DC.Database.SqlQuery<LookupModel>(query, parameters.ToArray()).ToList();
        }

        public List<LookupModel> FillLookup(string tableName)
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");
            string query = $"SELECT * FROM {tableName}";
            return DC.Database.SqlQuery<LookupModel>(query).ToList();
        }

        public bool checkTextExistance(string tableName, string textToCompare)
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");
            string query = $"SELECT * FROM {tableName} WHERE TitleAr = @text";
            var result = DC.Database.SqlQuery<LookupModel>(query, new SqlParameter("@text", textToCompare)).ToList();
            return result.Any();
        }

        public LookupModel GetDetails(string tableName, string code)
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");
            string query = $"SELECT * FROM {tableName} WHERE Code = @code";
            return DC.Database.SqlQuery<LookupModel>(query, new SqlParameter("@code", code)).FirstOrDefault();
        }

        public void Insert(string tableName, string titleEn, string titleAr, string d_order = "", string refName = "", string refvalue = "", string img = "")
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");

            var cols = new List<string> { "TitleEn", "TitleAr" };
            var vals = new List<string> { "@titleEn", "@titleAr" };
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@titleEn", FixString(titleEn)),
                new SqlParameter("@titleAr", FixString(titleAr))
            };

            if (!string.IsNullOrEmpty(d_order))
            {
                cols.Add("D_Order");
                vals.Add("@d_order");
                parameters.Add(new SqlParameter("@d_order", d_order));
            }

            if (!string.IsNullOrEmpty(refName) && !string.IsNullOrEmpty(refvalue))
            {
                cols.Add(refName);
                vals.Add("@refvalue");
                parameters.Add(new SqlParameter("@refvalue", refvalue));
            }

            if (!string.IsNullOrEmpty(img))
            {
                cols.Add("img");
                vals.Add("@img");
                parameters.Add(new SqlParameter("@img", img));
            }

            string query = $"INSERT INTO {tableName} ({string.Join(",", cols)}) VALUES ({string.Join(",", vals)})";
            DC.Database.ExecuteSqlCommand(query, parameters.ToArray());
        }

        public void Update(string tableName, string code, string titleEn, string titleAr, string d_order = "", string img = "")
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");

            var updates = new List<string>
            {
                "TitleEn = @titleEn",
                "TitleAr = @titleAr"
            };
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@titleEn", FixString(titleEn)),
                new SqlParameter("@titleAr", FixString(titleAr)),
                new SqlParameter("@code", code)
            };

            if (!string.IsNullOrEmpty(d_order))
            {
                updates.Add("D_Order = @d_order");
                parameters.Add(new SqlParameter("@d_order", d_order));
            }

            if (!string.IsNullOrEmpty(img))
            {
                updates.Add("img = @img");
                parameters.Add(new SqlParameter("@img", img));
            }

            string query = $"UPDATE {tableName} SET {string.Join(", ", updates)} WHERE Code = @code";
            DC.Database.ExecuteSqlCommand(query, parameters.ToArray());
        }

        public void Delete(string tableName, string  id)
        {
            //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");
            string query = $"DELETE FROM {tableName} WHERE Code = @id";
            DC.Database.ExecuteSqlCommand(query, new SqlParameter("@id", id));
        }

        //public void DeleteList(string tableName, List<string> codes)
        //{
        //    //if (!IsValidTable(tableName)) throw new ArgumentException("Invalid table name");
        //    string codeList = string.Join(",", codes);
        //    string query = $"DELETE FROM {tableName} WHERE Code IN ({codeList})";
        //    DC.Database.ExecuteSqlCommand(query);
        //}
        public void DeleteList(string TableName, string list)
        {
            string q = "delete from " + TableName + " where code in (" + list + ")";
            DC.Database.ExecuteSqlCommand(q);
        }

        private string FixString(string value) => string.IsNullOrWhiteSpace(value) || value == "0" ? "0" : value;
    }
}






//using Infrastructure.DAL.Model.DB;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;

//namespace Infrastructure.DAL
//{

//    public class LookupModel
//    {
//        public int Code { get; set; }
//        public string TitleEn { get; set; }
//        public string TitleAr { get; set; }
//        public int d_order { get; set; }
//        public string img { get; set; }

//    }
//    public class LookupMaster : BaseRepository
//    {

//        public LookupMaster(AssetsEntitiesNew _context) : base(_context)
//        {

//        }

//        public List<LookupModel> GetItems(string TableName, string FilterStr, string d_order = "", string refname = "", string refvalue = "")
//        {
//            string query = "SELECT * ";
//            query += " FROM " + TableName;
//            query += " where 1=1";

//            if (FilterStr != "" && FilterStr != "0")
//            {
//                query += " and ( TitleEn like N'%" + FilterStr + "%' or TitleAr like N'%" + FilterStr + "%')";
//            }

//            if (refvalue != "" && refvalue != "0")
//            {
//                query += " and (" + refname + "=" + refvalue + ")";
//            }
//            if (d_order != "")
//            {
//                query += " order by  d_order asc";
//            }
//            var result = DC.Database.SqlQuery<LookupModel>(query);
//            return result.ToList<LookupModel>();
//        }

//        public List<LookupModel> FillLookup(string TableName)
//        {
//            string query = "select * from " + TableName;// " order by  TitleEn";
//                                                        // return ABOBasic.ins.ExecuteDs(query);
//            var result = DC.Database.SqlQuery<LookupModel>(query);
//            return result.ToList<LookupModel>();
//        }

//        public bool checkTextExistance(string TableName, string TextToCompair)
//        {
//            string query = ("select * from " + TableName + " where TitleAr like N'" + TextToCompair + "'");

//            var result = DC.Database.SqlQuery<LookupModel>(query).ToList();
//            // result.ToList<LookupModel>();

//            if (result != null && result.Count > 0)
//            {
//                return true;
//            }

//            return false;
//        }

//        public LookupModel GetDetails(string TableName, string code)
//        {
//            string query = ("select * from " + TableName + " where code=" + code);


//            var result = DC.Database.SqlQuery<LookupModel>(query);
//            return result.FirstOrDefault<LookupModel>();

//        }

//        public void Insert(string TableName, string TitleEn, string TitleAr, string D_Order = "", string refName = "", string refvalue = "", string img = "")
//        {
//            string q = "insert into " + TableName + "(TitleEn,TitleAr " + (D_Order != "" ? ",D_Order" : "") + (refName != "" ? "," + refName + "" : "") + (img != "" ? ",img" : "") + ")";
//            q += " values(N'" + FixString(TitleEn) + "',N'" + FixString(TitleAr) + "'" + (D_Order != "" ? "," + D_Order : "") + (refvalue != "" ? "," + refvalue : "") + (img != "" ? ",'" + img + "'" : "") + ")";

//            DC.Database.ExecuteSqlCommand(q);
//        }

//        public void Update(string TableName, string code, string TitleEn, string TitleAr, string D_Order = "", string img = "")
//        {
//            string q = "update " + TableName + " set TitleEn=N'" + FixString(TitleEn) + "',TitleAr=N'" + FixString(TitleAr) + "'" + (D_Order != "" ? ",D_Order=" + D_Order : "") + (img != "" ? ",img='" + img + "'" : "");
//            q += " where code = " + code;

//            DC.Database.ExecuteSqlCommand(q);

//        }
//        public void Delete(string TableName, string id)
//        {
//            string q = ("delete from " + TableName + " where code=" + id);

//            DC.Database.ExecuteSqlCommand(q);

//        }
//        public void DeleteList(string TableName, string list)
//        {
//            string q = "delete from " + TableName + " where code in (" + list + ")";
//            DC.Database.ExecuteSqlCommand(q);
//        }
//        private string FixString(string per)
//        {
//            if (per.Equals("") | per.Equals("0"))
//            {
//                return "0";
//            }
//            else
//            {
//                return per;
//            }
//        }
//    }
//}
