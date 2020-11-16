using System;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IUnitOfWork : IDisposable
  {
    IMemberRepository MemberRepository { get; }
    ISectionRepository SectionRepository { get; }
    IMemberSectionRepository MemberSectionRepository { get; }

    Task<int> SaveChangesAsync();

    Task DeleteDatabaseAsync();
    Task MigrateDatabaseAsync();
  }
}
