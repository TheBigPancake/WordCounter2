using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using WordCounterLib;

namespace WordCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WC_builder window_bulder;
        public MainWindow()
        {
            InitializeComponent();
            this.window_bulder = new(this);
        }
        public void UpdateElements(WordCounters counter)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateStatusBar(counter.GetProgress);
                if (counter.GetProgress >= 100)
                    UpdateDateGrid(counter.GetResult);
            });
        }
        private void UpdateStatusBar(double num)
        {
            if (num >= 0)
            {
                progress.Value = num;
                progress_text.Content = ((int)num) + "%";
            }
            else
                throw new Exception("The status bar cannot be below zero");
        }
        private void UpdateDateGrid(RepeatStatistic[] item)
        {
            if (item != null)
                grid.ItemsSource = item;
            else
                grid.ItemsSource = Array.Empty<RepeatStatistic>();
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            window_bulder.StartScanning();
            start_button.IsEnabled = false;
            cancel_button.IsEnabled = true;
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            window_bulder.StopScaning();
            start_button.IsEnabled = true;
            cancel_button.IsEnabled = false;
            UpdateStatusBar(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select text file";
            dialog.Filter = "Text files(*.txt)|*.txt";
            dialog.DefaultExt = Directory.GetCurrentDirectory();
            dialog.ShowDialog();
            
            file_path.Text = dialog.FileName;
            cancel_button.IsEnabled = false;
            start_button.IsEnabled = false;
            UpdateDateGrid(null);
            UpdateStatusBar(0);

            if (dialog.FileName is "") return;
            start_button.IsEnabled = true;
            window_bulder.FilePath = dialog.FileName;
        }
    }
}
