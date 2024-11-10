using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Sgnfurniture.Models
{
    public class DataLayer
    {
        string connectionString = "";
        public DataLayer()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("Con");
        }
        internal async Task<DataTable> select(string procedure)
        {
            DataTable table = new DataTable();
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                }
            }).ConfigureAwait(false);
            return table;
        }
        internal async Task<DataTable> select(SqlParameter[] parameters, string procedure)
        {
            DataTable table = new DataTable();
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                }
            }).ConfigureAwait(false);
            return table;
        }
        internal async Task<DataTable> select(string procedure, string connectionString)
        {
            DataTable table = new DataTable();
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                }
            }).ConfigureAwait(false);
            return table;
        }
        internal async Task<DataTable> select(SqlParameter[] parameters, string procedure, string connectionString)
        {
            DataTable table = new DataTable();
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(table);
                }
            }).ConfigureAwait(false);
            return table;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    if (table != null && table.Rows.Count > 0)
                    {
                        pk = Convert.ToInt32(table.Rows[0][0].ToString());
                    }
                    else
                    {
                        pk = -1;
                    }
                }
            }).ConfigureAwait(false);

            return pk;
        }
        internal async Task<int> Insert(SqlParameter[] parameters, string procedure)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlCommand cmd = new SqlCommand(procedure, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    pk = cmd.ExecuteNonQuery();
                }
            }).ConfigureAwait(false);

            return pk;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand(procedure, connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    pk = cmd.ExecuteNonQuery();
                }
            }).ConfigureAwait(false);

            return pk;
        }
        public async Task<List<SqlParameter>> GetStoredProcedureParameters(string storedProcedureName)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter in command.Parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                        {
                            parameters.Add(parameter);
                        }
                    }
                    connection.Close();
                }
            }).ConfigureAwait(false);
            return parameters;
        }
        public async Task<List<SqlParameter>> GetStoredProcedureParameters(string storedProcedureName, string connectionString)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter in command.Parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                        {
                            parameters.Add(parameter);
                        }
                    }
                    connection.Close();
                }
            }).ConfigureAwait(false);
            return parameters;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, string connectionString)
        {
            int pk = 0;
            await Task.Run(() =>
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(procedure, connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(parameters);
                            cmd.CommandTimeout = 999999;
                            cmd.CommandTimeout = 999999;
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            sqlDataAdapter.Fill(table);
                            if (table != null && table.Rows.Count > 0)
                            {
                                pk = Convert.ToInt32(table.Rows[0][0].ToString());
                            }
                            else
                            {
                                pk = -1;
                            }
                        }
                    }
                    catch (Exception ex) { }
                    finally { connection.Close(); }
                }
            }).ConfigureAwait(false);

            return pk;
        }
        public async Task<List<SqlParameter>> GetStoredProcedureParameters(string storedProcedureName, SqlConnection connection)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(command);
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        parameters.Add(parameter);
                    }
                }
            }).ConfigureAwait((false));
            return parameters;
        }
        public async Task<List<SqlParameter>> GetStoredProcedureParameters(string storedProcedureName, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            await Task.Run(() =>
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection, sqlTransaction);
                command.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(command);
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        parameters.Add(parameter);
                    }
                }
            }).ConfigureAwait(false);
            return parameters;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand(procedure, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    if (table != null && table.Rows.Count > 0)
                    {
                        pk = Convert.ToInt32(table.Rows[0][0].ToString());
                    }
                    else
                    {
                        pk = -1;
                    }
                }
            }).ConfigureAwait(false);

            return pk;
        }
        internal async Task<int> insertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand(procedure, connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    cmd.CommandTimeout = 999999;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);
                    if (table != null && table.Rows.Count > 0)
                    {
                        pk = Convert.ToInt32(table.Rows[0][0].ToString());
                    }
                    else
                    {
                        pk = -1;
                    }
                }
            }).ConfigureAwait(false);
            return pk;
        }
        internal async Task<int> insertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection)
        {
            int pk = 0;
            using (SqlCommand cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = 999999;
                cmd.CommandTimeout = 999999;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                await Task.Run(() => sqlDataAdapter.Fill(table)).ConfigureAwait(false);
                if (table != null && table.Rows.Count > 0)
                {
                    pk = Convert.ToInt32(table.Rows[0][0].ToString());
                }
                else
                {
                    pk = -1;
                }
            }
            return pk;
        }


        public DataTable GetTable(string sql)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter ad  = new SqlDataAdapter(sql, connection);
            ad.Fill(table);

            return table;   
        }

        internal DataTable GetHashCategories(SqlParameter[] sp, string v)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(v, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(sp);
            cmd.CommandTimeout = 999999;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(table);
            return table;
        }
    }
}