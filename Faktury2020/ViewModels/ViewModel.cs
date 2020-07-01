using Faktury2020.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel; //uwaga nie wspominają o tym w przykładzie
using System.Text;
using System.Windows.Input;

namespace Invoices2020.ViewModels
{
    public class ViewModel : ViewModelBase 
    {
        private string _nazwaNabywcy;
        public string nazwaNabywcy
        {
            get => _nazwaNabywcy;
            set => SetProperty(ref _nazwaNabywcy, value);
        }
        //public ICommand ChangeNameCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        // public string stalaCzescNumeruFaktury { get; set; }
        public string numerFaktury { get; set; }//transition to MVVM
        public string sposobZaplaty { get; set; }
        //public string nazwaNabywcy { get; set; }
        public string ulicaNabywcy { get; set; }
        public string miastoiKodNabywcy { get; set; }
        public string nipNabywcy { get; set; }
        public string nazwaTowaruLubUslugi { get; set; }//transition to MVVM
        public string wybranyPakiet { get; set; }  //NIE UŻYTE W VIEW!!!
        public string wybranyOkres { get; set; }  //NIE UŻYTE W VIEW!!!
        public string ilosc { get; set; }
        public string wartoscJednostkowaBrutto { get; set; }
        public string wartoscBrutto { get; set; }
        public string startPelnyMiesiac { get; set; } //NIE UŻYTE W VIEW!!!

        public string slownie { get; set; }//transition to MVVM
        public string Sum { get; set; }//transition to MVVM
        public string Paid { get; set; }//transition to MVVM
        public string LeftToPay { get; set; }//transition to MVVM
        public string additional2 {get; set;}//transition to MVVM

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // public event PropertyChangedEventHandler PropertyChanged;//uwaga inaczej niż w przykładzie

        private readonly DelegateCommand _changeNameCommand;
        public ICommand ChangeNameCommand => _changeNameCommand;

        public ViewModel()
        {
            _changeNameCommand = new DelegateCommand(OnChangeName);
        }

        private void OnChangeName(object commandParameter)
        {
            nazwaNabywcy = "Walter";

        }
    }
}
