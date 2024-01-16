using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Data.Entity;

namespace Infrastructure.DAL 
{
   public static  class Extenders
    {

        #region "Add ,  Update  ,Delete"

       public static int Add<T>(this DbContext DC, string entitySetName, T item)
            where T : IObjectWithChangeTracker
        {
          //  using (var DC = new WHMDBEntities())
            {
               // DC.ApplyChanges<T>(entitySetName, item);

                return DC.SaveChanges();
            }
        }

        public static int Delete<T>(this DbContext DC, string entitySetName, T item) where T : IObjectWithChangeTracker
        {
           // using (var DC = new WHMDBEntities())
            {
               // DC.AttachTo(entitySetName, item);

              //  DC.DeleteObject(item);

                return DC.SaveChanges();
            }
        }

        public static int Update<T>(this DbContext DC, string entitySetName, T item) where T : IObjectWithChangeTracker
        {
            //using (var DC = new WHMDBEntities())
            {
                try
                {
                   // DC.ApplyChanges<T>(entitySetName, item);

                    DC.SetModifiedProp<T>(item);

                    return DC.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static  void SetModifiedProp<T>(this DbContext DC, T obj)
        {
           // var stateEntry = DC.ObjectStateManager.GetObjectStateEntry(obj);
           // foreach (var propertyName in stateEntry.CurrentValues
           //                              .DataRecordInfo.FieldMetadata
           //                              .Select(fm => fm.FieldType.Name))
           // {
           //     try
           //     { 
           //     //You can dig into the context itself to mark the  properties as changed. 
           //     stateEntry.SetModifiedProperty(propertyName);
           //     }
           //     catch (Exception)
           //     {

           //         //throw;
           //     }
           //}
        }

        #endregion
    }
}
