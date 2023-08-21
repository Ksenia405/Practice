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
            WorkWihtData fun = new WorkWihtData();



            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(300);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["JsonFileURL"]);

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    WorkWihtData d = new WorkWihtData();
                   DataResultWrapper wrapper = JsonConvert.DeserializeObject<DataResultWrapper>(jsonString);
                  List<DataResult> dataResults = wrapper.results;
                    DataBase.openConnection();
                    d.DeleteLastData("Delete_XDate_Birth");
                    d.DeleteLastData("Delete_XAddress");
                    d.DeleteLastData("Delete_XDate");
                    d.DeleteLastData("Delete_XIDS");
                    d.DeleteLastData("Delete_XProg");
                    d.DeleteLastData("Delete_XLisence");
                    DataBase.DeleteData();
                    for (int i = 0; i < dataResults.Count; i++)
                         // for(int i=0; i<5000; i++)
                          {
                              var id_res=DataBase.InsertResult(dataResults[i]);

                              if (dataResults[i].ids != null)  DataBase.InsertIdsList(dataResults[i], id_res);
                              if (dataResults[i].addresses != null)  DataBase.InsertAdress(dataResults[i], id_res);
                              if (dataResults[i].alt_names != null) DataBase.InsertAlt_name(dataResults[i], id_res);
                              if (dataResults[i].programs != null) DataBase.InsertProgram(dataResults[i], id_res);
                              if (dataResults[i].dates_of_birth != null) DataBase.InsertDates_of_birth(dataResults[i], id_res);
                              if (dataResults[i].places_of_birth != null) DataBase.InsertPlaces_of_birth(dataResults[i], id_res);
                              if (dataResults[i].nationalities != null) DataBase.InsertNationalities(dataResults[i], id_res);
                          }

                          
               }
                      else
                      {
                          Console.WriteLine("Не удалось получить данные. Код ошибки: " + response.StatusCode);
                      }
                }
            string country = "RU";
            bool include_ids = false;
            bool include_lic = false;
            bool include_prog = false;
            string type_ids = "ISIN";
            string type_prog = "E.O. 13382";
            Console.WriteLine("Thats all");
            fun.Sort_Address(country);
            fun.Sort_Dates();
            fun.Sort_dates_of_birth();
            fun.Sort_ids(include_ids, type_ids);
            fun.Sort_license(include_lic);
            fun.Sort_program(include_prog, type_prog);
            DataBase.closeConnection();

        }
    }
}
