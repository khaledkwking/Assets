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
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.View_InboundList
                     orderby obj.Code descending
                     where 1 == 1
                   && (inboundSerial != "" ? obj.Serial == inboundSerial : 1 == 1)
                   && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : 1 == 1)
                   && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatFrom : 1 == 1)
                   && (InboundType != 0 ? obj.InboundTypeCode == InboundType : 1 == 1)
                   && (DekiveryOrderNo != "" ? obj.DeliveryOrderNo == DekiveryOrderNo : 1 == 1)
                    && (RefNo != "" ? obj.RefNo == RefNo : 1 == 1)


                     select obj);

                return result.ToList<View_InboundList>();
            }
        }
        public View_InboundList FillDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
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
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Inbound
                     where obj.OutBoundRefCode == OutBoundRefCode
                     select obj);

                return result.FirstOrDefault<Inbound>();
            }
        }


        public View_InboundList getInboundMasterBySerial(string serial)
        {
            using (var DC = new AssetsEntities())
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
            using (var DC = new AssetsEntities())
            {
                var result = (from obj in DC.Inbound
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
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Inbound
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<Inbound>();
            }
        }

        public List<View_InboundList> FillInboundMater(int _Code)
        {
            using (var DC = new AssetsEntities())
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
            using (var DC = new AssetsEntities())
            {
                DC.Inbound.Add(item as Inbound);
                return DC.SaveChanges();
            }
        }

        public int DeleteInbound<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Inbound).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateInbound<T>(T item)
        {
            using (var DC = new AssetsEntities())
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
        public D_ItemCard getItemCardDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_ItemCard
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<D_ItemCard>();
            }
        }


        public AssetsItemUnits GetInboundItemDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.AssetsItemUnits
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<AssetsItemUnits>();
            }
        }


        


        public List<view_ItemCard> fillItems()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.view_ItemCard
                     select obj);

                return result.ToList<view_ItemCard>();
            }
        }


        public List<view_inboubdItems> FillInboundItems(int InboundCode)
        {
            using (var DC = new AssetsEntities())
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
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.view_inboubdItems
                     where obj.Serial == serial
                     select obj);

                return result.ToList<view_inboubdItems>();
            }
        }



        public AssetsItemUnits getInboundItemDetails(int inboundItemId)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.AssetsItemUnits
                     where obj.Code == inboundItemId
                     select obj);

                return result.FirstOrDefault<AssetsItemUnits>();
            }
        }


        #region "Add ,  Update  ,Delete"Inbound Item Unit

        public int AddItemsUnit<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.AssetsItemUnits.Add(item as AssetsItemUnits);
                return DC.SaveChanges();
            }
        }
        public int DeleteItemsUnit<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as AssetsItemUnits).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateItemunit<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as AssetsItemUnits).State = System.Data.Entity.EntityState.Modified;
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
        public List<InboundNotes> FillInboundNotes(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundNotes
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundNotes>();
            }
        }

        public InboundNotes GetNotesDeatils(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundNotes
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundNotes>();
            }
        }
        #region "Add ,  Update  ,Delete" Inbound Transportation

        public int AddNotes<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.InboundNotes.Add(item as InboundNotes);
                return DC.SaveChanges();
            }
        }

        public int DeleteNotes<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundNotes).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateNotes<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as InboundNotes).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Inbound Attachment"
        public List<InboundAttachments> FillInboundAttachment(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundAttachments
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundAttachments>();
            }
        }

        public InboundAttachments GetAttachmentDeatils(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundAttachments
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundAttachments>();
            }
        }
        #region "Add ,  Update  ,Delete" Inbound Transportation

        public int AddAttachments<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.InboundAttachments.Add(item as InboundAttachments);
                return DC.SaveChanges();
            }
        }

        public int DeleteAttachment<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundAttachments).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateAttachment<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as InboundAttachments).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Inbound Customs Employee"
        public List<InboundStoreEmployee> FillInboundStoreEmployee(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundStoreEmployee
                     .Include("D_CustomsEmployee")
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundStoreEmployee>();
            }
        }

        public InboundStoreEmployee FillInboundStoreEmployeeDeatils(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundStoreEmployee
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundStoreEmployee>();
            }
        }

        #region "Add ,  Update  ,Delete" Inbound Customs Employee

        public int AddCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.InboundStoreEmployee.Add(item as InboundStoreEmployee);
                return DC.SaveChanges();
            }
        }

        public int DeleteCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundStoreEmployee).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateCustomsEmployee<T>(T item)
        {
            using (var DC = new AssetsEntities())
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
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundStatusTrack
                     .Include("D_InboundDepositeStatusType")
                     where obj.InboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<InboundStatusTrack>();
            }
        }

        public InboundStatusTrack FillInboundStatusDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.InboundStatusTrack
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<InboundStatusTrack>();
            }
        }

        #region "Add ,  Update  ,Delete"Inbound StatusTrack

        public int AddStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.InboundStatusTrack.Add(item as InboundStatusTrack);
                return DC.SaveChanges();
            }
        }

        public int DeleteStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as InboundStatusTrack).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateStatusTracking<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as InboundStatusTrack).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #endregion

    }
}
