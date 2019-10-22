using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace ASP_templateatwpf
{
    /// <summary>
    /// Interaction logic for Video.xaml
    /// </summary>
    public partial class Video : Window
    {
        MainWindow parent;
        DispatcherTimer timer = new DispatcherTimer();
        public Video(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_tick;
            SomeMedia.Play();
            SomeMedia.Pause();
        }

        bool StartNPause = false;// true - played, false - paused

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.videoisopen = true;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            time.Content = SomeMedia.Position.ToString(@"mm\:ss");
            sliderBack.Value = SomeMedia.Position.TotalSeconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StartNPause)
            {
                SomeMedia.Play();
                StartNPause = false;
                timer.Start();
            }
            else
            {
                SomeMedia.Pause();
                StartNPause = true;
                timer.Stop();
            }
        }

        private void SliderBack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SomeMedia.Pause();
            SomeMedia.Position = TimeSpan.FromSeconds(sliderBack.Value);
            SomeMedia.Play();
        }

        private void SomeMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderBack.Maximum = SomeMedia.NaturalDuration.TimeSpan.TotalSeconds;
        }
    }
}
