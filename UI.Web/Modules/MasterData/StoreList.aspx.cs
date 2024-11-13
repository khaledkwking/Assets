using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure.DAL;

namespace UI.Web.Modules.MasterData
{
    public partial class StoreList : System.Web.UI.Page
    {
        //public StoreListMaster objLookup = IoC.Resolve<StoreListMaster>();
        public static LooksUpsRepository ins = new LooksUpsRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        private void FillGrid()
        {
            var masterList = ins.FillLocation().Where(o=> o.LocationType==4).ToList();
            lblcount.Text = (Resources.Utilities.foundTotal + (masterList.Count.ToString() + Resources.Utilities.records));
            //decimal c = System.Math.Floor(Convert.ToDecimal(masterList.Count / grdData.PageSize));
            
            grdData.DataSource = masterList;
            grdData.DataBind();
            int _totalCount = masterList.Count;
            // pager1.ItemCount = _totalCount;

        }
    }
}