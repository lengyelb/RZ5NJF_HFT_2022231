using Microsoft.AspNetCore.Mvc;
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

        public SmartPhoneOSController(ISmartPhoneOSLogic smartPhoneOSLogic)
        {
            this.smartPhoneOSLogic = smartPhoneOSLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] SmartPhoneOS value)
        {
            this.smartPhoneOSLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.smartPhoneOSLogic.Delete(id);
        }
    }
}
