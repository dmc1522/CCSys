using LasMargaritas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LasMargaritas.BL.Cache
{
    public class Cacher<T>
    {
        public Cacher(string cachedFileName)
        {
            this.CachedFileName = cachedFileName;
        }
        public CachedModel<T> LoadFromCache()
        {
            if(File.Exists(CachedFileName))
            {
                string json = File.ReadAllText(CachedFileName);
                //write string to file
                CachedModel<T> cachedVersion = JsonConvert.DeserializeObject<CachedModel<T>>(json);
                CachedDate = cachedVersion.CachedDate;
                return cachedVersion;
            }
            return null;
        }
        public void SaveToCache(CachedModel<T> model)
        {
            string json = JsonConvert.SerializeObject(model);
            File.WriteAllText(CachedFileName, json);
            CachedDate = model.CachedDate;
        }
        public DateTime? CachedDate { get; set; }      

        public string CachedFileName { get; set; }

    }

    public class CachedModel<T>
    {
        public DateTime CachedDate { get; set; }

        public List<T> Models { get; set; }
    }
}