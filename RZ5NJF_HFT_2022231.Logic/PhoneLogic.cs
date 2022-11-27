using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public class PhoneLogic : IPhoneLogic
    {
        IRepository<Phone> repo;

        public PhoneLogic(IRepository<Phone> repo)
        {
            this.repo = repo;
        }

        #region CRUD Methods
        public void Create(Phone phone_to_create)
        {
            if (phone_to_create.Name.Length < 2 || phone_to_create.BatterySize < 500)
            {
                throw new ArgumentException("Phone name is too short");
            }
            this.repo.Create(phone_to_create);
        }

        public void Delete(int phone_to_delete_id)
        {
            this.repo.Delete(phone_to_delete_id);
        }

        public Phone Read(int phone_to_read_id)
        {
            var phone = this.repo.Read(phone_to_read_id);

            if (phone == null)
            {
                throw new ArgumentException("Phone does not exist");
            }

            return phone;
        }

        public IQueryable<Phone> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Phone phone_to_update)
        {
            this.repo.Update(phone_to_update);
        }
        #endregion

        #region non-CRUD Methods
        public IEnumerable<Phone> SupportedApple()
        {
            return this.repo.ReadAll().Where(t => t.Company.Name.ToLower().Contains("apple") && t.SmartPhoneOS.IsSupported);
        }

        public Phone OldestWirelessSamsung()
        {
            return this.repo.ReadAll().OrderBy(t => t.ReleaseDate).FirstOrDefault(t => t.WirelessCharging && t.Company.Name.ToLower().Contains("samsung"));
        }

        public Company MostpplAndroidMaker()
        {
            return this.repo.ReadAll().Where(t => t.SmartPhoneOS.Name.ToLower().Contains("android")).OrderByDescending(t=>t.Company.NumberOfEmployees).First().Company;
        }

        public IEnumerable<SmartPhoneOS> LargeBatteryOS()
        {
            return this.repo.ReadAll().Where(t => t.BatterySize >= 4000).Select(t => t.SmartPhoneOS).Distinct();
        }
        #endregion
    }
}
