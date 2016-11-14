using System;
using System.Collections.Generic;

namespace cm.frontend.core.Domain.Services.Caches.Base
{
    public class DataCache<TObject>
    {
        /// <summary>
        /// Dictionary holding all the regions of the cache.
        /// </summary>
        public IDictionary<string, DataRegion<TObject>> Regions { get; set; } = new Dictionary<string, DataRegion<TObject>>();

        /// <summary>
        /// The mutex object for locking the calls to the regions dictionary and making them thread safe.
        /// </summary>
        private readonly object _mutex = new object();

        /// <summary>
        /// The default time that an object is kept in the cache before it expires.
        /// </summary>
        public TimeSpan DefaultExpiryPeriod { get; set; }

        public virtual void Replace(string key, TObject value)
        {
            Remove(key);
            Add(key, value);
        }

        /// <summary>
        /// Adds an object to the default region in the cache.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache.</param>
        /// <param name="value">The object saved to the cache.</param>
        public virtual void Add(string key, TObject value)
        {
            Add(key, value, "default");
        }

        /// <summary>
        /// Adds an object to a region in the cache. If the region doesn't exist it's created.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache. </param>
        /// <param name="value">The object saved to the cache.</param>
        /// <param name="region">The name of the region to save the object in.</param>
        public virtual void Add(string key, TObject value, string region)
        {
            DataRegion<TObject> dataRegion;

            lock (_mutex)
            {
                if (!Regions.TryGetValue(region, out dataRegion))
                {
                    dataRegion = new DataRegion<TObject>();
                    Regions[region] = dataRegion;
                }
            }

            dataRegion.Add(key, value);
        }

        /// <summary>
        /// Removes an object to the default region in the cache.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache.</param>
        public virtual void Remove(string key)
        {
            Remove(key, "default");
        }

        /// <summary>
        /// Removes an object to a region in the cache. If the region doesn't exist it's created.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache. </param>
        /// <param name="region">The name of the region to remove the object from.</param>
        public virtual void Remove(string key, string region)
        {
            DataRegion<TObject> dataRegion;

            lock (_mutex)
            {
                if (!Regions.TryGetValue(region, out dataRegion))
                {
                    dataRegion = new DataRegion<TObject>();
                    Regions[region] = dataRegion;
                }
            }

            dataRegion.Remove(key);
        }

        /// <summary>
        /// Deletes all objects in the specified region.
        /// </summary>
        /// <param name="region">The name of the region whose objects are removed.</param>
        public virtual void ClearRegion(string region)
        {
            DataRegion<TObject> dataRegion;

            lock (_mutex)
            {
                Regions.TryGetValue(region, out dataRegion);
            }

            dataRegion?.Clear();
        }

        /// <summary>
        /// Creates a region.
        /// </summary>
        /// <param name="region">The name of the region that is created.</param>
        /// <returns>If the region has been created successfully or not. Should the region already exist, this method will return false.</returns>
        public virtual bool CreateRegion(string region)
        {
            lock (_mutex)
            {
                if (!Regions.ContainsKey(region))
                {
                    Regions[region] = new DataRegion<TObject>();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the region exists.
        /// </summary>
        /// <param name="region">The name of the region that needs to be checked.</param>
        /// <returns>If the region exists or not.</returns>
        public virtual bool ContainsRegion(string region)
        {
            lock (_mutex)
            {
                return Regions.ContainsKey(region);
            }
        }

        /// <summary>
        /// Gets an object from the cache using the specified key from the default region.
        /// </summary>
        /// <param name="key">The unique value that is used to identify the object in the cache.</param>
        /// <returns>The object that was cached by using the specified key. Null is returned if the key does not exist.</returns>
        public virtual TObject Get(string key)
        {
            return Get(key, "default");
        }

        /// <summary>
        /// Gets an object from the specified region by using the specified key. 
        /// </summary>
        /// <param name="key">The unique value that is used to identify the object in the region.</param>
        /// <param name="region">The name of the region where the object resides.</param>
        /// <returns>The object that was cached by using the specified key. Null is returned if the key does not exist.</returns>
        public virtual TObject Get(string key, string region)
        {
            DataRegion<TObject> dataRegion;

            lock (_mutex)
            {
                if (!Regions.TryGetValue(region, out dataRegion))
                {
                    dataRegion = new DataRegion<TObject>();
                    Regions[region] = dataRegion;
                }
            }

            return dataRegion.Get(key);
        }

        /// <summary>
        /// Gets an enumerable list of all cached objects in the specified region.
        /// </summary>
        /// <param name="region">The name of the region for which to return a list of all resident objects.</param>
        /// <returns>An enumerable list of all cached objects in the specified region.</returns>
        public virtual IEnumerable<KeyValuePair<string, TObject>> GetObjectsInRegion(string region = "default")
        {
            DataRegion<TObject> dataRegion;

            lock (_mutex)
            {
                if (!Regions.TryGetValue(region, out dataRegion))
                {
                    dataRegion = new DataRegion<TObject>();
                    Regions[region] = dataRegion;
                }
            }

            return dataRegion.GetObjects();
        }
    }
}
