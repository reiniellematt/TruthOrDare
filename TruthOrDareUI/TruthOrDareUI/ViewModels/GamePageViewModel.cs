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
        private CustomTimer _timer;

        private double _timeLeft = GlobalConfig.SecondsBeforeReveal;
        private string _time;
        private string _selectedName;
        private bool _showStartBtn;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _startCommand;

        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        public string SelectedName
        {
            get { return _selectedName; }
            set { SetProperty(ref _selectedName, value); }
        }
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
            Time = "START";

            if (GlobalConfig.PlayersFromLastSession.Count > 0)
            {
                SelectedName = "Let's start...";
                _timer = new CustomTimer(TimerTick);
            }
            else
            {
                SelectedName = "No players yet!";
            }

            ShowStartBtn = true;
        }

        private void ExecuteStartCommand()
        {
            if (_timer != null)
            {
                Time = _timeLeft.ToString();
                _timer.Start();
            }
        }

        private async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }

        private void TimerTick()
        {
            _timeLeft--;
            Time = _timeLeft.ToString();

            if (_timeLeft == 0)
            {
                _timer.Stop();
                ShowStartBtn = false;
                RaisePropertyChanged("ShowPicker");

                SelectedName = GlobalConfig.GenerateName();
            }
        }
    }
}