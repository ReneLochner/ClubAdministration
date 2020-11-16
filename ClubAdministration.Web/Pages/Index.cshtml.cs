using ClubAdministration.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages
{
  public class IndexModel : PageModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public IndexModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    public IActionResult OnPost()
    {
      return Page();
    }
  }
}
