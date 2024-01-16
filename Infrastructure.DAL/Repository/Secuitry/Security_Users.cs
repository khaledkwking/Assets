
using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DAL
{
    public class Security_Users
    {

        public static Security_Users ins = new Security_Users();

        #region "Users"

         public List<Security_pr_AdminType> FillAdminTypes()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_AdminType

                     orderby obj.NameAr
                     select obj);

                return result.ToList<Security_pr_AdminType>();
            }
        }
        public List<Security_pr_admin> GetItems(int admintype, string partofname, int DeptId)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_admin
                          .Include("Security_pr_AdminType")
                     orderby obj.id descending
                     where obj.id !=0
                       && (admintype != 0 ? obj.AdminType == admintype : 1 == 1)
                        && (partofname != "" ? obj.name.Contains(partofname) : 1 == 1)
                      select obj);

                return result.ToList<Security_pr_admin>();
            }
        }
        public List<Security_pr_admin> GetUserbyType(int admintype)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_admin
                         //.Include("NewsDetails")
                     orderby obj.id descending
                     where obj.AdminType == admintype
                     select obj);

                return result.ToList<Security_pr_admin>();
            }
        }
        
        public Security_pr_admin GetDetails(int Code)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_admin
                          .Include("Security_pr_AdminType")
                     orderby obj.id descending
                     where obj.id == Code
                     select obj);

                return result.FirstOrDefault();
            }
        }
        public bool CheckUserNameExitance(string UserName)
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_admin

                     orderby obj.id descending
                     where obj.username == UserName
                     select obj);

                return result.FirstOrDefault() != null ? true : false;
            }
        }
        public List<Security_pr_AdminType> GetAdminTypes()
        {
            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_AdminType

                     orderby obj.id descending

                     select obj);

                return result.ToList<Security_pr_AdminType>();
            }
        }
        #endregion

        #region "Permissions"

        public List<Security_SP_PRM_getuserPermissions_Result> getUserpermision(int jobId, int userId)
        {

            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_SP_PRM_getuserPermissions(jobId, userId)
                     select obj);

                return result.ToList<Security_SP_PRM_getuserPermissions_Result>();
            }

            
        }


        public void DeleteUserpermssion(int jobId, int userId)
        {

            using (var DC = new AssetsEntities())
            {
                var result =
                    (from obj in DC.Security_pr_Permission
                     where obj.jobid==jobId && obj.userid==userId
                     select obj).ToList();

                if (result!=null && result.Count>0)
                {
                    foreach (var item in result)
                    {
                        Deletepermssion(item);

                    }

                }
            }

        }
        #endregion
         
        #region "Add ,  Update  ,Delete" For Users



        public int Add<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.Security_pr_admin.Add(item as Security_pr_admin);
                return DC.SaveChanges();
            }
        }

        public int Delete<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Security_pr_admin).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int Update<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as Security_pr_admin).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


        public int AddPermission<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                DC.Security_pr_Permission.Add(item as Security_pr_Permission);
                return DC.SaveChanges();
            }
        }

        public int Deletepermssion<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                //var item = DC.News.Where(N => N.newsId == id).FirstOrDefault();
                DC.Entry(item as Security_pr_Permission).State = System.Data.Entity.EntityState.Deleted;
                return DC.SaveChanges();
            }
        }

        public int UpdatePermssion<T>(T item)
        {
            using (var DC = new AssetsEntities())
            {
                // Mark entity as modified
                DC.Entry(item as Security_pr_Permission).State = System.Data.Entity.EntityState.Modified;
                return DC.SaveChanges();
            }
        }


 


        #endregion



    }
}
