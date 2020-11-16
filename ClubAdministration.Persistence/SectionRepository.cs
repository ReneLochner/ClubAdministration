using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence {
    public class SectionRepository : ISectionRepository {
        private readonly ApplicationDbContext _dbContext;

        public SectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Section>> GetAllAsync()
        {
            return await _dbContext.Sections
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}