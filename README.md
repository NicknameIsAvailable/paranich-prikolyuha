# ПР 04
## Как это сделать 
1. Откройте ResTxtCsv
2. Скиньте туда файлы название_таблицы_csv.csv, название_таблицы_txt.txt, другое_название_таблицы_csv.csv и другое_название_таблицы_txt.txt подобного содержания:
   ``` csv
    id,createdAt,updatedAt,name,description,iconName,color,isDefault,userId
    1,2024-04-28T12:00:00Z,2024-04-28T12:00:00Z,Food,Expenses related to food,food,#FF5733,true,1
    2,2024-04-28T12:00:00Z,2024-04-28T12:00:00Z,Transport,Transportation expenses,transport,#45B39D,false,1
    3,2024-04-28T12:00:00Z,2024-04-28T12:00:00Z,Entertainment,Entertainment expenses,entertainment,#9B59B6,false,2
  // получается обычный CSV файл. Попросите гпт сгенерить его по вашей теме
   ```
3. В Program.cs названия файлов заменить на свои
  ``` csharp
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

  ```
4. Запустите ConsoleApp1.csproj
5. Обновленные файлы появятся в bin/Debug/net8.0/upd
