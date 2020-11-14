using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlStrip
{
    public static class Methods
    {

        public static String GetUrlData(String urlAddress)  //dá return do HTML inteiro
        {
            String urlData;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                urlData = readStream.ReadToEnd(); //a partir daqui podemos processar a string que recebemos e apenas ficar com o text que está dentro das tags de texto
                response.Close();
                readStream.Close();
            }
            else
            {
                Console.WriteLine("Não foi possível estabelecer uma ligação com sucesso ao Url inserido.");
                urlData = "";
            }
            return urlData;
        }

        public static String ReadInput()
        {
            Console.Write("?");
            string input = Console.ReadLine();
            return input;
        }

        public static String ExtractHtml(String urlData)
        {
            String htmlAfterStrip = Regex.Replace(urlData, "<.*?>", "");

            return htmlAfterStrip;
        }

        public static String RemoveWhiteSpaces( String value )
        {
            return Regex.Replace(value, @"\s+", "");
        }

        public static String OptionPasteHTML() 
        {
            string allLines = "";
            string line = "";
            ConsoleKeyInfo keyPressed;
            Console.WriteLine("Paste your code, and then on an empty line press 'END' key");
            while ( true )
            {   
                keyPressed = Console.ReadKey(true);
                if ( keyPressed.Key == ConsoleKey.End) break;
                line = Console.ReadLine();
                allLines += line;
            }
            return allLines;
        }
    }
}
