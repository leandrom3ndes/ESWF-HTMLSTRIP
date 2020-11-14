using System;

namespace HtmlStrip
{
    public static class Interface
    {
        public static void LoadUI()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("------------------HtmlStrip------------------");
            Console.WriteLine("Nome dos elementos do grupo");
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

        private static void GetInput()  //Escolhe a ação a fazer
        {
            string option = ReadInput();
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
            //Methods.ExtractHtml(data);
            LoadSecondScreen();
            ExtrationMode(data);
        }

        private static void ExtrationMode(String data)
        {
            string option = ReadInput();
            string HtmlStriped = Methods.ExtractHtml(data);
            string HtmlStripedWithoutEmptySpaces = Methods.RemoveWhiteSpaces(HtmlStriped);
            switch (option)
            {
                case "1":
                    Console.WriteLine(HtmlStripedWithoutEmptySpaces);
                    break;
                case "2":
                    //Extrair "HtmlStriped" para o ficheiro de texto
                    //chamar função do Leandro
                    break;
                default:
                    Console.WriteLine("Selecione uma das opções anteriores.");
                    ExtrationMode(data);
                    break;
            }

        }

        private static String GetHtmlByUrl()  //executa 1º opção
        {
            Console.WriteLine("Escreva o URL desejado");
            Console.Write("? ");
            string urlInput = Console.ReadLine();
            string urlData = Methods.GetUrlData(urlInput);
            return urlData;

        }

        private static String GetHtmlByPath() //executa 2º opção
        {
            Console.WriteLine("Escreva o caminho do ficheiro Html");
            Console.Write("? ");
            string pathInput = Console.ReadLine();
            
            //dar return da função do Leandro que extrai o HTML através do caminho
            return pathInput;
        }

        private static String GetHtmlByInput() //executa 3º opção
        {
            return Methods.OptionPasteHTML();

        }

        private static String ReadInput()
        {
            Console.Write("?");
            string input = Console.ReadLine();
            return input;
        }
    }
}
