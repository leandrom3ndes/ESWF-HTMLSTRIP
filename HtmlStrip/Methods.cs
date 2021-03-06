﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlStrip
{
    public static class Methods
    {
        private static string fileName;
        public static string pathString;

        public static string GetUrlData(string urlAddress)  //dá return do HTML inteiro
        {
            string urlData;
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

        public static string ReadInput()
        {
            Console.Write("?");
            string input = Console.ReadLine();
            return input;
        }

        public static string ExtractHtml(string urlData)
        {
            string htmlAfterStrip = Regex.Replace(urlData, "<(.|\n)*?>", "");

            return htmlAfterStrip;
        }

        public static string RemoveWhiteSpaces( string value )
        {
            return Regex.Replace(value, @"\s+", "");
        }

        public static string OptionPasteHTML() 
        {
            string allLines = "";
            string line = "";
            ConsoleKeyInfo keyPressed;
            Console.WriteLine("Escreve ou cola o código, e depois numa linha vazia pressiona a tecla 'END'");
            while ( true )
            {   
                keyPressed = Console.ReadKey(true);
                if ( keyPressed.Key == ConsoleKey.End) break;
                line = Console.ReadLine();
                allLines += line;
            }
            return allLines;
        }

        public static void criarFicheiro( string data )
        {
            Console.WriteLine("Indique o diretorio para colocar o ficheiro:");
            string diretorio = Console.ReadLine();

            if(String.IsNullOrWhiteSpace(diretorio)){
                Console.WriteLine("Necessita de adicionar um diretorio (EX: C:\\Users..)!");
                criarFicheiro( data );
            }

            // Especificar um nome para a Pasta 
            string folderName = @diretorio;


            //Criar uma subPasta nesse diretorio com um nome especifico
            pathString = System.IO.Path.Combine(folderName, "PastaHTMLSTRIP");

            // Criar o diretorio
            System.IO.Directory.CreateDirectory(pathString);

            Console.WriteLine("Escreva o nome do Ficheiro:");
            string userName = Console.ReadLine();

            if(String.IsNullOrWhiteSpace(userName)){
                Console.WriteLine("Necessita de adicionar um nome ao ficheiro!");
                criarFicheiro( data );
            }

            //Criar o nome do ficheiro
            fileName = userName + ".txt";

            //Adicionar o nome do ficheiro ao diretorio
            pathString = System.IO.Path.Combine(pathString, fileName);

            writeFile( data );
        }
        private static void writeFile( string data )
        {
            //System.IO.File.Create faz overwrite do ficheiro caso não exista
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {
                    using (var sr = new System.IO.StreamWriter(fs))
                    {
                        sr.WriteLine( data );
                    }
                }
                
                // Verify the path that you have constructed.
                Console.WriteLine("Ficheiro criado com sucesso! \n Caminho do ficheiro: {0}\n", pathString);
            }
            else
            {
                Console.WriteLine("*********************************************");
                Console.WriteLine("-> O ficheiro \"{0}\" já existe. <-", fileName);
                Console.WriteLine("*********************************************");
                Console.WriteLine("");
                Interface.FileUI();
                Interface.GetInputFile(data);
                
            }

        }

        public static string readFile( string path)
        {

            string result = "";
            try
            {
                result = System.IO.File.ReadAllText( path );
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
    
}
