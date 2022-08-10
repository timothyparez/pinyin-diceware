using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

const int LENGTH = 5;
const int COUNT = 10;
const int INDEX_DIGITS = 5;

using var rnd = RandomNumberGenerator.Create();

var wordList = File.ReadAllLines("pinyin.wordlist")
                   .Select(l => l.Split('\t', StringSplitOptions.RemoveEmptyEntries))
                   .Select(i => (int.Parse(i[0]), i[1], i[2]))
                   .ToDictionary(i => i.Item1);

var buffer = new byte[1];

for (int c = 0; c < COUNT; c++)
{
    var pinyinPhraseBuilder = new StringBuilder();
    var phraseBuilder = new StringBuilder();

    for (int i = 0; i < LENGTH; i++)
    {
        var wordIndexBuilder = new StringBuilder(5);

        for (int y = 0; y < INDEX_DIGITS; y++)
        {
            rnd.GetBytes(buffer);
            wordIndexBuilder.Append(buffer[0] % 6 + 1);
        }

        var wordIndex = int.Parse(wordIndexBuilder.ToString());
        var word = wordList[wordIndex];

        phraseBuilder.Append($"{word.Item3} ");
        pinyinPhraseBuilder.Append($"{word.Item2} ");
    }

    WriteLine($"{pinyinPhraseBuilder} ({phraseBuilder})");
}
