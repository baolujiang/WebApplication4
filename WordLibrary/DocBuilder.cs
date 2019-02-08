using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.IO;

namespace WordLibrary
{
    public enum Orientation
    {
        Portrait = 0,
        Landscape = 1
    }

    public class DocBuilder : IDisposable
    {

        private Application App;
        private Document Doc;
        private Section section;

        public DocBuilder(bool showDocument = false)
        {
            App = new Application();
            Doc = App.Documents.Add();
            section = Doc.Sections[1];

            if (showDocument) App.Visible = true;

        }

        public void ShowDocument()
        {
            App.Visible = true;
        }

        public void SetOrientation(Orientation orientation)
        {
            section.PageSetup.Orientation = (WdOrientation)(int)(orientation);
        }


        public void SetPageMargins(float left, float top, float right, float bottom)
        {
            section.PageSetup.LeftMargin = left;
            section.PageSetup.RightMargin = right;
            section.PageSetup.TopMargin = top;
            section.PageSetup.BottomMargin = bottom;
        }


        public void SetPageMargins_in_Inches(float left = 1, float top = 1, float right = 1, float bottom = 1)
        {
            SetPageMargins(App.InchesToPoints(left),
                            App.InchesToPoints(top),
                            App.InchesToPoints(right),
                            App.InchesToPoints(bottom)
                            );
        }

        private string defaultFontName;
        private float defaultFontSize;

        //section.Range.Font.Bold = 0;
        //    section.Range.Font.Italic = 0;
        //    section.Range.Font.Underline = WdUnderline.wdUnderlineNone;
        //    section.Range.Font.StrikeThrough = 0;
        //    section.Range.Font.Subscript = 0;
        //    section.Range.Font.Superscript = 0;

        public void SetDefaultFonts(string fontName, float fontSize)
        {
            //Save default font settings;
            defaultFontName = fontName;
            defaultFontSize = fontSize;

            Doc.Content.Font.Name = defaultFontName;
            Doc.Content.Font.Size = defaultFontSize;

            foreach (Section sec in Doc.Sections)
            {
                sec.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Font.Name = defaultFontName;
                sec.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Font.Name = defaultFontName;
            }

        }

        public void SetPageHeader(string logoFile, List<string> titles)
        {
            foreach (Section sec in Doc.Sections)
            {

                var headerRange = sec.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                headerRange.Font.Size = 10;

                var table = headerRange.Tables.Add(headerRange, 1, 2);
                table.Cell(1, 1).Range.InlineShapes.AddPicture(FileName: logoFile);
                table.Cell(1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                table.Columns[1].AutoFit();
                table.Cell(1, 2).Range.Text = string.Join(Environment.NewLine, titles);

                headerRange.Expand();
                headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                headerRange.InlineShapes.AddHorizontalLineStandard();

            }

        }

        public void SetPageFooter(List<string> footers)

        {
            foreach (Section sec in Doc.Sections)
            {
                var footerRange = sec.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                footerRange.Font.Size = 9;

                footerRange.Text = string.Join(Environment.NewLine, footers);

                footerRange.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                footerRange.Paragraphs[1].Range.Font.Size = 10;
                footerRange.Paragraphs[1].Range.Font.Bold = 1;

                footerRange.Paragraphs[1].Range.InlineShapes.AddHorizontalLineStandard();

            }

        }

        private float TableTitle_FontSize = 11;
        private float Table_FontSize = 11;
        private float TableNotes_FontSize = 10;

        public void SetTableFontSizes(
                    float titleFontSize = 11,
                    float tableFontSize = 11,
                    float notesFontSize = 10
                    )
        {
            TableTitle_FontSize = titleFontSize;
            Table_FontSize = tableFontSize;
            TableNotes_FontSize = notesFontSize;
        }


        public Range AppendParagraph(string text)
        {
            var rng = GetEndOfDocument();
            rng.InsertParagraphBefore();
            rng.InsertBefore(text);
            return rng;
        }

        public Range AppendEmptyLine(int lineHeight = 1)
        {
            var rng = GetEndOfDocument();
            rng.InsertParagraphBefore();
            rng.ParagraphFormat.LineSpacing = lineHeight;
            rng.ParagraphFormat.SpaceAfter = lineHeight;
            return rng;
        }

        public void InsertParagraphAfter(Range rng, string text)
        {
            rng.End -= 1;
            rng.InsertParagraphAfter();
            rng.Collapse(WdCollapseDirection.wdCollapseEnd);
            rng.InsertBefore(text);
        }

        public void InsertParagraphBefore(Range rng, string text)
        {
            rng.InsertParagraphBefore();
            rng.InsertBefore(text);
        }

     

        public void SetTableTitle_FirstPage(string tableNumber, string tableTitle)
        {
            var title = $"Table {tableNumber}: {tableTitle}";
            SetTableTitle(title);
        }

        public void SetTableTitle_Continued(string tableNumber, string tableTitle)
        {
            var title = $"Table {tableNumber} - Continued: {tableTitle}";
            SetTableTitle(title);
        }

        private void SetTableTitle(string title)
        {
            var rng = AppendEmptyLine();

            rng = AppendParagraph(title);
            rng.Font.Bold = 1;
            rng.Font.Size = TableTitle_FontSize;

        }

        private Range GetEndOfDocument()
        {
            var rng = Doc.Content;
            rng.Start = Doc.Content.End;

            return rng;
        }

        private Table table;
        private int headerRowCount = 0;

        private int rowIndex;
        private int groupStartRowIndex;

        public Table CreateTable(int numRows, int numCols)
        {
            var rng = Doc.Content;
            rng.Collapse(WdCollapseDirection.wdCollapseEnd);

            table = rng.Tables.Add(rng, numRows, numCols);
            rowIndex = 0;

            //Show lines
            //set table width to page width

            table.AllowAutoFit = true;
            table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);

            table.Range.ParagraphFormat.SpaceBefore = 2;
            table.Range.ParagraphFormat.SpaceAfter = 2;
            table.Range.Font.Size = Table_FontSize;

            return table;
        }

        private void ShowTableLines()
        {
            table.Borders[WdBorderType.wdBorderTop].Visible = true;
            table.Borders[WdBorderType.wdBorderBottom].Visible = true;
            table.Borders[WdBorderType.wdBorderLeft].Visible = true;
            table.Borders[WdBorderType.wdBorderRight].Visible = true;
            table.Borders[WdBorderType.wdBorderHorizontal].Visible = true;
            table.Borders[WdBorderType.wdBorderVertical].Visible = true;
        }

        public void AddTableHeaderRows(string[,] rows)
        {
            headerRowCount += rows.GetLength(0);

            groupStartRowIndex = rowIndex;

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                GetNewRow();

                for (int j = 0; j < rows.GetLength(1); j++)
                {
                    if (rows[i, j] != null)
                    {
                        table.Cell(i + 1, j + 1).Range.Text = rows[i, j];
                    }
                }

            }

            table.Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            table.Range.Font.Bold = 1;

            ShowTableLines();

        }

        public void MergeCells(int startRow, int startCol, int? endRow = null, int? endCol = null)
        {
            var startCell = table.Cell(groupStartRowIndex + startRow, startCol);

            if (!endRow.HasValue) endRow = table.Rows.Count;
            else endRow += groupStartRowIndex;

            if (!endCol.HasValue) endCol = table.Columns.Count;

            startCell.Merge(table.Cell(endRow.Value, endCol.Value));
        }


        private void GetNewRow()
        {
            rowIndex++;

            if (rowIndex > table.Rows.Count)
                table.Rows.Add();

        }

        public void AddTableContents(string[,] rows)
        {
            groupStartRowIndex = rowIndex;

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                GetNewRow();

                for (int j = 0; j < rows.GetLength(1); j++)
                {
                    if (rows[i, j] != null)
                    {
                        var rng = table.Cell(i + groupStartRowIndex + 1, j + 1).Range;
                        rng.Text = rows[i, j];
                        rng.Font.Bold = 0;
                    }
                }

            }


        }

        public void AddPageBreak()
        {
            var rng = Doc.Content;
            rng.InsertParagraphAfter();
            rng.Collapse(WdCollapseDirection.wdCollapseEnd);

            rng.InsertBreak(WdBreakType.wdPageBreak);
        }

        public void AddTableNotes(IEnumerable<string> notes)
        {
            var rng = AppendEmptyLine(8);

            rng = AppendParagraph(string.Join(Environment.NewLine, notes));

            rng.Font.Size = TableNotes_FontSize;

            rng.ParagraphFormat.SpaceBefore = 2;
            rng.ParagraphFormat.SpaceAfter = 2;
       
        }

        char[] BlankCharacters = { ' ', '\t', '\r', '\a' };

        public void DeleteLastPageIfEmpty()
        {

            //Select last page
            var rng=Doc.GoTo(What: WdGoToItem.wdGoToPage, Which: WdGoToDirection.wdGoToLast);

            rng.End = Doc.Content.End;
            rng.Select();

            //If page contains blank characters only, remove the page
            var txt = rng.Text.Trim(BlankCharacters);
            if (txt == string.Empty)                 App.Selection.Delete();

            //put cursor at the very front of the document
            rng=Doc.GoTo(What: WdGoToItem.wdGoToPage, Which: WdGoToDirection.wdGoToFirst);
            rng.Select();

        }

        public void Save(string fileName)
        {
            Doc.SaveAs2(FileName: fileName);
        }

        public void SaveAsPdf(string fileName)
        {
            Doc.ExportAsFixedFormat(OutputFileName: fileName, ExportFormat: WdExportFormat.wdExportFormatPDF);
        }

        public byte[] ExportAsByteArray(bool keepDocumentOpen = false)
        {
            //Save Document Before Export
            //
            var fileName = Doc.FullName;
            if (IsNewDocument())
            {
                fileName = Path.GetTempFileName();
                Doc.SaveAs2(FileName: fileName, FileFormat: WdSaveFormat.wdFormatXMLDocument);
            }
            else
            {
                Doc.Save();
            }

            //Close Document, so that it can be opened from .Net
            //
            Doc.Close();

            var fs = new FileStream(fileName, FileMode.Open);
            byte[] bytes = new byte[fs.Length];

            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            //In case the word document is still needed, re open it 
            if (keepDocumentOpen)
            {
                Doc = App.Documents.Open(fileName, ReadOnly: true);
            }

            return bytes;

        }

        public bool IsNewDocument()
        {
            return Doc.FullName.StartsWith("Document");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Doc.Close(SaveChanges: false);
                    App.Quit();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DocBuilder() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
