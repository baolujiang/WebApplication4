using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace WordLibrary
{
    public class Doc2Pdf: IDisposable
    {
        private Application App;

        public Doc2Pdf()
        {
            App = new Application();
        }

        public void Convert2Pdf(string pdfFile, string wordFile)
        {

            var doc = App.Documents.Open(FileName: wordFile, ReadOnly: true);
            doc.ExportAsFixedFormat(OutputFileName: pdfFile, ExportFormat: WdExportFormat.wdExportFormatPDF);
            doc.Close(SaveChanges: false);

            Dispose();

        }


        public void Convert2Pdf(string directory, string pdfFile, string wordFile)
        {
            var wordFilePath = System.IO.Path.Combine(directory, wordFile);
            var pdfFilePath = System.IO.Path.Combine(directory, pdfFile);

            Convert2Pdf(pdfFilePath, wordFilePath);

            Dispose();

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    App.Quit();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Doc2Pdf() {
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
