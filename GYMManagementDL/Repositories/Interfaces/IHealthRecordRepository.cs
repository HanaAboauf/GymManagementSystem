using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface IHealthRecordRepository
    {
        public IEnumerable<HealthRecord> GetAllHealthRecords();

        public HealthRecord? GetHealthRecordById(int id);

        public int AddHealthRecord(HealthRecord member);

        public int UpdateHealthRecord(HealthRecord member);

        public int DeleteHealthRecord(int id);
    }
}
