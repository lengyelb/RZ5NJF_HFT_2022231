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
    public class PhoneController : ControllerBase
    {
        IPhoneLogic phoneLogic;
        IHubContext<SignalRHub> hub;

        public PhoneController(IPhoneLogic phoneLogic, IHubContext<SignalRHub> hub)
        {
            this.phoneLogic = phoneLogic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Phone> ReadAll()
        {
            return this.phoneLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Phone Read(int id)
        {
            return this.phoneLogic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Phone value)
        {
            this.phoneLogic.Create(value);
            this.hub.Clients.All.SendAsync("PhoneCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Phone value)
        {
            this.phoneLogic.Update(value);
            this.hub.Clients.All.SendAsync("PhoneUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var phoneToDelete = this.phoneLogic.Read(id);
            this.phoneLogic.Delete(id);
            this.hub.Clients.All.SendAsync("PhoneDeleted", phoneToDelete);
        }
    }
}
