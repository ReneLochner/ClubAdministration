using System;
using ClubAdministration.Wpf.Common;
using System.Collections.Generic;
using System.Text;
using ClubAdministration.Core.DataTransferObjects;
using System.ComponentModel.DataAnnotations;
using ClubAdministration.Wpf.Common.Contracts;
using System.Windows.Input;
using ClubAdministration.Core.Entities;
using ClubAdministration.Core.Contracts;
using ClubAdministration.Persistence;

namespace ClubAdministration.Wpf.ViewModels {
    public class EditMemberViewModel : BaseViewModel {
        public string _firstname;
        public string _lastname;
        public MemberDto _member;

        public string Firstname {
            get => _firstname;
            set {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        public string Lastname {
            get => _lastname;
            set {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
                Validate();
            }
        }

        public MemberDto Member {
            get => _member;
            set {
                _member = value;
                OnPropertyChanged(nameof(Member));
            }
        }

        private ICommand _cmdSave;
        public ICommand CmdSave {
            get {
                if (_cmdSave == null)
                {
                    _cmdSave = new RelayCommand(
                        execute: async _ =>
                        {
                            try
                            {
                                using IUnitOfWork uow = new UnitOfWork();
                                Member memberInDb = await uow.MemberSectionRepository.GetMemberByIdAsync(_member.Id);
                                memberInDb.FirstName = Firstname;
                                memberInDb.LastName = Lastname;
                                uow.MemberSectionRepository.Update(memberInDb);
                                await uow.SaveChangesAsync();
                                Controller.CloseWindow(this);
                            }
                            catch (ValidationException validationException)
                            {
                                if (validationException.Value is IEnumerable<string> properties)
                                {
                                    foreach (var property in properties)
                                    {
                                        AddErrorsToProperty(property, new List<string> { validationException.ValidationResult.ErrorMessage });
                                    }
                                }
                                else
                                {
                                    DbError = validationException.ValidationResult.ToString();
                                }
                            }
                        },
                        canExecute: _ => IsValid
                    );
                }

                return _cmdSave;
            }
        }

        public EditMemberViewModel(IWindowController controller, MemberDto member) : base(controller)
        {
            Member = member;
            Init();
        }

        private void Init()
        {
            Firstname = Member.FirstName;
            Lastname = Member.LastName;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Lastname.Length < 2)
            {
                yield return new ValidationResult("Minimum length of Lastname is 2", new string[] {nameof(Lastname)});
            }
        }
    }
}
