using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ5NJF_HFT_2022231.Repository
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected SmartPhonesDbContext ctx;

        public GenericRepository(SmartPhonesDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Create(T item_to_create)
        {
            ctx.Set<T>().Add(item_to_create);
            ctx.SaveChanges();
        }
        public abstract T Read(int item_to_read_id);
        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        public abstract void Update(T item_to_update);
        public void Delete(int item_to_delete_id)
        {
            ctx.Set<T>().Remove(Read(item_to_delete_id));
            ctx.SaveChanges();
        }
    }
}
