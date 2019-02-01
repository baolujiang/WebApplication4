using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace WordLibrary
{
    public class DocMerger : IDisposable
    {

        private Application App;

        public DocMerger()
        {
            App = new Application();
        }

  
        public void MergeDocuments(string outputFile, params string[] inputFiles)
        {

            var mergedDoc = App.Documents.Open(inputFiles[0]);
            mergedDoc.SaveAs2(outputFile);

            for (int i = 1; i < inputFiles.Length; i++)
            {
                var sourceDoc = App.Documents.Open(FileName: inputFiles[i], ReadOnly: true);

                MergeOneDocument(mergedDoc, sourceDoc);

                sourceDoc.Close(SaveChanges: false);

            }

            mergedDoc.Save();
            mergedDoc.Close();

            Dispose(true);

        }

        public void MergeDocuments(string directory, string outputFile, params string[] inputFiles)
        {
             outputFile = Path.Combine(directory, outputFile);

            for(int i=0; i < inputFiles.Length; i++)
            {
                inputFiles[i] = Path.Combine(directory, inputFiles[i]);
            }

            MergeDocuments(outputFile, inputFiles);

        }


        private void MergeOneDocument(Document targetDoc, Document sourceDoc )
        {
            sourceDoc.Select();
            App.Selection.Copy();

            targetDoc.Select();

            App.Selection.Collapse(WdCollapseDirection.wdCollapseEnd);
            App.Selection.InsertParagraphAfter();
            App.Selection.InsertBreak(WdBreakType.wdSectionBreakNextPage);
            App.Selection.Collapse(WdCollapseDirection.wdCollapseEnd);

            App.Selection.PasteAndFormat(WdRecoveryType.wdFormatOriginalFormatting);

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
        // ~DocMerger() {
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
