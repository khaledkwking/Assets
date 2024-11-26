using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.Assets
{
    public partial class AssetsTransferOut : BaseFormAdmin
    {
        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public AssetsRepository objRepository2 = IoC.Resolve<AssetsRepository>();

        public string _PageTitle = Resources.Pages.orgChart;

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            btnSave.Attributes.Add("onclick", "return ValidateITems();");
            
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}

                ViewState["itemID"] = "0";
                //  FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstQunit, Resources.Pages.TitleFiled, "Code");

                if (Request.QueryString["pid"] != null)
                {
                    hdnSelectedNode.Value = Request.QueryString["pid"].ToString();


                }

            }

        }
      
        #endregion
     
    
        #region "Fill Information"

        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return Resources.Pages.editData;
            }

        }

        #endregion
        protected void lnkDeleteOrgLocation_Click(object sender, EventArgs e)
        {
            if (hdnSelectedNode.Value != "" && hdnSelectedNode.Value != "[]")
            {
                objRepository.ResetEntityLocation(ZeroIntergerIFNull(hdnSelectedNode.Value));
                string script = FormatpopupErrorMSG("تم فك الربط بنجاح ", "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                if (hdnSelectedNode.Value != "" && hdnSelectedNode.Value != "[]")
                {
                    int? orgId = ZeroIntergerIFNull(hdnSelectedNode.Value);

                    ViewState["itemID"] = orgId;
                    string fileName = getImage(txtFile);
                    string script = "";
                    try
                    {
                        Infrastructure.DAL.Model.DB.AssetsTransferOut TrOut = new Infrastructure.DAL.Model.DB.AssetsTransferOut
                        {
                            OrgRefCode = orgId,
                            TransferDate = NullDateifEmpty(txtTransDate.Text),
                            Notes = txtNotes.Text,
                            DeliveryAttach = fileName,
                            CreatedAt = DateTime.Now,
                            CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString())
                        };

                        en.AssetsTransferOuts.Add(TrOut);
                        en.SaveChanges();

                        int TransId = TrOut.Id;
                        var TrackingHeaders = en.AssetsEventTrackingHeaders.Where(o => o.OrgChartRefCode == orgId).ToList();
                        foreach (var item in TrackingHeaders)
                        {
                            item.TranferedRequestHeaderId = TransId;
                        }
                        en.SaveChanges();
                        txtTransDate.Text = String.Empty;
                        txtNotes.Text = String.Empty;

                        script = FormatpopupErrorMSG("تم التحويل بنجاح", "3");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                // Log or display the error message
                                string Property=validationError.PropertyName;
                                string Error= validationError.ErrorMessage;
                            }
                        }

                        // Optionally, rethrow the exception or handle it as needed
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // Handle non-validation exceptions
                        string An_error_occurred = ex.Message;
                        throw;
                    }
                }
            }
        }

        private string getImage(FileUpload txtFile)
        {
            string imgname;
            string temp;
            string ext;
            int inx;
            int i;
            int RandChar;
            string ValueString;
            Random rnd = new Random();
            imgname = "";
            ValueString = "";
            if (!System.IO.Directory.Exists(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"]))))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"])));
            }

            if (!(txtFile.PostedFile == null))
            {
                if ((txtFile.PostedFile.FileName != ""))
                {
                    imgname = txtFile.PostedFile.FileName;
                    imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
                    inx = imgname.LastIndexOf(".");
                    temp = imgname.Substring(0, inx);
                    ext = imgname.Substring((inx + 1));
                    for (i = 1; (i <= 24); i++)
                    {
                        RandChar = rnd.Next(0, i) + 65;
                        ValueString += RandChar.ToString();
                    }

                    imgname = (ValueString + ("." + ext));
                    txtFile.PostedFile.SaveAs(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"] + "/" + imgname)));
                }

            }

            return imgname;
        }
    }
}