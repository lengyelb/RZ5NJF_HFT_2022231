using RZ5NJF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Logic
{
    public interface IPhoneLogic
    {
        void Create(Phone phone_to_create);
        void Delete(int phone_to_delete_id);
        Phone Read(int phone_to_read_id);
        IQueryable<Phone> ReadAll();
        void Update(Phone phone_to_update);
    }
}
