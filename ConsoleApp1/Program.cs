using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

using WordLibrary;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var docBuilder = new DocBuilder();

            docBuilder.SetDefaultFonts("Times New Roman", 11);
            docBuilder.SetOrientation(Orientation.Landscape);
            docBuilder.SetPageMargins_in_Inches(top: 1.25f);

            var logoFile = Path.Combine(GetCurrentDirectory(), @"logo.gif");
            docBuilder.SetPageHeader(logoFile, new List<string> { "row 1", "Row 2", "row 3" });

            docBuilder.SetPageFooter(new List<string> { "Confidential Information",
                "The information in this study report is legally privileged and confidential.  Any disclosure, copying or distribution of the information contained within is strictly prohibited without written consent from the sponsor and Pharma Medica Research Inc."
                });

            docBuilder.SetTableFontSizes(titleFontSize: 12, tableFontSize: 10, notesFontSize: 9);

            var tableNumber = "1";
            var tableTitle = "Final Plasma Concentrations of Moxifloxacin(ng / mL), Period 1";

            docBuilder.SetTableTitle_FirstPage(tableNumber, tableTitle);
            docBuilder.CreateTable(1, 6);

            string[,] headerRows =
            {
                {"S","Sampling Time Hours",null,null,null,null },
                {null, "1","2","3","4","5" }
            };


            docBuilder.AddTableHeaderRows(headerRows);

            docBuilder.MergeCells(1, 1, 2, 1);
            docBuilder.MergeCells(1, 2, 1, null);

            string[,] contentRows =
            {
                {"001","1.01","2.01","3.01","4.01","5.0" },
                {"001","1.01","2.01","3.01","4.01","5.0" },
                {"001","1.01","2.01","3.01","4.01","5.0" },
                {null, "1","2","3","4","5" }
            };

            docBuilder.AddTableContents(contentRows);
            docBuilder.AddTableContents(contentRows);
            docBuilder.AddTableContents(contentRows);

            string[,] summaryRows =
{
                {"Summary","1.01","2.01","3.01","4.01","5.0" },
                {null,"1.01","2.01","Intentionally Blank",null,"5.0" },
                {null,"1.01","2.01",null,null,"5.0" },
                {null, "5.01","2.06",null,null,"5" }
            };

            docBuilder.AddTableContents(summaryRows);

            docBuilder.MergeCells(2, 4, null, 5);
            docBuilder.MergeCells(1, 1, null, 1);

            docBuilder.AddPageBreak();

            docBuilder.SetTableTitle_Continued(tableNumber, tableTitle);

            docBuilder.CreateTable(1, 6);
            docBuilder.AddTableHeaderRows(headerRows);

            docBuilder.MergeCells(1, 1, 2, 1);
            docBuilder.MergeCells(1, 2, 1, null);

            docBuilder.AddTableContents(contentRows);
            docBuilder.AddTableContents(contentRows);
            docBuilder.AddTableContents(contentRows);

            docBuilder.AddTableContents(summaryRows);

            docBuilder.MergeCells(2, 4, null, 5);
            docBuilder.MergeCells(1, 1, null, 1);

            var notes = new[] {
                "BLQ: Below the lower limit of quantitation(5.00 ng / mL)",
                "MS: Missing sample",
                "Calibration Range: 5.00 ng / mL - 5000 ng / mL",
                "S: Subject."
            };

            docBuilder.AddTableNotes(notes);

            docBuilder.DeleteLastPageIfEmpty();

            docBuilder.ShowDocument();



            //PlayWithRange();

            Console.WriteLine("Document Combined");
            Console.WriteLine("Press any key to continue");

            Console.ReadLine();

        }



        static string GetCurrentDirectory()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(path);
        }

        static void PlayWithRange()
        {
            var app = new Application();
            var doc = app.Documents.Add();
            app.Visible = true;

            var rng = doc.Content;
            rng.Text = "Para 1" + Environment.NewLine +
                    "Para 2" + Environment.NewLine +
                    "Para 3";
            rng.Select();

            rng = rng.Paragraphs[2].Range;
            //rng.Collapse(WdCollapseDirection.wdCollapseEnd);
            rng.Select();

            rng.InsertParagraphBefore();
            rng.Select();

            rng.InsertBefore("This is the text that replaced paragraph 2");
            rng.Select();

            rng.InsertParagraphAfter();
            rng.Select();

            rng.Collapse(WdCollapseDirection.wdCollapseEnd);
            rng.Text = "New Text after insert";
            rng.Select();

            #region Insert Break;
            //Insert PageBreak;

            //rng = doc.Content.Paragraphs[2].Range;
            //rng.Select();
            //// rng.Collapse(WdCollapseDirection.wdCollapseEnd);

            //rng.InsertBreak(WdBreakType.wdSectionBreakNextPage);
            //rng.Select();

            //rng.InsertAfter("|After Page Break|");
            //rng.Select(); 
            #endregion

            #region InsertAfter and InsertBefore
            //Method: InsertAfter, InsertBefore
            //rng = doc.Content.Paragraphs[1].Range;
            //rng.Select();

            //rng.End -= 1;

            //rng.InsertAfter("|After|");
            //rng.Select();

            //rng = doc.Content;
            //rng.Collapse(WdCollapseDirection.wdCollapseEnd);


            //rng.InsertBefore("|Before|");
            //rng.Select();

            #endregion

            doc.Close(SaveChanges: false);

            app.Quit();

        }

        static void CreateTestDoc()
        {
            var app = new Application();
            string path = Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(path, "doc2");

            var doc = app.Documents.Add();
            app.Visible = true;

            var section1 = doc.Sections[1];

            section1.Borders[WdBorderType.wdBorderLeft].Visible = true;
            section1.Borders[WdBorderType.wdBorderLeft].LineWidth = WdLineWidth.wdLineWidth100pt;
            section1.Borders[WdBorderType.wdBorderRight].Visible = true;
            section1.Borders[WdBorderType.wdBorderRight].LineWidth = WdLineWidth.wdLineWidth100pt;
            section1.Borders[WdBorderType.wdBorderTop].Visible = true;
            section1.Borders[WdBorderType.wdBorderTop].LineWidth = WdLineWidth.wdLineWidth100pt;
            section1.Borders[WdBorderType.wdBorderBottom].Visible = true;
            section1.Borders[WdBorderType.wdBorderBottom].LineWidth = WdLineWidth.wdLineWidth100pt;

            section1.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            section1.PageSetup.LeftMargin = app.InchesToPoints(1.5f);
            section1.PageSetup.RightMargin = app.InchesToPoints(1.5f);
            section1.PageSetup.TopMargin = app.InchesToPoints(2.0f);
            section1.PageSetup.BottomMargin = app.InchesToPoints(2.0f);


            var header = section1.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
            var pa = header.Range.Paragraphs.Add();
            pa.Range.InlineShapes.AddPicture(FileName: @"C:\Users\bjiang\Pictures\test.png", LinkToFile: false, SaveWithDocument: true);
            pa.Range.InlineShapes[1].LockAspectRatio = MsoTriState.msoCTrue;
            pa.Range.InlineShapes[1].ScaleHeight = app.CentimetersToPoints(1);
            pa.Range.InsertAfter("This is some text after the picture.");

            var pa1 = header.Range.Paragraphs.Add();
            pa1.Range.Text = "This is the first paragraph in header";
            pa1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            pa1.Range.Font.Bold = 1;

            var contentParagraph = section1.Range.Paragraphs.Add();

            contentParagraph.SpaceBefore = 48;
            contentParagraph.SpaceAfter = 48;
            contentParagraph.Range.Text = "This is paragraph 1";
            contentParagraph.Range.InsertParagraphAfter();


            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            object oEndOfDoc = "\\endofdoc";

            Range wrdRng = doc.Bookmarks.get_Item(oEndOfDoc).Range;

            var oTable = doc.Tables.Add(wrdRng, 3, 5);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Range.ParagraphFormat.SpaceBefore = 6;

            oTable.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;

            int r, c;
            string strText;
            for (r = 1; r <= 3; r++)
                for (c = 1; c <= 5; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Borders[WdBorderType.wdBorderLeft].Visible = true;
            oTable.Borders[WdBorderType.wdBorderRight].Visible = true;
            oTable.Borders[WdBorderType.wdBorderTop].Visible = true;
            oTable.Borders[WdBorderType.wdBorderBottom].Visible = true;
            oTable.Borders[WdBorderType.wdBorderVertical].Visible = true;
            oTable.Borders[WdBorderType.wdBorderHorizontal].Visible = true;

            oTable.Borders[WdBorderType.wdBorderTop].LineWidth = WdLineWidth.wdLineWidth150pt;

            oTable.Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


            var contentPa2 = section1.Range.Paragraphs.Add();

            contentPa2.SpaceBefore = 48;
            contentPa2.SpaceAfter = 48;
            contentPa2.Range.Text = "This is paragraph 2";
            contentPa2.Range.InsertParagraphAfter();

            wrdRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = doc.Tables.Add(wrdRng, 5, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            for (r = 1; r <= 5; r++)
                for (c = 1; c <= 2; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Columns[1].Width = app.InchesToPoints(2); //Change width of columns 1 & 2
            oTable.Columns[2].Width = app.InchesToPoints(3);


            var footer = section1.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
            var pa2 = footer.Range.Paragraphs.Add();
            pa2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            pa2.Range.Text = "This is the footer line";
            pa2.Range.Font.StrikeThrough = 1;

            var section2 = doc.Sections.Add();
            //section2.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            var header2 = section1.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
            var pa02 = header.Range.Paragraphs.Add();
            pa02.Range.Text = "This is the first paragraph in header";
            pa02.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            pa02.Range.Font.Bold = 1;

            var contentParagraph2 = section2.Range.Paragraphs.Add();

            contentParagraph2.SpaceBefore = 48;
            contentParagraph2.SpaceAfter = 48;
            contentParagraph2.Range.Text = "This is paragraph 1";
            contentParagraph2.Range.InsertParagraphAfter();

            AddWaterMark(doc, section2);

            doc.SaveAs2(filePath, FileFormat: WdSaveFormat.wdFormatDocument);
            app.Quit();

        }

        static void AddWaterMark(Document doc, Section section)
        {

            section.Range.Select();

            section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].
                Shapes.AddPicture(FileName: @"C:\Users\bjiang\Pictures\test.png",
                LinkToFile: false, SaveWithDocument: true).Select();

            var Selection = doc.ActiveWindow.ActivePane.Selection;

            Selection.ShapeRange.Name = "WordPictureWatermark32603288";
            Selection.ShapeRange.PictureFormat.Brightness = 0.85f;
            Selection.ShapeRange.PictureFormat.Contrast = 0.15f;
            Selection.ShapeRange.LockAspectRatio = MsoTriState.msoFalse;
            Selection.ShapeRange.HeightRelative = 100;
            Selection.ShapeRange.WidthRelative = 100;
            //    Selection.ShapeRange.WrapFormat.AllowOverlap = -1;
            //    Selection.ShapeRange.WrapFormat.Side = WdWrapSideType.wdWrapBoth;
            //    Selection.ShapeRange.WrapFormat.Type = WdWrapType.wdWrapFront;
            Selection.ShapeRange.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;
            Selection.ShapeRange.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;
            Selection.ShapeRange.Left = 0;
            Selection.ShapeRange.Top = 0;
        }
    }
}
