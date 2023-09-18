using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WordCounter_Type1;
using WordCounterLib;

namespace WordCounter
{
    public class WC_builder
    {
        MainWindow window;
        public WordCounters counter;
        public string FilePath { get; set; }
        public WC_builder(MainWindow window)
        {
            this.window = window;
        }

        public void StartScanning()
        {
            var text = GetText(FilePath);
            counter = new WC_Type1(text, window.UpdateElements);
            counter.StartCount();
        }
        private string GetText(string path)
        {
            string text = "";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        public void StopScaning() 
        {
            counter.StopCount();
        }
    }
}
