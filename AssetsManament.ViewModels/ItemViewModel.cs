using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class ItemViewModel
    {

        public int Code { get; set; }
        public string ItemRefCode { get; set; }
        public string ItemBarCode { get; set; }
        public string ItemRFIDCode { get; set; }
        public string ItemFinanceCode { get; set; }
        public string ItemQrCode { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemDescEn { get; set; }
        public string ItemDescAr { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<double> ItemBasePrice { get; set; }
        public Nullable<int> QUnitCode { get; set; }
        public Nullable<int> MinQty { get; set; }
        public string Notes { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public string ItemImage { get; set; }
        public string D_ItemsCategoryTitleAr { get; set; }
        public string D_ItemsCategoryTitleEn { get; set; }
        public string D_QtyUnitTitleAr { get; set; }
        public string D_QtyUnitTitleEn { get; set; }


    }
}