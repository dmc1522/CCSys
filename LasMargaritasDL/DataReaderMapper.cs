using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.DL
{
    public class DataReaderMapper
    {
        public static List<T> Map<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T objectToCreate = default(T);
            while (dr.Read())
            {
                objectToCreate = Activator.CreateInstance<T>();
                foreach (PropertyInfo propertyInfo in objectToCreate.GetType().GetProperties())
                {
                    if (!Equals(dr[propertyInfo.Name], DBNull.Value))
                    {
                        if(propertyInfo.PropertyType == typeof(float))
                            propertyInfo.SetValue(objectToCreate, Convert.ChangeType(dr[propertyInfo.Name], propertyInfo.PropertyType));
                        else
                            propertyInfo.SetValue(objectToCreate, dr[propertyInfo.Name]);
                    }
                }
                list.Add(objectToCreate);
            }
            return list;
        }
    }
}
