using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AssetsManament.ViewModels
{
    public class OrgChartViewModel
    {

       
        public int ENTITYCODE { get; set; }
        public string ENTITYTYPE { get; set; }
        public string ENTITYNAME { get; set; }
        public string ENTITYMGR { get; set; }
        public int? PARENTCODE { get; set; }


    }
}