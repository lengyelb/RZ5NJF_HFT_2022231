using RZ5NJF_HFT_2022231.Models;
using RZ5NJF_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public class CompanyLogic : ICompanyLogic
    {
        IRepository<Company> repo;

        public CompanyLogic(IRepository<Company> repo)
        {
            this.repo = repo;
        }

        #region CRUD Methods
        public void Create(Company company_to_create)
        {
            if (company_to_create.Name.Length < 2)
            {
                throw new ArgumentException("Company name is too short");
            }
            this.repo.Create(company_to_create);
        }

        public void Delete(int company_to_delete_id)
        {
            try
            {
                Read(company_to_delete_id);
            }
            catch (ArgumentException ex)
            { 
                throw new ArgumentException("Company to delte does not exist", ex);
            }
            this.repo.Delete(company_to_delete_id);
        }

        public Company Read(int company_to_read_id)
        {
            var company = this.repo.Read(company_to_read_id);

            if (company == null)
            {
                throw new ArgumentException("Company does not exist");
            }

            return company;
        }

        public IQueryable<Company> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Company company_to_update)
        {
            this.repo.Update(company_to_update);
        }
        #endregion

        #region non-CRUD Methods
        public Company MostpplAndroidMaker()
        {
            return this.repo.ReadAll().OrderByDescending(t => t.NumberOfEmployees).FirstOrDefault(t=>t.Phones.Count(p => p.SmartPhoneOS.OSFamily.ToLower().Contains("android")) > 0);
        }

        public SmartPhoneOS SmallestCompanyLatestOS()
        {
            return this.repo.ReadAll().OrderBy(t => t.NetWorth).First().Phones.OrderByDescending(t => t.SmartPhoneOS.ReleaseDate).First().SmartPhoneOS;
        }
        #endregion
    }
}
