using System;
using System.Text;
using System.IO;


public class LoadTxtCsv
    {
        public readonly int FORMAT_CSV = 0;
        public readonly int FORMAT_TXT = 1;

        public int CorrectTxtCsv(int ind_format, string str_table_name)
        {
            // 0) исходные данные
            string str_name = $"{str_table_name}_";
            char char_column_divider;
            string str_ext;
            if (ind_format == FORMAT_CSV)
            {
                char_column_divider = ';';
                str_ext = "csv";
            }
            else if (ind_format == FORMAT_TXT)
            {
                char_column_divider = '\t';
                str_ext = "txt";
            }
            else
                return -101; // разрешены только форматы csv/txt /// ДЛЯ ТЕСТИРОВАНИЯ ////

            str_name += str_ext; // имя первоначального листа по формату, например "items_csv"
            string str_items = (string)ResTxtCsv.ResTxtCsv.ResourceManager.GetObject(str_name); // items_csv;
            if (str_items == null)
                return -102; // если ресурс не найден /// ДЛЯ ТЕСТИРОВАНИЯ ////

            int count = 0; // для подсчета числа замен
            Console.WriteLine("{0}\nВыведены начальные значения {1}.{2}", str_items, str_table_name, str_ext);

            //Console.ReadKey();////////
            // 1) удаляем лишние " см", исправляем опечатки
            StringBuilder sb = new StringBuilder(str_items);
            sb.Replace(" см", ""); // str_items.Replace(" см", ""); не работает!!!
            sb.Replace("Длинна", "Длина");
            str_items = sb.ToString();

            // 2) удаляем ',' добавляя строки в соотношении "один-ко-многим"
            string[] str_strings = str_items.Split(new string[] { "\r\n" },
              StringSplitOptions.RemoveEmptyEntries); // получаем набор строк таблицы

            for (int i = 0; i < str_strings.Length; i++) // перебираем все строки таблицы
            {
                // получаем набор колонок таблицы csv/txt
                string[] str_columns = str_strings[i].Split(char_column_divider);
                if (str_columns == null) return -103; /// ДЛЯ ТЕСТИРОВАНИЯ ////

                for (int j = 0; j < str_columns.Length; j++) // перебираем все столбцы для данной строки
                {
                    string[] str_cell = str_columns[j].Split(','); // получаем строку-столбец (ячейку) таблицы
                    int cell_count = str_cell.Length; // число элементов "один-ко-многим" в ячейке

                    if (cell_count <= 1)
                        continue; // пропускаем итерацию, если ячейка не разделена на 2 и более частей

                    int index_cell = str_strings[i].IndexOf(str_columns[j]); // индекс символа начала искомого столбца
                    int len_cell = str_columns[j].Length; // число символов в искомом столбце (ячейке таблицы)
                    int len_string = str_strings[i].Length; // число символов во всей строке
                    string str_begin = str_strings[i].Substring(0, Math.Max(index_cell - 1, 0)); // повторяющаяся часть начала строки
                    string str_end = "";

                    if (index_cell + len_cell + 1 < len_string)
                    {
                        str_end = str_strings[i].Substring(index_cell + len_cell + 1, len_string - len_cell - index_cell - 1);
                    }

                    for (int k = 0; k < cell_count; k++)
                    {
                        // обновляем/добавляем строки с "частью" ячейки str_cell // ячейка по частям csv/txt
                        string str_add = $"{str_begin}{char_column_divider}{str_cell[k]}{char_column_divider}{str_end}";
                        count++;

                        if (k == 0) // обновляем 0ю строку (например, для ячейки "10.1,10.2,10.3" останется "10.1")
                            sb.Replace(str_strings[i], str_add); // путем замены
                        else // добавляем 1ю и далее (для ячеки "10.1,10.2,10.3" добавятся строки с "10.2" и "10.3")
                            sb.AppendLine(str_add); // путем добавления новой строки
                    }
                }
            }

            str_items = sb.ToString(); // преобразуем обратно в строку

            string updFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "upd");
            Directory.CreateDirectory(updFolderPath); // Создаем папку, если ее нет

            string new_file_name = $"{str_table_name}_upd.{str_ext}";
            string path = Path.Combine(updFolderPath, new_file_name);
            File.WriteAllText(path, str_items, Encoding.Default); // в файл вида "\\items_upd.csv";
            Console.WriteLine("{0}\nПроизведено {1} замен в {2}.{3}", str_items, count, str_table_name, str_ext);

            //Console.ReadKey();

            return 0; // по умолчанию
        }
    }

