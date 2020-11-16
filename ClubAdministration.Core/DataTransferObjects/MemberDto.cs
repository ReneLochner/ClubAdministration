
namespace ClubAdministration.Core.DataTransferObjects
{
  public class MemberDto
  {
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int CountSections { get; set; }

    public override string ToString() => $"Id: {Id}; LastName: {LastName}; FirstName: {FirstName}; CountSections: {CountSections}";

  }
}
