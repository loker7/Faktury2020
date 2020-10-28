using Faktury2020.ViewModels;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel; //uwaga nie wspominają o tym w przykładzie
using System.Text;
using System.Windows.Input;

namespace Invoices2020.ViewModels
{
    public class ViewModel : ViewModelBase 
    {
        private string _buyerName;
        public string buyerName
        {
            get => _buyerName;
            set => SetProperty(ref _buyerName, value);
        }
        private string _dateOfIssue;
        public string dateOfIssue
        {
            get => _dateOfIssue;
            set => SetProperty(ref _dateOfIssue, value);
        }
        private string _numberOfInvoice;
        public string numberOfInvoice
        {
            get => _numberOfInvoice;
            set => SetProperty(ref _numberOfInvoice, value);
        }
        private string _dateOfSale;
        public string dateOfSale
        {
            get => _dateOfSale;
            set => SetProperty(ref _dateOfSale, value);
        }
        private string _wayOfPayment;
        public string wayOfPayment
        {
            get => _wayOfPayment;
            set => SetProperty(ref _wayOfPayment, value);
        }
        private string _dateOfPayment;
        public string dateOfPayment
        {
            get => _dateOfPayment;
            set => SetProperty(ref _dateOfPayment, value);
        }
        private string _buyerStreet;
        public string buyerStreet
        {
            get => _buyerStreet;
            set => SetProperty(ref _buyerStreet, value);
        }
        private string _cityAndPostalCodeOfBuyer;
        public string cityAndPostalCodeOfBuyer
        {
            get => _cityAndPostalCodeOfBuyer;
            set => SetProperty(ref _cityAndPostalCodeOfBuyer, value);
        }
        private string _nipOfBuyer;
        public string nipOfBuyer
        {
            get => _nipOfBuyer;
            set => SetProperty(ref _nipOfBuyer, value);
        }
        private string _nameOfGoodOrService;
        public string nameOfGoodOrService
        {
            get => _nameOfGoodOrService;
            set => SetProperty(ref _nameOfGoodOrService, value);
        }
        private string _wybranyPakiet;
        public string wybranyPakiet
        {
            get => _wybranyPakiet;
            set => SetProperty(ref _wybranyPakiet, value);
        }
        private string _wybranyOkres;
        public string wybranyOkres
        {
            get => _wybranyOkres;
            set => SetProperty(ref _wybranyOkres, value);
        }
        private string _quantity;
        public string quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string _valueGrossSingle;
        public string valueGrossSingle
        {
            get => _valueGrossSingle;
            set => SetProperty(ref _valueGrossSingle, value);
        }
        private string _valueGrossSingle2;
        public string valueGrossSingle2
        {
            get => _valueGrossSingle2;
            set => SetProperty(ref _valueGrossSingle2, value);
        }
        private string _valueGrossTogether;
        public string valueGrossTogether
        {
            get => _valueGrossTogether;
            set => SetProperty(ref _valueGrossTogether, value);
        }
        private string _valueGrossTogether2;
        public string valueGrossTogether2
        {
            get => _valueGrossTogether2;
            set => SetProperty(ref _valueGrossTogether2, value);
        }
        private string _startPelnyMiesiac;
        public string startPelnyMiesiac
        {
            get => _startPelnyMiesiac;
            set => SetProperty(ref _startPelnyMiesiac, value);
        }   
        private string _inWords;
        public string inWords
        {
            get => _inWords;
            set => SetProperty(ref _inWords, value);
        }
        private string _Sum;
        public string Sum
        {
            get => _Sum;
            set => SetProperty(ref _Sum, value);
        }
        private string _Paid;
        public string Paid
        {
            get => _Paid;
            set => SetProperty(ref _Paid, value);
        }
        private string _LeftToPay;
        public string LeftToPay
        {
            get => _LeftToPay;
            set => SetProperty(ref _LeftToPay, value);
        }
        private string _additional2;
        public string additional2
        {
            get => _additional2;
            set => SetProperty(ref _additional2, value);
        }







        public ICommand ChangeNameCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
       // public string dateOfIssue { get; set; }
        // public string stalaCzescNumeruFaktury { get; set; }
       // public string numberOfInvoice { get; set; }//transition to MVVM
        //public string dateOfSale { get; set; }
       // public string wayOfPayment { get; set; }
       // public string dateOfPayment { get; set; }
        //public string nazwaNabywcy { get; set; }
        //public string buyerStreet { get; set; }
       // public string cityAndPostalCodeOfBuyer { get; set; }
       // public string nipOfBuyer { get; set; }
        //public string nameOfGoodOrService { get; set; }//transition to MVVM
      //  public string wybranyPakiet { get; set; }  //NIE UŻYTE W VIEW!!!
       // public string wybranyOkres { get; set; }  //NIE UŻYTE W VIEW!!!
       // public string quantity { get; set; }
        //public string valueGrossSingle { get; set; }
       // public string valueGrossSingle2 { get; set; }
     //   public string valueGrossTogether { get; set; }
      //  public string valueGrossTogether2 { get; set; }
        //public string startPelnyMiesiac { get; set; } //NIE UŻYTE W VIEW!!!

     //   public string inWords { get; set; }//transition to MVVM
      //  public string Sum { get; set; }//transition to MVVM
       // public string Paid { get; set; }//transition to MVVM
      //  public string LeftToPay { get; set; }//transition to MVVM
      //  public string additional2 {get; set;}//transition to MVVM

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // public event PropertyChangedEventHandler PropertyChanged;//uwaga inaczej niż w przykładzie

        private readonly DelegateCommand _changeNameCommand;
    //    public ICommand ChangeNameCommand => _changeNameCommand;

        public ViewModel()
        {
            _changeNameCommand = new DelegateCommand(OnChangeName);
        }

        private void OnChangeName(object commandParameter)
        {
            buyerName = "Walter";

        }
    }
}

