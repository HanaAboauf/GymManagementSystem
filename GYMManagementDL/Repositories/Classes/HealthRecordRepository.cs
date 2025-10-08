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
    internal class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymManagementDbContext _context;

        public HealthRecordRepository(GymManagementDbContext context)
        {
           
           _context = context;
        }

       

        public int AddHealthRecord(HealthRecord member)
        {
            _context.HealthRecords.Add(member);
            return _context.SaveChanges();
        }

        public int DeleteHealthRecord(int id)
        {
            var member = _context.HealthRecords.Find(id);
            if(member is null) return 0;
            _context.HealthRecords.Remove(member);
            return _context.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAllHealthRecords() => _context.HealthRecords.ToList();
        public HealthRecord? GetHealthRecordById(int id) => _context.HealthRecords.Find(id);

        public int UpdateHealthRecord(HealthRecord member)
        {
            _context.HealthRecords.Update(member);
            return _context.SaveChanges();

        }
    }
}
