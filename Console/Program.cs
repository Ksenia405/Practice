using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataCompany
{
    class Program
    {
        public static async Task Main()
        {

                using (HttpClient client = new HttpClient())
                {
                    string url = "https://data.trade.gov/downloadable_consolidated_screening_list/v1/consolidated.json";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        //List<DataResult> dataResults = JsonConvert.DeserializeObject<List<DataResult>>(jsonString);
                        //DataResult data = JsonConvert.DeserializeObject<DataResult>(jsonString);

                        /*for (int i=0; i<dataList.Count; i++)
                        {
                            Console.WriteLine(dataList[i]);
                        }*/
                        //Console.WriteLine(data);
                        DataResultWrapper wrapper = JsonConvert.DeserializeObject<DataResultWrapper>(jsonString);
                        /*while parsing value: [. Path 'results[4146].places_of_birth', line 1, position 4016829.*/
                        List<DataResult> dataResults = wrapper.results;
                        DataBase.openConnection();




                        int countA = 0;
                        int countAlt = 0;
                        int countP = 0;
                        int countDB = 0;
                        int countPB = 0;
                        int countN = 0;
                        int countIDS = 0;
                        int countL = 0;
                        for (int i = 0; i < dataResults.Count; i++)
                        {

                            {
                                string insertQuery = "INSERT INTO results (id_record, remarks, name, type, entity_number, source, source_information_url," +
                                    "source_list_url, call_sign, end_date, federal_register_notice, gross_registered_tonnage, gross_tonnage, license_policy, license_requirement, " +
                                    "standard_order, start_date, title, vessel_flag, vessel_owner, vessel_type, id) VALUES (@id_record, @remarks, @name, @type, @entity_number, @source, @source_information_url," +
                                    "@source_list_url, @call_sign, @end_date, @federal_register_notice, @gross_registered_tonnage, @gross_tonnage, @license_policy, @license_requirement," +
                                    "@standard_order, @start_date, @title, @vessel_flag, @vessel_owner, @vessel_type, @id)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@id_record", i);
                                    command.Parameters.AddWithValue("@remarks", dataResults[i].remarks ?? "");
                                    command.Parameters.AddWithValue("@name", dataResults[i].name ?? "");
                                    command.Parameters.AddWithValue("@type", dataResults[i].type ?? "");
                                    if(dataResults[i].entity_number is not null) command.Parameters.AddWithValue("@entity_number", dataResults[i].entity_number );
                                    else command.Parameters.AddWithValue("@entity_number", "");
                                command.Parameters.AddWithValue("@source", dataResults[i].source ?? "");
                                    command.Parameters.AddWithValue("@source_information_url", dataResults[i].source_information_url ?? "");
                                    command.Parameters.AddWithValue("@source_list_url", dataResults[i].source_list_url);
                                    command.Parameters.AddWithValue("@call_sign", dataResults[i].call_sign ?? "");
                                    if (dataResults[i].end_date.HasValue) command.Parameters.AddWithValue("@end_date", dataResults[i].end_date);
                                    else command.Parameters.AddWithValue("@end_date", "");
                                    command.Parameters.AddWithValue("@federal_register_notice", dataResults[i].federal_register_notice ?? "");
                                    command.Parameters.AddWithValue("@gross_registered_tonnage", dataResults[i].gross_registered_tonnage ?? "");
                                    command.Parameters.AddWithValue("@gross_tonnage", dataResults[i].gross_tonnage ?? "");
                                    command.Parameters.AddWithValue("@license_policy", dataResults[i].license_policy ?? "");
                                    command.Parameters.AddWithValue("@license_requirement", dataResults[i].license_requirement ?? "");

                                    command.Parameters.AddWithValue("@standard_order", dataResults[i].standard_order ?? "");
                                    if (dataResults[i].start_date.HasValue) command.Parameters.AddWithValue("@start_date", dataResults[i].start_date);
                                    else command.Parameters.AddWithValue("@start_date", "");
                                    command.Parameters.AddWithValue("@title", dataResults[i].title ?? "");
                                    command.Parameters.AddWithValue("@vessel_flag", dataResults[i].vessel_flag ?? "");
                                    command.Parameters.AddWithValue("@vessel_owner", dataResults[i].vessel_owner ?? "");
                                    command.Parameters.AddWithValue("@vessel_type", dataResults[i].vessel_type ?? "");
                                    command.Parameters.AddWithValue("@id", dataResults[i].id);


                                    command.ExecuteNonQuery();

                                }

                            }
                            if (dataResults[i].ids != null)
                            {
                                string insertQuery = "INSERT INTO ids (id, type, number, country) VALUES (@id,  @type, @number, @country)";
                                string insertQuerylist;
                                string selectQuery = "SELECT id FROM ids WHERE CAST(type AS nvarchar(max))=@type AND CAST(number AS nvarchar(max))=@number";

                                using (SqlCommand command = new SqlCommand(selectQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].ids)
                                    {
                                    command.CommandText = selectQuery;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@type", data.type ?? "");
                                        command.Parameters.AddWithValue("@number", data.number ?? "");
                                        command.Parameters.AddWithValue("@country", data.country ?? "");
                                    object result = command.ExecuteScalar();

                                    if (result == null)
                                        {
                                            command.CommandText = insertQuery;
                                            command.Parameters.AddWithValue("@id", countIDS);
                                            command.ExecuteNonQuery();

                                            insertQuerylist = "INSERT INTO list (id, id_ids, id_result) VALUES (@id,  @id_ids, @id_result)";
                                            using (SqlCommand command1 = new SqlCommand(insertQuerylist, DataBase.GetConnection()))
                                            {
                                                command1.Parameters.Clear();
                                                command1.Parameters.AddWithValue("@id", countL);
                                                command1.Parameters.AddWithValue("@id_ids", countIDS);
                                                command1.Parameters.AddWithValue("@id_result", i);
                                                command1.ExecuteNonQuery();
                                            }
                                            countIDS++;
                                        }
                                        else
                                        {
                                            int id_for_list;
                                            SqlDataReader dataReader1 = command.ExecuteReader();
                                            dataReader1.Read();
                                            id_for_list = Convert.ToInt32(dataReader1["id"]);
                                            dataReader1.Close();
                                            insertQuerylist = "INSERT INTO list (id, id_ids, id_result) VALUES (@id,  @id_ids, @id_result)";
                                            using (SqlCommand command1 = new SqlCommand(insertQuerylist, DataBase.GetConnection()))
                                            {
                                                command1.Parameters.Clear();
                                                command1.Parameters.AddWithValue("@id", countL);
                                                command1.Parameters.AddWithValue("@id_ids", id_for_list);
                                                command1.Parameters.AddWithValue("@id_result", i);
                                                command1.ExecuteNonQuery();
                                            }
                                        }
                                        countL++;
                                    }
                                }
                            }
                            if (dataResults[i].addresses != null)
                            {
                                string insertQuery = "INSERT INTO addresses (id, id_result, address, city, state, postal_code, country) VALUES (@id, @id_result, @address, @city, @state, @postal_code, @country)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].addresses)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countA);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@address", data.address ?? "");
                                        command.Parameters.AddWithValue("@city", data.city ?? "");
                                        command.Parameters.AddWithValue("@state", data.state ?? "");
                                        command.Parameters.AddWithValue("@postal_code", data.postal_code ?? "");
                                        command.Parameters.AddWithValue("@country", data.country ?? "");
                                        command.ExecuteNonQuery();
                                        countA++;
                                    }
                                }
                            }
                            if (dataResults[i].alt_names != null)
                            {
                                string insertQuery = "INSERT INTO alt_names (id, id_result, alt_name) VALUES (@id, @id_result, @alt_name)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].alt_names)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countAlt);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@alt_name", data);
                                        command.ExecuteNonQuery();
                                        countAlt++;
                                    }
                                }
                            }
                            if (dataResults[i].programs != null)
                            {
                                string insertQuery = "INSERT INTO programs (id, id_result, program) VALUES (@id, @id_result, @program)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].programs)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countP);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@program", data);
                                        command.ExecuteNonQuery();
                                        countP++;
                                    }
                                }
                            }
                            if (dataResults[i].dates_of_birth != null)
                            {
                                string insertQuery = "INSERT INTO dates_of_birth (id, id_result, date_of_birth) VALUES (@id, @id_result, @date_of_birth)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].dates_of_birth)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countDB);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@date_of_birth", data);
                                        command.ExecuteNonQuery();
                                        countDB++;
                                    }
                                }
                            }
                            if (dataResults[i].places_of_birth != null)
                            {
                                string insertQuery = "INSERT INTO places_of_birth (id, id_result, place_of_birth) VALUES (@id, @id_result, @place_of_birth)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].places_of_birth)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countPB);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@place_of_birth", data);
                                        command.ExecuteNonQuery();
                                        countPB++;
                                    }
                                }
                            }
                            if (dataResults[i].nationalities != null)
                            {
                                string insertQuery = "INSERT INTO nationalities (id, id_result, nationality) VALUES (@id, @id_result, @nationality)";

                                using (SqlCommand command = new SqlCommand(insertQuery, DataBase.GetConnection()))
                                {
                                    foreach (var data in dataResults[i].nationalities)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@id", countN);
                                        command.Parameters.AddWithValue("@id_result", i);
                                        command.Parameters.AddWithValue("@nationality", data);
                                        command.ExecuteNonQuery();
                                        countN++;
                                    }
                                }
                            }


                        }

                        DataBase.closeConnection();
                    }
                    else
                    {
                        Console.WriteLine("Не удалось получить данные. Код ошибки: " + response.StatusCode);
                    }
                }

            Console.WriteLine("Thats all");
        }
    }
}
