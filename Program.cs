using System;
using System.IO;
using System.Text;

namespace ConsoleApp_LoadTxtCsv
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoadTxtCsv load = new LoadTxtCsv();
            load.CorrectTxtCsv(load.FORMAT_CSV, "expense");
            load.CorrectTxtCsv(load.FORMAT_CSV, "category");
            load.CorrectTxtCsv(load.FORMAT_CSV, "user");
            load.CorrectTxtCsv(load.FORMAT_TXT, "expense");
            load.CorrectTxtCsv(load.FORMAT_TXT, "category");
            load.CorrectTxtCsv(load.FORMAT_TXT, "user");
            Console.ReadKey();
        }
    }
}
