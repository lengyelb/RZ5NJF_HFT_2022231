using Microsoft.AspNetCore.Mvc;
using RZ5NJF_HFT_2022231.Logic;
using RZ5NJF_HFT_2022231.Models;
using System.Collections.Generic;

namespace RZ5NJF_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        ICompanyLogic companyLogic;
        IPhoneLogic phoneLogic;
        ISmartPhoneOSLogic smartPhoneOsLogic;

        public NonCrudController(ICompanyLogic companyLogic, IPhoneLogic phoneLogic, ISmartPhoneOSLogic smartPhoneOsLogic)
        {
            this.companyLogic = companyLogic;
            this.phoneLogic = phoneLogic;
            this.smartPhoneOsLogic= smartPhoneOsLogic;
        }

        [HttpGet]
        public IEnumerable<Phone> SupportedApple()
        {
            return this.phoneLogic.SupportedApple();
        }

        [HttpGet]
        public Phone OldestWirelessSamsung()
        {
            return this.phoneLogic.OldestWirelessSamsung();
        }

        [HttpGet]
        public Company MostpplAndroidMaker()
        {
            return this.phoneLogic.MostpplAndroidMaker();
        }

        [HttpGet]
        public IEnumerable<Company> LatestOsCompany()
        {
            return this.smartPhoneOsLogic.LatestOsCompany();
        }

        [HttpGet]
        public SmartPhoneOS SmallestCompanyLatestOS()
        {
            return this.companyLogic.SmallestCompanyLatestOS();
        }

        [HttpGet]
        public IEnumerable<SmartPhoneOS> LargeBatteryOS()
        {
            return this.phoneLogic.LargeBatteryOS();
        }
    }
}
