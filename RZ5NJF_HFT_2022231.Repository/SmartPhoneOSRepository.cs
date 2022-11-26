using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Repository
{
    public class SmartPhoneOSRepository : GenericRepository<SmartPhoneOS>, IRepository<SmartPhoneOS>
    {
        public SmartPhoneOSRepository(SmartPhonesDbContext ctx) : base(ctx)
        {
        }

        public override SmartPhoneOS Read(int item_to_read_id)
        {
            return ctx.SmartPhoneOSes.FirstOrDefault(t => t.SmartPhoneOSID == item_to_read_id);
        }

        public override void Update(SmartPhoneOS item_to_update)
        {
            var old_os = Read(item_to_update.SmartPhoneOSID);

            foreach (var property in old_os.GetType().GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(x => x.IsVirtual) == null)
                {
                    property.SetValue(old_os, property.GetValue(item_to_update));
                }
            }

            ctx.SaveChanges();
        }
    }
}
