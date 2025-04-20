using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class CustodyListViewModel
    {

        public string ItemRefCode { get; set; }
        public int ItemCode { get; set; }
        public string ItemNameAr { get; set; }
        public Nullable<double> ItemBasePrice { get; set; }
        public Nullable<int> ToLocationId { get; set; }
        public Nullable<int> EmpRefCode { get; set; }
        public string EmpName { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> RequestItemPrice { get; set; }
        public Nullable<double> TotalPrice => Qty * RequestItemPrice;
        public string QtyUnitTitleAr { get; set; }
        public string LocationNameAr { get; set; }
        public Nullable<double> MissedQty { get; set; }
        public string StoreRequestRefCode { get; set; }
        public string path { get; set; }
        public string ItemsCategoryTitleAr { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<double> ServicePeriod { get; set; }
        public Nullable<double> ScrapPrice { get; set; }
        public Nullable<int> OrgChartRefCode { get; set; }
        public string  OrgChartRefName { get; set; }
        public string  City { get; set; }


    }
}