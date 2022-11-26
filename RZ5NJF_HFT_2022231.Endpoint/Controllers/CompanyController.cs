using Microsoft.AspNetCore.Mvc;
using RZ5NJF_HFT_2022231.Logic;
using RZ5NJF_HFT_2022231.Models;
using System.Collections.Generic;

namespace RZ5NJF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController
    {
        ICompanyLogic companyLogic;

        public CompanyController(ICompanyLogic companyLogic)
        {
            this.companyLogic = companyLogic;
        }

        [HttpGet]
        public IEnumerable<Company> ReadAll()
        {
            return this.companyLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Company Read(int id)
        {
            return this.companyLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Company value)
        {
            this.companyLogic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Company value)
        {
            this.companyLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.companyLogic.Delete(id);
        }
    }
}
