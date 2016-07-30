﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GitCommands.Utils
{
    public class WeakRefCache : IDisposable
    {
        public static readonly WeakRefCache Default = new WeakRefCache();
        private readonly Timer _clearTimer = new Timer(60 * 1000);
        private Dictionary<string, WeakReference> weakMap = new Dictionary<string, WeakReference>();

        public WeakRefCache()
        {
            _clearTimer.Elapsed += OnClearTimer;
            _clearTimer.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// TODO add expiration time (MemoryCache) after change to .net 4 full profile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectUniqueKey"></param>
        /// <param name="provideObject"></param>
        /// <returns></returns>
        public T Get<T>(string objectUniqueKey, Lazy<T> provideObject)
        {
            object cached = null;

            lock (weakMap)
            {
                WeakReference wref;
                if (weakMap.TryGetValue(objectUniqueKey, out wref))
                {
                    cached = wref.Target;
                }

                if (cached == null)
                {
                    cached = provideObject.Value;
                    weakMap[objectUniqueKey] = new WeakReference(cached);
                }
                else
                {
                    if (!(cached is T))
                    {
                        throw new InvalidCastException("Incompatible class for object: " + objectUniqueKey + ". Expected: " + typeof(T).FullName + ", found: " + cached.GetType().FullName);
                    }
                }
            }

            return (T)cached;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _clearTimer.Dispose();
        }

        private void OnClearTimer(object source, System.Timers.ElapsedEventArgs e)
        {
            lock (weakMap)
            {
                var toRemove = weakMap.Where(p => !p.Value.IsAlive).Select(p => p.Key).ToArray();
                foreach (var key in toRemove)
                    weakMap.Remove(key);
            }
        }
    }
}
