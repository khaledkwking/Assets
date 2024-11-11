using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class ItemRepository
    {

        #region List
        public List<view_ItemCard> GetList(string _FilterPartOfName,int CategoryId , int Qunit ,string RefCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.view_ItemCard
                     where 1 == 1
                    && (!_FilterPartOfName.Equals("") ? obj.ItemNameEn.Contains(_FilterPartOfName) || (obj.ItemRefCode == _FilterPartOfName || obj.ItemFinanceCode == _FilterPartOfName || obj.ItemQrCode == _FilterPartOfName || obj.ItemRFIDCode == _FilterPartOfName) : true)
                    && (CategoryId!=0 ? obj.ItemCategoryId== CategoryId : true)
                    && (Qunit != 0 ? obj.QUnitCode == Qunit : true)
                    && (!RefCode.Equals("") ? (obj.ItemRefCode== RefCode || obj.ItemFinanceCode == RefCode || obj.ItemQrCode == RefCode || obj.ItemRFIDCode == RefCode) : true)
                     select obj);
                return result.ToList<view_ItemCard>();
            }
        }


        //public List<D_ItemCard> GetList(int accuontType,string _FilterPartOfName)
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_ItemCard
        //             where 1==1
        //             && (!_FilterPartOfName.Equals("")?obj.ItemNameEn.Contains(_FilterPartOfName):true)
                   
        //             orderby obj.Code descending
        //             select obj);

        //        return result.ToList<D_ItemCard>();
        //    }
        //}

        public D_ItemCard GetDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemCard
                     where obj.Code== _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_ItemCard>();
            }
        }
        //public List<D_ItemsCategory>  GetDetailsCategory(int _Code)
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var result =
        //            (from obj in DC.D_ItemsCategory
        //             where obj.Code == _Code
        //             orderby obj.Code descending
        //             select obj);

        //        return result.ToList<D_ItemsCategory>();
        //    }
        //}
        #endregion

        #region "Add ,  Update  ,Delete" For Consignee

        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                
                DC.D_ItemCard.Add(item as D_ItemCard);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_ItemCard).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as D_ItemCard).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion



    }
}
