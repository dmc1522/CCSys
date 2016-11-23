using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace LasMargaritas.DL
{
    public class DataReaderMapper
    {
        public static List<T> Map<T>(IDataReader dr, List<string> excludeProperties=null)
        {
            List<T> list = new List<T>();
            T objectToCreate = default(T);
            while (dr.Read())
            {
                objectToCreate = Activator.CreateInstance<T>();
                foreach (PropertyInfo propertyInfo in objectToCreate.GetType().GetProperties())
                {
                    if(excludeProperties == null || !excludeProperties.Contains(propertyInfo.Name))
                    {
                        if (!Equals(dr[propertyInfo.Name], DBNull.Value))
                        {
                            if (propertyInfo.PropertyType == typeof(float))
                            {
                                propertyInfo.SetValue(objectToCreate, Convert.ChangeType(dr[propertyInfo.Name], propertyInfo.PropertyType));
                            }
                            else
                            {
                                if (propertyInfo.PropertyType == typeof(Image))
                                {
                                    if (dr[propertyInfo.Name] != null)
                                    {
                                        byte[] imageBytes = (byte[])dr[propertyInfo.Name];
                                        using (MemoryStream memotryStream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                                        {
                                            memotryStream.Write(imageBytes, 0, imageBytes.Length);
                                            propertyInfo.SetValue(objectToCreate, Image.FromStream(memotryStream));
                                        }
                                    }
                                }
                                else
                                {                                   
                                    propertyInfo.SetValue(objectToCreate, dr[propertyInfo.Name]);
                                }

                            }
                        }               
                    }                            
                }
                list.Add(objectToCreate);
            }
            return list;
        }
    }
}
