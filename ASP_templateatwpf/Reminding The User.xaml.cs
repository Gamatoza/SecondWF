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
namespace ASP_templateatwpf
{
    /// <summary>
    /// Interaction logic for Reminding_The_User.xaml
    /// </summary>
    

    public partial class Reminding_The_User : Window
    {
        MainWindow main;
        System.Windows.Threading.DispatcherTimer timer;
        System.Windows.Threading.DispatcherTimer flashingtimer;
        int currentless=0;

        public Reminding_The_User(MainWindow parent)
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            flashingtimer = new System.Windows.Threading.DispatcherTimer();
            flashingtimer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Pursue);
            timer.Interval = TimeSpan.FromTicks(30);
            main = parent;
            InitializeComponent();
            HiddenMenu();
            
            Second.Opacity = 0;
            Third.Opacity = 0;
            Fourth.Opacity = 0;
            Fifth.Opacity = 0;


            flashingtimer.Tick += Less;
            flashingtimer.Start();

        }

        void Pursue(object sender, EventArgs e)
        {
            main.Left = Left;
            main.Top = Top;
        }

        void Less(object sender, EventArgs e)
        {
            Flashing(leftellipse);
            Flashing(rightellipse);
            Flashing(LeftRect);
            Flashing(RightRect);
            Flashing(ThirdEllipse);
            Flashing(Last_Rectangle);
        }

        void ChangeTheLesson(int numofless)
        {
            switch (numofless)
            {
                case 0:
                    ChangingGridVisibility(First, 1);
                    ChangingGridVisibility(Second, 0);
                    break;
                case 1:
                    //-=third
                    ChangingGridVisibility(First, 0);
                    ChangingGridVisibility(Second, 1);
                    ChangingGridVisibility(Third, 0);
                    break;
                case 2:
                    if (main.firstanim)
                        HiddenMenu();
                    ChangingGridVisibility(Second, 0);
                    ChangingGridVisibility(Third, 1);
                    ChangingGridVisibility(Fourth, 0);
                    break;
                case 3:
                    if (!main.firstanim)
                        HiddenMenu();
                    ChangingGridVisibility(Third, 0);
                    ChangingGridVisibility(Fourth, 1);
                    ChangingGridVisibility(Fifth, 0);
                    break;
                case 4:
                    if (main.firstanim)
                        HiddenMenu();
                    ChangingGridVisibility(Fourth, 0);
                    ChangingGridVisibility(Fifth, 1);
                    break;
                case 5:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        void ChangingGridVisibility(Grid _this, double from, double to)
        {
            DoubleAnimation tas = new DoubleAnimation();
            tas.From = from;
            tas.To = to;
            tas.Duration = TimeSpan.FromSeconds(0.3);
            _this.BeginAnimation(Grid.OpacityProperty, tas);
        }

        void ChangingGridVisibility(Grid _this, double to)
        {
            DoubleAnimation tas = new DoubleAnimation();
            tas.From = _this.Opacity;
            tas.To = to;
            tas.Duration = TimeSpan.FromSeconds(0.3);
            _this.BeginAnimation(Grid.OpacityProperty, tas);
        }

        void Flashing(Ellipse _this)
        {
            DoubleAnimation tas = new DoubleAnimation();
            tas.From = 1;
            tas.To = 0;
            tas.AutoReverse = true;
            tas.Duration = TimeSpan.FromSeconds(0.5);
            _this.BeginAnimation(Ellipse.OpacityProperty, tas);
        }

        void Flashing(Rectangle _this)
        {
            DoubleAnimation tas = new DoubleAnimation();
            tas.From = 1;
            tas.To = 0;
            tas.AutoReverse = true;
            tas.Duration = TimeSpan.FromSeconds(0.5);
            _this.BeginAnimation(Rectangle.OpacityProperty, tas);
        }

        void HiddenMenu()
        {
            if (main.firstanim == false)
            {
                ThicknessAnimation tas = new ThicknessAnimation();
                tas.From = main.Horisontal_new.Margin;
                tas.To = new Thickness(570, 23, 0, 0);
                tas.Duration = TimeSpan.FromSeconds(0.3);
                main.Horisontal_new.BeginAnimation(Rectangle.MarginProperty, tas);

                ThicknessAnimation tas1 = new ThicknessAnimation();
                tas1.From = main.Hide_Menu_Button.Margin;
                tas1.To = new Thickness(530, 27, 0, 0);
                tas1.Duration = TimeSpan.FromSeconds(0.29);
                main.Hide_Menu_Button.BeginAnimation(Rectangle.MarginProperty, tas1);
                main.firstanim = true;
            }
            else
            {
                ThicknessAnimation tas = new ThicknessAnimation();
                tas.From = main.Horisontal_new.Margin;
                tas.To = new Thickness(800, 25, -230, 0);
                tas.Duration = TimeSpan.FromSeconds(0.3);
                main.Horisontal_new.BeginAnimation(Rectangle.MarginProperty, tas);

                ThicknessAnimation tas1 = new ThicknessAnimation();
                tas1.From = main.Hide_Menu_Button.Margin;
                tas1.To = new Thickness(745, 27, 0, 0);
                tas1.Duration = TimeSpan.FromSeconds(0.29);
                main.Hide_Menu_Button.BeginAnimation(Rectangle.MarginProperty, tas1);
                main.firstanim = false;
            }
        }

        private void Left_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentless--;
            if (currentless < 0) currentless = 0;
            else ChangeTheLesson(currentless);
        }

        private void Right_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentless++;
            ChangeTheLesson(currentless);
        }

        private void CloseThis_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            this.Close();
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            this.DragMove();
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.Focus();
        }
    }
}
