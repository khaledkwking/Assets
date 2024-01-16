using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class OrgEntityRepository
    {

        #region List
        public List<D_OrgChart> GetList()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_OrgChart
                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_OrgChart>();
            }
        }


        public List<D_OrgChart> GetList(int parentId, string _FilterPartOfName)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_OrgChart
                     where obj.ParentId == parentId
                   && (!_FilterPartOfName.Equals("") ? obj.EntityNameAr.Contains(_FilterPartOfName) : 1 == 1)
                     //  && (parentId!=0 ? obj.Cat_ParentId== parentId : 1 == 1)
                     //orderby obj.code descending
                     select obj);

                return result.ToList<D_OrgChart>();
            }
        }
         
        public D_OrgChart GetDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_OrgChart
                     where obj.Code == _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_OrgChart>();
            }
        }


        public bool CheckChildExistance(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_OrgChart
                     where obj.ParentId == _Code
                     select obj);
                if (result.ToList().Count > 0)
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
            using (var DC = new AssetsEntities())
            {

                DC.D_OrgChart.Add(item as D_OrgChart);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_OrgChart).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as D_OrgChart).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion



    }
}
