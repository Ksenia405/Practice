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
   public class DataBase
    {
        static SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-R88ICHUA\\SEREVERMSSQL;Initial Catalog=Companies;Integrated Security=True");
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

        public static int InsertResult(DataResult dataResults)
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
               
                return (Int32)command.Parameters["@id_result"].Value;
            }

        }
        public static void InsertIdsList(DataResult dataResults, int id_res)
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
                        if ((data.country is null) || (data.country.ToString() == "")) command.Parameters.AddWithValue("@country", DBNull.Value);
                        else command.Parameters.AddWithValue("@country", data.country);
                        command.CommandText = "InsertIDS";
                        SqlParameter idResultParam = new SqlParameter("@id_ids", SqlDbType.Int);
                        idResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(idResultParam);

                        command.ExecuteNonQuery();
                        var Id_ids = (Int32)command.Parameters["@id_ids"].Value;

                        using (SqlCommand command1 = new SqlCommand("InsertList", DataBase.GetConnection()))
                        {
                            command1.CommandType = System.Data.CommandType.StoredProcedure;
                            command1.Parameters.Clear();
                            command1.Parameters.AddWithValue("@id_ids", Id_ids);
                            command1.Parameters.AddWithValue("@id_result", id_res);
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
                            command1.Parameters.AddWithValue("@id_result", id_res);
                            command1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public static void InsertAdress(DataResult dataResults, int id_res)
        {
            using (SqlCommand command = new SqlCommand("InsertAddress", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.addresses)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
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
        public static void InsertAlt_name(DataResult dataResults, int id_res)
        {

            using (SqlCommand command = new SqlCommand("InsertAlt_names", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.alt_names)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
                    command.Parameters.AddWithValue("@alt_name", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertProgram(DataResult dataResults, int id_res)
        {

            using (SqlCommand command = new SqlCommand("InsertProgram", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.programs)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
                    command.Parameters.AddWithValue("@program", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertDates_of_birth(DataResult dataResults, int id_res)
        {

            using (SqlCommand command = new SqlCommand("InsertDate_of_birth", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.dates_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
                    command.Parameters.AddWithValue("@date_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertPlaces_of_birth(DataResult dataResults, int id_res)
        {
            using (SqlCommand command = new SqlCommand("InsertPlaces_of_birth", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.places_of_birth)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
                    command.Parameters.AddWithValue("@place_of_birth", data);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void InsertNationalities(DataResult dataResults, int id_res)
        {

            using (SqlCommand command = new SqlCommand("InsertNationalities", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var data in dataResults.nationalities)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_result", id_res);
                    command.Parameters.AddWithValue("@nationality", data);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteData()
        {
            using (SqlCommand command = new SqlCommand("DeleteAllData", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }
        public static List<DataResult> SelectAll()
        {
            return SelectData("SELECT * FROM dbo.GetAll()");
            
        }
        public static List<DataResult> SelectAddres()
        {
            return SelectData("SELECT * FROM dbo.GetAddress()");   
        }

        public static DataResult SelectResult(string id)
        {
            string sqlQuery = "SELECT * FROM dbo.GetResultsForId(@id)";

            using (SqlCommand command = new SqlCommand(sqlQuery, DataBase.GetConnection()))
            {
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                DataResult page1 = new DataResult();
                while (dataReader.Read())
                {
                        page1.id = id;
                        page1.remarks = Convert.ToString(dataReader["remarks"]);
                        page1.name = Convert.ToString(dataReader["name"]);
                        page1.type = Convert.ToString(dataReader[4]);
                        page1.entity_number = dataReader["entity_number"] != DBNull.Value ? Convert.ToInt64(dataReader["entity_number"]) : (long?)null;
                        page1.source = Convert.ToString(dataReader["source"]);
                        page1.source_information_url = Convert.ToString(dataReader["source_information_url"]);
                        page1.source_list_url = Convert.ToString(dataReader["source_list_url"]);
                        page1.call_sign = Convert.ToString(dataReader["call_sign"]);
                        page1.end_date = dataReader["end_date"] != DBNull.Value ? Convert.ToDateTime(dataReader["end_date"]) : (DateTime?)null;
                        page1.federal_register_notice = Convert.ToString(dataReader["federal_register_notice"]);
                        page1.gross_registered_tonnage = Convert.ToString(dataReader["gross_registered_tonnage"]);
                        page1.gross_tonnage = Convert.ToString(dataReader["gross_tonnage"]);
                        page1.license_policy = Convert.ToString(dataReader["license_policy"]);
                        page1.license_requirement = Convert.ToString(dataReader["license_requirement"]);
                        page1.standard_order = Convert.ToString(dataReader["standard_order"]);
                        page1.start_date = dataReader["start_date"] != DBNull.Value ? Convert.ToDateTime(dataReader["start_date"]) : (DateTime?)null;
                        page1.title = Convert.ToString(dataReader["title"]);
                        page1.vessel_flag = Convert.ToString(dataReader["vessel_flag"]);
                        page1.vessel_owner = Convert.ToString(dataReader["vessel_owner"]);
                        page1.vessel_type = Convert.ToString(dataReader["vessel_type"]);

                    DataAddresses addressData = new DataAddresses();
                    addressData.address = Convert.ToString(dataReader["address"]);
                    addressData.city = Convert.ToString(dataReader["city"]);
                    addressData.state = Convert.ToString(dataReader["state"]);
                    addressData.postal_code = Convert.ToString(dataReader["postal_code"]);
                    addressData.country = Convert.ToString(dataReader[26]);

                    DataIDS dataIDS = new DataIDS();
                    dataIDS.type = Convert.ToString(dataReader[32]);
                    dataIDS.number = Convert.ToString(dataReader["number"]);
                    dataIDS.country = Convert.ToString(dataReader[34]);

                    string alt_name = Convert.ToString(dataReader["alt_name"]);
                    string program = Convert.ToString(dataReader["program"]);
                    string date_of_birth = Convert.ToString(dataReader["date_of_birth"]);
                    string place_of_birth = Convert.ToString(dataReader["place_of_birth"]);
                    string nationality = Convert.ToString(dataReader["nationality"]);

                    if (page1.addresses == null)
                    {
                        page1.addresses = new List<DataAddresses>();
                    }
                    if (page1.ids == null)
                    {
                        page1.ids = new List<DataIDS>();
                    }
                    if (page1.alt_names == null)
                    {
                        page1.alt_names = new List<string>();
                    }
                    if (page1.programs == null)
                    {
                        page1.programs = new List<string>();
                    }
                    if (page1.dates_of_birth == null)
                    {
                        page1.dates_of_birth = new List<string>();
                    }
                    if (page1.places_of_birth == null)
                    {
                        page1.places_of_birth = new List<string>();
                    }
                    if (page1.nationalities == null)
                    {
                        page1.nationalities = new List<string>();
                    }
                    if (!page1.programs.Contains(program))  page1.programs.Add(program);
                    if (!page1.alt_names.Contains(alt_name)) page1.alt_names.Add(alt_name);
                    if (!page1.addresses.Contains(addressData)) page1.addresses.Add(addressData);
                    if (!page1.ids.Contains(dataIDS)) page1.ids.Add(dataIDS);
                    if (!page1.dates_of_birth.Contains(date_of_birth)) page1.dates_of_birth.Add(date_of_birth);
                    if (!page1.places_of_birth.Contains(place_of_birth)) page1.places_of_birth.Add(place_of_birth);
                    if (!page1.nationalities.Contains(nationality)) page1.nationalities.Add(nationality);
                }


                dataReader.Close();
                return page1;
            }
        }
        public static List<DataResult> SelectEndDate() {
            return SelectData("SELECT * FROM dbo.GetEndD()");     
        }
        public static List<DataResult> SelectLicense()
        {
            return SelectData("SELECT * FROM dbo.GetLicense()");
            
        }
        public static List<DataResult> SelectBirth()
        {
            return SelectData("SELECT * FROM dbo.GetDatesOfB()");
        }
        public static List<DataResult> SelectProgram()
        {
            return SelectData("SELECT * FROM dbo.GetProgram()");
            
        }
        public static List<DataResult> SelectIDS()
        {
            return SelectData("SELECT * FROM dbo.GetIDS()");
            
        }
        public static List<DataResult> SelectData(string com)
        {
            using (SqlCommand command = new SqlCommand(com, DataBase.GetConnection()))
            {
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dataReader = command.ExecuteReader();
                Dictionary<string, DataResult> resultDict = new Dictionary<string, DataResult>();
                while (dataReader.Read())
                {
                    string id = Convert.ToString(dataReader["id"]);
                    if (!resultDict.ContainsKey(id))
                    {
                        DataResult page1 = new DataResult();
                        page1.id = id;
                        page1.remarks = Convert.ToString(dataReader["remarks"]);
                        page1.name = Convert.ToString(dataReader["name"]);
                        page1.type = Convert.ToString(dataReader[4]);
                        page1.entity_number = dataReader["entity_number"] != DBNull.Value ? Convert.ToInt64(dataReader["entity_number"]) : (long?)null;
                        page1.source = Convert.ToString(dataReader["source"]);
                        page1.source_information_url = Convert.ToString(dataReader["source_information_url"]);
                        page1.source_list_url = Convert.ToString(dataReader["source_list_url"]);
                        page1.call_sign = Convert.ToString(dataReader["call_sign"]);
                        page1.end_date = dataReader["end_date"] != DBNull.Value ? Convert.ToDateTime(dataReader["end_date"]) : (DateTime?)null;
                        page1.federal_register_notice = Convert.ToString(dataReader["federal_register_notice"]);
                        page1.gross_registered_tonnage = Convert.ToString(dataReader["gross_registered_tonnage"]);
                        page1.gross_tonnage = Convert.ToString(dataReader["gross_tonnage"]);
                        page1.license_policy = Convert.ToString(dataReader["license_policy"]);
                        page1.license_requirement = Convert.ToString(dataReader["license_requirement"]);
                        page1.standard_order = Convert.ToString(dataReader["standard_order"]);
                        page1.start_date = dataReader["start_date"] != DBNull.Value ? Convert.ToDateTime(dataReader["start_date"]) : (DateTime?)null;
                        page1.title = Convert.ToString(dataReader["title"]);
                        page1.vessel_flag = Convert.ToString(dataReader["vessel_flag"]);
                        page1.vessel_owner = Convert.ToString(dataReader["vessel_owner"]);
                        page1.vessel_type = Convert.ToString(dataReader["vessel_type"]);
                        resultDict.Add(id, page1);
                    }
                    DataResult currentResult = resultDict[id];

                    DataAddresses address = new DataAddresses();
                    address.address = Convert.ToString(dataReader["address"]);
                    address.city = Convert.ToString(dataReader["city"]);
                    address.state = Convert.ToString(dataReader["state"]);
                    address.postal_code = Convert.ToString(dataReader["postal_code"]);
                    address.country = Convert.ToString(dataReader[26]);

                    DataIDS dataIDS = new DataIDS();
                    dataIDS.type = Convert.ToString(dataReader[32]);
                    dataIDS.number = Convert.ToString(dataReader["number"]);
                    dataIDS.country = Convert.ToString(dataReader[34]);

                    string alt_name = Convert.ToString(dataReader["alt_name"]);
                    string program = Convert.ToString(dataReader["program"]);
                    string date_of_birth = Convert.ToString(dataReader["date_of_birth"]);
                    string place_of_birth = Convert.ToString(dataReader["place_of_birth"]);
                    string nationality = Convert.ToString(dataReader["nationality"]);

                    if (currentResult.addresses == null)
                    {
                        currentResult.addresses = new List<DataAddresses>();
                    }
                    if (currentResult.ids == null)
                    {
                        currentResult.ids = new List<DataIDS>();
                    }
                    if (currentResult.alt_names == null)
                    {
                        currentResult.alt_names = new List<string>();
                    }
                    if (currentResult.programs == null)
                    {
                        currentResult.programs = new List<string>();
                    }
                    if (currentResult.dates_of_birth == null)
                    {
                        currentResult.dates_of_birth = new List<string>();
                    }
                    if (currentResult.places_of_birth == null)
                    {
                        currentResult.places_of_birth = new List<string>();
                    }
                    if (currentResult.nationalities == null)
                    {
                        currentResult.nationalities = new List<string>();
                    }
                    if (!currentResult.programs.Contains(program)) currentResult.programs.Add(program);
                    if (!currentResult.alt_names.Contains(alt_name)) currentResult.alt_names.Add(alt_name);
                    if (!currentResult.addresses.Contains(address)) currentResult.addresses.Add(address);
                    if (!currentResult.ids.Contains(dataIDS)) currentResult.ids.Add(dataIDS);
                    if (!currentResult.dates_of_birth.Contains(date_of_birth)) currentResult.dates_of_birth.Add(date_of_birth);
                    if (!currentResult.places_of_birth.Contains(place_of_birth)) currentResult.places_of_birth.Add(place_of_birth);
                    if (!currentResult.nationalities.Contains(nationality)) currentResult.nationalities.Add(nationality);
                }

                List<DataResult> resultList = resultDict.Values.ToList();

                dataReader.Close();
                return resultList;
            }
        }

    }
}
