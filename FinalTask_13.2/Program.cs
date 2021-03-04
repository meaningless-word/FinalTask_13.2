using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalTask_13._2
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = "Text1.txt";
			if (!File.Exists(path))
			{
				Console.WriteLine("Файл не нашёлся");
				return;
			}
			string readText = File.ReadAllText(path); 
			
			// удаление пунктуации и замена переносов строк на пробелы
			string noPunctuationText = new string(readText.Where(c => !char.IsPunctuation(c)).ToArray()).Replace("\n", " ");

			Console.WriteLine("объём файла до удаления знаков препинания {0} и после {1}", readText.Length, noPunctuationText.Length);
			
			//вау, кортежи пригодились! :)
			Dictionary<string, (string w, int c)> dctWords = new Dictionary<string, (string w, int c)>();
			String[] words = noPunctuationText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach(string word in words)
			{
				if (!dctWords.TryAdd(word, (w:word, c:1)))
				{
					dctWords[word] = (w:word, c:dctWords[word].c + 1);
				} 
			}

			List<(string w, int c)> backward = dctWords.Values.OrderByDescending(x => x.c).ToList();

			Console.WriteLine("Встречаем победителей:");
			for (int i = 0; i < backward.Count && i < 10; i++)
			{
				Console.WriteLine("слово \"{0}\" в количестве: {1}", backward[i].w, backward[i].c);
			}
		}
	}
}
