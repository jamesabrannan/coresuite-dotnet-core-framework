﻿using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace coresuite_dotnet_framework_console_cs.Generator
{
    public class SimpleReport
    {
        // Template for document elements
        public static Template template = new Template();

        // Page Dimensions of pages
        public static PageDimensions pageDimensions = new PageDimensions(PageSize.Letter, PageOrientation.Portrait, 54.0f);
        // Current page that elements are being added to
        public static Page currentPage = null;
        // Top Y coordinate for the body of the report
        public static float bodyTop = 38;
        // Bottom Y coordinate for the body of the report
        public static float bodyBottom = pageDimensions.Body.Bottom - pageDimensions.Body.Top;
        // Current Y coordinate where elements are being added
        public static float currentY = 0;

        // Used to control the alternating background
        public static bool alternateBG = false;

        public static List<SimpleReportData.Product> product = SimpleReportData.Products;

        public static void Run(string outputPdfPath)
        {
            // Create a document and set it's properties
            Document document = new Document
            {
                Creator = "SimpleReport",
                Author = "ceTe Software",
                Title = "Simple Report"
            };

            // Adds elements to the header template
            SetTemplate();
            document.Template = template;

 
            // Builds the report
            BuildDocument(document);

            // Outputs the SimpleReport to the current web page
            document.Draw(outputPdfPath);
        }

        public static void SetTemplate()
        {
            // Adds elements to the header template
            template.Elements.Add(new Label(DateTime.Now.ToString("dd MMM yyyy, H:mm:ss EST"), 0, 0, 504, 12, Font.HelveticaBold, 12));
            template.Elements.Add(new Label("Northwind Product List", 0, 0, 504, 12, Font.HelveticaBold, 12, TextAlign.Center));
            PageNumberingLabel pageNumLabel = new PageNumberingLabel("Page %%CP%% of %%TP%%", 0, 0, 504, 12, Font.HelveticaBold, 12, TextAlign.Right);
            template.Elements.Add(pageNumLabel);
            template.Elements.Add(new Label("Product", 2, 23, 236, 11, Font.TimesBold, 11));
            template.Elements.Add(new Label("Qty Per Unit", 242, 23, 156, 11, Font.TimesBold, 11));
            template.Elements.Add(new Label("Unit Price", 402, 23, 100, 11, Font.TimesBold, 11, TextAlign.Right));
            template.Elements.Add(new Line(0, 36, 504, 36));

            // Uncomment the line below to add a layout grid to each page
            //template.Elements.Add( new LayoutGrid() );
        }

        public static void BuildDocument(Document document)
        {
            // Builds the PDF document with data from the DataReader
            AddNewPage(document);

            foreach(var data in product) { 
                // Add current record to the document
                AddRecord(document, data);
            }
        }

        public static void AddRecord(Document document, SimpleReportData.Product data)
        {
            // Adds a new page to the document if needed
            if (currentY > bodyBottom) AddNewPage(document);

            // Adds alternating background to document if needed
            if (alternateBG)
            {
                currentPage.Elements.Add(new Rectangle(0, currentY, 504, 18, RgbColor.Black, new WebColor("E0E0FF"), 0.0F));
            }

            // Adds Labels to the document with data from current record
            currentPage.Elements.Add(new Label(data.ProductName, 2, currentY + 3, 236, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new Label(data.QuantityPerUnit, 242, currentY + 3, 156, 11, Font.TimesRoman, 11));
            currentPage.Elements.Add(new Label(data.UnitPrice.ToString("0.00"), 402, currentY + 3, 100, 11, Font.TimesRoman, 11, TextAlign.Right));

            // Toggles alternating background
            alternateBG = !alternateBG;

            // Increments the current Y position on the page
            currentY += 18;
        }

        public static void AddNewPage(Document document)
        {
            // Adds a new page to the document
            currentPage = new Page(pageDimensions);
            currentY = bodyTop;
            alternateBG = false;
            document.Pages.Add(currentPage);
        }     
    }
}
