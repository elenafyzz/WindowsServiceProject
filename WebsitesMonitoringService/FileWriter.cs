using System.IO;
using System.Text;

namespace WebsitesMonitoringService
{
    class FileWriter
    {
        private const string path = "WebsitesStatus.txt";
        private FileInfo file;

        public FileWriter()
        {
            file = new FileInfo(path);
            if (file.Exists)
                file.Delete();
            file.Create().Close();
        }

        public void Write(string info)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine(info);
            }
        }

        public string getFileName
        {
            get { return file.FullName; }
        }
    }
}
