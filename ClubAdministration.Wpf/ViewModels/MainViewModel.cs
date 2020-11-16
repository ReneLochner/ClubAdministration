using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels {
    public class MainViewModel : BaseViewModel {
        private ObservableCollection<Section> _sections;
        private ObservableCollection<MemberDto> _members;
        private MemberDto _selectedMember;
        private Section _selectedSection;
        private MemberDto _selectedMemberNow;

        public ObservableCollection<Section> Sections {
            get => _sections;
            set {
                _sections = value;
                OnPropertyChanged(nameof(Sections));
            }
        }

        public ObservableCollection<MemberDto> Members {
            get => _members;
            set {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        public MemberDto SelectedMember {
            get => _selectedMember;
            set {
                _selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
            }
        }

        public Section SelectedSection {
            get => _selectedSection;
            set {
                _selectedSection = value;
                OnPropertyChanged(nameof(SelectedSection));
                _ = LoadMembers();
            }
        }

        private async Task LoadMembers()
        {
            _selectedMemberNow = SelectedMember;
            using IUnitOfWork uow = new UnitOfWork();

            var members = await uow.MemberSectionRepository
                .GetBySectionWithMemberAsync(SelectedSection.Id);
            Members = new ObservableCollection<MemberDto>(members);

            if (_selectedMemberNow == null)
            {
                SelectedMember = Members.First();
            }
            else
            {
                SelectedMember = _selectedMember;
            }
        }

        private ICommand _cmdEditMember;
        public ICommand CmdEditMember {
            get {
                if (_cmdEditMember == null)
                {
                    _cmdEditMember = new RelayCommand(
                        execute: _ =>
                        {
                            Controller.ShowWindow(new EditMemberViewModel(Controller, SelectedMember), true);
                            _ = LoadDataAsync();
                        },
                        canExecute: _ => SelectedMember != null);
                }

                return _cmdEditMember;
            }
        }

        public MainViewModel(IWindowController windowController) : base(windowController)
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
        }

        private async Task LoadDataAsync()
        {
            using IUnitOfWork uow = new UnitOfWork();
            var sections = await uow.SectionRepository
                .GetAllAsync();
            Sections = new ObservableCollection<Section>(sections);
            _selectedSection = Sections.First();
            await LoadMembers();
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
