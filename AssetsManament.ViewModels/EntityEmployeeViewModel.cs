using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class EntityEmployeeViewModel
    {
        public int Code { get; set; }
        public Nullable<int> JobTitleId { get; set; }
        public Nullable<int> OrgRefId { get; set; }
        public Nullable<int> EmpCode { get; set; }
        public string EmpName { get; set; }
        public string CivilId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public string JolbTitleAr { get; set; }
        public string JolbTitleEn { get; set; }
        public string EntityNameEn { get; set; }
        public string EntityNameAr { get; set; }



    }
}