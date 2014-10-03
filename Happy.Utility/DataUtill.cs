using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Happy.Utility
{
    public class DataUtill
    {
        /// <summary>git
        /// DataTable을 Json형식으로 변환
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new

            System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows =
              new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        /// <summary>
        /// object null체크
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public string NullToString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
        /// <summary>
        /// object int로 변환
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static int ConvertInt(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(obj.ToString());
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        /// <summary>
        /// object float로 변환
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static float ConvertFloat(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return float.Parse(obj.ToString());
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        #region DataTable, DataRow Bind Class
        /// <summary>
        /// DataTable을 Class List로 변환
        /// </summary>
        /// <typeparam name="T">Class List</typeparam>
        /// <param name="datatable">DataTable</param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }

        }

        /// <summary>
        /// DataRow를 Class로 변환
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="datarow">DataRow</param>
        /// <returns></returns>
        public static dynamic ConvertToRow<T>(DataRow datarow) where T : new()
        {
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datarow.Table.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                return getObject<T>(datarow, columnsNames);
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// DataTable를 Class로 변환
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="datarow">DataRow</param>
        /// <returns></returns>
        public static dynamic ConvertToRow<T>(DataTable datatable) where T : new()
        {
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                if (datatable.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    return getObject<T>(datatable.Rows[0], columnsNames);
                }
            }
            catch
            {
                return null;
            }

        }

        private static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }
        #endregion
    }
}
