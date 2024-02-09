using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string pathToInputFile = "путь_к_вашему_файлу.txt"; // Укажите путь к файлу
        string pathToOutputFile = "результат.txt";

        try
        {
            var wordCount = new Dictionary<string, int>();
            using (var reader = new StreamReader(pathToInputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Удаление знаков препинания, кроме апострофов, и приведение к нижнему регистру
                    string cleanedLine = Regex.Replace(line, @"[\p{P}-['’]]+", "").ToLower();

                    foreach (var word in cleanedLine.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string cleanedWord = word.Trim();
                        if (cleanedWord != "")
                        {
                            if (!wordCount.ContainsKey(cleanedWord))
                                wordCount[cleanedWord] = 0;
                            wordCount[cleanedWord]++;
                        }
                    }
                }
            }

            // Сортировка и запись в файл
            using (var writer = new StreamWriter(pathToOutputFile))
            {
                foreach (var pair in wordCount.OrderByDescending(pair => pair.Value))
                {
                    writer.WriteLine($"{pair.Key}\t\t{pair.Value}");
                }
            }

            Console.WriteLine("Обработка завершена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}

