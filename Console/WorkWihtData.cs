using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompany
{
   interface ISort_dates_of_birth
    {
        public void Sort_dates_of_birth();
    }
    interface ISort_Address
    {
        public void Sort_Address(string country);
    }
    interface ISort_Dates
    {
        public void Sort_Dates();
    }
    interface ISort_ids
    {
        public void Sort_ids(bool include, string type);
    }
    interface ISort_program
    {
        public void Sort_program(bool include, string type);
    }
    interface ISort_license
    {
        public void Sort_license(bool include);
    }

    interface ILastData
    {
        public void DeleteLastData(string name);
    }

    public class WorkWihtData: ISort_dates_of_birth, ISort_Address, ISort_Dates, ISort_ids, ISort_program, ISort_license, ILastData
    {

        public  void Sort_dates_of_birth() {
            DeleteLastData("Delete_XDate_Birth");
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.SelectStrtDOB()", DataBase.GetConnection()))
                {

                    // command.CommandType = System.Data.CommandType.StoredProcedure;
                    int id_page;
                    string data;
                    Dictionary<int, string> data_dates_of_birth = new Dictionary<int, string>();
                    SqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    while (dataReader.Read())
                    {
                        id_page = Convert.ToInt32(dataReader["id_dates_of_birth"]);
                        data = Convert.ToString(dataReader["date_of_birth"]);
                        data_dates_of_birth.Add(id_page, data);
                    }
                    dataReader.Close();

                    foreach (var dates in data_dates_of_birth)
                    {
                        DateTime f, date_of_birth;
                        int OnlyYear;
                        DateTime.TryParse(dates.Value, out f);
                        int.TryParse(dates.Value, out OnlyYear);
                        if ((f.ToString() is not null) || (OnlyYear != 0))
                        {
                            if (OnlyYear > 1800) date_of_birth = new DateTime(OnlyYear, 1, 1);
                            else date_of_birth = f;
                            if ((DateTime.Today.Year - date_of_birth.Year) >= 5)
                            {
                                using (SqlCommand command1 = new SqlCommand("XSort_dates_of_birth", DataBase.GetConnection()))
                                {
                                    command1.CommandType = System.Data.CommandType.StoredProcedure;
                                    command1.Parameters.Clear();
                                    command1.Parameters.AddWithValue("@id_dates_od_birth", dates.Key);
                                    command1.ExecuteNonQuery();
                                }
                            }

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        public void Sort_Address(string country)
        {
            //string country = "RU";
            DeleteLastData("Delete_XAddress");
            using (SqlCommand command = new SqlCommand("XSort_Address", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@country", country);
                command.ExecuteNonQuery();
            }
        }
        public void Sort_Dates()
        {
            DeleteLastData("Delete_XDate");
            using (SqlCommand command = new SqlCommand("XSort_result_date", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
        }
        public  void Sort_ids(bool include, string type)
        {
            // bool include=true;
            //string type= "ISIN";
            DeleteLastData("Delete_XIDS");
            using (SqlCommand command = new SqlCommand("XSort_result_ids", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@include", include);
                command.ExecuteNonQuery();
            }
        }
        public void Sort_program(bool include, string type)
        {
            //bool include = true;
            //string type = "E.O. 13382";
            DeleteLastData("Delete_XProg");
            using (SqlCommand command = new SqlCommand("XSort_result_programs", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@include", include);
                command.ExecuteNonQuery();
            }

        }
        public  void Sort_license(bool include)
        {
            DeleteLastData("Delete_XLisence");
            using (SqlCommand command = new SqlCommand("XSort_license", DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@include", include);
                command.ExecuteNonQuery();
            }

        }
        public void DeleteLastData(string name)
        {
            using (SqlCommand command = new SqlCommand(name, DataBase.GetConnection()))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
        }
    }
}

