using GitStat.Core.Entities;
using System.Collections.Generic;

namespace ClubAdministration.Core.Entities
{
  public class Section : EntityObject
  {
    public string Name { get; set; }

    public ICollection<MemberSection> MemberSections { get; set; }

    public override string ToString() => $"Id: {Id}; Name: {Name}; MemberSections: {MemberSections?.Count}";
  }
}
