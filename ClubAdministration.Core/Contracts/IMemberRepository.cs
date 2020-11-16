using ClubAdministration.Core.Entities;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository {
        bool CheckDuplicateMember(Member member);
    }
}
