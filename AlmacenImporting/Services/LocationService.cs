using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlmacenImporting.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using AlmacenImporting.ViewModels;

namespace AlmacenImporting.Services
{
    public class LocationService : ServiceBase<Locations>
    {
        public async Task<Locations> Get(int id)
        {
            return await dbset.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Locations>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<int> Update(Locations entity)
        {
            try
            {
                dbset.Attach(entity);
                DBContext.Entry(entity).State = EntityState.Modified;

                return await Save();
            }
            catch
            {
                DBContext.Entry(entity).State = EntityState.Detached;
                throw;
            }
        }

        public async Task<int> Create(Locations entity)
        {
            try
            {
                dbset.Add(entity);
                return await Save();
            }
            catch
            {
                DBContext.Entry(entity).State = EntityState.Detached;
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            var entity = await Get(id);
            dbset.Remove(entity);
            return await Save();
        }

        public async Task<int> Save()
        {
            return await DBContext.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Products>> Teachers()
        //{
        //    var teacherList = (await GetAll()).Where(t => t.CompanyRole == CompanyRole.Teacher);
        //    return (teacherList);
        //}
    }
}