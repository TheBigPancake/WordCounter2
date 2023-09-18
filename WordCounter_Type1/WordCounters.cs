using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounterLib
{
    public abstract class WordCounters
    {
        protected string text;
        protected string[] all_words;
        protected string[] uni_words;
        protected bool is_stopped;
        protected double progress;
        protected RepeatStatistic[] statistic;
        protected Thread process;
        protected Action<WordCounters> Update;

        public WordCounters(string text, Action<WordCounters> update)
        {
            this.text = text;
            is_stopped = true;
            statistic = new RepeatStatistic[0];
            all_words = new string[0];
            uni_words = new string[0];
            progress = 0;
            Update = update;
        }

        public void StartCount()
        {
            is_stopped = false;
            process.Start();
        }
        public void StopCount()
        {
            is_stopped = true;
        }
        public double GetProgress { get { return progress; } }
        public int GetNumberOfWords { get { return all_words.Length; } }
        public int GetNumberOfUniWords { get { return statistic.Length; } }
        public RepeatStatistic[] GetResult { get
            {
                if (statistic == null || statistic.Length == 0) return Array.Empty<RepeatStatistic>();
                return statistic;
            } }
    }
}
