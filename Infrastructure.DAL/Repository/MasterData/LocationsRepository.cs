using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        /// <summary>
        /// Gets locations by LocationType with complete parent tree path
        /// </summary>
        /// <param name="locationTypeId">LocationType Code to filter by</param>
        /// <returns>List of D_Locations with parent names concatenated</returns>
        public List<D_Locations> GetLocationsByTypeWithParentPath(int locationTypeId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var locations = DC.D_Locations
                    .Where(loc => loc.LocationType == locationTypeId)
                    .Include("D_LocationType")
                    .ToList();

                return locations;
            }
        }

        /// <summary>
        /// Gets locations by LocationType with parent tree path as a string property (ViewModel recommended)
        /// </summary>
        /// <param name="locationTypeId">LocationType Code to filter by</param>
        /// <returns>List of tuples containing Location and Parent Path</returns>
        //public List<(D_Locations location, string parentPath)> GetLocationsByTypeWithParentPathString(int locationTypeId , int? LocationParentId)
        //{
        //    using (var DC = new AssetsEntitiesNew())
        //    {
        //        var locations = DC.D_Locations
        //            .Where(loc => loc.LocationType == locationTypeId)
        //            .ToList();

        //        var result = new List<(D_Locations, string)>();

        //        foreach (var location in locations)
        //        {
        //            string parentPath = GetLocationPath(location.Code, DC);
        //            result.Add((location, parentPath));
        //        }

        //        return result;
        //    }
        //}
        public List<(D_Locations location, string parentPath)> GetLocationsByTypeWithParentPathString(
    int locationTypeId, int? locationParentId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var query = DC.D_Locations
                    .Where(loc => loc.LocationType == locationTypeId);

                // Apply the parent filter only if locationParentId > 0
               //var query = DC.D_Locations.Where(x => x.LocationType == locationTypeId);

                if (locationParentId.HasValue && locationParentId.Value > 0)
                {
                    var ids = GetAllChildIds(locationParentId.Value, DC);
                    query = query.Where(x =>  x.LocationParentId.HasValue &&   ids.Contains(x.LocationParentId.Value));


                    
                }

                var locations = query.ToList();

                var result = new List<(D_Locations, string)>();

                foreach (var location in locations)
                {
                    string parentPath = GetLocationPath(location.Code, DC);
                    result.Add((location, parentPath));
                }

                return result;
            }
        }
        private List<int> GetAllChildIds(int parentId, AssetsEntitiesNew dc)
        {
            var ids = new List<int> { parentId };

            var children = dc.D_Locations
                             .Where(x => x.LocationParentId == parentId)
                             .Select(x => x.Code)
                             .ToList();

            foreach (var childId in children)
            {
                ids.AddRange(GetAllChildIds(childId, dc));
            }

            return ids;
        }

        /// <summary>
        /// Helper method to build complete parent tree path for a location
        /// </summary>
        private string GetLocationPath(int locationCode, AssetsEntitiesNew DC)
        {
            var pathList = new List<string>();
            var currentLocation = DC.D_Locations.FirstOrDefault(l => l.Code == locationCode);

            while (currentLocation != null)
            {
                pathList.Insert(0, currentLocation.LocationNameAr ?? currentLocation.LocationNameEn ?? "");
                
                if (currentLocation.LocationParentId.HasValue && currentLocation.LocationParentId > 0)
                {
                    currentLocation = DC.D_Locations.FirstOrDefault(l => l.Code == currentLocation.LocationParentId);
                }
                else
                {
                    break;
                }
            }

            return string.Join(" > ", pathList);
        }

        /// <summary>
        /// Gets locations by LocationType with parent tree path (English names)
        /// </summary>
        public List<(D_Locations location, string parentPathEn)> GetLocationsByTypeWithParentPathEnglish(int locationTypeId)
        {
            using (var DC = new AssetsEntitiesNew())
            {
                var locations = DC.D_Locations
                    .Where(loc => loc.LocationType == locationTypeId)
                    .ToList();

                var result = new List<(D_Locations, string)>();

                foreach (var location in locations)
                {
                    string parentPath = GetLocationPathEnglish(location.Code, DC);
                    result.Add((location, parentPath));
                }

                return result;
            }
        }

        /// <summary>
        /// Helper method to build complete parent tree path (English) for a location
        /// </summary>
        private string GetLocationPathEnglish(int locationCode, AssetsEntitiesNew DC)
        {
            var pathList = new List<string>();
            var currentLocation = DC.D_Locations.FirstOrDefault(l => l.Code == locationCode);

            while (currentLocation != null)
            {
                pathList.Insert(0, currentLocation.LocationNameEn ?? currentLocation.LocationNameAr ?? "");

                if (currentLocation.LocationParentId.HasValue && currentLocation.LocationParentId > 0)
                {
                    currentLocation = DC.D_Locations.FirstOrDefault(l => l.Code == currentLocation.LocationParentId);
                }
                else
                {
                    break;
                }
            }

            return string.Join(" > ", pathList);
        }
    }
}


