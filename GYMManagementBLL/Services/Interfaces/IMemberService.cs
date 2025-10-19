using GYMManagementBLL.ViewModel.MemberViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel CreatedMember);

        MemberViewModel? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecord(int MemberId);

        MemberToUpDateViewModel? GetMemberToUpDate(int MemberId);

        bool UpdateMemberDetails(int id,MemberToUpDateViewModel updatedMember);

        bool RemoveMember(int id);

    }
}
