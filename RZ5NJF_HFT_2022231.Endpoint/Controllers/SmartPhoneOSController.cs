using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RZ5NJF_HFT_2022231.Endpoint.Services;
using RZ5NJF_HFT_2022231.Logic.Interface;
using RZ5NJF_HFT_2022231.Models;
using System.Collections.Generic;

namespace RZ5NJF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SmartPhoneOSController : ControllerBase
    {
        ISmartPhoneOSLogic smartPhoneOSLogic;
        IHubContext<SignalRHub> hub;

        public SmartPhoneOSController(ISmartPhoneOSLogic smartPhoneOSLogic, IHubContext<SignalRHub> hub)
        {
            this.smartPhoneOSLogic = smartPhoneOSLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<SmartPhoneOS> ReadAll()
        {
            return this.smartPhoneOSLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public SmartPhoneOS Read(int id)
        {
            return this.smartPhoneOSLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] SmartPhoneOS value)
        {
            this.smartPhoneOSLogic.Create(value);
            this.hub.Clients.All.SendAsync("SmartPhoneOSCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] SmartPhoneOS value)
        {
            this.smartPhoneOSLogic.Update(value);
            this.hub.Clients.All.SendAsync("SmartPhoneOSUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var companyToDelete = this.smartPhoneOSLogic.Read(id);
            this.smartPhoneOSLogic.Delete(id);
            this.hub.Clients.All.SendAsync("SmartPhoneOSDeleted", companyToDelete);
        }
    }
}
