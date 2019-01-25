using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var app = new Application();
            //string path = Environment.CurrentDirectory;
            //string filePath = System.IO.Path.Combine(path, "doc1.docx");
            //string outputfilepath = System.IO.Path.Combine(path, "doc1.pdf");
            //var doc = app.Documents.Open(filePath);
            //doc.SaveAs2(outputfilepath, FileFormat: WdSaveFormat.wdFormatPDF);
            //app.Quit();

            CreateTestDoc();

            Console.ReadLine();

        }

        static void CreateTestDoc()
        {
            var app = new Application();
            string path = Environment.CurrentDirectory;
            string filePath = System.IO.Path.Combine(path, "doc2");
            var doc = app.Documents.Add();

            var section1=doc.Sections[1];
            
            section1.Borders[WdBorderType.wdBorderLeft].Visible = true;
            section1.Borders[WdBorderType.wdBorderLeft].LineWidth = WdLineWidth.wdLineWidth300pt;

            section1.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
            section1.PageSetup.LeftMargin = app.InchesToPoints(1.5f);
            section1.PageSetup.RightMargin = app.InchesToPoints(1.5f);
            section1.PageSetup.TopMargin = app.InchesToPoints(2.0f);
            section1.PageSetup.BottomMargin = app.InchesToPoints(2.0f);


            var header = section1.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
            var pa = header.Range.Paragraphs.Add();
            pa.Range.Text = "This is the first paragraph in header";
            pa.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            pa.Range.Font.Bold = 1;

            var contentParagraph=section1.Range.Paragraphs.Add();

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
            oTable.Range.ParagraphFormat.SpaceBefore=6;

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
            section2.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

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


            doc.SaveAs2(filePath, FileFormat: WdSaveFormat.wdFormatDocument);
            app.Quit();

        }
    }
}
