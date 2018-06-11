using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
    public class AboutPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _returnCommand;

        public DelegateCommand ReturnCommand =>
            _returnCommand ?? (_returnCommand = new DelegateCommand(ExecuteReturnCommand));

        public AboutPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private async void ExecuteReturnCommand()
        {
            await _navigationService.GoBackAsync();
        }
    }
}