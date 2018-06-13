using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
    public class ChallengePageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private CustomTimer _timer;
        private bool _hasStarted = false;
        private int _mins = GlobalConfig.MinutesToCompleteChallenge, _secs = 0;

        private string _mainButtonText;
        private string _challengeType;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _mainButtonCommand;

        public string MainButtonText
        {
            get { return _mainButtonText; }
            set { SetProperty(ref _mainButtonText, value); }
        }
        public string ChallengeType
        {
            get { return _challengeType; }
            set { SetProperty(ref _challengeType, value); }
        }
        public string Time
        {
            get { return $"{_mins}:{_secs:00}"; }
        }
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));
        public DelegateCommand MainButtonCommand => _mainButtonCommand ?? (_mainButtonCommand = new DelegateCommand(ExecuteMainButtonCommand));

        public ChallengePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            ChallengeType = GlobalConfig.ChallengeType;
            _hasStarted = false;
            _timer = new CustomTimer(TimerTick);
            MainButtonText = "START";
        }

        private async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }

        private async void ExecuteMainButtonCommand()
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                MainButtonText = "DONE";

                _timer.Start();
            }
            else
            {
                _timer.Stop();

                await _navigationService.NavigateAsync("GamePage");
            }
        }

        private void TimerTick()
        {
            _secs--;

            if (_secs < 0)
            {
                _mins--;
                _secs = 59;
            }

            if (_mins == 0 && _secs == 0)
            {
                _timer.Stop();

                _dialogService.DisplayAlertAsync("Uh oh!", "Failed to complete the challenge :(", "OK");

                MainButtonText = "RETURN";
            }

            RaisePropertyChanged("Time");

        }
    }
}