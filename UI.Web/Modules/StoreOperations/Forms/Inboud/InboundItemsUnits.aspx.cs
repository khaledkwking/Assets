using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.StoreOperations.Forms.Inboud
{
    public partial class InboundItemsUnits : BaseFormAdmin
    {
        #region "Page Members"
        public InboundRepository objRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = "Inbound Items ";

        #endregion

        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundOperrations.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}

                fillLookups();

                txtbarcode.Text = GenerateBar("0");

                if (Request.QueryString["id"] == null)
                {
                    string script = FormatpopupErrorMSG(Resources.Alerts.SorryFailToretriveData + " Query string Missing", "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    ViewState["itemID"] = "0";
                    return;
                }
                else
                {
                    ViewState["itemID"] = Request.QueryString["id"].ToString();

                }

                ViewState["inboundItemID"] = "0";
                if (Request.QueryString["Itemid"] != null)
                {
                    ViewState["inboundItemID"] = Request.QueryString["Itemid"].ToString();
                    fillItemInformation();
                }


            }

        }
   

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveItemInformation();
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ClearForm();
        }
     
        protected void btnNew_Click(object sender, EventArgs e)
        {

            this.ClearForm();
           
        }
        #endregion

        #region "Fill Information"
         private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return "Add New Record Information";
            }
            else
            {
                return "Edit Record Information";
            }

        }
        private void fillItemInformation()
        {
            //var objList = objRepository.showInboundItemDetails(ZeroIntergerIFNull(ViewState["inboundItemID"].ToString()));
            //if ((objList != null))
            //{

            //    txtShippmentReceiptNo.Text = objList.ShippmentReceiptNo;
            //    txtShippmentReceiptDate.Text = objList.ShippmentReceiptDate.ToString();
            //    lstConsignee.SelectedValue = gets(objList.ConsigneeCode);

            //    txtbarcode.Text = objList.barcode;

            //    lstItemType.SelectedValue = gets(objList.ItemType);
            //    txtAlertParty.Text = objList.AlertParty;
            //    lstGoodCategoryCode.SelectedValue = gets(objList.GoodCategoryCode);

            //    txtConsiderations.Text = objList.Considerations;
            //    txtGoodDescirption.Text = objList.GoodDescirption;
            //    lstWeightUnitCode.SelectedValue = gets(objList.WeightUnitCode);

            //    txtNetWeight.Text = gets(objList.NetWeight);
            //    txtGrossWeight.Text = gets(objList.GrossWeight);
            //    lstQtyUnitCode.SelectedValue = gets(objList.QtyUnitCode);

            //    txtQty.Text = gets(objList.Qty);
            //    txtEstimatedAmount.Text = gets(objList.EstimatedAmount);
            //    lstCurrency.SelectedValue = gets(objList.CurrencyCode);
            //    txtQtyActualReceived.Text = gets(objList.QtyActualReceived);

            //    txtNetWeightActualReceived.Text = gets(objList.NetWeightActualReceived);

            //    txtGrossWeightActualReceived.Text = gets(objList.GrossWeightActualReceived);
            //    lstLocationCode.SelectedValue = gets(objList.LocationCode);
            //    txtLocationNo.Text = gets(objList.LocationNo);

            //    txtNotes.Text = gets(objList.Notes);
            //    txtGoodNotes.Text = gets(objList.GoodNotes);


            //}

           
            lblSubTitle.Text = this.GetTitle(false);
            BindUnits();
            // Fill Item Unites
          

        }
        private void ClearForm()
        {
            //txttitleEn.Text = "";
            //txttitleAr.Text = "";
            //txtRef.Text = "";

            ViewState["inboundItemID"] ="0";
           
            lblSubTitle.Text = this.GetTitle(true);
        }
        private void BindUnits()
        {



          //  var objUnitList = objRepository.FillItemUnits(ZeroIntergerIFNull(ViewState["inboundItemID"].ToString()));

          //  //if (objUnitList != null)
            



          //  //Bind Grid
          ////  List<ItemUnits> itemsUnits = new List<ItemUnits>();

          //  if (txtQty.Text != "")
          //  {
          //      if (objUnitList == null || objUnitList.Count == 0)
          //      {
          //          for (int i = 0; i < Convert.ToInt32(txtQty.Text); i++)
          //          {
          //              objUnitList.Add(new ItemUnits());
          //          }

          //          grdItemUnits.DataSource = objUnitList;
          //          grdItemUnits.DataBind();

          //      }
          //      else if (Convert.ToInt32(txtQty.Text) > objUnitList.Count)
          //      {

          //          for (int i = objUnitList.Count; i < Convert.ToInt32(txtQty.Text); i++)
          //          {
          //              objUnitList.Add(new ItemUnits());
          //          }
          //          grdItemUnits.DataSource = objUnitList;
          //          grdItemUnits.DataBind();
          //      }
          //      else
          //      {
          //          grdItemUnits.DataSource = objUnitList;
          //          grdItemUnits.DataBind();
          //      }

          //  }

          //  //Check Item type

          //  switch (lstItemType.SelectedValue)
          //  {
          //      case "1"://Cargo

                     
          //          grdItemUnits.Columns[9].Visible = false;
          //          grdItemUnits.Columns[10].Visible = false;
          //          grdItemUnits.Columns[11].Visible = false;
          //          grdItemUnits.Columns[12].Visible = false;

          //          grdItemUnits.Columns[13].Visible = true;
          //          grdItemUnits.Columns[14].Visible = true;

          //          break;
          //      case "2"://Container

                   
          //          grdItemUnits.Columns[9].Visible = false;
          //          grdItemUnits.Columns[10].Visible = false;
          //          grdItemUnits.Columns[11].Visible = false;
          //          grdItemUnits.Columns[12].Visible = false;

          //          grdItemUnits.Columns[13].Visible = true;
          //          grdItemUnits.Columns[14].Visible = true;

          //          break;
          //      case "3"://Vehicle 7-11
                   
          //          grdItemUnits.Columns[9].Visible = true;
          //          grdItemUnits.Columns[10].Visible = true;
          //          grdItemUnits.Columns[11].Visible = true;
          //          grdItemUnits.Columns[12].Visible = true;

          //          grdItemUnits.Columns[13].Visible = false;
          //          grdItemUnits.Columns[14].Visible = false;

          //          break;
          //      default:
          //          break;
          //  }

        }
        private void fillLookups()
        {
            //FillDll(LooksUpsRepository.ins.FillConsignee(), lstConsignee, "FullNameEn", "Code");
            //FillDll(LooksUpsRepository.ins.FillItemsTypes(), lstItemType, "TitleEn", "Code");
            //FillDll(LooksUpsRepository.ins.FillGoodCategory(), lstGoodCategoryCode, "TitleEn", "Code");


            //FillDll(LooksUpsRepository.ins.FillWeightUnitCode(), lstWeightUnitCode, "TitleEn", "Code");

            //FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstQtyUnitCode, "TitleEn", "Code");
            ////  FillDll(LooksUpsRepository.ins.FillDepositeDeclaration(), lstDepositeDeclarationType, "TitleEn", "Code");

            //FillDll(LooksUpsRepository.ins.FillLocation(), lstLocationCode, "TitleEn", "Code");
            //FillDll(LooksUpsRepository.ins.Fillcurrency(), lstCurrency, "TitleEn", "Code");



            //// Fill Gride 


            //var VehicleTypeList = LooksUpsRepository.ins.FillVehicleTypes();
            //var VehicleCategoryList = LooksUpsRepository.ins.FillVehicleCategory();
            //var VehiclecolorList = LooksUpsRepository.ins.FillcolorList();

            //Session["VehicleTypeList"] = VehicleTypeList;
            //Session["VehicleCategoryList"] = VehicleCategoryList;
            //Session["VehiclecolorList"] = VehiclecolorList;


        }

        private void SaveItemUnitInformation(long ItemID,int? ItemType)
        {


            //for (int i = 0; i <= grdItemUnits.Items.Count - 1; i++)
            //{
            //    if (grdItemUnits.Items[i].ItemType == ListItemType.Item || grdItemUnits.Items[i].ItemType == ListItemType.AlternatingItem)
            //    {
            //        if (grdItemUnits.Items[i].Cells[1].Text == "" || grdItemUnits.Items[i].Cells[1].Text == "&nbsp;")
            //        {
            //            //insert Item Unit

            //            ItemUnits objUnit = new ItemUnits();
            //            objUnit.ItemCode = ItemID;
            //            objUnit.UnitRef = ((TextBox)grdItemUnits.Items[i].FindControl("txtRef")).Text;
            //            if (gets(ItemType).Equals("3"))
            //            {
            //                objUnit.VTypeCode = ZeroIntergerIFNull(((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitType")).SelectedValue);
            //                objUnit.VCategoryCode = ZeroIntergerIFNull(((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitBrand")).SelectedValue);

            //            }


            //            objUnit.VModel = ((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitmodel")).SelectedValue;
            //            objUnit.VColor = ((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitColor")).SelectedValue;
            //            objUnit.Notes = ((TextBox)grdItemUnits.Items[i].FindControl("txtUnitNote")).Text;

            //            objUnit.ContainerSize = ((TextBox)grdItemUnits.Items[i].FindControl("txtContainersize")).Text;
            //            objUnit.ContainerType = ((TextBox)grdItemUnits.Items[i].FindControl("txtContainerType")).Text;

            //            objUnit.ItemUnitStatus = "0";
            //            objRepository.AddItemsUnit(objUnit);
            //            // Update Actual Quantity and Weight;


            //        }
            //        else
            //        {//Update Uint

            //            var objUnit = objRepository.getItemUnitDetails(ZeroIntergerIFNull( grdItemUnits.Items[i].Cells[0].Text));
            //            if (objUnit != null)
            //            {
            //                objUnit.ItemCode = ItemID;
            //                objUnit.UnitRef = ((TextBox)grdItemUnits.Items[i].FindControl("txtRef")).Text;

            //                if (gets(ItemType).Equals("3"))
            //                {
            //                    objUnit.VTypeCode = ZeroIntergerIFNull(((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitType")).SelectedValue);
            //                    objUnit.VCategoryCode = ZeroIntergerIFNull(((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitBrand")).SelectedValue);

            //                }

            //                objUnit.VModel = ((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitmodel")).SelectedValue;
            //                objUnit.VColor = ((DropDownList)grdItemUnits.Items[i].FindControl("lstUnitColor")).SelectedItem.Text;
            //                objUnit.Notes = ((TextBox)grdItemUnits.Items[i].FindControl("txtUnitNote")).Text;

            //                objUnit.ContainerSize = ((TextBox)grdItemUnits.Items[i].FindControl("txtContainersize")).Text;
            //                objUnit.ContainerType = ((TextBox)grdItemUnits.Items[i].FindControl("txtContainerType")).Text;


            //                objRepository.UpdateItemunit(objUnit);
            //            }
                     
            //        }

            //    }
            //}



        }
        private void SaveItemInformation()
        {

            //string script = "";
            //try
            //{
            //    Item obj = new Item();
            //    if (gets(ViewState["inboundItemID"]).Equals("0"))
            //    {//Save
            //        obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());

            //        obj.ShippmentReceiptNo = txtShippmentReceiptNo.Text;
            //                obj.barcode = txtbarcode.Text;

            //        obj.ShippmentReceiptDate = NullDateifEmpty(txtShippmentReceiptDate.Text);
            //        obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
            //        obj.ItemType = ZeroIntergerIFNull(lstItemType.SelectedValue);

            //        obj.AlertParty = txtAlertParty.Text;
            //        obj.GoodCategoryCode = ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue);
            //        obj.Considerations = txtConsiderations.Text;
            //        obj.GoodDescirption = txtGoodDescirption.Text;
            //        obj.WeightUnitCode = ZeroIntergerIFNull(lstWeightUnitCode.SelectedValue);
            //        obj.NetWeight = ZeroIFNull(txtNetWeight.Text);
            //        obj.GrossWeight = ZeroIFNull(txtGrossWeight.Text);

            //        obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
            //        obj.Qty = ZeroIntergerIFNull(txtQty.Text);
            //        obj.EstimatedAmount = ZeroIFNull(txtEstimatedAmount.Text);
            //        obj.CurrencyCode = ZeroIntergerIFNull(lstCurrency.SelectedValue);
            //        obj.QtyActualReceived = ZeroIFNull(txtQtyActualReceived.Text);

            //        obj.NetWeightActualReceived = ZeroIFNull(txtNetWeightActualReceived.Text);
            //        obj.GrossWeightActualReceived = ZeroIFNull(txtGrossWeightActualReceived.Text);

            //        //obj.LocationCode = ZeroIntergerIFNull(lstLocationCode.SelectedValue);
            //        //obj.LocationNo = txtLocationNo.Text;
            //        obj.Notes = txtNotes.Text;
            //        obj.GoodNotes = txtGoodNotes.Text;

            //        objRepository.AddItems(obj);

            //        //Save Item Unites
            //        SaveItemUnitInformation(obj.Code, obj.ItemType);







            //    }
            //    else
            //    { //Update 
            //        obj = objRepository.GetInboundItemDetails(ZeroIntergerIFNull(ViewState["inboundItemID"].ToString()));
            //        obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());

            //        obj.ShippmentReceiptNo = txtShippmentReceiptNo.Text;
            //        obj.ShippmentReceiptDate = NullDateifEmpty(txtShippmentReceiptDate.Text);
            //        obj.barcode = txtbarcode.Text;

            //        obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
            //        obj.ItemType = ZeroIntergerIFNull(lstItemType.SelectedValue);

            //        obj.AlertParty = txtAlertParty.Text;
            //        obj.GoodCategoryCode = ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue);
            //        obj.Considerations = txtConsiderations.Text;
            //        obj.GoodDescirption = txtGoodDescirption.Text;
            //        obj.WeightUnitCode = ZeroIntergerIFNull(lstWeightUnitCode.SelectedValue);
            //        obj.NetWeight = ZeroIFNull(txtNetWeight.Text);
            //        obj.GrossWeight = ZeroIFNull(txtGrossWeight.Text);

            //        obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
            //        obj.Qty = ZeroIntergerIFNull(txtQty.Text);
            //        obj.EstimatedAmount = ZeroIFNull(txtEstimatedAmount.Text);
            //        obj.CurrencyCode = ZeroIntergerIFNull(lstCurrency.SelectedValue);
            //        obj.QtyActualReceived = ZeroIFNull(txtQtyActualReceived.Text);

            //        obj.NetWeightActualReceived = ZeroIFNull(txtNetWeightActualReceived.Text);
            //        obj.GrossWeightActualReceived = ZeroIFNull(txtGrossWeightActualReceived.Text);

            //        obj.LocationCode = ZeroIntergerIFNull(lstLocationCode.SelectedValue);
            //        obj.LocationNo = txtLocationNo.Text;
            //        obj.Notes = txtNotes.Text;
            //        obj.GoodNotes = txtGoodNotes.Text;
            //        objRepository.UpdateItem(obj);


            //        //Upodate Item Units list

            //        //Save Item Unites
            //        SaveItemUnitInformation(obj.Code, obj.ItemType);

            //    }

            //    ClearForm();


            //    script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            //}
            //catch (Exception ex)
            //{


            //    script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            //}

        }


        #endregion

      

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            BindUnits();
        }

        protected void grdItemUnits_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            // FIll Units Gride Unites
            if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                switch (lstItemType.SelectedValue)
                {
                    case "1"://Cargo
                        break;
                    case "2"://Container
                        break;
                    case "3"://Vehicle
                        {
                            var VehicleTypeList = Session["VehicleTypeList"];
                            var VehicleCategoryList = Session["VehicleCategoryList"];
                            var VehiclecolorList = Session["VehiclecolorList"];

                            FillDll(VehicleTypeList, (DropDownList)e.Item.FindControl("lstUnitType"), "TitleEn", "Code");
                            FillDll(VehicleCategoryList, (DropDownList)e.Item.FindControl("lstUnitBrand"), "TitleEn", "Code");
                            FillDll(VehiclecolorList, (DropDownList)e.Item.FindControl("lstUnitColor"), "TitleEn", "TitleEn");

                            for (int j = DateTime.Now.Year - 20; j < DateTime.Now.Year + 1; j++)
                            {
                                ((DropDownList)e.Item.FindControl("lstUnitmodel")).Items.Add(new ListItem(j.ToString(), j.ToString()));
                            }

                            try
                            {
                                ((DropDownList)e.Item.FindControl("lstUnitType")).SelectedValue = e.Item.Cells[4].Text;
                            }
                            catch (Exception)
                            {

                               
                            }

                            try
                            {
                                ((DropDownList)e.Item.FindControl("lstUnitBrand")).SelectedValue = e.Item.Cells[5].Text;
                            }
                            catch (Exception)
                            {


                            }

                            try
                            {
                                ((DropDownList)e.Item.FindControl("lstUnitmodel")).SelectedValue = e.Item.Cells[6].Text;
                            }
                            catch (Exception)
                            {


                            }

                            try
                            {
                                ((DropDownList)e.Item.FindControl("lstUnitColor")).SelectedValue = e.Item.Cells[7].Text;
                            }
                            catch (Exception)
                            {


                            }



                            // Set BoundedVlaues


                            break;
                        }
                    default:
                        break;
                }

            }

          

        }

        protected void lstItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtQty.Text=="")
            {
                txtQty.Text = "1";
            }
            BindUnits();
        }
    }
}