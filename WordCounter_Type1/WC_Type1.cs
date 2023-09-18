using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCounterLib;

namespace WordCounter_Type1
{
    public class WC_Type1 : WordCounters
    {
        public WC_Type1(string text, Action<WordCounters> update):base(text, update)
        {
            process = new(() => {
                SplitWords();
                if (is_stopped is not true) GetUniWords();
                if (is_stopped is not true) GetStatistic();
                if (is_stopped is true)
                {
                    statistic = new RepeatStatistic[0];
                    progress = 0d;
                    Update(this);
                }
            });
        }
        private void SplitWords()
        {
            var claer_text = text.Replace("[^a-zA-Z ']", "");
            all_words = claer_text.Split(new char[] { ' ', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            progress += 5;
            Update(this);
        }
        private void GetUniWords()
        {
            uni_words = all_words.Distinct().ToArray();
            progress += 5;
            Update(this);
        }
        private void GetStatistic()
        {
            double progress_bonus = 90d / uni_words.Length;
            var all_words_list = all_words.ToList();

            statistic = new RepeatStatistic[uni_words.Length];

            for (int i = 0; i < uni_words.Length; i++)
            {
                var count = all_words_list.RemoveAll((x) => x == uni_words[i]);
                statistic[i] = new(uni_words[i], count);
                progress += progress_bonus;
                if (is_stopped is true)
                    return;
                if (i % 100 == 0)
                    Update(this);
            }
            Array.Sort(statistic);
            Array.Reverse(statistic);

            Update(this);
        }
    }
}
