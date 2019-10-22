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
using System.Windows.Media.Animation;
using System.Windows.Threading;
namespace ASP_templateatwpf
{
    /// <summary>
    /// Логика взаимодействия для Basic.xaml
    /// </summary>
    public partial class Basic : Window
    {
        DispatcherTimer timer;
        
        double ActualScroll = 0;
        double NeededScroll = 0;
        MainWindow parent;
        public Basic(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(40);
            scrollV.CanContentScroll = true;
            timer.Tick += ScrollTo;
        }

        void ScrollTo(object sender, EventArgs e)
        {
            if (ActualScroll < NeededScroll)
            {
                scrollV.LineDown();
                ActualScroll++;
            }
            else
            {
                scrollV.LineUp();
                ActualScroll--;
            }
            if (ActualScroll == (int)scrollV.ContentVerticalOffset) timer.Stop();
        }
        

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation das = new DoubleAnimation(300,TimeSpan.FromSeconds(0.3));
            Hidden.BeginAnimation(WidthProperty, das);
            ThicknessAnimation tas = new ThicknessAnimation(new Thickness(10, 1, -2, -3), TimeSpan.FromSeconds(0.3));
            HiddenContent.BeginAnimation(Rectangle.MarginProperty, tas);
            //10,1,-2,-3
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation das = new DoubleAnimation(50, TimeSpan.FromSeconds(0.3));
            Hidden.BeginAnimation(WidthProperty, das);
            //-241,-1,-1,-1
            ThicknessAnimation tas = new ThicknessAnimation(new Thickness(-241, -1, -1, -1), TimeSpan.FromSeconds(0.3));
            HiddenContent.BeginAnimation(Rectangle.MarginProperty, tas);
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ActualScroll = (int)scrollV.ContentVerticalOffset;
            //NeededScroll = 0;
            //timer.Start();
            scrollV.ScrollToHome();
        }

        private void Border_PreviewMouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            //ActualScroll = (int)scrollV.ContentVerticalOffset;
            //NeededScroll = 6838;
            //timer.Start();
            scrollV.ScrollToEnd();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.basicisopen = false;
        }
    }
}
