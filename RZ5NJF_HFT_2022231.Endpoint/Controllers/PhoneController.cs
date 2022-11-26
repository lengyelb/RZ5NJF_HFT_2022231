using Microsoft.AspNetCore.Mvc;
using RZ5NJF_HFT_2022231.Logic;
using RZ5NJF_HFT_2022231.Models;
using System.Collections.Generic;

namespace RZ5NJF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhoneController
    {
        IPhoneLogic phoneLogic;

        public PhoneController(IPhoneLogic phoneLogic)
        {
            this.phoneLogic = phoneLogic;
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
        }

        [HttpPut]
        public void Update([FromBody] Phone value)
        {
            this.phoneLogic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.phoneLogic.Delete(id);
        }
    }
}
