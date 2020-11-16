using ClubAdministration.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Members
{
  public class EditModel : PageModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public EditModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet(int? id)
    {
      return Page();
    }

    public IActionResult OnPost()
    {
      return Page();
    }
  }
}
