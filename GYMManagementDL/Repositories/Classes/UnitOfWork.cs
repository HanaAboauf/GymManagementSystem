using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork // I act with unit of work as a my dbcontext 
    {
        private readonly GymManagementDbContext context;
        private readonly Dictionary<Type, object> _repositories = new(); 

        public UnitOfWork(GymManagementDbContext context)
        {
            this.context = context;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);

            if (_repositories.TryGetValue(EntityType, out var Repo)) return (IGenericRepository<TEntity>)Repo;

            var NewRepo=new GenericRepository<TEntity>(context);
            _repositories[EntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
