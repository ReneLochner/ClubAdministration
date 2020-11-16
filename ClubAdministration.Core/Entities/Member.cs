using GitStat.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Core.Entities
{
  public class Member : EntityObject
  {
    [Required]
    [MinLength(2, ErrorMessage = "FirstNames minimum length is 2")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "FirstNames minimum length is 2")]
    public string LastName { get; set; }

    public ICollection<MemberSection> MemberSections { get; set; }

    public override string ToString() => $"Id: {Id}; LastName: {LastName}; FirstName: {FirstName}; MemberSections: {MemberSections?.Count}";
  }
}
