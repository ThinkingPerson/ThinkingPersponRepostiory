
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Kiss_Core
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        //public BaseDBContext _baseDB;
        public IDbCommand command;
        public BaseRepository(BaseDBContext baseDB)
        {
            IDbConnection dbConnection = baseDB.Connection;
            dbConnection.Open();

            command = dbConnection.CreateCommand();

        }

        public int Delete(string deleteSql)
        {
            command.CommandText = deleteSql;
            return command.ExecuteNonQuery();
        }

        public object Select(string selectOneSql)
        {
            command.CommandText = selectOneSql;
            return command.ExecuteScalar();
        }

        public IList<T> Selects(string selectSql)
        {
            command.CommandText = selectSql;
            IDataReader dataReader = command.ExecuteReader();

            using (dataReader)
            {
                List<T> list = new List<T>();

                Type modeType = typeof(T);

                int count = dataReader.FieldCount;

                while (dataReader.Read())
                {
                    T model = Activator.CreateInstance<T>();

                    for (int i = 0; i < count; i++)
                    {
                        if (!IsNullOrDBNull(dataReader[i]))
                        {
                            PropertyInfo propertyInfo = modeType.GetProperty(dataReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(model, HackType(dataReader[i], propertyInfo.PropertyType), null);
                            }
                        }
                    }

                    list.Add(model);
                }

                return list;
            }
        }

        public int Insert(string insertSql)
        {
            command.CommandText = insertSql;
            return command.ExecuteNonQuery();
        }

        public int Update(string updateSql)
        {
            command.CommandText = updateSql;
            return command.ExecuteNonQuery();
        }

        #region 辅助方法

        /// <summary>
        ///  将IDataReader转换为DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable DataTableToIDataReader(IDataReader reader)
        {
            DataTable objDataTable = new DataTable("Table");
            int intFieldCount = reader.FieldCount;
            for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
            {
                objDataTable.Columns.Add(reader.GetName(intCounter).ToUpper(), reader.GetFieldType(intCounter));
            }
            objDataTable.BeginLoadData();
            object[] objValues = new object[intFieldCount];
            while (reader.Read())
            {
                reader.GetValues(objValues);
                objDataTable.LoadDataRow(objValues, true);
            }
            reader.Close();
            objDataTable.EndLoadData();
            return objDataTable;
        }

        public static T ReaderToModel<T>(IDataReader dr)
        {
            try
            {
                using (dr)
                {
                    if (dr.Read())
                    {
                        List<string> list = new List<string>(dr.FieldCount);
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            list.Add(dr.GetName(i).ToLower());
                        }
                        T model = Activator.CreateInstance<T>();
                        foreach (PropertyInfo pi in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
                        {
                            if (list.Contains(pi.Name.ToLower()))
                            {
                                if (!IsNullOrDBNull(dr[pi.Name]))
                                {
                                    pi.SetValue(model, HackType(dr[pi.Name], pi.PropertyType), null);
                                }
                            }
                        }
                        return model;
                    }
                }
                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> ReaderToList<T>(IDataReader dr)
        {
            using (dr)
            {
                List<string> field = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    field.Add(dr.GetName(i).ToLower());
                }
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (field.Contains(property.Name.ToLower()))
                        {
                            if (!IsNullOrDBNull(dr[property.Name]))
                            {
                                property.SetValue(model, HackType(dr[property.Name], property.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }

        //这个类对可空类型进行判断转换，要不然会报错
        private static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        private static bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString())) ? true : false;
        }

        #endregion
    }
}
