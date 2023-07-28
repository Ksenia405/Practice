using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DataCompany
{
    class Program
    {
        


        public static async Task Main()
        {

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["JsonFileURL"]);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        DataResultWrapper wrapper = JsonConvert.DeserializeObject<DataResultWrapper>(jsonString);  
                        List<DataResult> dataResults = wrapper.results;
                        DataBase.openConnection();


                        for (int i = 0; i < dataResults.Count; i++)
                        //for(int i=0; i<3000; i++)
                        {
                            DataBase.InsertResult(dataResults[i]);
                            if (dataResults[i].ids != null)  DataBase.InsertIdsList(dataResults[i]);
                            if (dataResults[i].addresses != null)  DataBase.InsertAdress(dataResults[i]);
                            if (dataResults[i].alt_names != null) DataBase.InsertAlt_name(dataResults[i]);
                            if (dataResults[i].programs != null) DataBase.InsertProgram(dataResults[i]);
                            if (dataResults[i].places_of_birth != null) DataBase.InsertDates_of_birth(dataResults[i]);
                            if (dataResults[i].places_of_birth != null) DataBase.InsertPlaces_of_birth(dataResults[i]);
                            if (dataResults[i].nationalities != null) DataBase.InsertNationalities(dataResults[i]);
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
