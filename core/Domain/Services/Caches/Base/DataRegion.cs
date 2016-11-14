using System.Collections.Generic;
using System.Linq;

namespace cm.frontend.core.Domain.Services.Caches.Base
{
    /// <summary>
    /// The DataRegion holds a collection of objects by their keys.
    /// </summary>
    public class DataRegion<TObject>
    {
        /// <summary>
        /// Dictionary holding all the regions of the cache.
        /// </summary>
        public IDictionary<string, TObject> Objects { get; set; } = new Dictionary<string, TObject>();

        /// <summary>
        /// The mutex object for locking the calls to the dictionary and making them thread safe.
        /// </summary>
        private readonly object _mutex = new object();

        /// <summary>
        /// Name of the region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Deletes all objects in the region.
        /// </summary>
        public void Clear()
        {
            lock (_mutex)
            {
                Objects.Clear();
            }
        }

        /// <summary>
        /// Adds an object to the region in the cache.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache.</param>
        /// <param name="value">The object saved to the cache.</param>
        public void Add(string key, TObject value)
        {
            lock (_mutex)
            {
                Objects[key] = value;
            }
        }

        /// <summary>
        /// Removes an object from the region in the cache.
        /// </summary>
        /// <param name="key">A unique value that is used to store and retrieve the object from the cache.</param>
        public void Remove(string key)
        {
            lock (_mutex)
            {
                Objects.Remove(key);
            }
        }

        /// <summary>
        /// Gets an object from the region in the cache by using the specified key.
        /// </summary>
        /// <param name="key">The unique value that is used to identify the object in the cache.</param>
        /// <returns>The object that was cached by using the specified key. Null is returned if the key does not exist.</returns>
        public TObject Get(string key)
        {
            var cachedObject = default(TObject);

            if (!string.IsNullOrEmpty(key))
            {
                lock (_mutex)
                {
                    Objects.TryGetValue(key, out cachedObject);
                }
            }

            return cachedObject;
        }

        /// <summary>
        /// Gets an enumerable list of all cached objects in the region.
        /// </summary>
        /// <returns>An enumerable list of all cached objects in the region.</returns>
        public IEnumerable<KeyValuePair<string, TObject>> GetObjects()
        {
            lock (_mutex)
            {
                return Objects.ToList();
            }
        }
    }
}
