using ceTe.DynamicPDF;
using ceTe.DynamicPDF.LayoutEngine;
using ceTe.DynamicPDF.LayoutEngine.LayoutElements;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.BarCoding;

namespace coresuite_dotnet_standard_cs.reportwriter
{
    public class PlaceHolderReport
    {
        public static void Run(string outputPdfPath)
        {
            //Create the document's layout from a DLEX template
            DocumentLayout documentLayout = new DocumentLayout(Util.GetResourcePath("DLEX/PlaceHolder.dlex"));

            // Retrieve the placeholder for the dynamic image and attach an event to it
            ceTe.DynamicPDF.LayoutEngine.LayoutElements.PlaceHolder barcode = (ceTe.DynamicPDF.LayoutEngine.LayoutElements.PlaceHolder)documentLayout.GetLayoutElementById("Barcode");
            barcode.LaidOut += Barcode_LaidOut;

            // Specify the Data to use when laying out the document
            NameValueLayoutData layoutData = new NameValueLayoutData();
            layoutData.Add("HeaderData", "Popular Websites");
            layoutData.Add("Websites", PlaceHolderExampleData.Websites);

            // Layout the PDF document
            Document document = documentLayout.Layout(layoutData);
            document.Author = "DynamicPDF ReportWriter";
            document.Title = "PlaceHolder Example";

            // Outputs the document to a file
            document.Draw(outputPdfPath);
        }

        private static void Barcode_LaidOut(object sender, PlaceHolderLaidOutEventArgs e)
        {
            // Retrieve the image bytes from the layout data for the current record
            string url = e.LayoutWriter.Data["Url"].ToString();

            // Create a barcode page element from the data and set its properties
            QrCode qrCode = new QrCode(url, 0, 0, 2);

            // Add the barcode and a link to the placeholder's content area
            e.ContentArea.Add(qrCode);
            e.ContentArea.Add(new Link(0, 0, qrCode.GetSymbolWidth(), qrCode.GetSymbolHeight(), new UrlAction(url)));
        }
    }
}