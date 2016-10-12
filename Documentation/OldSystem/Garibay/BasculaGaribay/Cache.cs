using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace BasculaGaribay
{
    public enum CacheTables
    {
        PRODUCTORES = 1,
        PRODUCTOS,
        BODEGAS,
        PRODUCTORESFORCMB,
        CICLOS,
        CLIENTESVENTAS,
        PROVEEDORESGANADO,
        BOLETASPENDIENTES
    }
#region CACHE ELEMENT CLASS
        [Serializable()]
        public class CacheElement : Object, ISerializable
        {
            //value cached
            private object _Value;

            public object Value
            {
                get { return _Value; }
                set { _Value = value; }
            }
            /// <summary>
            /// this time save the time to be added to the start time to know if the value still valid.
            /// </summary>
            private TimeSpan m_TimeToExpire;
            /// <summary>
            /// This time start to count if the cache is valid
            /// </summary>
            private DateTime m_TimeStart;
            private bool m_NeverExpires = true;
            /// <summary>
            /// check if the cache still valid
            /// </summary>
            public bool CacheStillValid
            {
                get 
                {
                    return m_TimeToExpire.TotalSeconds == 0? true : (DateTime.Now - (m_TimeStart + m_TimeToExpire)).TotalSeconds < 0;
                }
            }
            public CacheElement()
            {

            }
            /// <summary>
            /// element without time to expire
            /// </summary>
            /// <param name="Value"> Value to cache</param>
            public CacheElement(object Value): this(Value, new TimeSpan())
            {
            }
            /// <summary>
            /// element with time to cache
            /// </summary>
            /// <param name="Value"></param>
            /// <param name="ts">Value to cache</param>
            public CacheElement(object Value, TimeSpan ts)
            {
                this._Value = Value;
                this.m_TimeStart = DateTime.Now;
                this.m_TimeToExpire = ts;
            }
//             public CacheElement(object Value, bool NeverExpires = true)
//             {
// 
//             }

            public CacheElement(SerializationInfo info, StreamingContext context)
            {
                _Value = info.GetValue("_Value",typeof(object));
                m_TimeToExpire = (TimeSpan)info.GetValue("m_TimeToExpire", typeof(TimeSpan));
                m_TimeStart = info.GetDateTime("m_TimeStart");
                m_NeverExpires = info.GetBoolean("m_NeverExpires");
            }

            #region ISerializable Members

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("_Value", _Value);
                info.AddValue("m_TimeToExpire", m_TimeToExpire);
                info.AddValue("m_TimeStart", m_TimeStart);
                info.AddValue("m_NeverExpires", m_NeverExpires);
            }

            #endregion
        }
#endregion
    public class Cache
    {
        private string CacheFileName = "CacheData.bin";
        
        private static Cache m_instance = new Cache();
        private Hashtable CacheTable = new Hashtable(); 

        public static Cache Instance
        {
            get
            {
                return m_instance;
            }
        }

        private Cache()
        {
            this.CacheTable.Clear();
        }

        public void AddCache(object Key, CacheElement Element)
        {
            CacheTable.Add(Key, Element);
            this.SaveCache();
        }
        public void AddCacheElement(object key, object Value)
        {
            this.AddCacheElement(key, Value, new TimeSpan());
            this.SaveCache();
        }
        public void AddCacheElement(object key, object Value, TimeSpan ts)
        {
            this.CacheTable.Add(key, new CacheElement(Value, ts));
            this.SaveCache();
        }

        public CacheElement GetCache(object key)
        {
            return (CacheElement)this.CacheTable[key];
        }

        public bool CheckIfCacheIsValid(object key)
        {
            if (WSConnector.Instance.IsOfflineMode)
            {
                return true;
            }
            else
            {
                return this.GetCache(key) != null ? this.GetCache(key).CacheStillValid : false;
            }
        }

        public void InvalidChaceEntry(object key)
        {
            if(this.GetCache(key) != null)
            {
                this.CacheTable.Remove(key);
            }
        }

        public bool SaveCache()
        {
            bool bResult = false;
            try
            {
                using (Stream stream = File.Open(this.CacheFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, this.CacheTable);
                    bResult = true;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            return bResult;
        }

        public bool LoadCache()
        {
            bool bResult = false;
            try
            {
                if(File.Exists(this.CacheFileName))
                {
                    using (Stream stream = File.Open(this.CacheFileName, FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        this.CacheTable = (Hashtable) bin.Deserialize(stream);
                        bResult = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            return bResult;
        }

    }
}
