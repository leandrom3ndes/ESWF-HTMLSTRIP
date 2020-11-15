using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlStrip
{
    public static class Methods
    {
        private static string fileName;
        private static string pathString;

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
            string htmlAfterStrip = Regex.Replace(urlData, "<.*?>", "");

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
        // Especificar um nome para a Pasta 
        string folderName = @diretorio;

        //Criar uma subPasta nesse diretorio com um nome especifico
        pathString = System.IO.Path.Combine(folderName, "SubFolder");

        // Criar o diretorio
        System.IO.Directory.CreateDirectory(pathString);

        Console.WriteLine("Escreva o nome do Ficheiro:");
        string userName = Console.ReadLine();
        //Criar o nome do ficheiro
        fileName = userName + ".txt";

        // Use Combine again to add the file name to the path.
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
            Console.WriteLine("Ficheiro " + fileName + " já existe. Tenta outro nome.");
            criarFicheiro( data );
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
