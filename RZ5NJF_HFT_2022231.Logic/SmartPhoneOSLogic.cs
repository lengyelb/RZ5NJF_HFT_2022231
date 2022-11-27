using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public class SmartPhoneOSLogic : ISmartPhoneOSLogic
    {
        IRepository<SmartPhoneOS> repo;

        public SmartPhoneOSLogic(IRepository<SmartPhoneOS> repo)
        {
            this.repo = repo;
        }

        #region CRUD Methods
        public void Create(SmartPhoneOS os_to_create)
        {
            if (os_to_create.Name.Length < 2 || os_to_create.ReleaseDate < new DateTime(1990, 1, 1))
            {
                throw new ArgumentException("Opaerating system name is too short");
            }
            this.repo.Create(os_to_create);
        }

        public void Delete(int os_to_delete_id)
        {
            this.repo.Delete(os_to_delete_id);
        }

        public SmartPhoneOS Read(int os_to_read_id)
        {
            var Operating_system = this.repo.Read(os_to_read_id);

            if (Operating_system == null)
            {
                throw new ArgumentException("Operating system does not exist");
            }

            return Operating_system;
        }

        public IQueryable<SmartPhoneOS> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(SmartPhoneOS os_to_update)
        {
            this.repo.Update(os_to_update);
        }
        #endregion

        #region non-CRUD Methods
        public IEnumerable<Company> LatestOsCompany()
        {
            return this.repo.ReadAll().OrderByDescending(t => t.ReleaseDate).First().Phones.Select(t => t.Company);
        }
        #endregion
    }
}
