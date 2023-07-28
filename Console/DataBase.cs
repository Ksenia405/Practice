using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DataCompany
{

    class DataBase
    {
        static SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);


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

        public static int ReturnId(string tableName)
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
        }
        public static void InsertResult(DataResult dataResults)
        {

            string insertQuery = "EXEC InsertResult @remarks, @name, @type, @entity_number, @source, @source_information_url," +
                                   "@source_list_url, @call_sign, @end_date, @federal_register_notice, @gross_registered_tonnage, @gross_tonnage, @license_policy, @license_requirement," +
                                   "@standard_order, @start_date, @title, @vessel_flag, @vessel_owner, @vessel_type, @id";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
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

                command.ExecuteNonQuery();

            }

        }
        public static void InsertIdsList(DataResult dataResults)
        {
            string insertQuery = "EXEC  InsertIDS  @type, @number, @country";
            string insertQuerylist;
            string selectQuery = "EXEC SelectIdenticalIds @type, @number";

            using (SqlCommand command = new SqlCommand(selectQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.ids)
                {
                    command.CommandText = selectQuery;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@type", data.type);
                    command.Parameters.AddWithValue("@number", data.number);
                    command.Parameters.AddWithValue("@country", data.country);
                    for (int g = 0; g < command.Parameters.Count; g++)
                    {
                        if ((command.Parameters[g].Value is null) || (command.Parameters[g].Value.ToString() == "")) command.Parameters[g].Value = DBNull.Value;
                    }
                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        command.CommandText = insertQuery;
                        command.ExecuteNonQuery();

                        insertQuerylist = "EXEC InsertList  @id_ids, @id_result";
                        using (SqlCommand command1 = new SqlCommand(insertQuerylist, DataBase.GetConnection()))
                        {
                            command1.Parameters.Clear();
                            command1.Parameters.AddWithValue("@id_ids", DataBase.ReturnId("ids"));
                            command1.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
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
                        insertQuerylist = "EXEC InsertList  @id_ids, @id_result";
                        using (SqlCommand command1 = new SqlCommand(insertQuerylist, DataBase.GetConnection()))
                        {
                            command1.Parameters.Clear();
                            command1.Parameters.AddWithValue("@id_ids", id_for_list);
                            command1.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                            command1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public static void InsertAdress(DataResult dataResults)
        {
            string insertQuery = "EXEC InsertAddress @id_result, @address, @city, @state, @postal_code, @country";
            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.addresses)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
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
            string insertQuery = "EXEC InsertAlt_names @id_result, @alt_name";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.alt_names)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                    command.Parameters.AddWithValue("@alt_name", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertProgram(DataResult dataResults)
        {
            string insertQuery = "EXEC InsertProgram @id_result, @program";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.programs)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                    command.Parameters.AddWithValue("@program", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertDates_of_birth(DataResult dataResults)
        {
            string insertQuery = "EXEC InsertDate_of_birth @id_result, @date_of_birth";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.dates_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                    command.Parameters.AddWithValue("@date_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertPlaces_of_birth(DataResult dataResults)
        {
            string insertQuery = "EXEC InsertPlaces_of_birth @id_result, @place_of_birth";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.places_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                    command.Parameters.AddWithValue("@place_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertNationalities(DataResult dataResults)
        {
            string insertQuery = "EXEC InsertNationalities @id_result, @nationality";

            using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
            {
                foreach (var data in dataResults.nationalities)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", DataBase.ReturnId("results"));
                    command.Parameters.AddWithValue("@nationality", data);
                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
