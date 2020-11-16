using ClubAdministration.Core.Contracts;

namespace ClubAdministration.Persistence
{
  public class SectionRepository : ISectionRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public SectionRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }
  }
}