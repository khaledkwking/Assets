using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class LocationViewModel
    {
        //public int Code { get; set; }
        //public Nullable<int> LocationType { get; set; }
        //public string LocationNameEn { get; set; }
        //public string LocationNameAr { get; set; }
        //public int LocationParentId { get; set; }
        //public Nullable<int> OrgChartRefCode { get; set; }
        //public Nullable<int> EmpInCharge { get; set; }
        //public Nullable<bool> IsScrap { get; set; }
        //public string LocationRefCode { get; set; }

        //public string LocationTypeTitleEn { get; set; }
        //public string LocationTypeTitleAr { get; set; }

        //public List<LocationViewModel> subs { get; set; }


        public int id { get; set; }
        public string title { get; set; }
        public int ParentId { get; set; }
        public List<LocationViewModel> subs { get; set; }


    }

    public class LocationViewModelEdit
    {
        public int Code { get; set; }
        public Nullable<int> LocationType { get; set; }
        public string LocationNameEn { get; set; }
        public string LocationNameAr { get; set; }
        public int LocationParentId { get; set; }
        public Nullable<int> OrgChartRefCode { get; set; }
        public Nullable<int> EmpInCharge { get; set; }
        public Nullable<bool> IsScrap { get; set; }
        public string LocationRefCode { get; set; }

        public string LocationTypeTitleEn { get; set; }
        public string LocationTypeTitleAr { get; set; }

        public List<LocationViewModel> subs { get; set; }





    }

    public class EntityViewModelEdit
    {
        public int Code { get; set; }
        public Nullable<int> EntityType { get; set; }
        public string EntityNameEn { get; set; }
        public string EntityNameAr { get; set; }
        public int ParentId { get; set; }
        public List<EntityViewModelEdit> subs { get; set; }



    }


}