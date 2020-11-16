using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubAdministration.Wpf.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    public MainViewModel(IWindowController windowController) : base(windowController)
    {
      LoadCommands();
    }

    private void LoadCommands()
    {
    }

    private Task LoadDataAsync()
    {
      throw new NotImplementedException();
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }

    public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
    {
      var viewModel = new MainViewModel(windowController);
      await viewModel.LoadDataAsync();
      return viewModel;
    }
  }
}
