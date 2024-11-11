using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class GoodsCategoryRepository
    {

        #region List
        public List<D_ItemsCategory> GetList()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemsCategory
                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_ItemsCategory>();
            }
        }
        

        public List<D_ItemsCategory> GetList( int parentId, string _FilterPartOfName)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemsCategory
                     where obj.Cat_ParentId == parentId
                  && (!_FilterPartOfName.Equals("") ? obj.TitleAr.Contains(_FilterPartOfName) : 1 == 1)
                //  && (parentId!=0 ? obj.Cat_ParentId== parentId : 1 == 1)
                     //orderby obj.code descending
                     select obj);

                return result.ToList<D_ItemsCategory>();
            }
        }

        public D_ItemsCategory GetDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemsCategory
                     where obj.Code== _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_ItemsCategory>();
            }
        } 
        
        public bool CheckChildExistance(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_ItemsCategory
                     where obj.Cat_ParentId== _Code
                     select obj);
                if (result.ToList().Count>0)
                {
                    return true;

                }
                return false;
            }
        }
        #endregion

        #region "Add ,  Update  ,Delete" For Consignee

        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                
                DC.D_ItemsCategory.Add(item as D_ItemsCategory);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_ItemsCategory).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as D_ItemsCategory).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion



    }
}
