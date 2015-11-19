using ComboBoxMVVMExample.Model; // ExampleEnum
using System;
using System.Collections.Generic; // IEnumerable
using System.Windows; // MessageBox
using System.Windows.Input; // ICommand
using System.Collections.ObjectModel; // ObservableCollection<Country>


namespace ComboBoxMVVMExample.ViewModel
{
    /// <summary>
    /// This class inherits from ViewModelBase class
    /// </summary>
    public class ExampleViewModel : ViewModelBase
    {
        #region ComboBox using enum type
        // Private Fields
        private IEnumerable<EnumItem> _EnumItems;
        private string _EnumSelectedItem;
        private ICommand _ShowEnumItemCommand;

        // Public Properties - Used for binding with the View
        public IEnumerable<EnumItem> EnumItems
        {
            get {
                // Whatever type of the enum, return them the same too
                return (EnumItem[])Enum.GetValues(typeof(EnumItem));
            }
            set
            {
                if (value != _EnumItems)
                {
                    _EnumItems = value;
                    OnPropertyChanged("EnumItems");
                }
            }
        }
        public string EnumSelectedItem
        {
            get { return _EnumSelectedItem; }
            set
            {
                _EnumSelectedItem = value;
                OnPropertyChanged("EnumSelectedItem");
            }
        }
        public ICommand ShowEnumItemCommand
        {
            get
            {
                _ShowEnumItemCommand = new RelayCommand(
                    param => ShowEnumItemMethod()
                );
                return _ShowEnumItemCommand;
            }
        }

        // Private Method
        private void ShowEnumItemMethod()
        {
            // Get combobox current selected value
            MessageBox.Show(EnumSelectedItem);
        }
        #endregion

        #region Cascaded ComboBox using List<T> class
        // Private Fields
        private List<Country> _CountryList;
        private string _SelectedCountryCode;
        private List<State> _StateList;
        private string _SelectedState;

        // Public Properties - Used for binding with the View
        public List<Country> CountryList {
            get { return _CountryList; }
            set
            {
                _CountryList = value;
                OnPropertyChanged("CountryList");
            }
        }
        public string SelectedCountryCode
        {
            get { return _SelectedCountryCode; }
            set
            {
                _SelectedCountryCode = value;
                OnPropertyChanged("SelectedCountryCode");
                OnPropertyChanged("AllowStateSelection"); // Trigger Enable/Disable UI element when particular country is selected
                getStateList(); // Generate a new list of states based on a selected country
            }
        }
        public List<State> StateList
        {
            get { return _StateList; }
            set
            {
                _StateList = value;
                OnPropertyChanged("StateList");
            }
        }
        
        public string SelectedState
        {
            get { return _SelectedState; }
            set
            {
                _SelectedState = value;
                OnPropertyChanged("SelectedState");
            }
        }
        public bool AllowStateSelection
        {
            get { return (SelectedCountryCode != null); }
        }

        // Constructor
        public ExampleViewModel()
        {
            // Instantiate, get a list of countries from the Model
            Country _Country = new Country();
            CountryList = _Country.getCountries();
        }

        // Private Method
        private void getStateList()
        {
            // Instantiate, get a list of states based on selected country two-letter code from the Model
            State _State = new State();
            StateList = _State.getStateByCountryCode(SelectedCountryCode);
        }
        #endregion
    }
}
