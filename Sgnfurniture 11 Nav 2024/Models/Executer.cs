using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Sgnfurniture.Models
{
    public class Query<T>
    {
       public int returnKey;
       public SqlTransaction usedTransaction;
       public SqlConnection usedConnection;
       public bool isSuccess;
       public bool isTransactional;
        internal List<T> resultData;

        public string ErrorMessage { get; internal set; }

        internal void Close()
        {
            this.usedConnection.Close();
        }
        internal void Commit()
        {
            this.usedTransaction.Commit();
        }
        internal void RollBack()
        {
            this.usedTransaction.Rollback();
        }
    }
    public class Executer
    {
        DataLayer dataLayer;
        string connectionString = "";
        public Executer()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("con");
            dataLayer = new DataLayer();
        }
        public static T getObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        internal async Task<Query<V>> select<T,V>(T obj, string procedure)
        {
            Query<V> query = new Query<V>();
            List<V> list = new List<V>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                DataTable dataTable = await Task.Run(() => dataLayer.select(parameters, procedure)).ConfigureAwait(false);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    string[] header = new string[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        header[i] = dataTable.Columns[i].ToString();
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        V temp = getObject<V>();
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            try
                            {
                                if (temp.GetType().GetProperty(header[j]) != null)
                                {
                                    temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                                }
                            }
                            catch
                            {
                            }
                        }
                        list.Add(temp);
                    }
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;

                }
            }
            query.resultData = list;
            return query;
        }
        internal async Task<Query<T>> select<T>(string procedure)
        {
            Query<T> query = new Query<T>();
            List<T> list = new List<T>();
            DataTable dataTable = await Task.Run(() => dataLayer.select(procedure)).ConfigureAwait(false);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string[] header = new string[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ToString();
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    T temp = getObject<T>();
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        try
                        {
                            if (temp.GetType().GetProperty(header[j]) != null)
                            {
                                temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                    list.Add(temp);
                }
            }
            query.resultData = list;
            return query;
        }
        internal async Task<List<T>> select<T>(string procedure, string connectionString)
        {
            List<T> list = new List<T>();
            DataTable dataTable = await Task.Run(()=> dataLayer.select(procedure, connectionString)).ConfigureAwait(false);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string[] header = new string[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ToString();
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    T temp = getObject<T>();
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        try
                        {
                            if (temp.GetType().GetProperty(header[j]) != null)
                            {
                                temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                    list.Add(temp);
                }
            }
            return list;
        }
        internal async Task<List<T>> select<T>(T obj, string procedure, string connectionString)
        {
            List<T> list = new List<T>();

            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, connectionString));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                DataTable dataTable = await Task.Run(() => dataLayer.select(parameters, procedure, connectionString)).ConfigureAwait(false);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    string[] header = new string[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        header[i] = dataTable.Columns[i].ToString();
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        T temp = getObject<T>();
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            try
                            {
                                if (temp.GetType().GetProperty(header[j]) != null)
                                {
                                    temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                                }
                            }
                            catch
                            {
                            }
                        }
                        list.Add(temp);
                    }
                }
            }
            return list;
        }
        internal async Task<Query<T>> select<T>(T obj ,string procedure)
        {
            Query<T> query = new Query<T>();
            List<T> list = new List<T>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                DataTable dataTable = await Task.Run(() => dataLayer.select(parameters,procedure)).ConfigureAwait(false);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    string[] header = new string[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        header[i] = dataTable.Columns[i].ToString();
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        T temp = getObject<T>();
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            try
                            {
                                if (temp.GetType().GetProperty(header[j]) != null)
                                {
                                    temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                                }
                            }
                            catch
                            {
                            }
                        }
                        list.Add(temp);
                    }
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;

                }
            }
            query.resultData = list;
            return query;
        }
        internal async Task<Query<T>> InsertAsync<T>(T obj, string procedure)
        {
            Query<T> query = new Query<T>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.Insert(parameters, procedure)).ConfigureAwait(false);
                if (primaryKey != -1)
                {
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;
                }
            }
            return query;
        }
        internal async Task<Query<T>> InsertAndGetIdentityAsync<T>(T obj, string procedure)
        {
            Query<T> query = new Query<T>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure)).ConfigureAwait(false);
                if (primaryKey != -1)
                {
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;
                }
            }
            return query;
        }
        internal async Task<Query<T>> InsertAndGetIdentityAsync<T>(T obj, string procedure, string connectionString)
        {
            Query<T> query = new Query<T>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, connectionString));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, connectionString)).ConfigureAwait(false);
                if (primaryKey != 0)
                {
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;
                }
            }
            return query;
        }
        internal async Task<Query<T>> InsertAndGetIdentityAsync<T>(T obj, string procedure, bool havingTransaction)
        {
            Query<T> query = new Query<T>();
            SqlTransaction transaction = null;
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                await sqlConnection.OpenAsync().ConfigureAwait(false);
            }
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, sqlConnection)).ConfigureAwait(false);

            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                if (havingTransaction)
                {
                    try
                    {

                        transaction = sqlConnection.BeginTransaction();
                        int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection, transaction)).ConfigureAwait(false);
                        if (primaryKey != 0)
                        {
                            query.isTransactional = true;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        query.isSuccess = false;
                    }
                    finally
                    {
                        query.usedTransaction = transaction;
                        query.usedConnection = sqlConnection;
                    }
                }
                else
                {
                    try
                    {
                        int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection)).ConfigureAwait(false);

                        if (primaryKey != 0)
                        {
                            query.isTransactional = false;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        query.isSuccess = false;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return query;
        }  
        internal async Task<Query<T>> InsertAndGetIdentityAsync<T>(T obj, string procedure, string connectionString, bool havingTransaction)
        {
            Query<T> query = new Query<T>();
            SqlTransaction transaction = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                await sqlConnection.OpenAsync().ConfigureAwait(false);
            }
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, sqlConnection)).ConfigureAwait(false);

            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                if (havingTransaction)
                {
                    try
                    {

                        transaction = sqlConnection.BeginTransaction();
                        int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection, transaction)).ConfigureAwait(false);
                        if (primaryKey != 0)
                        {
                            query.isTransactional = true;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        query.isSuccess = false;
                    }
                    finally
                    {
                        query.usedTransaction = transaction;
                        query.usedConnection = sqlConnection;
                    }
                }
                else
                {
                    try
                    {
                        int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection)).ConfigureAwait(false);

                        if (primaryKey != 0)
                        {
                            query.isTransactional = false;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        query.isSuccess = false;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return query;
        }    
        internal async Task<Query<T>> InsertAndGetIdentityAsync<T>(T obj, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            Query<T> query = new Query<T>();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, connection, transaction));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, connection, transaction)).ConfigureAwait(false);
                if (primaryKey != 0)
                {
                    query.isTransactional = true;
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                    query.usedTransaction = transaction;
                    query.usedConnection = connection;
                }
            }
            else
            {
                query.ErrorMessage = "Paras Are Not Passed";
            }
            return query;
        }
    }
}