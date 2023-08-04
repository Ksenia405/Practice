using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataCompany
{

    class DataBase
    {
        static SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        static Int32 LastIdResult;
        static int Id_ids;

        public static void openConnection()
        {

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public static void closeConnection()
        {

            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public static SqlConnection GetConnection()
        {
            return sqlConnection;
        }

     /*   public static int ReturnId(string tableName)
        {
            string query = "EXEC LastID @tableName";

            using (SqlCommand command = new SqlCommand(query, DataBase.GetConnection()))
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tableName", tableName);
                SqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                int res = Convert.ToInt32(dataReader[0]);
                dataReader.Close();
                return res;

            }
        }*/
        public static void InsertResult(DataResult dataResults)
        {

            using (SqlCommand command = new SqlCommand("InsertResult", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@remarks", dataResults.remarks);
                command.Parameters.AddWithValue("@name", dataResults.name);
                command.Parameters.AddWithValue("@type", dataResults.type);
                command.Parameters.AddWithValue("@entity_number", dataResults.entity_number);
                command.Parameters.AddWithValue("@source", dataResults.source);
                command.Parameters.AddWithValue("@source_information_url", dataResults.source_information_url);
                command.Parameters.AddWithValue("@source_list_url", dataResults.source_list_url);
                command.Parameters.AddWithValue("@call_sign", dataResults.call_sign);
                command.Parameters.AddWithValue("@end_date", dataResults.end_date);
                command.Parameters.AddWithValue("@federal_register_notice", dataResults.federal_register_notice);
                command.Parameters.AddWithValue("@gross_registered_tonnage", dataResults.gross_registered_tonnage);
                command.Parameters.AddWithValue("@gross_tonnage", dataResults.gross_tonnage);
                command.Parameters.AddWithValue("@license_policy", dataResults.license_policy);
                command.Parameters.AddWithValue("@license_requirement", dataResults.license_requirement);

                command.Parameters.AddWithValue("@standard_order", dataResults.standard_order);
                command.Parameters.AddWithValue("@start_date", dataResults.start_date);
                command.Parameters.AddWithValue("@title", dataResults.title);
                command.Parameters.AddWithValue("@vessel_flag", dataResults.vessel_flag);
                command.Parameters.AddWithValue("@vessel_owner", dataResults.vessel_owner);
                command.Parameters.AddWithValue("@vessel_type", dataResults.vessel_type);
                command.Parameters.AddWithValue("@id", dataResults.id);

                for (int g = 0; g < command.Parameters.Count; g++)
                {
                    if ((command.Parameters[g].Value is null) || (command.Parameters[g].Value.ToString() == "")) command.Parameters[g].Value = DBNull.Value;
                }
                SqlParameter idResultParam = new SqlParameter("@id_result", SqlDbType.Int);
                idResultParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(idResultParam);

                command.ExecuteNonQuery();
               LastIdResult = (Int32)command.Parameters["@id_result"].Value;
               

            }

        }
        public static void InsertIdsList(DataResult dataResults)
        {
            using (SqlCommand command = new SqlCommand("SelectIdenticalIds", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.ids)
                {
                    command.CommandText = "SelectIdenticalIds";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", data.type);
                    command.Parameters.AddWithValue("@number", data.number);
                    for (int g = 0; g < command.Parameters.Count; g++)
                    {
                        if ((command.Parameters[g].Value is null) || (command.Parameters[g].Value.ToString() == "")) command.Parameters[g].Value = DBNull.Value;
                    }
                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        if ((data.country is null)||(data.country.ToString()=="")) command.Parameters.AddWithValue("@country", DBNull.Value);
                        else command.Parameters.AddWithValue("@country", data.country);
                        command.CommandText = "InsertIDS";
                        SqlParameter idResultParam = new SqlParameter("@id_ids", SqlDbType.Int);
                        idResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(idResultParam);

                        command.ExecuteNonQuery();
                        Id_ids = (Int32)command.Parameters["@id_ids"].Value;

                        using (SqlCommand command1 = new SqlCommand("InsertList", DataBase.GetConnection()))
                        {
                            command1.CommandType = System.Data.CommandType.StoredProcedure;
                            command1.Parameters.Clear();
                            command1.Parameters.AddWithValue("@id_ids", Id_ids);
                            command1.Parameters.AddWithValue("@id_result", LastIdResult);
                            command1.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int id_for_list;
                        SqlDataReader dataReader1 = command.ExecuteReader();
                        dataReader1.Read();
                        id_for_list = Convert.ToInt32(dataReader1["id_ids"]);
                        dataReader1.Close();
                        using (SqlCommand command1 = new SqlCommand("InsertList", DataBase.GetConnection()))
                        {
                            command1.CommandType = System.Data.CommandType.StoredProcedure;
                            command1.Parameters.Clear();
                            command1.Parameters.AddWithValue("@id_ids", id_for_list);
                            command1.Parameters.AddWithValue("@id_result", LastIdResult);
                            command1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public static void InsertAdress(DataResult dataResults)
        {
            using (SqlCommand command = new SqlCommand("InsertAddress", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.addresses)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@address", data.address);
                    command.Parameters.AddWithValue("@city", data.city);
                    command.Parameters.AddWithValue("@state", data.state);
                    command.Parameters.AddWithValue("@postal_code", data.postal_code);
                    command.Parameters.AddWithValue("@country", data.country);
                    for (int g = 0; g < command.Parameters.Count; g++)
                    {
                        if ((command.Parameters[g].Value is null) || (command.Parameters[g].Value.ToString() == "")) command.Parameters[g].Value = DBNull.Value;
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertAlt_name(DataResult dataResults)
        {

            using (SqlCommand command = new SqlCommand("InsertAlt_names", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.alt_names)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@alt_name", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertProgram(DataResult dataResults)
        {

            using (SqlCommand command = new SqlCommand("InsertProgram", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.programs)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@program", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertDates_of_birth(DataResult dataResults)
        {

            using (SqlCommand command = new SqlCommand("InsertDate_of_birth", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.dates_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@date_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertPlaces_of_birth(DataResult dataResults)
        {
            using (SqlCommand command = new SqlCommand("InsertPlaces_of_birth", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.places_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@place_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertNationalities(DataResult dataResults)
        {

            using (SqlCommand command = new SqlCommand("InsertNationalities", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.nationalities)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", LastIdResult);
                    command.Parameters.AddWithValue("@nationality", data);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
