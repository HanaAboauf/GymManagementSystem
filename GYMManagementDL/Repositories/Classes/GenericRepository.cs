using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        
        private readonly GymManagementDbContext _context;

        public GenericRepository(GymManagementDbContext dbcontext)
        {
            _context = dbcontext;
        }
   

        public int Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return _context.SaveChanges();

        }

        public int DeleteMember(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
          if(condition is null)
                return _context.Set<TEntity>().ToList();
            return _context.Set<TEntity>().Where(condition).ToList();

        }

        public TEntity? GetById(int id)=> _context.Set<TEntity>().Find(id);

        public int Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
