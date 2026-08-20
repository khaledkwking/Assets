using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class InboundRepository
    {

        #region "Inbound master Data"

        #region List
        public IList<View_InboundList> GetList(string inboundSerial, DateTime TransactionDatFrom, DateTime TransactionDatTo, int InboundType, int DepositeType, int CustomeDepartment
            , int RefType, string RefNo, string ManifestNo, string DekiveryOrderNo, int Decalrationtype, int PurchaseOrderId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_InboundList
                     orderby obj.Code descending
                     where 1 == 1
                   && (inboundSerial != "" ? obj.Serial == inboundSerial : 1 == 1)
                   && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : 1 == 1)
                   && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatTo : 1 == 1)
                   && (InboundType != 0 ? obj.InboundTypeCode == InboundType : 1 == 1)
                   && (DekiveryOrderNo != "" ? obj.DeliveryOrderNo == DekiveryOrderNo : 1 == 1)
                    && (RefNo != "" ? obj.RefNo == RefNo : 1 == 1)


                     select obj);

                return result.ToList<View_InboundList>();
            }
        }
        public View_InboundList FillDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_InboundList
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<View_InboundList>();
            }
        }
        public Inbound getInboundRelatedTooutVount(int OutBoundRefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Inbounds
                     where obj.OutBoundRefCode == OutBoundRefCode
                     select obj);

                return result.FirstOrDefault<Inbound>();
            }
        }


        public View_InboundList getInboundMasterBySerial(string serial)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_InboundList
                     where obj.Serial == serial
                     select obj);

                return result.FirstOrDefault<View_InboundList>();
            }
        }

        public int getCurrentYearInboundCount(int TargetYear)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result = (from obj in DC.Inbounds
                              where obj.TransDate.Value.Year == TargetYear
                              select obj).ToList();


                if (result != null && result.Count > 0)
                {
                    // return Convert.ToInt32(result.Max(x => x.Serial));
                    return result.Count;
                }
                else { return 0; }


            }
        }

        public Inbound GetDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Inbounds
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<Inbound>();
            }
        }

        public List<View_InboundList> FillInboundMater(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_InboundList
                     where obj.Code == _Code
                     select obj);

                return result.ToList<View_InboundList>();
            }
        }


       


        #endregion

        #region "Add ,  Update  ,Delete" Inbound Operation

        public int AddInbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.Inbounds.Add(item as Inbound);
                return DC.SaveChanges();
            }
        }

        public int DeleteInbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Inbound).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateInbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as Inbound).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion


        #endregion

        #region "Inbound Child Information"


        #region "Inbound Items Units"
        public D_ItemCard getItemCardDetails(long _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemCard
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<D_ItemCard>();
            }
        }


        public AssetsItemUnit GetInboundItemDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsItemUnits
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<AssetsItemUnit>();
            }
        }


        


        public List<view_ItemCard> fillItems()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_ItemCard
                     select obj);

                return result.ToList<view_ItemCard>();
            }
        }

        /// <summary>
        /// Get items available in a specific location (store)
        /// </summary>
        public List<view_ItemCard> fillItemsByLocation(int locationId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from tracking in DC.AssetsEventTrackings
                     join item in DC.view_ItemCard on tracking.AssetCode equals item.Code
                     where tracking.ToLocationId == locationId
                     group item by item.Code into grp
                     select grp.FirstOrDefault())
                    .ToList();

                return result;
            }
        }

        public List<view_inboubdItems> FillInboundItems(int InboundCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_inboubdItems
                     where obj.InboundCode == InboundCode
                     select obj);

                return result.ToList<view_inboubdItems>();
            }
        }


        public List<view_inboubdItems> FilterInboundItems(string serial)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_inboubdItems
                     where obj.Serial == serial
                     select obj);

                return result.ToList<view_inboubdItems>();
            }
        }



        public AssetsItemUnit getInboundItemDetails(int inboundItemId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsItemUnits
                     where obj.Code == inboundItemId
                     select obj);

                return result.FirstOrDefault<AssetsItemUnit>();
            }
        }


        #region "Add ,  Update  ,Delete"Inbound Item Unit

        public int AddItemsUnit<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.AssetsItemUnits.Add(item as AssetsItemUnit);
                return DC.SaveChanges();
            }
        }
        public int DeleteItemsUnit<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as AssetsItemUnit).State = System.Data.Entity.EntityState.Deleted;


                return DC.SaveChanges();


            }
        }

        public int UpdateItemunit<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as AssetsItemUnit).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion



        #region "Transportation"

        #region "Add ,  Update  ,Delete" Inbound Transportation



        #endregion

        #endregion

        #region "Inbound Notes"
        public List<InboundNote> FillInboundNotes(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundNotes
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundNote>();
            }
        }

        public InboundNote GetNotesDeatils(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundNotes
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundNote>();
            }
        }
        #region "Add ,  Update  ,Delete" Inbound Transportation

        public int AddNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.InboundNotes.Add(item as InboundNote);
                return DC.SaveChanges();
            }
        }

        public int DeleteNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundNote).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as InboundNote).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Inbound Attachment"
        public List<InboundAttachment> FillInboundAttachment(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundAttachments
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundAttachment>();
            }
        }

        public InboundAttachment GetAttachmentDeatils(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundAttachments
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundAttachment>();
            }
        }
        #region "Add ,  Update  ,Delete" Inbound Transportation

        public int AddAttachments<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.InboundAttachments.Add(item as InboundAttachment);
                return DC.SaveChanges();
            }
        }

        public int DeleteAttachment<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundAttachment).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateAttachment<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as InboundAttachment).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Inbound Customs Employee"
        public List<InboundStoreEmployee> FillInboundStoreEmployee(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundStoreEmployees
                     .Include("D_CustomsEmployee")
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundStoreEmployee>();
            }
        }

        public InboundStoreEmployee FillInboundStoreEmployeeDeatils(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundStoreEmployees
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundStoreEmployee>();
            }
        }

        #region "Add ,  Update  ,Delete" Inbound Customs Employee

        public int AddCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.InboundStoreEmployees.Add(item as InboundStoreEmployee);
                return DC.SaveChanges();
            }
        }

        public int DeleteCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundStoreEmployee).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as InboundStoreEmployee).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Inbound Status Tracking"
        public List<InboundStatusTrack> FillInboundStatusTracking(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundStatusTracks
                     .Include("D_InboundDepositeStatusType")
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundStatusTrack>();
            }
        }

        public InboundStatusTrack FillInboundStatusDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundStatusTracks
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundStatusTrack>();
            }
        }

        public InboundStatusTrack GetInboundStatusDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.InboundStatusTracks
                     where obj.InboundCode == _Code
                     select obj);

                return result.FirstOrDefault<InboundStatusTrack>();
            }
        }

        #region "Add ,  Update  ,Delete"Inbound StatusTrack

        public int AddStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.InboundStatusTracks.Add(item as InboundStatusTrack);
                return DC.SaveChanges();
            }
        }

        public int DeleteStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundStatusTrack).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as InboundStatusTrack).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #region "Asset Validation"
        /// <summary>
        /// Check if an asset exists in AssetsEventTracking by AssetCode and ToLocationId
        /// </summary>
        public AssetsEventTracking CheckAssetInEventTracking(long assetCode, int toLocationId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.AssetsEventTrackings
                     where obj.AssetCode == assetCode
                     && obj.ToLocationId == toLocationId
                     select obj).FirstOrDefault();

                return result;
            }
        }

        /// <summary>
        /// Check if barcode or serial already exists in the same location (ToLocationId)
        /// Returns the existing AssetsEventTracking if found, null otherwise
        /// </summary>
        public AssetsEventTracking CheckBarcodeOrSerialInLocation(string barcode, string serial, int toLocationId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Only check if both barcode and serial are not empty
                if (string.IsNullOrEmpty(barcode) && string.IsNullOrEmpty(serial))
                {
                    return null; // No validation needed if both are empty
                }

                var result =
                    (from obj in DC.AssetsEventTrackings
                     where obj.ToLocationId == toLocationId
                     && ((!string.IsNullOrEmpty(barcode) && obj.Item_BarCode == barcode)
                         || (!string.IsNullOrEmpty(serial) && obj.Item_Serial == serial))
                     select obj).FirstOrDefault();

                return result;
            }
        }
        #endregion

        #endregion

        #endregion

    }
}
