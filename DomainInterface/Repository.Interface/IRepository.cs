using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainInterface.Repository 
{
    public interface IRepository 
    {


            
            IList<T> GetAll<T>();
            // IList<T> GetBy(string find, string property, bool exactMatch);
            
            int Update<T>( T item);
            T Save<T>(T item);
            int Delete<T>(T item);
            int Add<T>(T item);
            IList<T> Save<T>(IList<T> items);
           
           
            // T GetNewObject<T>(); 
         
    }
}
