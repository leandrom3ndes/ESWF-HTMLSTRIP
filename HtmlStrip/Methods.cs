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
        private static string fileName;
        private static string pathString;

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

    public static void criarFicheiro( string data )
    {
        Console.WriteLine("Indique o diretorio para colocar o ficheiro:");
        string diretorio = Console.ReadLine();
        // Especificar um nome para a Pasta 
        string folderName = @diretorio;
        //string folderName = @"C:\Users\joaol\OneDrive\Documentos\GitHub\ESWF-HTMLSTRIP\HtmlStrip\FicheirosCodigo";

        //Criar uma subPasta nesse diretorio com um nome especifico
        pathString = System.IO.Path.Combine(folderName, "SubFolder");

        // Criar o diretorio
        System.IO.Directory.CreateDirectory(pathString);

        Console.WriteLine("Escreva o nome do Ficheiro:");
        string userName = Console.ReadLine();
        //Criar o nome do ficheiro
        fileName = userName + ".txt";

        // This example uses a random string for the name, but you also can specify
        // a particular name.
        //string fileName = "MyNewFile.txt";

        // Use Combine again to add the file name to the path.
        pathString = System.IO.Path.Combine(pathString, fileName);

        // Verify the path that you have constructed.
        Console.WriteLine("Path to my file: {0}\n", pathString);
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
        }
        else
        {
            Console.WriteLine("Ficheiro \"{0}\" já existe.", fileName);
            return;
        }

    }

    public static String readFile( String path)
    {

        string result = "";
        try
        {
            result = System.IO.File.ReadAllText( path );
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        
        return result;
        }
    }
}
