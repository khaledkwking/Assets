using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class VendorRepository
    {

        #region List
        public List<D_VendorData> GetList()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_VendorData

                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_VendorData>();
            }
        }


        public List<D_VendorData> GetList(int accuontType,string _FilterPartOfName,int countryId)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_VendorData
                     .Include("D_Country")
                     where 1==1
                     && (!_FilterPartOfName.Equals("")?obj.VendorNameEn.Contains(_FilterPartOfName):true)
                       && (countryId!=0 ? obj.Country==countryId : true)

                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_VendorData>();
            }
        }

        public D_VendorData GetDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_VendorData
                     where obj.Code== _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_VendorData>();
            }
        }
        #endregion

        #region "Add ,  Update  ,Delete" For Consignee

        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                
                DC.D_VendorData.Add(item as D_VendorData);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_VendorData).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as D_VendorData).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion



    }
}
