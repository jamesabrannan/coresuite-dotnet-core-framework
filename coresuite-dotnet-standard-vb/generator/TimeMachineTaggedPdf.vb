﻿Imports System.IO
Imports System
Imports ceTe.DynamicPDF
Imports ceTe.DynamicPDF.PageElements
Imports ceTe.DynamicPDF.PageElements.BarCoding

Namespace coresuite_dotnet_standard_vb.generator
    Public Class TimeMachineTaggedPdf

        ' Templates for header And footer elements
        Public Shared footerTemplate As Template = New Template()
        Public Shared headerTemplate As EvenOddTemplate = New EvenOddTemplate()

        Public Shared Sub Run(ByVal outputPdfPath As String)
            ' Create a document and set it's properties
            ' Specify document as tagged PDF
            Dim MyDocument As Document = New Document With {
                .Creator = "TimeMachineTaggedPdf.aspx",
                .Author = "H. G. Wells",
                .Title = "The Time Machine",
                .Template = headerTemplate,
                .Tag = New TagOptions()
            }

            ' Adds elements to the header And footer groups
            SetPageHeaderTemplate()
            SetPageFooterTemplate()

            ' Sets up outline hierarchy
            Dim titlePageOutline As Outline = MyDocument.Outlines.Add("The Time Machine", New XYDestination(1, 0, 0))
            Dim chapterOutline As Outline = titlePageOutline.ChildOutlines.Add("Chapters")

            ' Builds the report
            BuildDocument(MyDocument, titlePageOutline, chapterOutline)

            'Outputs the ContactList to the current web MyPage
            MyDocument.Draw(outputPdfPath)
        End Sub

        Public Shared Sub BuildDocument(ByVal document As Document, ByVal titlePage As Outline, ByVal chapters As Outline)
            ' Adds Title page to document
            document.Sections.Begin(NumberingStyle.None, "Cover")
            AddTitlePage(document)
            document.Sections.Begin(NumberingStyle.Numeric, "Page ")

            ' Adds Chapters to the document
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter1.txt"), "1", "Chapter 1", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter2.txt"), "2", "Chapter 2", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter3.txt"), "3", "Chapter 3", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter4.txt"), "4", "Chapter 4", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter5.txt"), "5", "Chapter 5", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter6.txt"), "6", "Chapter 6", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter7.txt"), "7", "Chapter 7", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter8.txt"), "8", "Chapter 8", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter9.txt"), "9", "Chapter 9", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter10.txt"), "10", "Chapter 10", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter11.txt"), "11", "Chapter 11", chapters)
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Chapter12.txt"), "12", "Chapter 12", chapters)

            ' Add the Epilogue to the document
            AddChapter(document, Util.GetResourcePath("Text/TimeMachine_Epilogue.txt"), "Epilogue", "Epilogue", titlePage)
        End Sub

        Public Shared Sub SetPageHeaderTemplate()
            ' Uncomment the line below to add a layout grid to the title header page
            'headerTemplate.Elements.Add( New LayoutGrid() )

            ' Adds elements to the header template
            headerTemplate.OddElements.Add(New Label("The Time Machine", -18, -18, 324, 11, Font.TimesRoman, 11, TextAlign.Left))
            headerTemplate.OddElements.Add(New Label("H. G. Wells", 18, -18, 324, 11, Font.TimesRoman, 11, TextAlign.Right))
            headerTemplate.EvenElements.Add(New Label("H. G. Wells", -18, -18, 324, 11, Font.TimesRoman, 11, TextAlign.Left))
            headerTemplate.EvenElements.Add(New Label("The Time Machine", 18, -18, 324, 11, Font.TimesRoman, 11, TextAlign.Right))
        End Sub

        Public Shared Sub SetPageFooterTemplate()

            ' Adds elements to the footer template
            Dim pageNumberLabel As PageNumberingLabel = New PageNumberingLabel("- %%CP%% -", 0, 478, 324, 11, Font.TimesRoman, 11, TextAlign.Center)
            pageNumberLabel.PageOffset = -1
            footerTemplate.Elements.Add(pageNumberLabel)
        End Sub

        Public Shared Sub AddTitlePage(ByVal document As Document)
            'Adds a title page to the document
            Dim page As Page = New Page(396, 540, 36)

            ' Uncomment the line below to add a layout grid to the title page
            'page.Elements.Add( new LayoutGrid() )

            Dim Disclaimer As String = "This document is in the public domain. Permission to use, copy, modify, and distribute this document for any purpose and without fee is hereby granted, without any conditions or restrictions. The barcode below is for demonstration purposes only."
            Dim Generated As String = "Generated by" & vbCrLf & "DynamicPDF Generator for .NET" & vbCrLf & "on " + DateTime.Now.ToLongDateString() + "," & vbCrLf & "at " + DateTime.Now.ToLongTimeString() + " EST"
            page.Elements.Add(New Label("The Time Machine", 36, 36, 252, 30, Font.TimesBold, 30, TextAlign.Center))
            page.Elements.Add(New Label("by H. G. Wells", 36, 96, 252, 22, Font.TimesBold, 22, TextAlign.Center))
            page.Elements.Add(New Label("1895", 36, 148, 252, 22, Font.TimesBold, 22, TextAlign.Center))
            page.Elements.Add(New Image(Util.GetResourcePath("Images/DPDFLogo.png"), 62, 208, 0.21F))
            page.Elements.Add(New Label(Generated, 132, 206, 182, 55, Font.TimesRoman, 11))
            page.Elements.Add(New Label(Disclaimer, 36, 274, 252, 66, Font.TimesRoman, 11, TextAlign.Justify))
            page.Elements.Add(New Ean13Sup5("201234567890", "90000", 82, 360))
            page.ApplyDocumentTemplate = False

            document.Pages.Add(page)
        End Sub

        Public Shared Sub AddChapter(ByVal document As Document, ByVal filePath As String, ByVal title As String, ByVal bookmarkText As String, ByVal parentOutline As Outline)
            ' Retrieves the text from the sections file
            Dim SectionText As String = GetTextFromFile(filePath)

            ' Adds the first page of the section
            Dim page As Page = AddSectionHeaderPage(title, bookmarkText, parentOutline)
            page.ApplyDocumentTemplate = False

            ' Creates a TextArea for the sections text
            Dim textArea As TextArea = New TextArea(SectionText, 0, 146, 324, 322, Font.TimesRoman, 11)
            textArea.Leading = 14
            textArea.ParagraphSpacing = 20
            page.Elements.Add(textArea)
            document.Pages.Add(page)

            ' Creates a TextArea for the overflow text
            textArea = textArea.GetOverflowTextArea(0, 0, 324, 468)

            ' Loops until no overflow text is found.
            While Not textArea Is Nothing
                ' Adds a new page to the document
                page = New Page(396, 540, 36)

                page.Elements.Add(textArea)
                ' Adds new page to the document
                document.Pages.Add(page)
                ' Creates a TextArea for the overflow text
                textArea = textArea.GetOverflowTextArea()
            End While
        End Sub

        Public Shared Function AddSectionHeaderPage(ByVal title As String, ByVal bookmarkText As String, ByVal parentOutline As Outline) As Page
            ' Adds the first page of a section to the document
            Dim page As Page = New Page(396, 540, 36)

            ' Uncomment the line below to add a layout grid to the section header pages
            'page.Elements.Add( new LayoutGrid() )

            page.Elements.Add(New Bookmark(bookmarkText, 0, 0, parentOutline))
            page.Elements.Add(New Label("The Time Machine", 0, 36, 324, 30, Font.TimesBold, 30, TextAlign.Center))
            page.Elements.Add(New Label(title, 0, 96, 324, 22, Font.TimesBold, 22, TextAlign.Center))
            page.Elements.Add(New Line(120, 128, 204, 128))
            Dim pageNumberLabel As PageNumberingLabel = New PageNumberingLabel("- %%CP%% -", 0, 478, 324, 11, Font.TimesRoman, 11, TextAlign.Center)
            pageNumberLabel.PageOffset = -1
            page.Elements.Add(pageNumberLabel)

            Return page
        End Function

        Public Shared Function GetTextFromFile(ByVal filePath As String) As String
            ' Opens a text file and returns the text from it.
            Dim textFile As StreamReader = File.OpenText(filePath)
            Dim sectionText As String = textFile.ReadToEnd()
            textFile.Close()
            Return sectionText
        End Function
    End Class
End Namespace