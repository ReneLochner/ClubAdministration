using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence {
    public class MemberSectionRepository : IMemberSectionRepository {
        private readonly ApplicationDbContext _dbContext;

        public MemberSectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(IEnumerable<MemberSection> memberSections)
          => await _dbContext.AddRangeAsync(memberSections);

        public async Task<IEnumerable<MemberDto>> GetBySectionWithMemberAsync(int sectionId)
        {
            var members = await _dbContext.MemberSections
                .Include(memberSection => memberSection.Member)
                .Include(memberSection => memberSection.Section)
                .Where(memberSection => memberSection.SectionId == sectionId)
                .Select(memberSection => new MemberDto
                {
                    FirstName = memberSection.Member.FirstName,
                    LastName = memberSection.Member.LastName,
                    Id = memberSection.MemberId

                })
                .OrderBy(member => member.LastName)
                .ThenBy(member => member.FirstName)
                .ToListAsync();

            members.ForEach(member => member.CountSections = _dbContext.MemberSections.Count(memberSection => memberSection.MemberId == member.Id));

            return members;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _dbContext.Members
                .SingleAsync(member => member.Id == id);
        }

        public void Update(Member memberInDb)
        {
            _dbContext.Members
               .Update(memberInDb);
        }
    }
}