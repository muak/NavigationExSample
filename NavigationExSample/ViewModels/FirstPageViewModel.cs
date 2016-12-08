using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace NavigationExSample.ViewModels
{
    public class FirstPageViewModel : BindableBase,INavigationAware
    {

        private DelegateCommand _NextCommand;
        public DelegateCommand NextCommand {
            get { return _NextCommand = _NextCommand ?? new DelegateCommand(async() => {
                await _navigationService.NavigateAsync("NextPage");
            }); }
        }

        private INavigationService _navigationService;
        public FirstPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
