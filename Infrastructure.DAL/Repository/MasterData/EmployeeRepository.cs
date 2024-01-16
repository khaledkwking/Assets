using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;
using Infrastructure.DAL.Model.DB;
using Infrastructure.DAL.PartialClasses;

namespace Infrastructure.DAL
{
    public partial class EmployeeRepository
    {

        #region List
        public List<D_EmployeeList> GetList()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_EmployeeList

                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_EmployeeList>();
            }
        }


        public List<D_EmployeeList> GetList(int EntityCode,int jobTitle,string _FilterPartOfName,int EmpCode)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_EmployeeList
                     .Include("D_JobTitle")
                     .Include("D_OrgChart")
                     where 1==1
                     && (!_FilterPartOfName.Equals("")?obj.EmpName.Contains(_FilterPartOfName)  : true)
                       //&& (EntityCode != 0 ? obj.OrgRefId == EntityCode : true)
                       && (jobTitle != 0 ? obj.JobTitleId == jobTitle : true)
                       && (EmpCode != 0 ? obj.EmpCode == EmpCode : true)

                     orderby obj.Code descending
                     select obj);

                return result.ToList<D_EmployeeList>();
            }
        }

        public D_EmployeeList GetDetails(int _Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.D_EmployeeList
                     where obj.Code== _Code
                     orderby obj.Code descending
                     select obj);

                return result.FirstOrDefault<D_EmployeeList>();
            }
        }
        #endregion

        #region "Add ,  Update  ,Delete" For Consignee

        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                
                DC.D_EmployeeList.Add(item as D_EmployeeList);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as D_EmployeeList).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as D_EmployeeList).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        #endregion



    }
}
