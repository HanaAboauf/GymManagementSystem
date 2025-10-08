using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        public IEnumerable<Category> GetAllMembers();

        public Category? GetMemberById(int id);

        public int AddMember(Category member);

        public int UpdateMember(Category member);

        public int DeleteMember(int id);
    }
}
