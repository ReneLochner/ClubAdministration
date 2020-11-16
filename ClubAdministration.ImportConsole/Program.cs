using ClubAdministration.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.ImportConsole
{
  class Program
  {
    static async Task Main()
    {
      Console.WriteLine("Import der Sections und Members in die Datenbank");

      using UnitOfWork unitOfWork = new UnitOfWork();
      Console.WriteLine("Datenbank löschen");
      await unitOfWork.DeleteDatabaseAsync();
      Console.WriteLine("Datenbank migrieren");
      await unitOfWork.MigrateDatabaseAsync();
      Console.WriteLine("Members werden von members.csv eingelesen");
      var memberSections = await ImportController.ReadFromCsvAsync();
      if (memberSections.Length == 0)
      {
        Console.WriteLine("!!! Es wurden keine Members eingelesen");
        return;
      }

      Console.WriteLine($"  Es wurden {memberSections.GroupBy(ms => ms.Member).Count()} Members eingelesen");
      Console.WriteLine($"  Es wurden {memberSections.GroupBy(ms => ms.Section).Count()} Sections eingelesen");
      Console.WriteLine($"  Es wurden {memberSections.Count()} Mitgliedschaften eingelesen. Speichern in Datenbank ...");
      await unitOfWork.MemberSectionRepository.AddRangeAsync(memberSections);
      int savedRows = await unitOfWork.SaveChangesAsync();
      Console.WriteLine($"{savedRows} Datensätze wurden in Datenbank gespeichert!");
      Console.WriteLine();
      Console.Write("Beenden mit Eingabetaste ...");
      Console.ReadLine();
    }
  }
}
