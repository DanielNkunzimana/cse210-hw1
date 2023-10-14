using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = LoadScripturesFromFile("scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Please provide a file with scriptures.");
            return;
        }

        Random random = new Random();

        foreach (Scripture scripture in scriptures)
        {
            Console.WriteLine(scripture.GetFullText());

            Console.WriteLine("Please! Press Enter to hide some words or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            while (!scripture.IsHidden())
            {
                int wordIndex = random.Next(scripture.WordCount());
                scripture.HideWord(wordIndex);

                Console.Clear();
                Console.WriteLine(scripture.GetFullText());

                Console.WriteLine("Please! Press Enter to hide more words or type 'quit' to exit:");
                input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    break;
            }

            Console.Clear();
        }

        Console.WriteLine("Yes, All scriptures have been processed.");
    }

    static List<Scripture> LoadScripturesFromFile(string filename)
    {
        List<Scripture> scriptures = new List<Scripture>();
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                Scripture currentScripture = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                    {
                        if (currentScripture != null)
                            scriptures.Add(currentScripture);

                        currentScripture = new Scripture(line, "");
                    }
                    else if (currentScripture != null)
                    {
                        currentScripture.AddText(line);
                    }
                }

                if (currentScripture != null)
                    scriptures.Add(currentScripture);
            }
        }
        return scriptures;
    }
}

class Scripture
{
    private string reference;
    private string text;
    private List<string> hiddenWords;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        hiddenWords = new List<string>();
    }

    public void AddText(string text)
    {
        this.text += " " + text;
    }

    public string GetFullText()
    {
        return reference + " " + text;
    }

    public int WordCount()
    {
        return text.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public void HideWord(int index)
    {
        string[] words = text.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (index >= 0 && index < words.Length)
        {
            hiddenWords.Add(words[index]);
            words[index] = new string('_', words[index].Length);
            text = string.Join(" ", words);
        }
    }

    public bool IsHidden()
    {
        return hiddenWords.Count == WordCount();
    }
}
