 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

using System.Collections.ObjectModel;
using DomainInterface;
 



namespace Infrastructure.DAL 
{ 
        public class EntityCollection<TInterface, TEntity> :
            IEntityCollection<TInterface>
            where TInterface : class
            where TEntity : class ,  TInterface
        {

            private   TrackableCollection <TEntity> _entityCollection;

            public TrackableCollection<TEntity> GetentityCollection
            { get { return _entityCollection; } }

            
            private object _syncRoot;

            
            public virtual bool IsSynchronized
            {
                get { return false; }

            }

            public virtual object SyncRoot
            {
                get
                {
                    if (_syncRoot == null)
                    {
                        Interlocked.CompareExchange(ref _syncRoot, new object(), null);
                    }
                    return _syncRoot;
                }
            }
            public EntityCollection()
            {
            }
            public EntityCollection(TrackableCollection<TEntity> entityCollection)
            {
                _entityCollection = (TrackableCollection<TEntity>)entityCollection;
            }

            public void setCollection(TrackableCollection<TEntity> entityCollection)
            {
                _entityCollection = (TrackableCollection<TEntity>)entityCollection;
                for (int i = 0; i < entityCollection.Count; i++)
                {
                    this.Add(entityCollection.ElementAt(i));
                }

            }
         

            //ToDo test
            public void setCollection(IList<TEntity> entityCollection)
            {
                _entityCollection = new TrackableCollection<TEntity>(); 
                for (int i = 0; i < entityCollection.Count; i++)
                {
                    this.Add(entityCollection.ElementAt(i));
                } 
            }
       
            public void CopyTo(Array array, int arrayIndex)
            {
            
                var entitiesArray = array.Cast<TEntity>().ToArray();
                CopyTo(entitiesArray, arrayIndex);
            }
            public void CopyTo(TEntity[] array, int arrayIndex)
            {
                  _entityCollection.CopyTo(array, arrayIndex);
            }


 
            public TInterface this[int i]
            {
                get
                {
                    return this._entityCollection.ElementAt(i);
                }
                set
                {
                    this._entityCollection.ToArray()[i] =  value as TEntity ;
                }
            } 
            public int Count
            {
                get { return _entityCollection.Count; }
            }   
            public void Add(TInterface entity)
            {
                _entityCollection.Add((TEntity)entity);
            } 
            #region IEntityCollection<TInterface> Members


            public bool Remove(TInterface entity)
            {
                return this._entityCollection.Remove(entity as TEntity);
            }

            #endregion

            #region IEnumerable<TInterface> Members

            public IEnumerator<TInterface> GetEnumerator()
            {
                return _entityCollection.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _entityCollection.GetEnumerator();
            }

            #endregion

            #region IList<TInterface> Members

            public int IndexOf(TInterface item)
            {
                return this._entityCollection.IndexOf(item as TEntity);
            }

            public void Insert(int index, TInterface item)
            {
                this._entityCollection.Insert(index,item as TEntity);
            }

            public void RemoveAt(int index)
            {
                this._entityCollection.RemoveAt(index);
            }

            #endregion

            #region ICollection<TInterface> Members


            public void Clear()
            {
                this._entityCollection.Clear();
            }

            public bool Contains(TInterface item)
            {
                return this._entityCollection.Contains(item as TEntity);
            }

            public void CopyTo(TInterface[] array, int arrayIndex)
            {
                this._entityCollection.CopyTo(array as TEntity[], arrayIndex);
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            #endregion
        }  
}
