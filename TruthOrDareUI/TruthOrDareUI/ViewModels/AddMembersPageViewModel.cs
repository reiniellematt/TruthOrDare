using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
    public class AddMembersPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        private string _newNameEntry;
        private DelegateCommand _addCommand;
        private DelegateCommand _nextCommand;
        private DelegateCommand<string> _removeCommand;

        public ObservableCollection<string> Names
        {
            get { return GlobalConfig.PlayersFromLastSession; }
        }
        public string NewNameEntry
        {
            get { return _newNameEntry; }
            set { SetProperty(ref _newNameEntry, value); }
        }
        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand));
        public DelegateCommand NextCommand => _nextCommand ?? (_nextCommand = new DelegateCommand(ExecuteNextCommand));
        public DelegateCommand<string> RemoveCommand => _removeCommand ?? (_removeCommand = new DelegateCommand<string>(ExecuteRemoveCommand));

        public AddMembersPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void ExecuteAddCommand()
        {
            if (!string.IsNullOrWhiteSpace(NewNameEntry))
            {
                GlobalConfig.PlayersFromLastSession.Add(NewNameEntry);
                NewNameEntry = string.Empty;
            }
        }

        private async void ExecuteNextCommand()
        {
            await GlobalConfig.SavePlayersFromLastSession();
            await _navigationService.NavigateAsync("GamePage");
        }

        private void ExecuteRemoveCommand(string selectedName)
        {
            GlobalConfig.PlayersFromLastSession.Remove(selectedName);
            RaisePropertyChanged("Names");
        }
    }
}