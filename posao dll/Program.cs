using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace posao_dll
{
    class Program
    {
        // moze i za ceo ja sam stavio muzika radi testiranja

        public static string Location = "Dimitrije: posao dll";


        static void Main(string[] args)
        {

            const string currentFolder = @"D:/neki folder test programiranje";

            var directoryInfo = new DirectoryInfo(currentFolder);

            var stringBuilder = new StringBuilder();

            var xmlSettings = new XmlWriterSettings

            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                Indent = true
            };


            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlSettings))
            {

                xmlWriter.WriteStartElement("import");
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    string title = Path.GetFileNameWithoutExtension(file.Name);

                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("type", "document");
                    xmlWriter.WriteAttributeString("action", "create");

                    xmlWriter.WriteStartElement("title");
                    xmlWriter.WriteAttributeString("language", "en_US");
                    xmlWriter.WriteString(title);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteElementString("location", Location);
                    xmlWriter.WriteElementString("file", file.FullName);
                    xmlWriter.WriteEndElement();    //node



                }

                TraverseDirectory(directoryInfo, stringBuilder);

                using (StreamWriter sw = File.CreateText(@"C:\Users\Ana\Desktop\Posao Dimitrije\posao-master\posao dll\FilesfromDrive.xml"))
                {
                    sw.WriteLine(stringBuilder.ToString());
                }
                Console.WriteLine("\nFile successfully created.\n");
                Console.ReadKey();


            }
        }


        public static void TraverseDirectory(DirectoryInfo directoryInfo, StringBuilder stringBuilder)
        {


            var xmlSettings = new XmlWriterSettings

            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                Indent = true
            };

            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlSettings);
            xmlWriter.WriteStartElement("node");
            xmlWriter.WriteAttributeString("type", "document");
            xmlWriter.WriteAttributeString("action", "create");
            xmlWriter.WriteAttributeString("Folder", "Folder");




            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                string title = Path.GetFileNameWithoutExtension(file.Name);

                xmlWriter.WriteStartElement("node");
                xmlWriter.WriteAttributeString("type", "document");
                xmlWriter.WriteAttributeString("action", "create");

                xmlWriter.WriteStartElement("title");
                xmlWriter.WriteAttributeString("language", "en_US");
                xmlWriter.WriteString(title);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteElementString("location", Location);
                xmlWriter.WriteElementString("file", file.FullName);
                xmlWriter.WriteEndElement();    //node
            }

            directoryInfo.GetDirectories().AsParallel().ForAll(x => TraverseDirectory(x, stringBuilder));

            xmlWriter.WriteElementString("location", Location);
            xmlWriter.WriteElementString("Folder", directoryInfo.FullName);
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();







        }
    }
}
