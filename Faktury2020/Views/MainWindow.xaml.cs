using System.Windows;
using System.Windows.Controls;
//using System.Windows.Documents;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using CsvHelper;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System;
using System.Windows.Media;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Invoices2020.ViewModels;

namespace Invoices2020
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        int numerzListy=0;
        public static bool uwagaoZaliczce;
        public static bool zaliczka;
              List<Invoice> proformyDlaDanegoMiesiaca = new List<Invoice>();
        public MainWindow()
        {
            var viewModel = new ViewModel();

            viewModel.numberOfInvoice = "-------------------------";
            viewModel.wayOfPayment = "-------";
            viewModel.buyerName = "----------------";
            viewModel.buyerStreet = "-------------";
            viewModel.cityAndPostalCodeOfBuyer = "---------------";
            viewModel.nipOfBuyer = "-----------------";
            viewModel.nameOfGoodOrService = "-------------------------------------------------";
            viewModel.quantity = "-";
            viewModel.valueGrossSingle = "-------";
            viewModel.valueGrossTogether = "------";
            viewModel.inWords = "-------------";
            viewModel.Sum = "------";
            viewModel.Paid = "----";
            viewModel.LeftToPay = "----";

            viewModel.additional2 = "------------------------------------------------------";

            DataContext = viewModel;
            InitializeComponent();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            txtdataWystawienia.Text = txtdataWystawienia2.Text;

           // viewModel.buyerName = "Aldona NNNNNXXX"; 
          //  viewModel.OnPropertyChanged(nameof(ViewModel.nazwaNabywcy)); //usuwam bo dodałem ViewModelBase
        }

        private void DatePicker_SelectedDateChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;
          
            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                txtterminPlatnosci.Text = ""; //ZROBIĆ PÓŹNIEJ~!!!!!!!!!!!
            }
            else
            {
                // ... No need to display the time.
                txtterminPlatnosci.Text = date.Value.ToShortDateString();
            }
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            //w tym guziku się dzieje w commandzie
        }

        private void chkPozycja2_Checked(object sender, RoutedEventArgs e)
        {

            txtLp2.IsEnabled = true;
            txtnazwaTowaruLubUslugi2.IsEnabled = true;
            txtilosc2.IsEnabled = true;
            txtjednostka2.IsEnabled = true;
            txtwartoscJednostkowaBrutto2.IsEnabled = true;
            txtwartoscBrutto2.IsEnabled = true;

            txtLp2.Text = "2.";
        }

        private void chkPozycja2_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLp2.IsEnabled = false;
            txtnazwaTowaruLubUslugi2.IsEnabled = false;
            txtilosc2.IsEnabled = false;
            txtjednostka2.IsEnabled = false;
            txtwartoscJednostkowaBrutto2.IsEnabled = false;
            txtwartoscBrutto2.IsEnabled = false;

            //txtLp2.Text = "";
        }

        private void chkPozycja1_Checked(object sender, RoutedEventArgs e)
        {
            txtLp1.IsEnabled = true;
            txtnazwaTowaruLubUslugi1.IsEnabled = true;
            txtilosc1.IsEnabled = true;
            txtjednostka1.IsEnabled = true;
            txtwartoscJednostkowaBrutto1.IsEnabled = true;
            txtwartoscBrutto1.IsEnabled = true;

            txtLp1.Text = "1.";
            txtLp2.Text = "2.";
        }
        private void chkPozycja1_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLp1.IsEnabled = false;
            txtnazwaTowaruLubUslugi1.IsEnabled = false;
            txtilosc1.IsEnabled = false;
            txtjednostka1.IsEnabled = false;
            txtwartoscJednostkowaBrutto1.IsEnabled = false;
            txtwartoscBrutto1.IsEnabled = false;

            txtLp1.Text = "";
            txtLp2.Text = "1.";
        }
        public void generujProformy()
        {
            try
            {
                using (var reader = new StreamReader("xb_baza.csv")) //"path\\to\\file.csv"
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var faktury = csvReader.GetRecords<Invoice>();//.ToList();
                        foreach (var faktura in faktury)
                        {
                            if (czyTerazProforma(faktura))
                            {
                                string pomocniczy;
                                txtnumerFaktury.Text = "1-PROFORMA-" + faktura.stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text;
                                if (faktura.sposobZaplaty == "P" | faktura.sposobZaplaty == "P") { pomocniczy = "przelew"; }
                                else if (faktura.sposobZaplaty == "G" | faktura.sposobZaplaty == "g") { pomocniczy = "GOTÓWKA"; }
                                else if (faktura.sposobZaplaty == "PG" | faktura.sposobZaplaty == "pg") { pomocniczy = "przelew/GOTÓWKA"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtsposobZaplaty.Text = pomocniczy;
                                txtnazwaNabywcy.Text = faktura.nazwaNabywcy;
                                txtulicaNabywcy.Text = faktura.ulicaNabywcy;
                                txtmiastoiKodNabywcy.Text = faktura.miastoiKodNabywcy;
                                txtnipNabywcy.Text = faktura.nipNabywcy;
                                if (faktura.wybranyOkres == "1") { pomocniczy = "1-MIESIĄC"; }
                                else if (faktura.wybranyOkres == "3") { pomocniczy = "3-MIESIĄCE"; }
                                else if (faktura.wybranyOkres == "6") { pomocniczy = "6-MIESIĘCY"; }
                                else if (faktura.wybranyOkres == "12") { pomocniczy = "12-MIESIĘCY"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtnazwaTowaruLubUslugi1.Text = "Abonament xBiuro - pakiet " + faktura.wybranyPakiet + " na " + pomocniczy;
                                txtilosc1.Text = faktura.ilosc;
                                txtwartoscJednostkowaBrutto1.Text = faktura.wartoscJednostkowaBrutto;
                                txtwartoscBrutto1.Text = faktura.wartoscBrutto;
                                txtrazem.Text = faktura.razem;
                                txtPozostaloDoZaplaty.Text = faktura.razem;
                                pomocniczy = faktura.razem.Substring(faktura.razem.Length - 2) + "/100 groszy brutto";
                                decimal pomocniczyDec = decimal.Parse(faktura.razem);
                                pomocniczyDec = Math.Truncate(pomocniczyDec);
                                int pomocniczyInt = Decimal.ToInt32(pomocniczyDec);
                                txtslownie.Text = "SŁOWNIE: " + AmointInWords.LiczbaSlownie(pomocniczyInt) + " " + AmointInWords.WalutaSlownie(pomocniczyInt, "PLN") + " i " + pomocniczy;
                                txtslownie.Text = txtslownie.Text.ToUpper();

                                generujPDF();
                                //if (faktura.czyZaplacono == "T") txtZaplacono.Text = faktura.razem;
                                //else if (faktura.czyZaplacono == "N") txtPozostaloDoZaplaty.Text = faktura.razem;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak bazy danych w folderze programu. Umieść plik xb_baza.csv folderze programu i spróbuj ponownie.", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnGenerujProformy_Click(object sender, RoutedEventArgs e)
        {
            generujProformy();
        }

        private void generujPDF()
        {
            Invoice faktura = new Invoice();
            // Books books = new Books();

            PdfDocument document = new PdfDocument(); // Create a new PDF document
            document.Info.Title = "Faktura";
            document.Info.Author = "xSolutions Sp.z o.o.";
            PdfPage page = document.AddPage(); // Create an empty page
            XGraphics gfx = XGraphics.FromPdfPage(page);  // Get an XGraphics object for drawing
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);  // Set font encoding to unicode
            //albo mogę ew. spróbować jeśli przestanie działać:
            //gfx.MUH = PdfFontEncoding.Unicode;
            //gfx.MFEH = PdfFontEmbedding.Default;
            XFont font = new XFont("Times New Roman", 12, XFontStyle.Regular, options); //Then you'll create a font:

            Document doc = new Document(); //tu startuje migradoc, po kolei: dokument, sekcja i paragraf..
            Section sec = doc.AddSection();

            //def tableMiejsceiDataWystawienia TUTAJ
            Table tableMiejsceiDataWystawienia = new Table();
            tableMiejsceiDataWystawienia.Borders.Width = 0.0;

            Column columnMiejsceiDataWystawienia = tableMiejsceiDataWystawienia.AddColumn(Unit.FromCentimeter(5));
            columnMiejsceiDataWystawienia.Format.Alignment = ParagraphAlignment.Left;
            Column columnMiejsceiDataWystawienia2 = tableMiejsceiDataWystawienia.AddColumn(Unit.FromCentimeter(12));
            columnMiejsceiDataWystawienia2.Format.Alignment = ParagraphAlignment.Right;

            Row rowMiejsceiDataWystawienia = tableMiejsceiDataWystawienia.AddRow();
            Cell cellMiejsceiDataWystawienia = rowMiejsceiDataWystawienia.Cells[0];
            cellMiejsceiDataWystawienia = rowMiejsceiDataWystawienia.Cells[0];
            // cellMiejsceiDataWystawienia.AddParagraph(faktura.miejsce_wystawienia);
            cellMiejsceiDataWystawienia.AddParagraph(lblmiejsce_wystawienia.Content.ToString());
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;

            cellMiejsceiDataWystawienia = rowMiejsceiDataWystawienia.Cells[1];

            //PONIŻEJ INACZEJ GDY ZALICZKA
            if (zaliczka == true)
            {
                cellMiejsceiDataWystawienia.AddParagraph("Wystawiono dnia: " + txtterminPlatnosci.Text);
            }
            else
            {
                cellMiejsceiDataWystawienia.AddParagraph("Wystawiono dnia: " + txtdataWystawienia.Text);
            }
            doc.LastSection.Add(tableMiejsceiDataWystawienia);

            //Paragraph miasto = sec.AddParagraph();
            //miasto.AddText("Katowice");
            //miasto.Format.Alignment = ParagraphAlignment.Left;

            //Paragraph dataWystawienia = sec.AddParagraph();
            //dataWystawienia.AddText("Wystawiono dnia: 16-03-2020");
            //dataWystawienia.Format.Alignment = ParagraphAlignment.Right;

            sec.AddParagraph();

            Paragraph numerFaktury = sec.AddParagraph();
            if (zaliczka == true)
            {
                numerFaktury.AddText("Faktura zaliczka 1-" + faktura.stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text);
            }
            else
            {
                numerFaktury.AddText(txtnumerFaktury.Text);
            }

            numerFaktury.Format.Font.Bold = true;
            numerFaktury.Format.Alignment = ParagraphAlignment.Center;

            sec.AddParagraph();

            Paragraph dataSprzedazy = sec.AddParagraph();
            dataSprzedazy.AddText("Data sprzedaży: " + txtdataSprzedazy.Text);
            dataSprzedazy.Format.Font.Bold = true;
            dataSprzedazy.Format.Alignment = ParagraphAlignment.Right;

            Paragraph sposobZaplaty = sec.AddParagraph();
            sposobZaplaty.AddText("Forma płatności: " + txtsposobZaplaty.Text);
            sposobZaplaty.Format.Font.Bold = true;
            sposobZaplaty.Format.Alignment = ParagraphAlignment.Right;

            Paragraph terminPlatnosci = sec.AddParagraph();
            terminPlatnosci.AddText("Termin płatności: " + txtterminPlatnosci.Text);
            terminPlatnosci.Format.Font.Bold = true;
            terminPlatnosci.Format.Alignment = ParagraphAlignment.Right;

            //def table sprzedawca-nabywca(SN) and its parts
            Table tableSN = new Table();
            tableSN.Borders.Width = 0.0;
            Column columnSN = tableSN.AddColumn(Unit.FromCentimeter(9));
            columnSN.Format.Alignment = ParagraphAlignment.Left;
            _ = tableSN.AddColumn(Unit.FromCentimeter(9));
            columnSN.Format.Alignment = ParagraphAlignment.Left;
            Row rowSN = tableSN.AddRow();
            Cell cellSN = rowSN.Cells[0];
            cellSN = rowSN.Cells[0];
            cellSN.AddParagraph("Sprzedawca");
            cellSN.Format.Font.Bold = true;
            cellSN = rowSN.Cells[1];
            cellSN.AddParagraph("Nabywca");
            cellSN.Format.Font.Bold = true;

            Row rowEmpty = tableSN.AddRow();
            Cell cellEmpty = rowEmpty.Cells[0];
            cellEmpty = rowEmpty.Cells[0];
            // cellEmpty.AddParagraph(""); //to jest niepotrzebne = i tak jest pusta linia
            cellEmpty = rowEmpty.Cells[1];
            // cellEmpty.AddParagraph(""); //to jest niepotrzebne = i tak jest pusta linia

            Row rowNazwaSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellNazwaSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellNazwaSprzedawcyiNabywcy = rowNazwaSprzedawcyiNabywcy.Cells[0];
            cellNazwaSprzedawcyiNabywcy.AddParagraph(txtnazwaSprzedawcy.Text);
            cellNazwaSprzedawcyiNabywcy.Format.Font.Bold = true;
            cellNazwaSprzedawcyiNabywcy = rowNazwaSprzedawcyiNabywcy.Cells[1];
            cellNazwaSprzedawcyiNabywcy.AddParagraph(txtnazwaNabywcy.Text);
            cellNazwaSprzedawcyiNabywcy.Format.Font.Bold = true;

            Row rowUlicaSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellUlicaSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellUlicaSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[0];
            cellUlicaSprzedawcyiNabywcy.AddParagraph(txtulicaSprzedawcy.Text);
            cellUlicaSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[1];
            cellUlicaSprzedawcyiNabywcy.AddParagraph(txtulicaNabywcy.Text);

            Row rowMiastoiKodSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellMiastoiKodSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellMiastoiKodSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[0];
            cellMiastoiKodSprzedawcyiNabywcy.AddParagraph(txtmiastoiKodSprzedawcy.Text);
            cellMiastoiKodSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[1];
            cellMiastoiKodSprzedawcyiNabywcy.AddParagraph(txtmiastoiKodNabywcy.Text);

            Row rowEmpty2 = tableSN.AddRow();
            // Cell cellEmpty2 = rowEmpty2.Cells[0];
            //cellEmpty2 = rowEmpty2.Cells[0];

            //cellEmpty2 = rowEmpty2.Cells[1];

            Row rowNIPSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellNIPSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellNIPSprzedawcyiNabywcy = rowNIPSprzedawcyiNabywcy.Cells[0];
            cellNIPSprzedawcyiNabywcy.AddParagraph(txtnipSprzedawcy.Text);
            cellNIPSprzedawcyiNabywcy.Format.Font.Bold = true;
            cellNIPSprzedawcyiNabywcy = rowNIPSprzedawcyiNabywcy.Cells[1];
            cellNIPSprzedawcyiNabywcy.AddParagraph("NIP: " + txtnipNabywcy.Text);
            cellNIPSprzedawcyiNabywcy.Format.Font.Bold = true;

            Row rowEmpty3 = tableSN.AddRow();

            //def table bank i konto bankowe
            Table tableBankiKontoBankowe = new Table();
            tableBankiKontoBankowe.Borders.Width = 0.0;
            Column columnBankiKontoBankowe1 = tableBankiKontoBankowe.AddColumn(Unit.FromCentimeter(5));
            columnBankiKontoBankowe1.Format.Alignment = ParagraphAlignment.Right;
            Column columnBankiKontoBankowe2 = tableBankiKontoBankowe.AddColumn(Unit.FromCentimeter(9));
            columnBankiKontoBankowe2.Format.Alignment = ParagraphAlignment.Left;
            Row rowBankiKontoBankowe = tableBankiKontoBankowe.AddRow();
            Cell cellBankiKontoBankowe = rowBankiKontoBankowe.Cells[0];
            cellBankiKontoBankowe = rowBankiKontoBankowe.Cells[0];
            cellBankiKontoBankowe.AddParagraph("Bank: ");
            cellBankiKontoBankowe.Format.Font.Bold = true;
            cellBankiKontoBankowe = rowBankiKontoBankowe.Cells[1];
            cellBankiKontoBankowe.AddParagraph(txtbankSprzedawcy.Text);
            cellBankiKontoBankowe.Format.Font.Bold = true;

            //def konto bankowe 
            Row rowKontoBankowe = tableBankiKontoBankowe.AddRow();
            Cell cellKontoBankowe = rowKontoBankowe.Cells[0];
            cellKontoBankowe = rowKontoBankowe.Cells[0];
            cellKontoBankowe.AddParagraph("Konto: ");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellKontoBankowe.Format.Font.Bold = true;

            cellKontoBankowe = rowKontoBankowe.Cells[1];
            cellKontoBankowe.AddParagraph(txtkontoSprzedawcy.Text);
            cellKontoBankowe.Format.Font.Bold = true;

            Row rowEmpty4 = tableBankiKontoBankowe.AddRow();

            // chcę, aby 'POZYCJE FAKTURY' było napisane nie w obramowaniu a nad tabelą, robię więc manewr polegający na przedłużeniu niewidocznej tabeli 
            Row rowPozycjeFaktury = tableBankiKontoBankowe.AddRow();
            Cell cellPozycjeFaktury = rowPozycjeFaktury.Cells[1];
            cellPozycjeFaktury = rowPozycjeFaktury.Cells[1];
            cellPozycjeFaktury.AddParagraph("POZYCJE FAKTURY");
            cellPozycjeFaktury.Format.Font.Bold = true;
            rowPozycjeFaktury.Format.Alignment = ParagraphAlignment.Left;

            doc.LastSection.Add(tableSN);
            doc.LastSection.Add(tableBankiKontoBankowe);

            //def table Pozycje-Faktury(PF)
            Table table = new Table();
            table.Borders.Width = 0.5;

            //def column Lp
            Column column = table.AddColumn(Unit.FromCentimeter(1));

            //def colum NazwaTowaruLubUsługi
            _ = table.AddColumn(Unit.FromCentimeter(6));
            // def column Ilość 
            _ = table.AddColumn(Unit.FromCentimeter(1));
            // def column Jednostka
            _ = table.AddColumn(Unit.FromCentimeter(1.5));
            // def column Wartość jednostkowa brutto
            _ = table.AddColumn(Unit.FromCentimeter(4));
            // def column Wartość brutto
            _ = table.AddColumn(Unit.FromCentimeter(3));

            //def header of table
            Row row = table.AddRow();

            Cell cell0 = row.Cells[0];
            cell0.AddParagraph("Lp.");
            cell0.Format.Font.Bold = true;
            cell0.Format.Alignment = ParagraphAlignment.Center;

            Cell cell1 = row.Cells[1];
            cell1.AddParagraph("Nazwa towaru lub usługi");
            cell1.Format.Font.Bold = true;
            cell1.Format.Alignment = ParagraphAlignment.Center;

            Cell cell2 = row.Cells[2];
            cell2.AddParagraph("Ilość");
            cell2.Format.Font.Bold = true;
            cell2.Format.Alignment = ParagraphAlignment.Center;

            Cell cell3 = row.Cells[3];
            cell3.AddParagraph("Jedn.");
            cell3.Format.Font.Bold = true;
            cell3.Format.Alignment = ParagraphAlignment.Center;

            Cell cell4 = row.Cells[4];
            cell4.AddParagraph("Wartość jednostkowa brutto PLN");
            cell4.Format.Font.Bold = true;
            cell4.Format.Alignment = ParagraphAlignment.Center;

            Cell cell5 = row.Cells[5];
            cell5.AddParagraph("Wartość brutto \n PLN");
            cell5.Format.Font.Bold = true;
            cell5.Format.Alignment = ParagraphAlignment.Center;
            if (chkPozycja11.IsChecked == true)
            {
                Row row2 = table.AddRow();
                Cell cellR2C0 = row2.Cells[0];
                cellR2C0.AddParagraph(txtLp1.Text);
                cellR2C0.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C1 = row2.Cells[1];
                cellR2C1.AddParagraph(txtnazwaTowaruLubUslugi1.Text);
                cellR2C1.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C2 = row2.Cells[2];
                cellR2C2.AddParagraph(txtilosc1.Text);
                cellR2C2.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C3 = row2.Cells[3];
                cellR2C3.AddParagraph(txtjednostka1.Text);
                cellR2C3.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C4 = row2.Cells[4];
                cellR2C4.AddParagraph(txtwartoscJednostkowaBrutto1.Text);
                cellR2C4.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C5 = row2.Cells[5];
                cellR2C5.AddParagraph(txtwartoscBrutto1.Text);
                cellR2C5.Format.Alignment = ParagraphAlignment.Center;
            }
            if (chkPozycja2.IsChecked == true)
            {
                Row row2 = table.AddRow();
                Cell cellR2C0 = row2.Cells[0];
                cellR2C0.AddParagraph(txtLp2.Text);
                cellR2C0.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C1 = row2.Cells[1];
                cellR2C1.AddParagraph(txtnazwaTowaruLubUslugi2.Text);
                cellR2C1.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C2 = row2.Cells[2];
                cellR2C2.AddParagraph(txtilosc2.Text);
                cellR2C2.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C3 = row2.Cells[3];
                cellR2C3.AddParagraph(txtjednostka2.Text);
                cellR2C3.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C4 = row2.Cells[4];
                cellR2C4.AddParagraph(txtwartoscJednostkowaBrutto2.Text);
                cellR2C4.Format.Alignment = ParagraphAlignment.Center;

                Cell cellR2C5 = row2.Cells[5];
                cellR2C5.AddParagraph(txtwartoscBrutto2.Text);
                cellR2C5.Format.Alignment = ParagraphAlignment.Center;
            }

            //add table to document
            doc.LastSection.Add(table);

            Paragraph slownie = sec.AddParagraph();
            slownie.Format.Font.Bold = true;
            slownie.AddText("\n " + txtslownie.Text);

            //def tablePODSUMOWANIE Razem, Zapłacono, Pozostało do zapłaty
            Table tablePodsumowanie = new Table();
            tablePodsumowanie.Borders.Width = 0.5;
            Column columnPodsumowanie1 = tablePodsumowanie.AddColumn(Unit.FromCentimeter(8.25));
            columnPodsumowanie1.Format.Alignment = ParagraphAlignment.Right;
            Column columnPodsumowanie2 = tablePodsumowanie.AddColumn(Unit.FromCentimeter(8.25));
            columnPodsumowanie2.Format.Alignment = ParagraphAlignment.Center;
            Row rowPodsumowanie1 = tablePodsumowanie.AddRow();
            Cell cellPodsumowanie1 = rowPodsumowanie1.Cells[0];
            cellPodsumowanie1 = rowPodsumowanie1.Cells[0];
            cellPodsumowanie1.AddParagraph("Razem");
            cellPodsumowanie1.Format.Font.Bold = true;
            cellPodsumowanie1 = rowPodsumowanie1.Cells[1];
            cellPodsumowanie1.AddParagraph(txtrazem.Text);
            cellPodsumowanie1.Format.Font.Bold = false;

            Row rowPodsumowanie2 = tablePodsumowanie.AddRow();
            Cell cellPodsumowanie2 = rowPodsumowanie2.Cells[0];
            cellPodsumowanie2 = rowPodsumowanie2.Cells[0];
            cellPodsumowanie2.AddParagraph("Zapłacono");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellPodsumowanie2.Format.Font.Bold = true;

            cellPodsumowanie2 = rowPodsumowanie2.Cells[1];
            cellPodsumowanie2.AddParagraph(txtZaplacono.Text);
            cellPodsumowanie2.Format.Font.Bold = false;

            Row rowPodsumowanie3 = tablePodsumowanie.AddRow();
            Cell cellPodsumowanie3 = rowPodsumowanie3.Cells[0];
            cellPodsumowanie3 = rowPodsumowanie3.Cells[0];
            cellPodsumowanie3.AddParagraph("Pozostało do zaplaty");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellPodsumowanie3.Format.Font.Bold = true;

            cellPodsumowanie3 = rowPodsumowanie3.Cells[1];
            cellPodsumowanie3.AddParagraph(txtPozostaloDoZaplaty.Text);
            cellPodsumowanie3.Format.Font.Bold = false;

            doc.LastSection.Add(tablePodsumowanie);

            Paragraph uwagi = sec.AddParagraph();
            uwagi.Format.Font.Bold = false;
            uwagi.AddText("UWAGI: " + txtuwagi.Text + " \n \n \n" + txtuwagi2.Text + " \n \n \n");

            ////PONIŻEJ INACZEJ GDY ZALICZKA
            //if (uwagaoZaliczce == true) txtuwagi2.Text = "Dokument wystawiany do Faktura zaliczka 1-" + faktura.stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text + " z dnia " + txtdataWystawienia.Text;
            //else txtuwagi2.Text = "";

           //def table bank i konto bankowe
           Table tablePodpisy = new Table();
            tablePodpisy.Borders.Width = 0.0;
            Column columnPodpisy1 = tablePodpisy.AddColumn(Unit.FromCentimeter(8.25));
            columnPodpisy1.Format.Alignment = ParagraphAlignment.Center;
            Column columnPodpisy2 = tablePodpisy.AddColumn(Unit.FromCentimeter(8.25));
            columnPodpisy2.Format.Alignment = ParagraphAlignment.Center;
            Row rowPodpisy = tablePodpisy.AddRow();
            Cell cellPodpisy = rowPodpisy.Cells[0];
            cellPodpisy = rowPodpisy.Cells[0];
            cellPodpisy.AddParagraph("Faktura bez podpisu odbiorcy faktury ");
            cellPodpisy = rowPodpisy.Cells[1];
            cellPodpisy.AddParagraph("Osoba upoważniona do wystawienia faktury ");


            Row rowPodpisy2 = tablePodpisy.AddRow();
            Cell cellPodpisy2 = rowPodpisy2.Cells[0];
            cellPodpisy2 = rowPodpisy.Cells[0];
            cellPodpisy2.AddParagraph(" ");

            cellPodpisy2 = rowPodpisy.Cells[1];
            cellPodpisy2.AddParagraph("\n \n " + txtosobaUpowaznionaDoWystawieniaFaktury.Text);

            doc.LastSection.Add(tablePodpisy);

            // Create a renderer and prepare (=layout) the document
            MigraDoc.Rendering.DocumentRenderer docRenderer = new MigraDoc.Rendering.DocumentRenderer(doc);
            docRenderer.PrepareDocument();
            gfx.MUH = PdfFontEncoding.Unicode;

            docRenderer.RenderPage(gfx, 1);

            string filename = txtnumerFaktury.Text + ".pdf"; //When drawing is done, write the file
            document.Save(filename); // Save the document...
        }
        private void txtdataWystawienia2_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtdataWystawienia.Text = txtdataWystawienia2.Text;
        }
        private void txtdataSprzedazy2_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtdataSprzedazy.Text = txtdataSprzedazy2.Text;
        }

        private void WczytajzBazy(int numerzListy)
        {
            try
            {//TUTAJ RTY - CATCH DLA OPCJI BRAK BAZY
                using (var reader = new StreamReader("xb_baza.csv")) //"path\\to\\file.csv"
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var listaAll = csvReader.GetRecords<Invoice>().ToList();
                        wczytajProformyDlaDanegoMca();
                        //TUTAJ ZARAZ DODAMY OPCJĘ DLA WCZYTAJ KOŃCOWE
                        if (optAll.IsChecked == true)
                        {
                            string pomocniczy;
                            txtnumerFaktury.Text = "-" + listaAll[numerzListy].stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text;
                            if (listaAll[numerzListy].sposobZaplaty == "P" | listaAll[numerzListy].sposobZaplaty == "P") { pomocniczy = "przelew"; }
                            else if (listaAll[numerzListy].sposobZaplaty == "G" | listaAll[numerzListy].sposobZaplaty == "g") { pomocniczy = "GOTÓWKA"; }
                            else if (listaAll[numerzListy].sposobZaplaty == "PG" | listaAll[numerzListy].sposobZaplaty == "pg") { pomocniczy = "przelew/GOTÓWKA"; }
                            else { pomocniczy = "BŁĄD"; }
                            txtsposobZaplaty.Text = pomocniczy;
                            txtnazwaNabywcy.Text = listaAll[numerzListy].nazwaNabywcy;
                            txtulicaNabywcy.Text = listaAll[numerzListy].ulicaNabywcy;
                            txtmiastoiKodNabywcy.Text = listaAll[numerzListy].miastoiKodNabywcy;
                            txtnipNabywcy.Text = listaAll[numerzListy].nipNabywcy;
                            if (listaAll[numerzListy].wybranyOkres == "1") { pomocniczy = "1-MIESIĄC"; }
                            else if (listaAll[numerzListy].wybranyOkres == "3") { pomocniczy = "3-MIESIĄCE"; }
                            else if (listaAll[numerzListy].wybranyOkres == "6") { pomocniczy = "6-MIESIĘCY"; }
                            else if (listaAll[numerzListy].wybranyOkres == "12") { pomocniczy = "12-MIESIĘCY"; }
                            else { pomocniczy = "BŁĄD"; }
                            txtnazwaTowaruLubUslugi1.Text = "Abonament xBiuro - pakiet " + listaAll[numerzListy].wybranyPakiet + " na " + pomocniczy;
                            txtilosc1.Text = listaAll[numerzListy].ilosc;
                            txtwartoscJednostkowaBrutto1.Text = listaAll[numerzListy].wartoscJednostkowaBrutto;
                            txtwartoscBrutto1.Text = listaAll[numerzListy].wartoscBrutto;
                            txtrazem.Text = listaAll[numerzListy].razem;
                            pomocniczy = listaAll[numerzListy].razem.Substring(listaAll[numerzListy].razem.Length - 2) + "/100 groszy brutto";
                            decimal pomocniczyDec = decimal.Parse(listaAll[numerzListy].razem);
                            pomocniczyDec = Math.Truncate(pomocniczyDec);
                            int pomocniczyInt = Decimal.ToInt32(pomocniczyDec);
                            txtslownie.Text = "SŁOWNIE: " + AmointInWords.LiczbaSlownie(pomocniczyInt) + " " + AmointInWords.WalutaSlownie(pomocniczyInt, "PLN") + " i " + pomocniczy;
                            txtslownie.Text = txtslownie.Text.ToUpper();

                            lblKolejna.Content = "Dokument " + (++numerzListy).ToString() + " z " + (listaAll.Count).ToString();
                            btnZapisz.IsEnabled = true;
                            if (numerzListy == 1) btnBack.IsEnabled = false;
                            else btnBack.IsEnabled = true;
                            if (listaAll.Count == numerzListy) btnNext.IsEnabled = false;

                            else btnNext.IsEnabled = true;
                        }
                        else if (optProformy.IsChecked == true)
                        {
                            string pomocniczy;
                            try
                            {
                                txtnumerFaktury.Text = "1-PROFORMA-" + proformyDlaDanegoMiesiaca[numerzListy].stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text;
                                if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "P" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "P") { pomocniczy = "przelew"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "G" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "g") { pomocniczy = "GOTÓWKA"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "PG" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "pg") { pomocniczy = "przelew/GOTÓWKA"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtsposobZaplaty.Text = pomocniczy;
                                txtnazwaNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].nazwaNabywcy;
                                txtulicaNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].ulicaNabywcy;
                                txtmiastoiKodNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].miastoiKodNabywcy;
                                txtnipNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].nipNabywcy;
                                if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "1") { pomocniczy = "1-MIESIĄC"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "3") { pomocniczy = "3-MIESIĄCE"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "6") { pomocniczy = "6-MIESIĘCY"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "12") { pomocniczy = "12-MIESIĘCY"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtnazwaTowaruLubUslugi1.Text = "Abonament xBiuro - pakiet " + proformyDlaDanegoMiesiaca[numerzListy].wybranyPakiet + " na " + pomocniczy;
                                txtilosc1.Text = proformyDlaDanegoMiesiaca[numerzListy].ilosc;
                                txtwartoscJednostkowaBrutto1.Text = proformyDlaDanegoMiesiaca[numerzListy].wartoscJednostkowaBrutto;
                                txtwartoscBrutto1.Text = proformyDlaDanegoMiesiaca[numerzListy].wartoscBrutto;
                                txtrazem.Text = proformyDlaDanegoMiesiaca[numerzListy].razem;
                                txtZaplacono.Text = proformyDlaDanegoMiesiaca[numerzListy].razem;
                                txtPozostaloDoZaplaty.Text = proformyDlaDanegoMiesiaca[numerzListy].razem;
                                pomocniczy = proformyDlaDanegoMiesiaca[numerzListy].razem.Substring(proformyDlaDanegoMiesiaca[numerzListy].razem.Length - 2) + "/100 groszy brutto";
                                decimal pomocniczyDec = decimal.Parse(proformyDlaDanegoMiesiaca[numerzListy].razem);
                                pomocniczyDec = Math.Truncate(pomocniczyDec);
                                int pomocniczyInt = Decimal.ToInt32(pomocniczyDec);
                                txtslownie.Text = "SŁOWNIE: " + AmointInWords.LiczbaSlownie(pomocniczyInt) + " " + AmointInWords.WalutaSlownie(pomocniczyInt, "PLN") + " i " + pomocniczy;
                                txtslownie.Text = txtslownie.Text.ToUpper();

                                lblKolejna.Content = "Dokument " + (++numerzListy).ToString() + " z " + (proformyDlaDanegoMiesiaca.Count).ToString();
                                btnZapisz.IsEnabled = true;
                                if (numerzListy == 1) btnBack.IsEnabled = false;
                                else btnBack.IsEnabled = true;
                                Label1.Content = proformyDlaDanegoMiesiaca.Count.ToString();
                                if (proformyDlaDanegoMiesiaca.Count == numerzListy) btnNext.IsEnabled = false;
                                else btnNext.IsEnabled = true;
                            }
                            catch
                            {
                                wyzerujDesigner();
                                btnZapisz.IsEnabled = false;
                                MessageBox.Show("Nie ma takiej pozycji w bazie", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                        else if (optKoncowe.IsChecked == true)
                        {
                            //zrobić, by dużo zależało od daty zapłaty
                            string pomocniczy;
                            try
                            { 
                                txtnumerFaktury.Text = "Faktura 1-" + proformyDlaDanegoMiesiaca[numerzListy].stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text;
                                if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "P" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "P") { pomocniczy = "przelew"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "G" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "g") { pomocniczy = "GOTÓWKA"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "PG" | proformyDlaDanegoMiesiaca[numerzListy].sposobZaplaty == "pg") { pomocniczy = "przelew/GOTÓWKA"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtsposobZaplaty.Text = pomocniczy;
                                txtnazwaNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].nazwaNabywcy;
                                txtulicaNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].ulicaNabywcy;
                                txtmiastoiKodNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].miastoiKodNabywcy;
                                txtnipNabywcy.Text = proformyDlaDanegoMiesiaca[numerzListy].nipNabywcy;
                                if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "1") { pomocniczy = "1-MIESIĄC"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "3") { pomocniczy = "3-MIESIĄCE"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "6") { pomocniczy = "6-MIESIĘCY"; }
                                else if (proformyDlaDanegoMiesiaca[numerzListy].wybranyOkres == "12") { pomocniczy = "12-MIESIĘCY"; }
                                else { pomocniczy = "BŁĄD"; }
                                txtnazwaTowaruLubUslugi1.Text = "Abonament xBiuro - pakiet " + proformyDlaDanegoMiesiaca[numerzListy].wybranyPakiet + " na " + pomocniczy;
                                txtilosc1.Text = proformyDlaDanegoMiesiaca[numerzListy].ilosc;
                                txtwartoscJednostkowaBrutto1.Text = proformyDlaDanegoMiesiaca[numerzListy].wartoscJednostkowaBrutto;
                                txtwartoscBrutto1.Text = proformyDlaDanegoMiesiaca[numerzListy].wartoscBrutto;
                                txtrazem.Text = proformyDlaDanegoMiesiaca[numerzListy].razem;
                                txtZaplacono.Text= proformyDlaDanegoMiesiaca[numerzListy].razem;
                                txtPozostaloDoZaplaty.Text = "0,00";

                                pomocniczy = proformyDlaDanegoMiesiaca[numerzListy].razem.Substring(proformyDlaDanegoMiesiaca[numerzListy].razem.Length - 2) + "/100 groszy brutto";
                                decimal pomocniczyDec = decimal.Parse(proformyDlaDanegoMiesiaca[numerzListy].razem);
                                pomocniczyDec = Math.Truncate(pomocniczyDec);
                                int pomocniczyInt = Decimal.ToInt32(pomocniczyDec);
                                txtslownie.Text = "SŁOWNIE: " + AmointInWords.LiczbaSlownie(pomocniczyInt) + " " + AmointInWords.WalutaSlownie(pomocniczyInt, "PLN") + " i " + pomocniczy;
                                txtslownie.Text = txtslownie.Text.ToUpper();

                                lblKolejna.Content = "Dokument " + (++numerzListy).ToString() + " z " + (proformyDlaDanegoMiesiaca.Count).ToString();
                                btnZapisz.IsEnabled = true;
                                if (numerzListy == 1) btnBack.IsEnabled = false;
                                else btnBack.IsEnabled = true;
                                if (proformyDlaDanegoMiesiaca.Count == numerzListy) btnNext.IsEnabled = false;

                                else btnNext.IsEnabled = true;
                            }
                            catch
                            {
                                wyzerujDesigner();
                                btnZapisz.IsEnabled = false;
                                MessageBox.Show("Nie ma takiej pozycji w bazie", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                btnZapisz.IsEnabled = false;
                            }
                        }
                    }

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak bazy danych w folderze programu. Umieść plik xb_baza.csv folderze programu i spróbuj ponownie.", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void wyzerujDesigner()
        {
            txtnumerFaktury.Text = "---";
            txtsposobZaplaty.Text = "---";
            txtnazwaNabywcy.Text = "---";
            txtulicaNabywcy.Text = "---";
            txtmiastoiKodNabywcy.Text = "---";
            txtnipNabywcy.Text = "---";
            txtnazwaTowaruLubUslugi1.Text = "---";
            txtilosc1.Text = "---";
            txtwartoscJednostkowaBrutto1.Text = "---";
            txtwartoscBrutto1.Text = "---";
            txtrazem.Text = "---";
            txtslownie.Text = "SŁOWNIE: " + "---";
            lblKolejna.Content = "Brak danych w bazie spełniających kryteria";
            txtZaplacono.Text= "---";
            txtPozostaloDoZaplaty.Text = "---";
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            WczytajzBazy(++numerzListy);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            WczytajzBazy(--numerzListy);
        }
        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            //NAJPIERW WSTAWIAMY DATĘ PŁATNOŚCI
            //SPRAWDZAMY CZY DATA PŁATNOŚCI JEST PÓŹNIEJSZA OD DATY WYSTAWIANIA ->JEŚLI TAK TO KWOTA POZOSTAJE DO ZAPŁATY
            //JEŚLI NIE TO KWOTA JEST 'ZAPŁACONO'
            //JESLI DATA ZAPŁATY JEST Z WCZEŚJENISZEGO MIESIĄCA NIŻ DATA SPRZEDAŻY TO WYSTAWINA JEST FAKTURA ZALICZKA I TO W SPOSÓB TAKI, ŻEBY USER JĄ WIDZIAŁ

              
               // DateTime date1 = picker.SelectedDate;
            if (optKoncowe.IsChecked == true)
                {
                if (txtterminPlatnosci.Text == "")
                {
                    MessageBox.Show("Proszę wprowadzić faktyczną datę opłacenia faktury (pole: Termin zapłaty) w formacie DD-MM-RRRR", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                    if (czyTerazFakturaZaliczkowa())
                    {
                        string pomocniczy = "";
                        if (txtterminPlatnosci.Text.Substring(3, 1) == "0")
                        {
                            pomocniczy = txtterminPlatnosci.Text.Substring(4, 6);
                        }
                        else
                        {
                            pomocniczy = txtterminPlatnosci.Text.Substring(3, 7);
                        }

                        txtuwagi2.Text = "Dokument wystawiany do Faktura zaliczka 1-" + proformyDlaDanegoMiesiaca[numerzListy].stalaCzescNumeruFaktury + "-" + pomocniczy + " z dnia " + txtterminPlatnosci.Text;
                        //jak tutaj: czyTerazFakturaZaliczkowa()odcina z daty dzień, z pkt księgowości jest on nieistotny
                        generujPDF();//generujemy fakturę końcową z adnotacją o wystawionej do niej fakturze zaliczkowej

                        //poniżej zmiana danych na właściwe do wygenerowania faktury zaliczkowej
                        txtnumerFaktury.Text = "Faktura zaliczka 1-" + proformyDlaDanegoMiesiaca[numerzListy].stalaCzescNumeruFaktury + "-" + pomocniczy;
                        txtdataWystawienia.Text = txtterminPlatnosci.Text;
                        txtuwagi2.Text = "";
                        generujPDF();//Generujemy tym razem fakturę zaliczkową
                        MessageBox.Show("Do wystawionej faktury końcowej została automatycznie wygenerowana faktura zaliczkowa.", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        txtuwagi2.Text = "";
                        generujPDF();//generujemy fakturę końcową bez adnotacji o wystawionej do niej fakturze zaliczkowej
                    }
                }
            }
        }
        //PONIŻSZA metoda zwraca true, jeżeli miesiąc zapłaty jest wcześniejszy niż termin 
        //sprzedaży - czyli, gdy jest potrzebna faktura zaliczka
        //działa ona dla dat w formacie Daty sprzedaży: MM-RRRR oraz Daty zapłaty: DD-MM-RRRR
        public bool czyTerazFakturaZaliczkowa() 
        {
            
            string sterminPlatnosci = txtterminPlatnosci.Text;
            string sdataSprzedazy = txtdataSprzedazy.Text;

            sterminPlatnosci = sterminPlatnosci.Substring(3, 7).ToString();//odcina z daty dzień, z pkt księgowości jest on nieistotny

            DateTime terminPlatnosci = Convert.ToDateTime(sdataSprzedazy);
            DateTime dataSprzedazy = Convert.ToDateTime(sterminPlatnosci);
            
            int result = DateTime.Compare(dataSprzedazy, terminPlatnosci);
            // Label1.Content = terminPlatnosci.ToString();
            if (result < 0) return true;
            else return false;

        }

        public bool czyTerazProforma(Invoice faktura) //mechanizm sprawdzający czy w danym miesiącu kończy się okres abonamentowy
        {
            int p1 = 0;
            int p2 = 0;
              
            p1 = int.Parse(faktura.startPelnyMiesiac);
            p2 = int.Parse(faktura.wybranyOkres);

            int LicznikPomocniczy = p1;

            int miesiacA, miesiacB, miesiacC, miesiacD;
            int miesiacSprzedazyLiczba = 0;

            if (txtdataSprzedazy.Text.Length == 6)
            {
                miesiacSprzedazyLiczba = int.Parse(txtdataSprzedazy.Text.Remove(1).ToString());
            }
            else if (txtdataSprzedazy.Text.Length == 7)
            {
                miesiacSprzedazyLiczba = int.Parse(txtdataSprzedazy.Text.Remove(2).ToString());
            }
            
            if (faktura.wybranyOkres == "1")
            {
                return true; //jest 12 punktów (tj. momentów) płatności w roku, ponieważ proformy genruję raz na miesiąc, to za każdym razem jest true
            }
            else if (faktura.wybranyOkres == "3")
            {
                miesiacA = LicznikPomocniczy + p2; //są 4 punkty (tj. miesiące) w roku , kiedy należy wystawić taką proformę    
                miesiacB = LicznikPomocniczy + p2 + p2;//7
                miesiacC = LicznikPomocniczy + p2 + p2 + p2;//itd
                miesiacD = LicznikPomocniczy + p2 + p2 + p2 + p2;
                // Label1.Content = miesiacB;
                if (miesiacA > 12) miesiacA = miesiacA - 12; //to nie zajdzie w opcji okresu abonamntowego 3mce ale kod niech będzie jak najbardziej uniwersalny
                if (miesiacB > 12) miesiacB = miesiacB - 12;
                if (miesiacC > 12) miesiacC = miesiacC - 12;
                if (miesiacD > 12) miesiacD = miesiacD - 12;
                if ((miesiacA == miesiacSprzedazyLiczba) ^ (miesiacB == miesiacSprzedazyLiczba) ^ (miesiacC == miesiacSprzedazyLiczba) ^ (miesiacD == miesiacSprzedazyLiczba))
                {
                    return true;
                }
            }
            else if (faktura.wybranyOkres == "6")
            {
                miesiacA = LicznikPomocniczy + p2; //są 2 punkty (tj. miesiące) w roku , kiedy należy wystawić taką proformę
                miesiacB = LicznikPomocniczy + p2 + p2;
                if (miesiacA > 12) miesiacA = miesiacA - 12;
                if (miesiacB > 12) miesiacB = miesiacB - 12;
                // int miesiacSprzedazyLiczba = int.Parse(txtdataSprzedazy.Text.Substring(txtdataSprzedazy.Text.Length - 5));
                if ((miesiacA == miesiacSprzedazyLiczba) ^ (miesiacB == miesiacSprzedazyLiczba))
                {
                    return true;
                }
            }
            if (faktura.wybranyOkres == "12" && p1 == miesiacSprzedazyLiczba)
                    {
                // int miesiacSprzedazyLiczba = int.Parse(txtdataSprzedazy.Text.Substring(txtdataSprzedazy.Text.Length - 5));                         
                return true;
            } 
            else
            {
                return false;
            }
        }

        public void wczytajProformyDlaDanegoMca()
        {
            proformyDlaDanegoMiesiaca.Clear();
            using (var reader = new StreamReader("xb_baza.csv")) //"path\\to\\file.csv"
            {
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var faktury = csvReader.GetRecords<Invoice>();//.ToList();
                    foreach (var faktura in faktury)
                    {
                        if (czyTerazProforma(faktura))
                        {
                            string pomocniczy;
                            txtnumerFaktury.Text = "1-PROFORMA-" + faktura.stalaCzescNumeruFaktury + "-" + txtdataSprzedazy.Text;
                            if (faktura.sposobZaplaty == "P" | faktura.sposobZaplaty == "P") { pomocniczy = "przelew"; }
                            else if (faktura.sposobZaplaty == "G" | faktura.sposobZaplaty == "g") { pomocniczy = "GOTÓWKA"; }
                            else if (faktura.sposobZaplaty == "PG" | faktura.sposobZaplaty == "pg") { pomocniczy = "przelew/GOTÓWKA"; }
                            else { pomocniczy = "BŁĄD"; }
                            txtsposobZaplaty.Text = pomocniczy;
                            txtnazwaNabywcy.Text = faktura.nazwaNabywcy;
                            txtulicaNabywcy.Text = faktura.ulicaNabywcy;
                            txtmiastoiKodNabywcy.Text = faktura.miastoiKodNabywcy;
                            txtnipNabywcy.Text = faktura.nipNabywcy;
                            if (faktura.wybranyOkres == "1") { pomocniczy = "1-MIESIĄC"; }
                            else if (faktura.wybranyOkres == "3") { pomocniczy = "3-MIESIĄCE"; }
                            else if (faktura.wybranyOkres == "6") { pomocniczy = "6-MIESIĘCY"; }
                            else if (faktura.wybranyOkres == "12") { pomocniczy = "12-MIESIĘCY"; }
                            else { pomocniczy = "BŁĄD"; }
                            txtnazwaTowaruLubUslugi1.Text = "Abonament xBiuro - pakiet " + faktura.wybranyPakiet + " na " + pomocniczy;
                            txtilosc1.Text = faktura.ilosc;
                            txtwartoscJednostkowaBrutto1.Text = faktura.wartoscJednostkowaBrutto;
                            txtwartoscBrutto1.Text = faktura.wartoscBrutto;
                            txtrazem.Text = faktura.razem;
                            pomocniczy = faktura.razem.Substring(faktura.razem.Length - 2) + "/100 groszy brutto";
                            decimal pomocniczyDec = decimal.Parse(faktura.razem);
                            pomocniczyDec = Math.Truncate(pomocniczyDec);
                            int pomocniczyInt = Decimal.ToInt32(pomocniczyDec);
                            txtslownie.Text = "SŁOWNIE: " + AmointInWords.LiczbaSlownie(pomocniczyInt) + " " + AmointInWords.WalutaSlownie(pomocniczyInt, "PLN") + " i " + pomocniczy;
                            txtslownie.Text = txtslownie.Text.ToUpper();

                            proformyDlaDanegoMiesiaca.Add(faktura);
                            //if (faktura.czyZaplacono == "T") txtZaplacono.Text = faktura.razem;
                            //else if (faktura.czyZaplacono == "N") txtPozostaloDoZaplaty.Text = faktura.razem;
                        }
                    }
                }
            }
        }

        private void txtdataWystawienia_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtdataWystawienia2.Text = txtdataWystawienia.Text;
        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            if (optProformy.IsChecked==true)
            {
                numerzListy = 0;
                WczytajzBazy(numerzListy);
            } 
            else if (optKoncowe.IsChecked == true)
            {
                numerzListy = 0;
                txtterminPlatnosci.Text = "";
                WczytajzBazy(numerzListy);
            } else if (optAll.IsChecked == true)
            {
                numerzListy = 0;
                WczytajzBazy(numerzListy);
            }
            else
            {
                MessageBox.Show("Proszę zaznaczyć jedną z powyższych opcji", "Faktury 2020", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtterminPlatnosciCopy_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtterminPlatnosci.Text = txtterminPlatnosciCopy.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //UWAGA TO DZIAŁA!!!
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            // from
            message.From.Add(new MailboxAddress("from_name", "teraz@xbiuro.com"));
            // to
            message.To.Add(new MailboxAddress("to_name", "loker7@wp.pl"));
            // reply to
            message.ReplyTo.Add(new MailboxAddress("reply_name", "loker7@wp.pl"));

            message.Subject = "MAil testowy z aplikacji";
            bodyBuilder.HtmlBody = "html body";
            message.Body = bodyBuilder.ToMessageBody();
            bodyBuilder.Attachments.Add(@"SKM_C.pdf"); //tu może być pełna ścieżka
            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect("loker7.mail.ncse.pl", 465, SecureSocketOptions.SslOnConnect);
            client.Authenticate("teraz@xbiuro.com", "123Test");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}