using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using Infrastructure.DAL.Model.DB;
using UI.Web.Core.Enums;
using UI.Web.Admin.Controller;
using Infrastructure.DAL;
using Infrastructure;

namespace UI.Web.Modules.Assets
{
    public partial class MigrateDataaspx : BaseFormAdmin
    {
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public LocationsRepository objLocationRepository = IoC.Resolve<LocationsRepository>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMigrate_Click(object sender, EventArgs e)
        {
            using (var en = new AssetsEntitiesNew())
            {
                //var queryMember = en.Item_tbl.Where(o=> o.Emp_Id!=0).GroupBy(x => new { x.Emp_Id, x.Item_AssDate}).Select(r => new ItemDTO { Emp_Id = r.Key.Emp_Id,Item_AssDate = r.Key.Item_AssDate}).ToList();
                var queryMember = en.Item_tbl.Where(o => o.Emp_Id != 0 && o.Emp_Id != null).GroupBy(x => new { x.Emp_Id }).Select(r => new ItemDTO{Emp_Id = r.Key.Emp_Id }).ToList();
                foreach (var item in queryMember)
                {
                    int? Emp_Id = item.Emp_Id;
                    var QDetails = en.Item_tbl.Where(o => o.Emp_Id == Emp_Id).ToList();
                    DateTime? AssetDate = QDetails.FirstOrDefault().Item_AssDate;

                    AssetsEventTrackingHeader objHeader = new AssetsEventTrackingHeader();
                    objHeader.RequestDate = AssetDate;
                    objHeader.DueDate = AssetDate; 
                    objHeader.RequestRefCode = Guid.NewGuid().ToString();
                    objHeader.RequestActionType = 1;  //(int) CustodyRequestType.CheckOut;
                    objHeader.ProcessType = 1;
                    if (AssetDate != null)
                    {
                        objHeader.TMonth = AssetDate.Value.Month;
                        objHeader.TYear = AssetDate.Value.Year;
                    }
                 
                    objHeader.Serial = generateRequestSerial();
                    objHeader.RequestNotes = "تقرير الجرد في نظام العهد بتاريخ : " + DateTime.Now.ToString();

                    long? RoomID = en.Item_tbl.Where(o => o.Emp_Id == Emp_Id).FirstOrDefault().Room_Id;
                    int LocationID = 0;
                    int? OrgChartID = 0;
                    var QLocation = en.D_Locations.Where(o => o.MigrationRef == RoomID && o.LocationType==3).ToList();
                    if(QLocation.Count > 0)
                    {
                        LocationID= QLocation.FirstOrDefault().Code;
                        OrgChartID = QLocation.FirstOrDefault().OrgChartRefCode;
                        objHeader.ToLocationId = LocationID;
                        objHeader.OrgChartRefCode = OrgChartID;
                        // Check if Emplyee Location Saved 
                        if (Emp_Id != 0)
                        {
                            var emplocation = objRepository.getEmployeeLocations(ZeroIntergerIFNull(Emp_Id.ToString()));
                            if (emplocation == null)
                            {
                                D_EmployeeLocations locationObj = new D_EmployeeLocations();
                                locationObj.EmpCode = ZeroIntergerIFNull(Emp_Id.ToString());
                                locationObj.LocationCode = QLocation.FirstOrDefault().Code;
                                objRepository.AddEmployeeLoation(locationObj);
                            }
                        }
                    }
                   

                    string EmpName = "";
                    var QEmployee = en.Employee_tbl.Where(o => o.Emp_Id == Emp_Id).ToList();
                    if(QEmployee.Count>0)
                        EmpName = QEmployee.FirstOrDefault().Emp_Name;

                    objHeader.EmpName = EmpName;
                    objHeader.EmpRefCode = Emp_Id;


                    objHeader.CreatedAt = DateTime.Now;
                    objHeader.CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                    objRepository.AddAssetsEventTrackingHeader(objHeader);
                    int HeaderID = objHeader.Code;
                    
                  //  var QDetails = en.Item_tbl.Where(o => o.Emp_Id == Emp_Id).ToList();
                    foreach (var itemD in QDetails)
                    {
                        AssetsEventTracking obj = new AssetsEventTracking();

                        int? Code = null;
                        var QCard= en.D_ItemCard.Where(o => o.REFID == itemD.CatSub_Id).ToList();
                        if(QCard.Count>0)
                        {
                            Code = QCard.FirstOrDefault().Code;
                        }

                        obj.RequestHeaderCode = HeaderID;
                        obj.AssetCode = Code;
                        //obj.RequestItemPrice = ZerodoubleIFNull(itemD.PurchasingPower);
                        obj.ActionDate = AssetDate;
                        obj.actionId = 2;// checkout;
                        obj.statusId = 2;// CHecked OUt ;
                        obj.ToLocationId = LocationID;

                        obj.EmpName = EmpName;
                        obj.EmpRefCode = Emp_Id;
                        
                        obj.Notes = "تقرير الجرد في نظام العهد بتاريخ : " + DateTime.Now.ToString();
                        obj.StoreRequestRefCode = null;
                        obj.Qty = itemD.Item_Count;

                        obj.CreatedAt = AssetDate;
                        obj.CreatedBy = null;

                        objRepository.AddEventTracking(obj);

                    }
                }
            }
        }

        protected void btnMigrateRoom_Click(object sender, EventArgs e)
        {
            using (var en = new AssetsEntitiesNew())
            {
                //var queryMember = en.Item_tbl.Where(o=> o.Emp_Id!=0).GroupBy(x => new { x.Emp_Id, x.Item_AssDate}).Select(r => new ItemDTO { Emp_Id = r.Key.Emp_Id,Item_AssDate = r.Key.Item_AssDate}).ToList();
                // var queryRoom = en.Item_tbl.Where(o => o.Room_Id != 0 && o.Room_Id != -1 && (o.Emp_Id == 0|| o.Emp_Id == null)).GroupBy(x => new { x.Room_Id }).Select(r => new ItemDTORoom { Room_Id = r.Key.Room_Id }).ToList();
                var queryRoom = en.Item_tbl.Where(o => o.Emp_Id == 0 || o.Emp_Id == null).GroupBy(x => new { x.Room_Id }).Select(r => new ItemDTORoom { Room_Id = r.Key.Room_Id }).ToList();

                foreach (var item in queryRoom)
                {
                    long? Room_ID = item.Room_Id;
                    var QDetails = en.Item_tbl.Where(o => o.Room_Id == Room_ID).ToList();
                    DateTime? AssetDate = QDetails.FirstOrDefault().Item_AssDate;

                    AssetsEventTrackingHeader objHeader = new AssetsEventTrackingHeader();
                    objHeader.RequestDate = AssetDate;
                    objHeader.DueDate = null;
                    objHeader.RequestRefCode = Guid.NewGuid().ToString();
                    objHeader.RequestActionType = 1;  //(int) CustodyRequestType.CheckOut;
                    objHeader.ProcessType = 2;
                    if (AssetDate != null)
                    {
                        objHeader.TMonth = AssetDate.Value.Month;
                        objHeader.TYear = AssetDate.Value.Year;
                    }
                    objHeader.Serial = generateRequestSerial();
                    
                    int LocationID = 0;
                    int? OrgChartID = 0;
                    var QLocation = en.D_Locations.Where(o => o.LocationRefCode == Room_ID.ToString() && o.LocationType == 3).ToList();
                    if (QLocation.Count > 0)
                    {
                        LocationID = QLocation.FirstOrDefault().Code;
                        OrgChartID = QLocation.FirstOrDefault().OrgChartRefCode;
                        objHeader.ToLocationId = LocationID;
                        objHeader.OrgChartRefCode = OrgChartID;
                        // Check if Emplyee Location Saved 
                    }

                    objHeader.RequestNotes = "تقرير الجرد في نظام العهد بتاريخ : " + DateTime.Now.ToString();



                    objHeader.CreatedAt = AssetDate;
                    objHeader.CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                    objRepository.AddAssetsEventTrackingHeader(objHeader);
                    int HeaderID = objHeader.Code;

                    //var QDetails = en.Item_tbl.Where(o => o.Room_Id == Room_ID).ToList();
                    foreach (var itemD in QDetails)
                    {
                        AssetsEventTracking obj = new AssetsEventTracking();

                        int? Code = null;
                        var QCard = en.D_ItemCard.Where(o => o.REFID == itemD.CatSub_Id).ToList();
                        if (QCard.Count > 0)
                        {
                            Code = QCard.FirstOrDefault().Code;
                        }

                        obj.RequestHeaderCode = HeaderID;
                        obj.AssetCode = Code;
                        //obj.RequestItemPrice = ZerodoubleIFNull(itemD.PurchasingPower);
                        obj.ActionDate = AssetDate;
                        obj.actionId = 2;// checkout;
                        obj.statusId = 2;// CHecked OUt ;
                        obj.ToLocationId = LocationID;

                        obj.Notes = "تقرير الجرد في نظام العهد بتاريخ : " + DateTime.Now.ToString();
                        obj.StoreRequestRefCode = null;
                        obj.Qty = itemD.Item_Count;

                        obj.CreatedAt = AssetDate;
                        obj.CreatedBy = null;

                        objRepository.AddEventTracking(obj);
                    }
                }
            }
        }
    }
    public class ItemDTO
    {
        public int? Emp_Id { get; set; }
        public DateTime? Item_AssDate { get; set; }
    }
    public class ItemDTORoom
    {
        public long? Room_Id { get; set; }
        public DateTime? Item_AssDate { get; set; }
    }
}