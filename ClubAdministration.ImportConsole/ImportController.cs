using ClubAdministration.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ClubAdministration.ImportConsole {
    public class ImportController {
        const string FileName = "members.csv";

        public static async Task<MemberSection[]> ReadFromCsvAsync()
        {
            string[][] matrix = await MyFile.ReadStringMatrixFromCsvAsync(FileName, true);
            var member = matrix
                .Select(member => new Member
                {
                    FirstName = member[1],
                    LastName = member[0],

                })
                .GroupBy(line => line.FirstName + line.LastName)
                .Select(member => member.First())
                .ToArray();

            var section = matrix
                .GroupBy(line => line[2])
                .Select(section => new Section
                {
                    Name = section.Key
                })
                .ToArray();

            var memberSection = matrix
                .Select(memberSection => new MemberSection
                {
                    Member = member.Single(member => member.FirstName == memberSection[1] && member.LastName == memberSection[0]),
                    Section = section.Single(member => member.Name == memberSection[2])
                })
                .ToArray();

            return memberSection;
        }
    }
}
