using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class TempFilesCleaner
    {

        public void DeleteAll()
        {
            try
            {
                Directory.Delete(Path.GetTempPath(), true);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void DeleteOldFiles()
        {
            DeleteFilesOlderThan(DateTime.Now.AddDays(-7));
        }


        public void DeleteFilesOlderThan(DateTime creationDate)
        {
            var tempPath = Path.GetTempPath();

            var dirInfo = new DirectoryInfo(tempPath);

            foreach (var item in dirInfo.EnumerateDirectories()
                                        .Where(p => p.CreationTime <= creationDate))
            {
                try
                {
                    item.Delete(true);
                }
                catch (Exception)
                {
                    //throw;
                }
            }

            foreach (var item in dirInfo.EnumerateFiles()
                                        .Where(p => p.CreationTime <= creationDate))
            {
                try
                {
                    item.Delete();
                }
                catch (Exception)
                {
                    //throw;
                }
            }
        }


    }
}
