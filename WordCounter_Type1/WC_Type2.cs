using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounterLib
{
    public class WC_Type2 : WordCounters
    {
        public WC_Type2(string text, Action<WordCounters> update) : base(text, update)
        {
            process = new(() => {
                all_words = string_Into_Words_FromArray(text);
                uni_words = all_words.Distinct().ToArray();
                progress = 20;
                Update(this);
                SetStatistic();
            });
        }
        public static string[] string_Into_Words_FromArray(string text)
        {
            string[] words = new string[Sum_Words(text) + 1];
            string word = "";
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case ' ':
                    case '.':
                    case ',':
                    case ':':
                    case ';':
                    case '\n':
                    case '\r':
                    case '!':
                    case '?':
                        if (i - 1 >= 0)
                            if (text[i - 1] != ' ' &&
                                    text[i - 1] != '.' &&
                                    text[i - 1] != ',' &&
                                    text[i - 1] != ':' &&
                                    text[i - 1] != ';' &&
                                    text[i - 1] != '\n' &&
                                    text[i - 1] != '?' &&
                                    text[i - 1] != '!' &&
                                    text[i - 1] != '\r')
                            {
                                words[j++] = word;
                                word = "";
                            }
                        break;
                    //Ignored
                    case '—':
                    case '"':
                    case '\t':
                    case '(':
                    case ')':
                    case ']':
                    case '[':
                    case '{':
                    case '}':
                    case '*':
                    case '^':
                    case '~':
                        break;
                    default://letters
                        word += text[i];
                        break;
                }
            }
            return words;
        }
        public static int Sum_Words(string text)
        {
            int words = 0;
            foreach (char symbol in text)
            {
                switch (symbol)
                {
                    case ' ':
                    case '.':
                    case ',':
                    case ':':
                    case ';':
                    case '\n':
                    case '\r':
                    case '!':
                    case '?':
                        {
                            words++;
                        }
                        break;
                }
            }
            return words;
        }
        private void SetStatistic()
        {
            double progress_bonus = 80d / uni_words.Length;
            statistic = new RepeatStatistic[uni_words.Length];
            int i = 0;
            foreach (string word in uni_words)
            {
                statistic[i++] = new RepeatStatistic(word, SearchWord(word, i));
                progress += progress_bonus;
                Update(this);
            }
        }
        private int SearchWord(string key_word, int from)
        {
            int sum_repeat = 0;
            for (int i = from; i < all_words.Length; i++)
            {
                if (all_words[i] != null)
                    if (Char.Equals(all_words[i].ToLower(), key_word.ToLower()))
                    {
                        sum_repeat++;
                    }
            }
            return sum_repeat;
        }
    }
}
