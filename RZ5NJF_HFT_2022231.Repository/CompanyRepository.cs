using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Repository
{
    internal class CompanyRepository : GenericRepository<Company>, IRepository<Company>
    {
        public CompanyRepository(SmartPhonesDbContext ctx) : base(ctx)
        {
        }

        public override Company Read(int item_to_read_id)
        {
            return ctx.Companies.FirstOrDefault(t => t.CompanyID == item_to_read_id);
        }

        public override void Update(Company item_to_update)
        {
            var old_company = Read(item_to_update.CompanyID);

            foreach (var property in old_company.GetType().GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
                {
                    property.SetValue(old_company, property.GetValue(item_to_update));
                }
            }
            ctx.SaveChanges();
        }
    }
}
