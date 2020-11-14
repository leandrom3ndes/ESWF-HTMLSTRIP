using System;
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
    class Program
    {
        private String urlData;
        static void Main(string[] args)
        {
            Program callMethod = new Program();
            callMethod.getUrlData();
            callMethod.FinalResult();
            //Console.WriteLine(callMethod.urlData);
        }

        private String writeUrl()
        {
            Console.WriteLine("Escreva o URL desejado.");
            Console.Write("? ");
            string UrlInput = Console.ReadLine();
            return UrlInput;
        }

        private void getUrlData()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(writeUrl());
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
        }

        private String SearchHtml()
        {
            return Regex.Replace(urlData, "<.*?>", "");
        }

        private void FinalResult()
        {
            String cleanHtml = SearchHtml();
            Console.WriteLine(cleanHtml);
        }
    }
}

