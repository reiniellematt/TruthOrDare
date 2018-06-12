using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private IPageDialogService _dialogService;
        private INavigationService _navigationService;

        private string _minutesToCompleteChallengeEntry = GlobalConfig.MinutesToCompleteChallenge.ToString();
        private string _secondsBeforeRevealEntry = GlobalConfig.SecondsBeforeReveal.ToString();
        private DelegateCommand _saveCommand;
        private DelegateCommand _cancelCommand;

        public string MinutesToCompleteChallengeEntry
        {
            get { return _minutesToCompleteChallengeEntry; }
            set { SetProperty(ref _minutesToCompleteChallengeEntry, value); }
        }
        public string SecondsBeforeRevealEntry
        {
            get { return _secondsBeforeRevealEntry; }
            set { SetProperty(ref _secondsBeforeRevealEntry, value); }
        }
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private async void ExecuteSaveCommand()
        {
            string validationMessage = ValidateForm();

            if (validationMessage.Length > 0)
            {
                await _dialogService.DisplayAlertAsync("Invalid Entries", validationMessage, "OK");
                return;
            }

            await GlobalConfig.SaveTimeConfig(MinutesToCompleteChallengeEntry, SecondsBeforeRevealEntry);

            GlobalConfig.MinutesToCompleteChallenge = int.Parse(MinutesToCompleteChallengeEntry);
            GlobalConfig.SecondsBeforeReveal = int.Parse(SecondsBeforeRevealEntry);

            await _navigationService.GoBackAsync();
        }
        private async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }
        private string ValidateForm()
        {
            if (!double.TryParse(MinutesToCompleteChallengeEntry, out double minutes))
            {
                return "Enter a valid number in the \"Minutes to Complete the Challenge\" field.";
            }

            if (!double.TryParse(SecondsBeforeRevealEntry, out double seconds))
            {
                return "Enter a valid number in the \"Seconds Before Reveal\" field.";
            }

            return string.Empty;
        }
    }
}