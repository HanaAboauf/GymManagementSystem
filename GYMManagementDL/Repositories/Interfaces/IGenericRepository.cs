using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);

        public TEntity? GetById(int id);

        public int Add(TEntity entity);

        public int Update(TEntity entity);

        public int DeleteMember(TEntity entity);
    }
}
