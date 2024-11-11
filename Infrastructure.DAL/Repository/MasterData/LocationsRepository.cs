using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class LocationsRepository
    {

        #region List
        public List<D_Locations> GetList()
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_Locations>();
            }
        }


        public List<D_Locations> GetList(int parentId, string _FilterPartOfName)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     .Include("D_LocationType")
                     where obj.LocationParentId == parentId
                   && (!_FilterPartOfName.Equals("") ? obj.LocationNameAr.Contains(_FilterPartOfName) : 1 == 1)
                     //  && (parentId!=0 ? obj.Cat_ParentId== parentId : 1 == 1)
                     //orderby obj.code descending
                     select obj);

                return result.ToList<D_Locations>();
            }
        }
        public List<D_Locations> getEntityLocations(int OrgChartRefCode, string _FilterPartOfName)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     .Include("D_LocationType")
                     where obj.OrgChartRefCode == OrgChartRefCode
                   && (!_FilterPartOfName.Equals("") ? obj.LocationNameAr.Contains(_FilterPartOfName) : 1 == 1)
                     //  && (parentId!=0 ? obj.Cat_ParentId== parentId : 1 == 1)
                     //orderby obj.code descending
                     select obj);

                return result.ToList<D_Locations>();
            }
        }
        public D_Locations GetDetails(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     where obj.Code == _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_Locations>();
            }
        }



        public D_EmployeeLocations GetEmployeeLocation(int EmpCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_EmployeeLocations
                     where obj.EmpCode == EmpCode
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_EmployeeLocations>();
            }
        }


        public bool CheckChildExistance(int _Code)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var result =
                    (from obj in DC.D_Locations
                     where obj.LocationParentId == _Code
                     select obj);
                if (result.ToList().Count > 0)
                {
                    return true;

                }
                return false;
            }
        }


        public void ResetEntityLocation(int EntityCode)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.sp_clearEntityLocations(EntityCode);
            }
        }

        public void SetEntityLocation(int EntityCode,string TragetLocations)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                DC.sp_setEntityLocations(EntityCode, TragetLocations);
            }
        }
        #endregion

        #region "Add ,  Update  ,Delete" For Consignee

        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {

                DC.D_Locations.Add(item as D_Locations);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_Locations).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as D_Locations).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }





        #endregion



        #region "Add ,  Update  ,Delete" For Update Employee Location

        public int AddD_EmployeeLocations<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {

                DC.D_EmployeeLocations.Add(item as D_EmployeeLocations);
                return DC.SaveChanges();
            }
        }

        public int DeleteD_EmployeeLocations<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_EmployeeLocations).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdateD_EmployeeLocations<T>(T item)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                // Mark entity as modified
                DC.Entry(item as D_EmployeeLocations).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }





        #endregion


    }
}
