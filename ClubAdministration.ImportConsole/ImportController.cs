using ClubAdministration.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ClubAdministration.ImportConsole
{
  public class ImportController
  {
    const string FileName = "members.csv";

    public static Task<MemberSection[]> ReadFromCsvAsync()
    {
      throw new NotImplementedException();
    }

  }
}
