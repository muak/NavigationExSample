using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Xamarin.Forms;
using Prism;
using NavigationExSample.Navigation;
using NavigationExSample.Views;

namespace NavigationExSample.ViewModels
{
    public class SecondPageViewModel : BindableBase, INavigationAware, IActiveAware
    {
        private string _LogText;
        public string LogText {
            get { return _LogText; }
            set { SetProperty(ref _LogText, value); }
        }

        private DelegateCommand _NextCommand;
        public DelegateCommand NextCommand {
            get {
                return _NextCommand = _NextCommand ?? new DelegateCommand(async () => {
                    await _navigationService.NavigateAsync<NextPage>();
                });
            }
        }

        private DelegateCommand _ChangeTabCommand;
        public DelegateCommand ChangeTabCommand {
            get {
                return _ChangeTabCommand = _ChangeTabCommand ?? new DelegateCommand(() => {
                    _navigationService.ChangeTab<FirstPage>();
                });
            }
        }

        private bool _IsActive;
        public bool IsActive {
            get {
                return _IsActive;
            }

            set {
                _IsActive = value;
                if (value) {
                    OnActive();
                }
                else {
                    OnNonActive();
                }
            }
        }

        private INavigationService _navigationService;

        public event EventHandler IsActiveChanged;

        public SecondPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        void OnActive()
        {
            LogText += "OnActive Passed\n";
        }
        void OnNonActive()
        {
            LogText += "OnNonActive Passed\n";
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            LogText += "OnNavigatedFrom Passed\n";
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            LogText += "OnNavigatedTo Passed\n";
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            LogText += "OnNavigatingTo Passed\n";
        }
    }
}
