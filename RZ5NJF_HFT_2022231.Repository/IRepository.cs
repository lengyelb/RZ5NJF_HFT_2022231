using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T item_to_create);
        T Read(int item_to_read_id);
        IQueryable<T> ReadAll();
        void Update(T item_to_update);
        void Delete(int item_to_delete_id);
    }
}
