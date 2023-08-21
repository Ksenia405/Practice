using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCompany;
using System.Data.SqlClient;

namespace Test_swagger3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : Controller
    {
        [HttpGet]
        public IEnumerable<DataResult> Get(int page)
        {
            List<DataResult> returnList = new List<DataResult>();
            List<DataResult> allData = DataCompany.DataBase.SelectAll();
            if ((200 * (page - 1) + 200) < allData.Count)
            {
                for (int i = 200 * (page - 1); i < page * 200; i++)
                {
                    returnList.Add(allData[i]);
                }
            }
            else
            {
                for (int i = 200 * (page - 1); i < allData.Count; i++)
                {
                    returnList.Add(allData[i]);
                }
            }
            return returnList;
        }

        [HttpGet("country")]
        public IEnumerable<DataResult> GetCountry(string country)
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_Address(country);
            var result = DataCompany.DataBase.SelectAddres();
            return result;
        }
        [HttpGet("id")]
        public DataResult GetRes(string id)
        {
            return DataBase.SelectResult(id);
        }
        [HttpGet("end_date")]
        public IEnumerable<DataResult> GetEndRes()
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_Dates();
            return DataBase.SelectEndDate();
        }
        [HttpGet("license")]
        public IEnumerable<DataResult> GetLisence(bool include)
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_license(include);
            return DataBase.SelectLicense();
        }
        [HttpGet("dates_of_birth")]
        public IEnumerable<DataResult> GetBirthRes()
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_dates_of_birth();
            return DataBase.SelectBirth();
        }

        [HttpGet("program")]
        public IEnumerable<DataResult> GetProgRes(bool include, string type)
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_program(include, type);
            return DataBase.SelectProgram();
        }

        [HttpGet("IDS")]
        public IEnumerable<DataResult> GetIDSRes(bool include, string type)
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_ids(include, type);
            return DataBase.SelectIDS();
        }
    }
}
