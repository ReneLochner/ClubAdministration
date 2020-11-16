using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberSectionRepository
  {
    Task AddRangeAsync(IEnumerable<MemberSection> memberSections);
  }
}
