using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TruthOrDareUI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand<string> _navigateCommand;

        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));


        public MainPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;

        }
        
        private async void ExecuteNavigateCommand(string uri)
        {
            await _navigationService.NavigateAsync(uri);
        }
    }
}
