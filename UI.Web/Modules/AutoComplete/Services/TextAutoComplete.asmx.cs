using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;

namespace UI.Web.Modules.AutoComplete.Services
{
    /// <summary>
    /// Summary description for TextAutoComplete
    /// </summary>
    [WebService(Namespace = "http://cmgs.gov.kw/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TextAutoComplete : System.Web.Services.WebService
    {

        
        [WebMethod]
        public string[] ItemAutoCompete(string prefixText, int count)
        {
            System.Collections.Generic.List<string> items = new System.Collections.Generic.List<string>(count);
            ArrayList name = new ArrayList();
            ArrayList value = new ArrayList();

            name.Add("@pre");
            value.Add(prefixText.ToLower());

            var result = LooksUpsRepository.ins.FillItemsAuto(prefixText.ToLower());

             foreach (var item in result)
            {
                items.Add(item.Name);

            }

            return items.ToArray();

         }
         
    }
}
