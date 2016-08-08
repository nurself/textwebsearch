using System;
using websearch.Models;

namespace websearch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Program prog = new Program();
                prog.Start(args[0]);
            }
            catch (ProgramException ex)
            {
                Console.WriteLine(ex.ExtraErrorInfo);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message +
                                  Environment.NewLine +
                                  "Run websearch -mode" +
                                  Environment.NewLine +
                                  "Parameter -mode can be: " +
                                  Environment.NewLine +
                                  "1 - Yandex" +
                                  Environment.NewLine +
                                  "2 - Google");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Start(String mode)
        {
            Console.Write("Please input string for web search: "); 
            String input = Console.ReadLine();
            Page page;
            switch (mode)
            {
                case "1":
                    YandexSearch yandex = new YandexSearch();
                    page = yandex.Search(input);
                    break;
                case "2":
                    GoogleSearch google = new GoogleSearch();
                    page = google.Search(input);
                    break;
                default:
                    throw new Exception();
            }
            Console.WriteLine(page);
        }
    }
}
