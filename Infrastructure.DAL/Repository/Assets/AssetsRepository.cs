using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.IO;
using System.Web;

namespace Infrastructure.DAL
{
    [DataContract]
    public class EmployeeStatus
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
    public partial class AssetsRepository
    {
        #region "  master Data"

        public HashSet<int> GetExcludedCodes()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return DC.D_ExcludedOrg
                         .Select(x => x.Code)
                         .ToHashSet();
            }
        }

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
                  //&& (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : true)
                  //&& (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatFrom : true)
                  //&& (vendorCode != 0 ? obj.FromVendorCode == vendorCode : true)
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
        public List<view_CustodyList> getAssetReceiptbyHeaderIds(int[] nodeIds)
        {
      
                using (var DC = new AssetsEntitiesNew())
                {
                    var result =
                        (from obj in DC.view_CustodyList
                         where  (obj.RequestHeaderCode.HasValue) && nodeIds.Contains(obj.RequestHeaderCode.Value)
                         
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
                     //where obj.InboubdItemId == itemId
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
        public view_AssetsEventTrackingHeader getTrackingRequestHeaderByAssetOwnerCode(string CivilID)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsEventTrackingHeader
                     where obj.AssetOrgOwnerRefCode == CivilID
                     select obj);

                return result.FirstOrDefault();
            }
        }
        public AssetsEventTrackingHeader getTrackingRequestHeaderByCodeNew(int headerCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsEventTrackingHeaders
                     where obj.Code == headerCode
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

        public List<AssetsEventTrackingHeader> getTrackingHeaderByNodeId(int NodeId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsEventTrackingHeaders
                     where obj.OrgChartRefCode == NodeId
                     select obj);

                return result.ToList();
            }
        }
        public List<AssetsEventTrackingHeader> GetTrackingHeaderByNodeIds(int[] nodeIds)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                return DC.AssetsEventTrackingHeaders
                         .Where(x => x.OrgChartRefCode.HasValue &&
                                     nodeIds.Contains(x.OrgChartRefCode.Value))
                         .ToList();
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
        public List<Store> getStores()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Stores
                     orderby obj.Id descending
                     where obj.isDeleted == false

                     select obj);

                return result.ToList<Store>();
            }
        }
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
        public IList<view_AssetsEventTrackingHeader> getAssetsRequestList(
         string requestSerial,
         int requestType,
         DateTime TransactionDatFrom,
         DateTime TransactionDatTo,
         int targetLocation,
         int empRef,
         int OrgRefCode,
         int EmpStatus, // -1 = لا فلترة، 1 = نشط، 0 = غير نشط
         string itemNameAr)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // 🔹 استعلام قاعدة البيانات الأساسي
                var query =
        from obj in DC.view_AssetsEventTrackingHeader
        orderby obj.RequestDate descending
        where (string.IsNullOrEmpty(requestSerial) ||
              obj.Serial == requestSerial ||
              obj.Ora_EmpCivilId == requestSerial ||
              obj.Ora_EmpName.Contains(requestSerial) ||
              obj.EmpName.Contains(requestSerial) ||
              obj.EmpRefCode.ToString() == requestSerial ||
              obj.Code.ToString() == requestSerial)
            && (requestType == 0 ? true :
                requestType == 1 ? (obj.ProcessType == 1 && (obj.EmpRefCode != null || obj.AssetOrgOwnerName != null)) :
                requestType == 2 ? (obj.ProcessType == 2 && obj.OrgChartRefCode != null) :
                requestType == 3 ? ((obj.ProcessType == 1 && (obj.EmpRefCode == null && obj.AssetOrgOwnerName == null)) ||
                                    (obj.ProcessType == 2 && obj.OrgChartRefCode == null)) :
                false)
            && (TransactionDatFrom != new DateTime(1990, 1, 1) ? obj.RequestDate >= TransactionDatFrom : true)
            && (TransactionDatTo != new DateTime(1990, 1, 1) ? obj.RequestDate <= TransactionDatTo : true)
            && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
            && (empRef != 0 ? obj.EmpRefCode == empRef : true)
            && (OrgRefCode != 0 ? obj.OrgChartRefCode == OrgRefCode : true)
            && (string.IsNullOrEmpty(itemNameAr) ||
                DC.view_CustodyList.Any(c => c.RequestHeaderCode == obj.Code && c.ItemNameAr.Contains(itemNameAr)))
        select obj; // ← هذه ضرورية


                var list = query.ToList();

                // 🔹 فلترة حالة الموظف باستخدام API إذا مطلوب
                if (EmpStatus != -1)
                {
                    var client = new WebClient();
                    client.Headers.Add("Content-Type", "application/json");

                    var empCache = new Dictionary<int, bool>(); // لتخزين حالة الموظفين المتكررين

                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        var item = list[i];
                        bool isActive;
                        int empId = item.Ora_EmpRefCode ?? 0; // 0 كقيمة افتراضية

                        if (empCache.ContainsKey(empId))
                        {
                            isActive = empCache[empId];
                        }
                        else
                        {
                            try
                            {
                               
                                string url = HttpContext.Current.Request.Url.Scheme + "://" +
                                             HttpContext.Current.Request.Url.Authority +
                                             "/api/hepler/GetEmployeeStatus?empId=" + item.Ora_EmpRefCode;

                                string json = client.DownloadString(url);

                                using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                                {
                                    var serializer = new DataContractJsonSerializer(typeof(EmployeeStatus));
                                    var result = (EmployeeStatus)serializer.ReadObject(ms);
                                    isActive = result != null && string.Equals(result.Status, "active", StringComparison.OrdinalIgnoreCase);
                                    empCache[empId] = isActive;
                                }
                            }
                            catch
                            {
                                isActive = false; // في حالة فشل API اعتبر غير فعال
                            }
                        }

                        if ((EmpStatus == 1 && !isActive) || (EmpStatus == 0 && isActive))
                        {
                            list.RemoveAt(i);
                        }
                    }
                }

                return list;
            }
        }

        //        public IList<view_AssetsEventTrackingHeader> getAssetsRequestList(string requestSerial, int requestType,
        //            DateTime TransactionDatFrom, DateTime TransactionDatTo,
        //            int targetLocation, int empRef, int OrgRefCode, int EmpStatus, string itemNameAr)
        //        {
        //            using (var DC = new AssetsEntitiesNew())
        //            {
        //                var result =
        //                    (from obj in DC.view_AssetsEventTrackingHeader
        //                     orderby obj.RequestDate descending
        //                     where 1 == 1
        //                  && (requestSerial != "" ? (obj.Serial == requestSerial
        //                  || obj.Ora_EmpCivilId == requestSerial
        //                  || obj.Ora_EmpName.Contains(requestSerial)
        //                  || obj.EmpName.Contains(requestSerial)
        //                  || obj.EmpRefCode.ToString() == requestSerial)
        //                   || obj.Code.ToString() == requestSerial : true)
        //                  //&& (requestType != 0 ? obj.RequestActionType == requestType : true)
        //                  && (
        //    requestType == 0 ? true :
        //    requestType == 1 ? (obj.ProcessType == 1 && (obj.EmpRefCode != null || obj.AssetOrgOwnerName !=null)) :
        //    requestType == 2 ? (obj.ProcessType == 2 && obj.OrgChartRefCode != null) :
        //    requestType == 3 ? (
        //        (obj.ProcessType == 1 && (obj.EmpRefCode == null && obj.AssetOrgOwnerName == null)) ||
        //        (obj.ProcessType == 2 && obj.OrgChartRefCode == null)
        //    ) :
        //    false
        //)
        //                  && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.RequestDate >= TransactionDatFrom : true)
        //                  && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.RequestDate <= TransactionDatTo : true)
        //                  && (targetLocation != 0 ? obj.ToLocationId == targetLocation : true)
        //                  && (empRef != 0 ? obj.EmpRefCode == empRef : true)
        //                  && (OrgRefCode != 0 ? obj.OrgChartRefCode == OrgRefCode : true)
        //                  && (EmpStatus != -1 ? (EmpStatus == 1 ? obj.Emp_Active == true : obj.Emp_Active == false) : true)
        //                        && (string.IsNullOrEmpty(itemNameAr)
        //                         ? true
        //                : DC.view_CustodyList.Any(c =>
        //                             c.RequestHeaderCode == obj.Code &&
        //                             c.ItemNameAr.Contains(itemNameAr)))
        //                     select obj);

        //                return result.ToList<view_AssetsEventTrackingHeader>();
        //            }
        //        }
        public List<view_AssetsEventTrackingHeader> getAssetsRequestListByOrgRefCode(int OrgRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_AssetsEventTrackingHeader
                     orderby obj.RequestDate descending
                     where 1 == 1

                  && (OrgRefCode != 0 ? obj.OrgChartRefCode == OrgRefCode : true)
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
        //public int UpdateAssetsEventTrackingHeader<T>(T item)
        //{
        //    if (item == null)
        //    {
        //        throw new ArgumentNullException(nameof(item), "The entity cannot be null.");
        //    }

        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var entity = item as view_AssetsEventTrackingHeader;
        //        if (entity == null)
        //        {
        //            throw new InvalidOperationException("The entity type is incorrect.");
        //        }

        //        // Log null properties before updating
        //        var nullProperties = DC.Entry(entity).CurrentValues.PropertyNames
        //            .Where(p => DC.Entry(entity).CurrentValues[p] == null)
        //            .ToList();

        //        if (nullProperties.Any())
        //        {
        //            throw new InvalidOperationException($"The following properties are null: {string.Join(", ", nullProperties)}");
        //        }

        //        DC.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //        return DC.SaveChanges();
        //    }
        //}

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
            bool filter = true;
            if (RequestHeaderCode == 0 && ToLocationId == 0 && EmpRefCode == 0)
            {
                filter = false;
            }
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where filter
                    && (EmpRefCode != 0 ? obj.EmpRefCode == EmpRefCode : true)
                    && (ToLocationId != 0 ? obj.ToLocationId == ToLocationId : true)
                    && (RequestHeaderCode != 0 ? obj.RequestHeaderCode == RequestHeaderCode : true)
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }
        public List<view_CustodyListTransfer> getCustodyListByAssets(List<int?> assetIds)
        {
            if (assetIds == null || assetIds.Count == 0)
            {
                return new List<view_CustodyListTransfer>(); // لو القائمة فاضية، نرجع فارغ
            }

            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    from obj in DC.view_CustodyListTransfer
                    where assetIds.Contains(obj.Code) 
                    select obj;

                return result.ToList();
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
        public List<view_CustodyList> getCustodyList()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_CustodyList
                     where obj.OrgChartRefCode != null && obj.EmpRefCode != null // Ensure these are properties of the object
                     select obj);

                return result.ToList<view_CustodyList>();
            }
        }
        public List<view_CustodyList> getCustodyListPaged(int pageNumber, int pageSize, string Parent, string Main, string Sub, string ORG)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Calculate the number of records to skip based on the current page number
                int skip = (pageNumber - 1) * pageSize;

                if (Sub == "")
                    Sub = "0";

                if (Parent == "--- اختر ---")
                    Parent = "0";
                if (Main == "--- اختر ---")
                    Main = "0";

                if (Sub == "--- اختر ---")
                    Sub = "0";

                if (ORG == "--- اختر ---")
                    ORG = "0";
                // Fetch the paged data with filtering and sorting
                //var result = (from obj in DC.view_CustodyList
                //              where obj.OrgChartRefCode != null && obj.EmpRefCode != null // Ensure these are properties of the object
                //              orderby obj.EmpName // Sort by EmpName
                //              select obj)
                //             .Skip(skip) // Skip the records for previous pages
                //             .Take(pageSize) // Take only the records for the current page
                //             .ToList();

                //return result;
                DC.Database.CommandTimeout = 180; // Set timeout to 3 minutes

                var result = DC.view_CustodyList.AsNoTracking()
                  .Where(obj =>
                      (Parent != "0" ? obj.ItemsMainParentCategoryTitleAr == Parent : 1 == 1) &&
                      (Main != "0" ? obj.ItemsParentCategoryTitleAr == Main : 1 == 1) &&
                      (Sub != "0" ? obj.ItemsCategoryTitleAr == Sub : 1 == 1) &&
                      (ORG != "0" ? obj.ORG_NAME == ORG : 1 == 1) &&
                      obj.OrgChartRefCode != null && obj.EmpRefCode != null
                  )
                  .OrderBy(obj => obj.EmpName)
                  .Skip(skip)
                  .Take(pageSize)

                  .ToList();


                return result;
            }
        }

        public int GetCustodyListCount(string Parent, string Main, string Sub)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                if (Sub == "")
                    Sub = "0";

                if (Parent == "--- اختر ---")
                    Parent = "0";
                if (Main == "--- اختر ---")
                    Main = "0";

                if (Sub == "--- اختر ---")
                    Sub = "0";

                // Count the total number of records
                //return DC.view_CustodyList.Count();

                var result = DC.view_CustodyList.AsNoTracking()
                 .Where(obj =>
                     (Parent != "0" ? obj.ItemsMainParentCategoryTitleAr == Parent : 1 == 1) &&
                     (Main != "0" ? obj.ItemsParentCategoryTitleAr == Main : 1 == 1) &&
                     (Sub != "0" ? obj.ItemsCategoryTitleAr == Sub : 1 == 1) &&
                     obj.OrgChartRefCode != null && obj.EmpRefCode != null
                 )

                 .ToList();

                return result.Count;
            }
        }
        public List<view_CustodyList> GetCustodyListWithFilter(string Parent, string Main, string Sub)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                if (Sub == "")
                    Sub = "0";

                if (Parent == "--- اختر ---")
                    Parent = "0";
                if (Main == "--- اختر ---")
                    Main = "0";

                if (Sub == "--- اختر ---")
                    Sub = "0";

                // Count the total number of records
                //return DC.view_CustodyList.Count();

                var result = DC.view_CustodyList.AsNoTracking()
                 .Where(obj =>
                     (Parent != "0" ? obj.ItemsMainParentCategoryTitleAr == Parent : 1 == 1) &&
                     (Main != "0" ? obj.ItemsParentCategoryTitleAr == Main : 1 == 1) &&
                     (Sub != "0" ? obj.ItemsCategoryTitleAr == Sub : 1 == 1) &&

                     ((obj.RequestActionType == 1 && obj.EmpRefCode != null) || (obj.RequestActionType == 2 && obj.OrgChartRefCode != null))

                    // &&

                    // obj.OrgChartRefCode != null &&
                   //  obj.EmpRefCode != null
                 )

                 .ToList();

                return result;
            }
        }
        #endregion "Reporting"
    }

}