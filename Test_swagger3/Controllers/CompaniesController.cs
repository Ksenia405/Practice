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
        public IEnumerable<DataResult> Get()
        {
            return DataCompany.DataBase.SelectAll();
        }

        [HttpGet("{country}")]
        public IEnumerable<DataResult> GetCountry(string country)
        {
            WorkWihtData fun = new WorkWihtData();
            fun.Sort_Address(country);
            var result = DataCompany.DataBase.SelectAdres();
            return result;
        }
        [HttpGet("{id}")]
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

    }
}
