using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
	public class ChallengePageViewModel : BindableBase
	{
        private readonly INavigationService _navigationService;

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        public ChallengePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
