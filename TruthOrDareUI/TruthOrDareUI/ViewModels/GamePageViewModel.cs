using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
	public class GamePageViewModel : BindableBase
	{
        private INavigationService _navigationService;

        private bool _showStartBtn;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _startCommand;

        public bool ShowStartBtn
        {
            get { return _showStartBtn; }
            set { SetProperty(ref _showStartBtn, value); }
        }
        public bool ShowPicker
        {
            get { return !ShowStartBtn; }
        }
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));
        public DelegateCommand StartCommand => _startCommand ?? (_startCommand = new DelegateCommand(ExecuteStartCommand));

        public GamePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ShowStartBtn = true;
        }

        private void ExecuteStartCommand()
        {
            ShowStartBtn = false;
            RaisePropertyChanged("ShowPicker");
        }

        private async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }
    }
}