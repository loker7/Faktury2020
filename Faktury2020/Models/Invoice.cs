using System;
using System.Collections.Generic;
using System.Text;

namespace Invoices2020
{

    public class Invoice
    {
        // private string miejsce_wystawienia;

        //public Faktura(string miejsce_wystawienia)
        //{
        //    this.miejsce_wystawienia = "Katowice";
        //}

  

        //public string miejsce_wystawienia  = "Katowice";
        //public string data_wystawienia { get; set; }
        ////data_wystawienia = "16-03-2020";
        public string stalaCzescNumeruFaktury { get; set; }
        //"Faktura proforma 1-TEST-2-2020"
        //public string numerFaktury {
           
        //     get { return numerFaktury; } set { numerFaktury = "1-" + stalaCzescNumeruFaktury + "-" + dataSprzedazy; }
        //}
        //"Faktura proforma 1-TEST-2-2020"
        //public string dataSprzedazy { get; set; }
        ////2-2020
        public string sposobZaplaty { get; set; }
        //GOTÓWKA
        //public string terminPlatnosci { get; set; }
        //01-04-2020
        //public string NazwaSprzedawcy { get; set; }
        //"xSolutions Sp. z o.o."
        public string nazwaNabywcy { get; set; }
        //"Aldona Nieznana"
        //public string UlicaSprzedawcy { get; set; }
        //"ul. Mickiewicza 29"
        public string ulicaNabywcy { get; set; }
        //"ul Nieznana 20"
        //public string MiastoiKodSprzedawcy { get; set; }
        //"40-085 Katowice"
        public string miastoiKodNabywcy { get; set; }
        //"41-200 Sosnowiec"
       // public string NIPSprzedawcy { get; set; }
        //"NIP 634-293-59-61"
        public string nipNabywcy { get; set; }
        //"NIP 000-000-00-00"
       // public string BankSprzedawcy { get; set; }
        //"Nest Bank"
       // public string KontoBankoweSprzedawcy { get; set; }
        //"44 2530 0008 2064 1044 1937 0001"
        public string wybranyPakiet { get; set; }
        //"np. STANDARD
        public string wybranyOkres { get; set; }
        //1,3,6 lub 12 mcy
        public string ilosc { get; set; }
        //"1"
        public string wartoscJednostkowaBrutto { get; set; }
        //"723,24"
        public string wartoscBrutto { get; set; }
        //"723,24"
        public string startPelnyMiesiac { get; set; }
        //T/Y /N
        public string razem { get; set; }
        //"723,24 PLN"
        //public string zaplacono { get; set; }
        //"0,00 PLN"
        //public string pozostalo_do_zaplaty { get; set; }
        //"723,24 PLN"
        //public string uwagi { get; set; }
        //"Zwolnienie podmiotowe z VAT wg. art. 113 ust. 1 Ustawy o VAT. W przypadku braku opłacenia faktury w terminie świadczenie usługi zostanie automatycznie wstrzymane."
        //public string osobaUpowaznionaDoWystawieniaFaktury { get; set; }
        //"Tomasz Chajduga"
        //public string NazwaFaktury { get; set; }
        //"HelloWorld4.pdf"
        //public string nazwaFaktury
        //{
        //    get { return nazwaFaktury; }
        //    set { nazwaFaktury = numerFaktury; }
        //}

    }

}
