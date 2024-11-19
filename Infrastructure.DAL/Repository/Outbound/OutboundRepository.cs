using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class OutboundRepository
    {

        #region "Outbound master Data"

        #region List
        public List<View_OutboundList> GetList(string inboundSerial, DateTime TransactionDatFrom, DateTime TransactionDatTo, 
            int InboundType, int WithdrawTypeCode
            , int RefType, string RefNo  )
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_OutboundList

                     orderby obj.Code descending
                     where 1 == 1
                   && (inboundSerial != ""? obj.Serial == inboundSerial : 1 == 1)
                   && (TransactionDatFrom != new DateTime(1990, 01, 01) ? obj.TransDate >= TransactionDatFrom : 1 == 1)
                   && (TransactionDatTo != new DateTime(1990, 01, 01) ? obj.TransDate <= TransactionDatFrom : 1 == 1)
                   && (InboundType != 0 ? obj.TypeCode == InboundType : 1 == 1)
                   && (WithdrawTypeCode != 0 ? obj.WithdrawTypeCode == WithdrawTypeCode : 1 == 1)
                   && (RefType != 0 ? obj.RefTypeCode == RefType : 1 == 1)
                   && (RefNo != "" ? obj.RefNo == RefNo : 1 == 1)
                     select obj);

                return result.ToList<View_OutboundList>();
            }
        }
        public Outbound FillDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Outbounds
                     .Include("Outbound_Items")
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<Outbound>();
            }
        }


        public List<View_OutboundList> FillOutboundMaster(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_OutboundList
                     where obj.Code == _Code
                     select obj);

                return result.ToList<View_OutboundList>();
            }
        }
        public Outbound GetDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Outbounds
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<Outbound>();
            }
        }

        public int getCurrentYearOutboundCount(int TargetYear)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result = (from obj in DC.Outbounds
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

        #endregion

        #region "Add ,  Update  ,Delete" Outbound Operation

        public int AddOutbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.Outbounds.Add(item as Outbound);
                return DC.SaveChanges();
            }
        }

        public int DeleteOutbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Outbound).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateOutbound<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as Outbound).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion


        #endregion

        #region "Outbound Child Information"

        #region "Outbound Items "

        public view_ItemCard getItemCardDetails(int itemCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_ItemCard
                     where obj.Code == itemCode
                     select obj);

                return result.FirstOrDefault<view_ItemCard>();
            }
        }
        public List<View_OutboundItems> FillOutboundItemList(int _outboundCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_OutboundItems
                        where obj.OutboundCode == _outboundCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<View_OutboundItems>();
            }
        }

        public List<View_OutboundItems> FillOutboundItems(int _InboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.View_OutboundItems
                     where obj.OutboundCode == _InboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<View_OutboundItems>();
            }
        }

        
        public Outbound_Items GetOutboundItemDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Outbound_Items
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<Outbound_Items>();
            }
        }

       
        public bool CheckOutboundItemExistance(long _ItemCode, long _outboundCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.Outbound_Items
                     where obj.ItemCode == _ItemCode && obj.OutboundCode == _outboundCode
                     select obj);

                if (result.ToList<Outbound_Items>().Count >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        public int AddOutboundItems<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.Outbound_Items.Add(item as Outbound_Items);
                return DC.SaveChanges();
            }
        }
        public int DeleteItems<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Outbound_Items).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateOutboundItems<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as Outbound_Items).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }

        #endregion


         

         

        #region "Outbound Notes"
        public List<OutboundNote> FillOutboundNotes(int _OutboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.OutboundNotes
                     where obj.OutboundCode == _OutboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<OutboundNote>();
            }
        }

        public OutboundNote GetNotesDeatils(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.OutboundNotes
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<OutboundNote>();
            }
        }
        #region "Add ,  Update  ,Delete" Outbound Transportation

        public int AddNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.OutboundNotes.Add(item as OutboundNote);
                return DC.SaveChanges();
            }
        }

        public int DeleteNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as OutboundNote).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateNotes<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as OutboundNote).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

        #region "Outbound Attachment"
        public List<OutboundAttachment> FillOutboundAttachment(int _OutboundMasterCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.OutboundAttachments
                     where obj.OutboundCode == _OutboundMasterCode
                     orderby obj.Code descending
                     select obj);

                return result.ToList<OutboundAttachment>();
            }
        }

        public OutboundAttachment GetAttachmentDeatils(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.OutboundAttachments
                     where obj.Code == _Code
                     select obj);

                return result.FirstOrDefault<OutboundAttachment>();
            }
        }
        #region "Add ,  Update  ,Delete" Outbound Transportation

        public int AddAttachments<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.OutboundAttachments.Add(item as OutboundAttachment);
                return DC.SaveChanges();
            }
        }

        public int DeleteAttachment<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as OutboundAttachment).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateAttachment<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as OutboundAttachment).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion

        #endregion

         

         

        #endregion



    }
}
