using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompany
{
    public class DataResultWrapper
    {
        public List<DataResult> results { get; set; }
    }
    public class DataResult
    {
        public string id { get; set; }
        public List<string> programs { get; set; }//
        public string remarks { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<string> alt_names { get; set; }//
        public List<DataAddresses> addresses { get; set; }//
        public List<DataIDS> ids { get; set; }
        public long? entity_number { get; set; }
        public string source { get; set; }
        public string source_information_url { get; set; }
        public string source_list_url { get; set; }
        public string call_sign { get; set; }
        public DateTime? end_date { get; set; }
        public string federal_register_notice { get; set; }
        public string gross_registered_tonnage { get; set; }
        public string gross_tonnage { get; set; }
        public string license_policy { get; set; }
        public string license_requirement { get; set; }
        public string standard_order { get; set; }
        public DateTime? start_date { get; set; }
        public string title { get; set; }
        public string vessel_flag { get; set; }
        public string vessel_owner { get; set; }
        public string vessel_type { get; set; }


        public List<string> dates_of_birth { get; set; }
        public List<string> places_of_birth { get; set; }
        public List<string> nationalities { get; set; }
    }

    

    public class DataAddresses
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
    }

    public class DataIDS
    {
        public string type { get; set; }
        public string number { get; set; }
        public string country { get; set; }
    }
}
