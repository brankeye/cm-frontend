using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Services.Realms.Base
{
    public class AsyncRealm<T>
        where T : RealmObject, ILocalEntity, new()
    {
        public Realm RealmInstance => _realmInstance = Realm.GetInstance();
        private Realm _realmInstance;

        public virtual async Task WriteAsync(Action<AsyncRealm<T>> mutation)
        {
            await RealmInstance.WriteAsync(tempRealm =>
            {
                mutation?.Invoke(new AsyncRealm<T>());
            });
        }

        public virtual Transaction BeginWrite()
        {
            var transaction = RealmInstance.BeginWrite();
            return transaction;
        }

        public virtual RealmResults<T> GetRealmResults()
        {
            var results = RealmInstance.All<T>();
            return results;
        }

        public virtual T Get(int localId)
        {
            T item;
            try
            {
                item = RealmInstance.All<T>().First(x => x.LocalId == localId);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                item = null;
            }
            return item;
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            T item;
            try
            {
                item = RealmInstance.All<T>().First(predicate);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                item = null;
            }
            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var list = RealmInstance.All<T>().AsEnumerable();
            return list ?? new List<T>();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            var list = RealmInstance.All<T>().Where(predicate).AsEnumerable();
            return list ?? new List<T>();
        }

        public virtual IEnumerable<T> GetAllOrdered(Expression<Func<T, dynamic>> orderPredicate)
        {
            var list = RealmInstance.All<T>().OrderBy(orderPredicate).AsEnumerable();
            return list ?? new List<T>();
        }

        public virtual IEnumerable<T> GetAllOrdered(Expression<Func<T, bool>> wherePredicate, Expression<Func<T, dynamic>> orderPredicate)
        {
            var list = RealmInstance.All<T>().Where(wherePredicate).OrderBy(orderPredicate).AsEnumerable();
            return list ?? new List<T>();
        }

        public virtual T CreateObject()
        {
            var item = RealmInstance.CreateObject<T>();
            item.LocalId = GetAutoId();
            return item;
        }

        public virtual bool Manage(T item)
        {
            if (item.IsManaged) return false;

            if (item.LocalId <= 0)
            {
                item.LocalId = GetAutoId();
            }
            RealmInstance.Manage(item);

            return true;
        }

        public virtual bool Manage(T item, bool update)
        {
            if (item.IsManaged) return false;

            if (item.LocalId <= 0)
            {
                item.LocalId = GetAutoId();
            }
            RealmInstance.Manage(item, update);

            return true;
        }

        public virtual bool ManageAll(List<T> list)
        {
            var result = true;
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                result = result && Manage(item);
            }
            return result;
        }

        public virtual bool Remove(int localId)
        {
            T item;
            try
            {
                item = RealmInstance.All<T>().First(x => x.LocalId == localId);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                item = null;
            }
            var result = Remove(item);
            return result;
        }

        public virtual bool Remove(T item)
        {
            if (item == null || !item.IsManaged) return false;
            RealmInstance.Remove(item);
            return true;
        }

        public virtual bool Remove(Expression<Func<T, bool>> predicate)
        {
            var item = RealmInstance.All<T>().First(predicate);
            var result = Remove(item);
            return result;
        }

        public virtual bool RemoveAll(List<T> list)
        {
            var result = true;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                result = result && Remove(item);
            }
            return result;
        }

        public virtual void RemoveRange(RealmResults<T> range)
        {
            RealmInstance.RemoveRange(range);
        }

        public virtual void Subscribe(RealmResults<T>.NotificationCallback callback)
        {
            RealmInstance.All<T>().SubscribeForNotifications(callback);
        }

        public virtual void Refresh()
        {
            RealmInstance.Refresh();
        }

        // returns an autoincremented Id
        public int GetAutoId()
        {
            return GetNextId();
        }

        private int GetNextId()
        {
            var modelId = GetLastId();
            var resultingId = Interlocked.Increment(ref modelId);
            return resultingId;
        }

        private int GetLastId()
        {
            var lastId = 0;
            T item;
            try
            {
                item = RealmInstance.All<T>().OrderByDescending(x => x.LocalId).First();
                lastId = item.LocalId;
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                item = null;
            }
            if (item == null) lastId = 0;
            return lastId;
        }
    }
}
