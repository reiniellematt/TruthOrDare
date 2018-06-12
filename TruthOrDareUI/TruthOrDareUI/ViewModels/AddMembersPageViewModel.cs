using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TruthOrDareUI.ViewModels
{
    public class AddMembersPageViewModel : BindableBase
    {
        private ObservableCollection<string> _names = new ObservableCollection<string>();
        private string _newNameEntry;
        private DelegateCommand _addCommand;

        public ObservableCollection<string> Names
        {
            get { return _names; }
            set { SetProperty(ref _names, value); }
        }
        public string NewNameEntry
        {
            get { return _newNameEntry; }
            set { SetProperty(ref _newNameEntry, value); }
        }
        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand));

        public AddMembersPageViewModel()
        {
            LoadSampleData();
        }

        private void ExecuteAddCommand()
        {
            if (!string.IsNullOrWhiteSpace(NewNameEntry))
            {
                Names.Add(NewNameEntry);
                NewNameEntry = string.Empty;
            }
        }

        private void LoadSampleData()
        {
            Names.Add("Matt");
            Names.Add("Tim");
            Names.Add("Sue");
        }
    }
}
