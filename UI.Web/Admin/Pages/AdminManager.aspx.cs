using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace UI.Web.Admin.Pages
{
    public partial class AdminManager : BaseFormAdmin
    {
        public string _PageTitle = Resources.Pages.userList;
        public PrincipalSearchResult<Principal> userlist;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";

            btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("Default.aspx");
                //}

                ViewState["Item"] = 0;
                FillDll(Security_Users.ins.FillAdminTypes(), lstadminType, "Nameen", "id");

                lstActivDirectoryUser.Items.Add(new ListItem("إختر", "0"));
                using (var context = new PrincipalContext(ContextType.Domain, "CMGS0"))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        userlist = searcher.FindAll();
                        Session["userList"] = userlist;
                        foreach (var result in userlist)
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            //Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                            //Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                            //Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                            //Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                            //Console.WriteLine();
                            lstActivDirectoryUser.Items.Add(new ListItem(" (" + gets(de.Properties["SamAccountName"].Value) + ") " + gets(de.Properties["DisplayName"].Value), gets(de.Properties["SamAccountName"].Value)));


                        }
                    }
                }
                //FillDllwithoptional_ALL(Security_Users.ins.FillAdminTypes(),   LstFilterAdminType, "Nameen", "id", "الكل");


                FillGrid();
            }

        }

        private void FillGrid()
        {
            var userList = Security_Users.ins.GetItemsByOrgCharts(0, "", ZeroIntergerIFNull(hdnSelectedNode.Value));

            if (userList != null)
            {
                lblcount.Text = (Resources.Utilities.foundTotal + (userList.Count.ToString() + Resources.Utilities.records));
                decimal c = System.Math.Ceiling(Convert.ToDecimal(userList.Count / grdData.PageSize));
                if ((c <= grdData.CurrentPageIndex))
                {
                    grdData.CurrentPageIndex = 0;
                }

                grdData.DataSource = userList;
                grdData.DataBind();


            }

        }





        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; (i
                      <= (grdData.Items.Count - 1)); i++)
            {
                string id = grdData.Items[i].Cells[0].Text;
                CheckBox check = ((CheckBox)(grdData.Items[i].FindControl("chkItem")));
                if (check.Checked)
                {
                    Security_Users.ins.Delete((Security_pr_admin)Security_Users.ins.GetDetails(ZeroIntergerIFNull(id)));
                }

            }
            this.FillGrid();
        }

        protected void grdData_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            this.ClearForm();
            ViewState["Item"] = id;
            this.FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
        }

        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#EFEFEF\';");
            }

        }

        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return "Edit Record Information";
            }

        }


        private void FillForm()
        {

            var userDetails = Security_Users.ins.GetDetails(ZeroIntergerIFNull(ViewState["Item"].ToString()));

            if (userDetails != null)
            {


                txtName.Text = userDetails.username;
                txtPassword.Text = userDetails.password;
                txtfullName.Text = userDetails.name;
                chkisactive.Checked = getBool(userDetails.IsActive);
                chkOperation.Checked = getBool(userDetails.isOperation);
                txtmobile.Text = userDetails.mobile;
                txtEmail.Text = userDetails.Email;
                txtaddress.Text = userDetails.Address;
                txtPassword.Attributes.Add("value", userDetails.password);

                if (userDetails.AdminType != null && userDetails.AdminType != 0)
                {
                    lstadminType.SelectedValue = gets(userDetails.AdminType);
                }
                lstActivDirectoryUser.SelectedValue = userDetails.username;

            }


            tblAdd.Visible = true;
            lblSubTitle.Text = this.GetTitle(false);

        }

        private string getImage(ref FileUpload txtFile)
        {
            string imgname = "";
            string temp;
            string ext;
            int inx;
            int i;
            string RandChar;
            string ValueString;
            //  Microsoft.VisualBasic.VBMath.Randomize();
            imgname = "";
            ValueString = "";
            if (!(txtFile.PostedFile == null))
            {
                if ((txtFile.PostedFile.FileName != ""))
                {
                    imgname = txtFile.PostedFile.FileName;
                    imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
                    inx = imgname.LastIndexOf(".");
                    temp = imgname.Substring(0, inx);
                    ext = imgname.Substring((inx + 1));
                    for (i = 1; (i <= 16); i++)
                    {
                        //  RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());

                        RandChar = (new Random().Next(i, 16)).ToString();
                        ValueString += RandChar;
                    }

                    imgname = (ValueString + ("." + ext));
                    txtFile.PostedFile.SaveAs(Server.MapPath(("/Layout/uploads/Adminprofile/" + imgname)));
                }

            }


            //////string imgname = "";
            //////int inx = 0;
            //////string temp = "";
            //////string ext = "";
            //////string RandChar = "";
            //////string ValueString = "";
            //////Microsoft.VisualBasic.VBMath.Randomize();
            //////imgname = "";
            //////imgname = txtImage.PostedFile.FileName;
            //////imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
            //////inx = imgname.LastIndexOf('.');
            //////temp = imgname.Substring(0, inx);
            //////ext = imgname.Substring((inx + 1));
            //////for (int i = 1; (i <= 10); i++)
            //////{
            //////    RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());
            //////    ValueString += RandChar;
            //////}
            //////imgname = (temp + (ValueString + ("." + ext)));
            //////return imgname;
            return imgname;
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtmobile.Text = "";
            txtfullName.Text = "";
            chkOperation.Checked = false;
            chkisactive.Checked = false;
            txtPassword.Text = "";
            ViewState["Item"] = 0;
            tblAdd.Visible = false;

            tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string Script = "";
            try
            {
                //    string img1 = "";
                string img1 = this.getImage(ref txtImage);
                string displayName = "";

                using (var context = new PrincipalContext(ContextType.Domain, "CMGS0"))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        userlist = searcher.FindAll();
                        Session["userList"] = userlist;
                        foreach (var result in userlist)
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                            if (gets(de.Properties["SamAccountName"].Value) == lstActivDirectoryUser.SelectedValue)
                            {
                                displayName = userlist.ToList().Where(x => x.SamAccountName == lstActivDirectoryUser.SelectedValue).FirstOrDefault().DisplayName;
                            }


                        }
                    }
                }

                Security_pr_admin obj = new Security_pr_admin();
                if (ViewState["Item"].ToString().Equals("0"))
                {
                    // CHeck User Existance
                    if (Security_Users.ins.CheckUserNameExitance(lstActivDirectoryUser.SelectedValue))
                    {
                        Script = FormatpopupErrorMSG("عفوا , هذا الاسم تم اضافته من قبل ", "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
                        return;
                    }

                    //obj.name = txtfullName.Text;
                    //obj.username = txtName.Text;
                    obj.name = displayName;
                    obj.username = lstActivDirectoryUser.SelectedValue;

                    obj.password = txtPassword.Text;
                    obj.AdminType = ZeroIntergerIFNull(lstadminType.SelectedValue);
                    obj.IsActive = chkisactive.Checked;
                    obj.isOperation = chkOperation.Checked;
                    obj.mobile = txtmobile.Text;
                    obj.Email = txtEmail.Text;
                    obj.Address = txtaddress.Text;
                    if (img1 != "")
                        obj.AdminPhoto = img1;




                    Security_Users.ins.Add(obj);

                }
                else
                {
                    obj = Security_Users.ins.GetDetails(ZeroIntergerIFNull(ViewState["Item"].ToString()));
                    //obj.name = txtfullName.Text;
                    //obj.username = txtName.Text;
                    obj.name = displayName;
                    obj.username = lstActivDirectoryUser.SelectedValue;
                    obj.password = txtPassword.Text;
                    obj.AdminType = ZeroIntergerIFNull(lstadminType.SelectedValue);
                    obj.IsActive = chkisactive.Checked;
                    obj.isOperation = chkOperation.Checked;
                    obj.mobile = txtmobile.Text;
                    obj.Email = txtEmail.Text;
                    obj.Address = txtaddress.Text;
                    if (img1 != "")
                        obj.AdminPhoto = img1;


                    Security_Users.ins.Update(obj);
                }

                Script = FormatpopupErrorMSG("Data Saved Successfully", "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
                this.ClearForm();
                this.FillGrid();
            }
            catch (Exception ex)
            {
                Script = FormatpopupErrorMSG("Fail To Save Data", "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
                lblerror.ForeColor = System.Drawing.Color.Red;
                lblerror.Text = ("Error :" + ex.Message);
            }

        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ClearForm();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }



        protected void btnNew_Click(object sender, EventArgs e)
        {

            this.ClearForm();
            tblAdd.Visible = true;
            tblshow.Visible = false;
        }
    }
}