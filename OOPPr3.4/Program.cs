using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPPr3._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                string folderPath = "files";

                Func<string, IEnumerable<string>> takenFile = filePath =>
                {
                    string text = File.ReadAllText(filePath);
                    return text.Split(new[] { ' ', '.', ',', '!', '?', ';', ':', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                };

                Func<IEnumerable<string>, IDictionary<string, int>> countWords = words =>
                {
                    var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (var word in words)
                        if (wordCounts.ContainsKey(word))
                            wordCounts[word]++;
                        else
                            wordCounts[word] = 1;
                    return wordCounts;
                };

                Action<IDictionary<string, int>> displayWordCounts = wordCounts =>
                {
                    foreach (var pair in wordCounts.OrderByDescending(pair => pair.Value))
                        Console.WriteLine($"{pair.Key}: {pair.Value}");
                };

                var files = Directory.EnumerateFiles(folderPath, "*.txt");
            Console.WriteLine("Result of proccessing all files: ");
            foreach (var file in files)
                {
                    var words = takenFile(file);
                    var wordCounts = countWords(words);
                    Console.WriteLine($"Word counts in file {file}:");
                    displayWordCounts(wordCounts);
                    Console.WriteLine();
                }
            
        }
    }
}
