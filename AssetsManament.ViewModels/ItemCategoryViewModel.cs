using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class ItemCategoryViewModel
    {
        public int Code { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public Nullable<int> Cat_ParentId { get; set; }
        public string FinanceRefCode { get; set; }


        public Nullable<double> ServicePeriod { get; set; }
        public Nullable<double> ScrapPrice { get; set; }

    }
}