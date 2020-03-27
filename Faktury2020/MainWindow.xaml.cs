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

namespace Faktury2020
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }


       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string[] texts = new string[]
{
  // International version of the text in English.
  "English\n" +
  "PDFsharp is a .NET library for creating and processing PDF documents 'on the fly'. " +
  "The library is completely written in C# and based exclusively on safe, managed code. " +
  "PDFsharp offers two powerful abstraction levels to create and process PDF documents.\n" +
  "For drawing text, graphics, and images there is a set of classes which are modeled similar to the classes " +
  "of the name space System.Drawing of the .NET framework. With these classes it is not only possible to create " +
  "the content of PDF pages in an easy way, but they can also be used to draw in a window or on a printer.\n" +
  "Additionally PDFsharp completely models the structure elements PDF is based on. With them existing PDF documents " +
  "can be modified, merged, or split with ease.\n" +
  "The source code of PDFsharp is Open Source under the MIT license (http://en.wikipedia.org/wiki/MIT_License). " +
  "Therefore it is possible to use PDFsharp without limitations in non open source or commercial projects/products.",
 
  // PDFsharp is 'Made in Germany'.
  "German (deutsch)\n" +
  "PDFsharp ist eine .NET-Bibliothek zum Erzeugen und Verarbeiten von PDF-Dokumenten 'On the Fly'. " +
  "Die Bibliothek ist vollständig in C# geschrieben und basiert ausschließlich auf sicherem, verwaltetem Code. " +
  "PDFsharp bietet zwei leistungsstarke Abstraktionsebenen zur Erstellung und Verarbeitung von PDF-Dokumenten.\n" +
  "Zum Zeichnen von Text, Grafik und Bildern gibt es einen Satz von Klassen, die sehr stark an die Klassen " +
  "des Namensraums System.Drawing des .NET Frameworks angelehnt sind. Mit diesen Klassen ist es nicht " +
  "nur auf einfache Weise möglich, den Inhalt von PDF-Seiten zu gestalten, sondern sie können auch zum " +
  "Zeichnen in einem Fenster oder auf einem Drucker verwendet werden.\n" +
  "Zusätzlich modelliert PDFsharp vollständig die Stukturelemente, auf denen PDF basiert. Dadurch können existierende " +
  "PDF-Dokumente mit Leichtigkeit zerlegt, ergänzt oder umgebaut werden.\n" +
  "Der Quellcode von PDFsharp ist Open-Source unter der MIT-Lizenz (http://de.wikipedia.org/wiki/MIT-Lizenz). " +
  "Damit kann PDFsharp auch uneingeschränkt in Nicht-Open-Source- oder kommerziellen Projekten/Produkten eingesetzt werden.",
 
  // Greek version.
  // The text was translated by Babel Fish. We here in Germany have no idea what it means.
  // If you are a native speaker please correct it and mail it to mailto:PDFsharpSupport@pdfsharp.de
  "Greek (Translated with Babel Fish)\n" +
  "Το PDFsharp είναι βιβλιοθήκη δικτύου α. για τη δημιουργία και την επεξεργασία των εγγράφων PDF 'σχετικά με τη μύγα'. " +
  "Η βιβλιοθήκη γράφεται εντελώς γ # και βασίζεται αποκλειστικά εκτός από, διοικούμενος κώδικας. " +
  "Το PDFsharp προσφέρει δύο ισχυρά επίπεδα αφαίρεσης για να δημιουργήσει και να επεξεργαστεί τα έγγραφα PDF. " +
  "Για το κείμενο, τη γραφική παράσταση, και τις εικόνες σχεδίων υπάρχει ένα σύνολο κατηγοριών που διαμορφώνονται " +
  "παρόμοιος με τις κατηγορίες του διαστημικού σχεδίου συστημάτων ονόματος του. πλαισίου δικτύου. " +
  "Με αυτές τις κατηγορίες που είναι όχι μόνο δυνατό να δημιουργηθεί το περιεχόμενο των σελίδων PDF με έναν εύκολο " +
  "τρόπο, αλλά αυτοί μπορεί επίσης να χρησιμοποιηθεί για να επισύρει την προσοχή σε ένα παράθυρο ή σε έναν εκτυπωτή. " +
  "Επιπλέον PDFsharp διαμορφώνει εντελώς τα στοιχεία PDF δομών είναι βασισμένο. Με τους τα υπάρχοντα έγγραφα PDF " +
  "μπορούν να τροποποιηθούν, συγχωνευμένος, ή να χωρίσουν με την ευκολία. Ο κώδικας πηγής PDFsharp είναι ανοικτή πηγή " +
  "με άδεια MIT (http://en.wikipedia.org/wiki/MIT_License). Επομένως είναι δυνατό να χρησιμοποιηθεί PDFsharp χωρίς " +
  "προβλήματα στη μη ανοικτή πηγή ή τα εμπορικά προγράμματα/τα προϊόντα.",
 
  // Russian version (by courtesy of Alexey Kuznetsov).
  "Russian\n" +
  "PDFsharp это .NET библиотека для создания и обработки PDF документов 'налету'. " +
  "Библиотека полностью написана на языке C# и базируется исключительно на безопасном, управляемом коде. " +
  "PDFsharp использует два мощных абстрактных уровня для создания и обработки PDF документов.\n" +
  "Для рисования текста, графики, и изображений в ней используется набор классов, которые разработаны аналогично с" +
  "пакетом System.Drawing, библиотеки .NET framework. С помощью этих классов возможно не только создавать" +
  "содержимое PDF страниц очень легко, но они так же позволяют рисовать напрямую в окне приложения или на принтере.\n" +
  "Дополнительно PDFsharp имеет полноценные модели структурированных базовых элементов PDF. Они позволяют работать с существующим PDF документами " +
  "для изменения их содержимого, склеивания документов, или разделения на части.\n" +
  "Исходный код PDFsharp библиотеки это Open Source распространяемый под лицензией MIT (http://ru.wikipedia.org/wiki/MIT_License). " +
  "Теоретически она позволяет использовать PDFsharp без ограничений в не open source проектах или коммерческих проектах/продуктах.",
 
  // French version (by courtesy of Olivier Dalet).
  "French (Français)\n" +
  "PDFSharp est une librairie .NET permettant de créer et de traiter des documents PDF 'à la volée'. " +
  "La librairie est entièrement écrite en C# et exclusivement basée sur du code sûr et géré. " +
  "PDFSharp fournit deux puissants niveaux d'abstraction pour la création et le traitement des documents PDF.\n" +
  "Un jeu de classes, modélisées afin de ressembler aux classes du namespace System.Drawing du framework .NET, " +
  "permet de dessiner du texte, des graphiques et des images. Non seulement ces classes permettent la création du " +
  "contenu des pages PDF de manière aisée, mais elles peuvent aussi être utilisées pour dessiner dans une fenêtre ou pour l'imprimante.\n" +
  "De plus, PDFSharp modélise complètement les éléments structurels de PDF. Ainsi, des documents PDF existants peuvent être " +
  "facilement modifiés, fusionnés ou éclatés.\n" +
  "Le code source de PDFSharp est Open Source sous licence MIT (http://fr.wikipedia.org/wiki/Licence_MIT). " +
  "Il est donc possible d'utiliser PDFSharp sans limitation aucune dans des projets ou produits non Open Source ou commerciaux.",
 
  // Dutch version (by giCalle)
  "Dutch\n" +
  "PDFsharp is een .NET bibliotheek om PDF documenten te creëren en te verwerken. " +
  "De bibliotheek is volledig geschreven in C# en gebruikt uitsluitend veilige, 'managed code'. " +
  "PDFsharp biedt twee krachtige abstractie niveaus aan om PDF documenten te maken en te verwerken.\n" +
  "Om tekst, beelden en foto's weer te geven zĳn er een reeks klassen beschikbaar, gemodelleerd naar de klassen " +
  "uit de 'System.Drawing' naamruimte van het .NET framework. Met behulp van deze klassen is het niet enkel mogelĳk " +
  "om de inhoud van PDF pagina's aan te maken op een eenvoudige manier, maar ze kunnen ook gebruikt worden om dingen " +
  "weer te geven in een venster of naar een printer. Daarbovenop implementeert PDFsharp de volledige elementen structuur " +
  "waarop PDF is gebaseerd. Hiermee kunnen bestaande PDF documenten eenvoudig aangepast, samengevoegd of opgesplitst worden.\n" +
  "De broncode van PDFsharp is opensource onder een MIT licentie (http://nl.wikipedia.org/wiki/MIT-licentie). " +
  "Daarom is het mogelĳk om PDFsharp te gebruiken zonder beperkingen in niet open source of commerciële projecten/producten.",
 
  // Danish version (by courtesy of Mikael Lyngvig).
  "Danish (Dansk)\n" +
  "PDFsharp er et .NET bibliotek til at dynamisk lave og behandle PDF dokumenter. " +
  "Biblioteket er skrevet rent i C# og indeholder kun sikker, managed kode. " +
  "PDFsharp tilbyder to stærke abstraktionsniveauer til at lave og behandle PDF dokumenter. " +
  "Til at tegne tekst, grafik og billeder findes der et sæt klasser som er modelleret ligesom klasserne i navnerummet " +
  "System.Drawing i .NET biblioteket. Med disse klasser er det ikke kun muligt at udforme indholdet af PDF siderne på en " +
  "nem måde – de kan også bruges til at tegne i et vindue eller på en printer. " +
  "Derudover modellerer PDFsharp fuldstændigt strukturelementerne som PDF er baseret på. " +
  "Med dem kan eksisterende PDF dokumenter nemt modificeres, sammenknyttes og adskilles. " +
  "Kildekoden til PDFsharp er Open Source under MIT licensen (http://da.wikipedia.org/wiki/MIT-Licensen). " +
  "Derfor er det muligt at bruge PDFsharp uden begrænsninger i både lukkede og kommercielle projekter og produkter.",
 
  // Portuguese version (by courtesy of Luís Rodrigues).
  "Portuguese (Português)\n" +
  "PDFsharp é uma biblioteca .NET para a criação e processamento de documentos PDF 'on the fly'." +
  "A biblioteca é completamente escrita em C# e baseada exclusivamente em código gerenciado e seguro. " +
  "O PDFsharp oferece dois níveis de abstração poderosa para criar e processar documentos PDF.\n" +
  "Para desenhar texto, gráficos e imagens, há um conjunto de classes que são modeladas de forma semelhante às classes " +
  "do espaço de nomes System.Drawing do framework .NET. Com essas classes não só é possível criar " +
  "o conteúdo das páginas PDF de uma maneira fácil, mas podem também ser usadas para desenhar numa janela ou numa impressora.\n" +
  "Adicionalmente, o PDFSharp modela completamente a estrutura dos elementos em que o PDF é baseado. Com eles, documentos PDF existentes " +
  "podem ser modificados, unidos, ou divididos com facilidade.\n" +
  "O código fonte do PDFsharp é Open Source sob a licença MIT (http://en.wikipedia.org/wiki/MIT_License). " +
  "Por isso, é possível usar o PDFsharp sem limitações em projetos/produtos não open source ou comerciais.",
 
  // Polish version (by courtesy of Krzysztof Jędryka)
  "Polish (polski)\n" +
  "PDFsharp jest  biblioteką .NET umożliwiającą tworzenie i przetwarzanie dokumentów PDF 'w locie'. " +
  "Biblioteka ta została stworzona w całości w języku C# i jest oparta wyłącznie na bezpiecznym i zarządzanym kodzie. " +
  "PDFsharp oferuje dwa rozbudowane poziomy abstrakcji do tworzenia i przetwarzania dokumentów PDF.\n" +
  "Do rysowania tekstu, grafiki i obrazów stworzono zbiór klas projektowanych na wzór klas przestrzeni nazw System.Drawing" +
  "platformy .NET. Z pomocą tych klas można tworzyć w wygodny sposób nie tylko zawartość stron dokumentu PDF, ale można również" +
  "rysować w oknie programu lub generować wydruki.\n" +
  "Ponadto PDFsharp w pełni odwzorowuje strukturę elementów na których opiera się format pliku PDF." +
  "Używając tych elementów, dokumenty PDF można modyfikować, łączyć lub dzielić z łatwością.\n" +
  "Kod źródłowy PDFsharp jest dostępny na licencji Open Source MIT (http://pl.wikipedia.org/wiki/Licencja_MIT). " +
  "Zatem można korzystać z PDFsharp bez żadnych ograniczeń w projektach niedostępnych dla społeczności Open Source lub komercyjnych.",
 
 
  // Your language may come here.
  "Invitation\n" +
  "If you use PDFsharp and haven't found your native language in this document, we will be pleased to get your translation of the text above and include it here.\n" +
  "Mail to PDFsharpSupport@pdfsharp.de"
};

            // Create new document
            PdfDocument document = new PdfDocument();

            // Set font encoding to unicode
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            XFont font = new XFont("Times New Roman", 12, XFontStyle.Regular, options);

            // Draw text in different languages
            for (int idx = 0; idx < texts.Length; idx++)
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.Alignment = XParagraphAlignment.Left;

                tf.DrawString(texts[idx], font, XBrushes.Black,
                  new XRect(100, 100, page.Width - 200, 600), XStringFormats.TopLeft);
            }

            const string filename = "Unicode_tempfile.pdf";
            // Save the document...
            document.Save(filename);
            // ...and start a viewer.
            // Process.Start(filename);


        }


      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Books books = new Books();

            PdfDocument document = new PdfDocument(); // Create a new PDF document
            document.Info.Title = "Created with PDFsharp";
            document.Info.Author = "Faktury2020";
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
            cellMiejsceiDataWystawienia.AddParagraph("Katowice");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;


            cellMiejsceiDataWystawienia = rowMiejsceiDataWystawienia.Cells[1];
            cellMiejsceiDataWystawienia.AddParagraph("Wystawiono dnia: 16-03-2020");

            doc.LastSection.Add(tableMiejsceiDataWystawienia);

            //Paragraph miasto = sec.AddParagraph();
            //miasto.AddText("Katowice");
            //miasto.Format.Alignment = ParagraphAlignment.Left;

            //Paragraph dataWystawienia = sec.AddParagraph();
            //dataWystawienia.AddText("Wystawiono dnia: 16-03-2020");
            //dataWystawienia.Format.Alignment = ParagraphAlignment.Right;

            sec.AddParagraph();

            Paragraph numerFaktury = sec.AddParagraph();
            numerFaktury.AddText("Faktura proforma 1-TEST-2-2020");
            numerFaktury.Format.Font.Bold = true;
            numerFaktury.Format.Alignment = ParagraphAlignment.Center;

            sec.AddParagraph();

            Paragraph dataSprzedazy = sec.AddParagraph();
            dataSprzedazy.AddText("Data sprzedaży: 2-2020");
            dataSprzedazy.Format.Font.Bold = true;
            dataSprzedazy.Format.Alignment = ParagraphAlignment.Right;

            Paragraph sposobZaplaty = sec.AddParagraph();
            sposobZaplaty.AddText("Forma płatności: GOTÓWKA");
            sposobZaplaty.Format.Font.Bold = true;
            sposobZaplaty.Format.Alignment = ParagraphAlignment.Right;
            
            Paragraph terminPlatnosci = sec.AddParagraph();
            terminPlatnosci.AddText("Termin płatności: 01-04-2020");
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
            cellNazwaSprzedawcyiNabywcy.AddParagraph("xSolutions Sp. z o.o.");
            cellNazwaSprzedawcyiNabywcy.Format.Font.Bold = true;
            cellNazwaSprzedawcyiNabywcy = rowNazwaSprzedawcyiNabywcy.Cells[1];
            cellNazwaSprzedawcyiNabywcy.AddParagraph("Aldona Nieznana");
            cellNazwaSprzedawcyiNabywcy.Format.Font.Bold = true;

            Row rowUlicaSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellUlicaSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellUlicaSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[0];
            cellUlicaSprzedawcyiNabywcy.AddParagraph("ul. Mickiewicza 29");
            cellUlicaSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[1];
            cellUlicaSprzedawcyiNabywcy.AddParagraph("ul Nieznana 20");

            Row rowMiastoiKodSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellMiastoiKodSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellMiastoiKodSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[0];
            cellMiastoiKodSprzedawcyiNabywcy.AddParagraph("40-085 Katowice");
            cellMiastoiKodSprzedawcyiNabywcy = rowUlicaSprzedawcyiNabywcy.Cells[1];
            cellMiastoiKodSprzedawcyiNabywcy.AddParagraph("41-200 Sosnowiec");

           Row rowEmpty2 = tableSN.AddRow();
           // Cell cellEmpty2 = rowEmpty2.Cells[0];
            //cellEmpty2 = rowEmpty2.Cells[0];
  
            //cellEmpty2 = rowEmpty2.Cells[1];
  

            Row rowNIPSprzedawcyiNabywcy = tableSN.AddRow();
            Cell cellNIPSprzedawcyiNabywcy = rowEmpty.Cells[0];
            cellNIPSprzedawcyiNabywcy = rowNIPSprzedawcyiNabywcy.Cells[0];
            cellNIPSprzedawcyiNabywcy.AddParagraph("NIP 634-293-59-61");
            cellNIPSprzedawcyiNabywcy.Format.Font.Bold = true;
            cellNIPSprzedawcyiNabywcy = rowNIPSprzedawcyiNabywcy.Cells[1];
            cellNIPSprzedawcyiNabywcy.AddParagraph("NIP 000-000-00-00");
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
            cellBankiKontoBankowe.AddParagraph("Nest Bank");
            cellBankiKontoBankowe.Format.Font.Bold = true;

            //def konto bankowe 
            Row rowKontoBankowe = tableBankiKontoBankowe.AddRow();
            Cell cellKontoBankowe = rowKontoBankowe.Cells[0];
            cellKontoBankowe = rowKontoBankowe.Cells[0];
            cellKontoBankowe.AddParagraph("Konto: ");
           // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellKontoBankowe.Format.Font.Bold = true;
           
            cellKontoBankowe = rowKontoBankowe.Cells[1];
            cellKontoBankowe.AddParagraph("44 2530 0008 2064 1044 1937 0001");
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

            Row row2 = table.AddRow();
            Cell cellR2C0 = row2.Cells[0];
            cellR2C0.AddParagraph("1");
            cellR2C0.Format.Alignment = ParagraphAlignment.Center;

            Cell cellR2C1 = row2.Cells[1];
            cellR2C1.AddParagraph("Abonament xBiuro - pakiet STANDARD na 12-MIESIĘCY");
            cellR2C1.Format.Alignment = ParagraphAlignment.Center;

            Cell cellR2C2 = row2.Cells[2];
            cellR2C2.AddParagraph("1");
            cellR2C2.Format.Alignment = ParagraphAlignment.Center;

            Cell cellR2C3 = row2.Cells[3];
            cellR2C3.AddParagraph("usługa");
            cellR2C3.Format.Alignment = ParagraphAlignment.Center;

            Cell cellR2C4 = row2.Cells[4];
            cellR2C4.AddParagraph("723,24");
            cellR2C4.Format.Alignment = ParagraphAlignment.Center;

            Cell cellR2C5 = row2.Cells[5];
            cellR2C5.AddParagraph("723,24");
            cellR2C5.Format.Alignment = ParagraphAlignment.Center;

            

            //foreach (var elem in books)
            //{
            //    row = table.AddRow();

            //    cell = row.Cells[0];
            //    cell.AddParagraph(elem.author);

            //    cell = row.Cells[1];
            //    cell.AddParagraph(elem.title);

            //    cell = row.Cells[2];
            //    cell.AddParagraph(elem.year.ToString());

            //}

            //add table to document
            doc.LastSection.Add(table);

            Paragraph slownie = sec.AddParagraph();
            slownie.Format.Font.Bold = true;
            slownie.AddText("\n SŁOWNIE: SIEDEMSET DWADZIEŚCIA TRZY ZŁOTE I 24/100 GROSZY BRUTTO");

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
            cellPodsumowanie1.AddParagraph("723,24 PLN");
            cellPodsumowanie1.Format.Font.Bold = false;
  
            Row rowPodsumowanie2 = tablePodsumowanie.AddRow();
            Cell cellPodsumowanie2 = rowPodsumowanie2.Cells[0];
            cellPodsumowanie2 = rowPodsumowanie2.Cells[0];
            cellPodsumowanie2.AddParagraph("Zapłacono");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellPodsumowanie2.Format.Font.Bold = true;

            cellPodsumowanie2 = rowPodsumowanie2.Cells[1];
            cellPodsumowanie2.AddParagraph("0,00 PLN");
            cellPodsumowanie2.Format.Font.Bold = false;

            Row rowPodsumowanie3 = tablePodsumowanie.AddRow();
            Cell cellPodsumowanie3 = rowPodsumowanie3.Cells[0];
            cellPodsumowanie3 = rowPodsumowanie3.Cells[0];
            cellPodsumowanie3.AddParagraph("Pozostało do zaplaty");
            // rowKontoBankowe.Format.Alignment = ParagraphAlignment.Right;
            cellPodsumowanie3.Format.Font.Bold = true;

            cellPodsumowanie3 = rowPodsumowanie3.Cells[1];
            cellPodsumowanie3.AddParagraph("723,24 PLN");
            cellPodsumowanie3.Format.Font.Bold = false;

            doc.LastSection.Add(tablePodsumowanie);

            Paragraph uwagi = sec.AddParagraph();
            uwagi.Format.Font.Bold = false;
            uwagi.AddText("UWAGI: Zwolnienie podmiotowe z VAT wg. art. 113 ust. 1 Ustawy o VAT. W przypadku braku opłacenia faktury w terminie świadczenie usługi zostanie automatycznie wstrzymane. \n \n \n");
           

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
            cellPodpisy2.AddParagraph("\n \n Tomasz Chajduga");

            doc.LastSection.Add(tablePodpisy);


            // Create a renderer and prepare (=layout) the document
            MigraDoc.Rendering.DocumentRenderer docRenderer = new MigraDoc.Rendering.DocumentRenderer(doc);
            docRenderer.PrepareDocument();
            gfx.MUH = PdfFontEncoding.Unicode;
   
            docRenderer.RenderPage(gfx, 1);


            const string filename = "HelloWorld4.pdf"; //When drawing is done, write the file
            document.Save(filename); // Save the document...

            //Process.Start(filename); // ...and start a viewer.
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
    }




