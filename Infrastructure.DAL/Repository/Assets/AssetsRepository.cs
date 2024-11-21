using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class AssetsRepository
    {
        #region "  master Data"

        public IList<view_AssetsList> getAssetsList(string inboundSerial, DateTime TransactionDatFrom, DateTime TransactionDatTo,
            int vendorCode, int LastStatusId, int LastActionId, int ItemCategoryId, int ItemCode, int EmprefCode, int targetLocation)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsList
                     orderby obj.ActionDate descending
                     where 1 == 1
                  && (inboundSerial != "" ? obj.Serial == inboundSerial : true)
                  && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : true)
                  && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatFrom : true)
                  && (vendorCode != 0 ? obj.FromVendorCode == vendorCode : true)
                  && (LastStatusId != 0 ? obj.statusId == LastStatusId : true)
                  && (LastActionId != 0 ? obj.actionId == LastActionId : true)
                  && (ItemCode != 0 ? obj.ItemCode == ItemCode : true)
                  && (ItemCategoryId != 0 ? obj.ItemCategoryId == ItemCategoryId : true)
                  && (EmprefCode != 0 ? obj.EmpRefCode == EmprefCode : true)
                  && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
                     select obj);

                return result.ToList<view_AssetsList>();
            }
        }

        public List<view_CustodyList> getAssetReceiptbyRequestCode(int headerCode, int EmprefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where 1 == 1
                      && (headerCode != 0 ? obj.RequestHeaderCode == headerCode : true)
                       && (EmprefCode != 0 ? obj.EmpRefCode == EmprefCode : true)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        public List<view_AssetsList> getAssetReceipt(int EmprefCode, int targetLocation)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsList
                     orderby obj.ActionDate descending
                     where 1 == 1
                  && (EmprefCode != 0 ? obj.EmpRefCode == EmprefCode : true)
                  && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
                     select obj);

                return result.ToList<view_AssetsList>();
            }
        }

        public IList<view_AssetsList> getAssetsWithLastAction(int LastActionId, int locationId, int EmpRef)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsList
                     orderby obj.ActionDate descending
                     where obj.actionId == LastActionId
                  && (locationId != 0 ? obj.ToLocationId == locationId : true)
                  && (EmpRef != 0 ? obj.EmpRefCode == EmpRef : true)
                     select obj);
                return result.ToList<view_AssetsList>();
            }
        }

        public view_ItemCard getItemMaster(string itemCode, string Desc)
        {
            //using (var DC = new AssetsEntitiesNew())
            //{
            //    var result =
            //        (from obj in DC.D_ItemCard
            //         .Include("D_QtyUnit")
            //         where (obj.ItemRefCode == itemCode || obj.ItemNameAr == Desc || obj.ItemNameEn == Desc)
            //         select obj);
            //    return result.First();
            //}
            if (itemCode != "")
            {
                using (var DC = new AssetsEntitiesNew())
                {
                    var result =
                        (from obj in DC.view_ItemCard
                         where obj.ItemRefCode == itemCode
                         select obj);
                    return result.First();
                }
            }
            else
            {
                using (var DC = new AssetsEntitiesNew())
                {
                    var result =
                        (from obj in DC.view_ItemCard
                         where obj.ItemNameAr == Desc || obj.ItemNameEn == Desc
                         select obj);
                    return result.First();
                }
            }
        }

        public view_AssetsList getItemDetails(int itemId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsList
                     where obj.InboubdItemId == itemId
                     select obj);

                return result.FirstOrDefault();
            }
        }

        public AssetsItemUnit getItemDetailsForEdit(int itemId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsItemUnits
                     where obj.Code == itemId
                     select obj);

                return result.FirstOrDefault();
            }
        }

        public IList<view_AssetEventLog> getAssetEventLog(int inboundItemCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetEventLog
                     orderby obj.CreatedAt descending
                     where 1 == 1
                  && (inboundItemCode != 0 ? obj.AssetCode == inboundItemCode : 1 == 1)

                     select obj);

                return result.ToList<view_AssetEventLog>();
            }
        }

        #endregion "  master Data"

        #region "Employee Informations"

        public Employee_tbl checkOraEmployeeExitance(int Emp_Id)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Employee_tbl
                     where obj.Ora_EmpRefCode == Emp_Id
                     select obj).FirstOrDefault();
                return result;
            }
        }

        public D_EmployeeList getEmployeeDetails(int _code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_EmployeeList
                     where obj.Code == _code
                     select obj).FirstOrDefault();
                return result;
            }
        }

        public D_EmployeeLocations getEmployeeLocations(int EmpCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_EmployeeLocations
                     where obj.EmpCode == EmpCode
                     select obj).FirstOrDefault();
                return result;
            }
        }

        public int AddEmployee<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.Employee_tbl.Add(item as Employee_tbl);
                return DC.SaveChanges();
            }
        }

        public int UpdateEmployee<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as Employee_tbl).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }

        #endregion "Employee Informations"

        #region "Assets Child Information"

        #region "Assets Acting Tracking"

        public AssetsEventTracking getTrackingDetails(int TrackingId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsEventTrackings
                     where obj.Code == TrackingId
                     select obj);

                return result.FirstOrDefault();
            }
        }

        public view_AssetsEventTrackingHeader getTrackingRequestHeaderDetails(int headerCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsEventTrackingHeader
                     where obj.Code == headerCode
                     select obj);

                return result.FirstOrDefault();
            }
        }

        public view_AssetsEventTrackingHeader getTrackingRequestHeaderByEmpCode(int empCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsEventTrackingHeader
                     where obj.Ora_EmpRefCode == empCode
                     select obj);

                return result.FirstOrDefault();
            }
        }

        public AssetsEventTrackingHeader getTrackingRequestHeaderByCode(string headerCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsEventTrackingHeaders
                     where obj.Serial == headerCode
                     select obj);

                return result.FirstOrDefault();
            }
        }

        #endregion "Assets Acting Tracking"

        #region "Event Acting Tracking"

        #region "Add ,  Update  ,Delete" Assets Acting Tracking

        public int AddEventTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.AssetsEventTrackings.Add(item as AssetsEventTracking);
                return DC.SaveChanges();
            }
        }

        public int DeleteEventTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as AssetsEventTracking).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateEventTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as AssetsEventTracking).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }

        #endregion "Add ,  Update  ,Delete" Assets Acting Tracking

        #endregion "Event Acting Tracking"

        #endregion "Assets Child Information"

        #region "Tracking Header"

        public int getCurrentYearRequestHeaderCount(int TargetYear)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result = (from obj in DC.AssetsEventTrackingHeaders
                              where obj.RequestDate.Value.Year == TargetYear
                              select obj).ToList();

                if (result != null && result.Count > 0)
                {
                    // return Convert.ToInt32(result.Max(x => x.Serial));
                    return result.Count;
                }
                else { return 0; }
            }
        }

        public IList<view_AssetsEventTrackingHeader> getAssetsRequestList(string requestSerial, int requestType,
            DateTime TransactionDatFrom, DateTime TransactionDatTo,
            int targetLocation, int empRef, int OrgRefCode, int EmpStatus)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsEventTrackingHeader
                     orderby obj.RequestDate descending
                     where 1 == 1
                  && (requestSerial != "" ? (obj.Serial == requestSerial
                  || obj.Ora_EmpCivilId == requestSerial
                  || obj.Ora_EmpName.Contains(requestSerial)
                  || obj.EmpName.Contains(requestSerial)
                  || obj.EmpRefCode.ToString() == requestSerial)
                   || obj.Code.ToString() == requestSerial : true)
                  && (requestType != 0 ? obj.RequestActionType == requestType : true)
                  && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.RequestDate >= TransactionDatFrom : true)
                  && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.RequestDate <= TransactionDatFrom : true)
                  && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
                  && (empRef != 0 ? obj.EmpRefCode == empRef : true)
                  && (OrgRefCode != 0 ? obj.OrgChartRefCode == OrgRefCode : true)
                  && (EmpStatus != -1 ? (EmpStatus == 1 ? obj.Emp_Active == true : obj.Emp_Active == false) : true)
                     select obj);

                    return result.ToList<view_AssetsEventTrackingHeader>();
            }
        }

        public IList<view_CustodyList> getFilteredCustodyList(string requestSerial, int requestType, DateTime TransactionDatFrom, DateTime TransactionDatTo,
            int targetLocation, int empRef, int[] OrgChartRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     orderby obj.ORG_NO, obj.AMANA_NO, obj.DEPT_NO, obj.SEC_NO, obj.SUB_SEC_NO
                     where 1 == 1
                  && (requestSerial != "" ? obj.Serial == requestSerial : true)
                  && (requestType != 0 ? obj.RequestActionType == requestType : true)
                  && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.RequestDate >= TransactionDatFrom : true)
                  && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.RequestDate <= TransactionDatFrom : true)
                  && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
                  && (empRef != 0 ? obj.EmpRefCode == empRef : true)
                  && (OrgChartRefCode.Contains(obj.OrgChartRefCode.Value))
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        public IList<view_CustodyList> getRequestAssets(int requestCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     orderby obj.ActionDate descending
                     where obj.RequestHeaderCode == requestCode
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        #region "Add ,  Update  ,Delete" Assets Acting Tracking

        public int AddAssetsEventTrackingHeader<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.AssetsEventTrackingHeaders.Add(item as AssetsEventTrackingHeader);
                return DC.SaveChanges();
            }
        }

        public int DeleteAssetsEventTrackingHeader<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as AssetsEventTrackingHeader).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateAssetsEventTrackingHeader<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as AssetsEventTrackingHeader).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }

        #endregion "Add ,  Update  ,Delete" Assets Acting Tracking

        #region "Add ,  Update  ,Delete" Assets Acting Tracking

        public int AddEmployeeLoation<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.D_EmployeeLocations.Add(item as D_EmployeeLocations);
                return DC.SaveChanges();
            }
        }

        public int UpdateEmployeeLoation<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as D_EmployeeLocations).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }

        #endregion "Add ,  Update  ,Delete" Assets Acting Tracking

        #endregion "Tracking Header"

        #region "Reporting"

        //public List<viewTemp> getAssetsInventory()
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.viewTemp

        //             where 1 == 1
        //             //&& (inboundSerial != "" ? obj.Serial == inboundSerial : true)
        //             //&& (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : true)
        //             //&& (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatFrom : true)
        //             //&& (vendorCode != 0 ? obj.FromVendorCode == vendorCode : true)
        //             //&& (LastStatusId != 0 ? obj.statusId == LastStatusId : true)
        //             //&& (LastActionId != 0 ? obj.actionId == LastActionId : true)
        //             //&& (ItemCode != 0 ? obj.ItemCode == ItemCode : true)
        //             //&& (ItemCategoryId != 0 ? obj.ItemCategoryId == ItemCategoryId : true)
        //             //&& (EmprefCode != 0 ? obj.EmpRefCode == EmprefCode : true)
        //             //&& (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
        //             select obj);

        //        return result.ToList<viewTemp>();
        //    }
        //    //using (var DC = new AssetsEntitiesNew())
        //    //{
        //    //    var result = DC.view_AssetsInventory
        //    //             .SqlQuery("Select * from view_AssetsInventory")
        //    //             .ToList<view_AssetsInventory>();

        //    //    return result;
        //    //}

        //}

        public IList<view_AssetsInventory> getAssetsInventory()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsInventory
                     select obj);

                return result.ToList<view_AssetsInventory>();
            }
        }

        public List<view_CustodyList> getCustodyList(int RequestHeaderCode, int ToLocationId, int EmpRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where 1 == 1
                     && (ToLocationId != 0 ? obj.ToLocationId == ToLocationId : true)
                     && (EmpRefCode != 0 ? obj.EmpRefCode == EmpRefCode : true)
                     && (RequestHeaderCode != 0 ? obj.RequestHeaderCode == RequestHeaderCode : true)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        public List<view_CustodyList> getCustodyListByMasterData(int RequestHeaderCode, int ToLocationId, int EmpRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where true
                    && (EmpRefCode != 0 ? obj.EmpRefCode == EmpRefCode : true)
                    && (ToLocationId != 0 ? obj.ToLocationId == ToLocationId : true)
                    && (RequestHeaderCode != 0 ? obj.RequestHeaderCode == RequestHeaderCode : true)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        public List<view_CustodyListGrouped> getCustodyListGrouped(int[] OrgChartRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyListGrouped
                     where OrgChartRefCode.Contains(obj.OrgChartRefCode.Value)
                     select obj);

                return result.ToList<view_CustodyListGrouped>();
            }
        }

        public List<view_CustodyList> getCustodyListHera(int[] OrgChartRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where OrgChartRefCode.Contains(obj.OrgChartRefCode.Value)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }

        #endregion "Reporting"
    }
}