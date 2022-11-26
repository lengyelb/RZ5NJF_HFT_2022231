using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Repository
{
    public class PhoneRepository : GenericRepository<Phone>, IRepository<Phone>
    {
        public PhoneRepository(SmartPhonesDbContext ctx) : base(ctx)
        {
        }

        public override Phone Read(int item_to_read_id)
        {
            return ctx.Phones.FirstOrDefault(t => t.PhoneID == item_to_read_id);
        }

        public override void Update(Phone item_to_update)
        {
            var old_phone = Read(item_to_update.PhoneID);

            foreach (var property in old_phone.GetType().GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
                {
                    property.SetValue(old_phone, property.GetValue(item_to_update));
                }
            }

            ctx.SaveChanges();
        }
    }
}
