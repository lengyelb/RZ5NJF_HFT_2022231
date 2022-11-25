using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public interface ICompanyLogic
    {
        void Create(Company company_to_create);
        void Delete(int company_to_delete_id);
        Company Read(int company_to_read_id);
        IQueryable<Company> ReadAll();
        void Update(Company company_to_update);
    }
}
