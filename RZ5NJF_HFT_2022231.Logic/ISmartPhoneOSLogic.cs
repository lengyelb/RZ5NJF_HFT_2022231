using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public interface ISmartPhoneOSLogic
    {
        void Create(SmartPhoneOS os_to_create);
        void Delete(int os_to_delete_id);
        SmartPhoneOS Read(int os_to_read_id);
        IQueryable<SmartPhoneOS> ReadAll();
        void Update(SmartPhoneOS os_to_update);
    }
}
