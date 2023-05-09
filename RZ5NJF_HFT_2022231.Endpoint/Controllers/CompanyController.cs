using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using RZ5NJF_HFT_2022231.Endpoint.Services;
using RZ5NJF_HFT_2022231.Logic.Interface;
using RZ5NJF_HFT_2022231.Models;
using System.Collections.Generic;

namespace RZ5NJF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyLogic companyLogic;
        IHubContext<SignalRHub> hub;

        public CompanyController(ICompanyLogic companyLogic, IHubContext<SignalRHub> hub)
        {
            this.companyLogic = companyLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("CompanyCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Company value)
        {
            this.companyLogic.Update(value);
            this.hub.Clients.All.SendAsync("CompanyUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var companyToDelete = this.companyLogic.Read(id);
            this.companyLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CompanyDeleted", companyToDelete);
        }
    }
}
