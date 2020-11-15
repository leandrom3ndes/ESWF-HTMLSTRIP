using System;
using System.IO;
namespace HtmlStrip
{
    public static class Interface
    {
        public static void LoadUI()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("------------------HtmlStrip------------------");
            Console.WriteLine("Nome dos elementos do grupo:\nFrancisco Pontes\nDiogo Mesquita\nFrancisco Chaves\nLeandro Mendes");
            Console.WriteLine("*********************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Que método deseja usar para extrair o texto do Html desejado?");
            Console.WriteLine("1. Através do Url de uma página Web");
            Console.WriteLine("2. Ficheiro Local");
            Console.WriteLine("3. Introduzir conteúdo HTML manualmente");
            Console.WriteLine("");

            GetInput();
        }

        private static void LoadSecondScreen()
        {
            Console.Clear();
            Console.WriteLine("*********************************************");
            Console.WriteLine("------------------HtmlStrip------------------");
            Console.WriteLine("*********************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Escolher modo de visualização do texto extraído do HTML:");
            Console.WriteLine("1. Visualizar texto na consola");
            Console.WriteLine("2. Extrair para um ficheiro de texto");
            Console.WriteLine("");
        }

        public static void FileUI()
        {
            Console.WriteLine("Escolha o que deseja fazer");
            Console.WriteLine("1. Escrever o novo conteudo");
            Console.WriteLine("2. Apagar o conteudo e escrever o novo");
            Console.WriteLine("3. Ir para trás");
            Console.WriteLine("");

        }

        private static void GetInput()  //Escolhe a ação a fazer
        {   
            try {
                string option = Methods.ReadInput();
                string data;
                switch (option)
                {
                    case "1":
                        data = GetHtmlByUrl();
                        break;
                    case "2":
                        data = GetHtmlByPath();
                        break;
                    case "3":
                        data = GetHtmlByInput();
                        break;
                    default:
                        data = "";
                        Console.WriteLine("Selecione uma das opções anteriores.");
                        GetInput();
                        break;
                }
  
                LoadSecondScreen();
                ExtrationMode(data);
            }
            catch ( Exception e ){
                Console.WriteLine( "Exception:" + e.Message + "\n Tente novamente.");
                LoadUI();
            }
        }

        public static void GetInputFile(string dataString)  //Escolhe a ação a fazer
        {

            string option = Methods.ReadInput();
            switch (option)
            {
                case "1":
                    File.AppendAllText(Methods.pathString, "\r\n" + dataString);
                    Console.WriteLine("Novo conteudo foi adicionado com sucesso!");
                    break;
                case "2":
                     File.WriteAllText(Methods.pathString, dataString + "\r\n");
                     Console.WriteLine("Apagou o conteudo antigo, e escreveu um novo com sucesso!");
                    break;
                case "3":
                    LoadSecondScreen();
                    ExtrationMode(dataString);
                    break;
                default:
                    data = "";
                    Console.WriteLine("Selecione uma das opções anteriores.");
                    GetInput();
                    break;
            }
        }   

        private static void ExtrationMode(string data)
        {
            string option = Methods.ReadInput();
            string HtmlStriped = Methods.ExtractHtml(data);
            string HtmlStripedWithoutEmptySpaces = Methods.RemoveWhiteSpaces(HtmlStriped);

            switch (option)
            {
                case "1":
                    Console.WriteLine( "Resultado do HTMLStrip: \n" + HtmlStripedWithoutEmptySpaces);
                    break;
                case "2":
                    Methods.criarFicheiro( HtmlStripedWithoutEmptySpaces );
                    break;
                default:
                    Console.WriteLine("Selecione uma das opções anteriores.");
                    ExtrationMode(HtmlStripedWithoutEmptySpaces);
                    break;
            }

        }

        private static string GetHtmlByUrl()  //executa 1º opção
        {
            Console.WriteLine("Escreva o URL desejado");
            Console.Write("? ");
            string urlInput = Console.ReadLine();
            string urlData = Methods.GetUrlData(urlInput);
            return urlData;

        }

        private static string GetHtmlByPath() //executa 2º opção
        {
            Console.WriteLine("Escreva o caminho do ficheiro Html");
            Console.Write("? ");
            string pathInput = Console.ReadLine();
            
            return Methods.readFile(pathInput);
        }

        private static string GetHtmlByInput() //executa 3º opção
        {
            return Methods.OptionPasteHTML();
        }
    }
}
