using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Xml;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Win32;
namespace ASP_templateatwpf
{

    public partial class MainWindow : Window
    {

        XmlDocument xDoc = new XmlDocument();
        XmlElement xRoot;
        XmlElement xRootSat;
        XmlElement xRootAny;

        System.Windows.Threading.DispatcherTimer timer;

        bool CustomDragMoveIsInButton = false;
        public bool firstanim = false;
        bool IsAnyPlanetActive = false;

        bool AstronautCheck = false;
        bool OptionsCheck = false;
        bool AboutCheck = false;

        BitmapImage checkmark_off = new BitmapImage(new Uri(@"Images/Buttons/checkmark_off.png", UriKind.RelativeOrAbsolute));
        BitmapImage checkmark_on = new BitmapImage(new Uri(@"Images/Buttons/checkmark_on.png", UriKind.RelativeOrAbsolute));


        string[] strplanets = Directory.GetFiles(@".../.../Images/Planets");
        List<string> filesname = Directory.GetFiles(@".../.../Images/Planets/").ToList();
        List<string> anyfilesname = Directory.GetFiles(@".../.../Images/AnyPlanets/").ToList();
        int length = new DirectoryInfo(".../.../Images/Planets").GetFiles().Length;
        int anylength = new DirectoryInfo(".../.../Images/AnyPlanets").GetFiles().Length;
        int currentimagenumber = 0;
        public ObservableCollection<string> list = new ObservableCollection<string>();

        public MainWindow()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            xDoc.Load(".../.../Info.xml");
            xRoot = xDoc.DocumentElement;
            xDoc.Load(".../.../SatellitesInfo.xml");
            xRootSat = xDoc.DocumentElement;
            xDoc.Load(".../.../AnyInfo.xml");
            xRootAny = xDoc.DocumentElement;
            InitializeComponent();
            //MainScroll.Height = 859;
            PlanetChanged(0, xRoot);
            Options.Opacity = 0;
            About.Opacity = 0;
            Astronaut.Opacity = 0;

            BackgroundSun.Visibility = Visibility.Hidden;
        }
        

        bool ReverseBool(bool value)
        {
            return value == true ? false : true;
        }

        Visibility ReverseVisability(Visibility vis)
        {
            return vis == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }

        ScrollBarVisibility ReverseVisability(ScrollBarVisibility vis)
        {
            return vis == ScrollBarVisibility.Hidden ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;
        }


        private void Hide_Menu_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = Hide_Menu_Button.ActualWidth;
            buttonAnimation.To = 32;
            buttonAnimation.Duration = TimeSpan.FromSeconds(0.15);
            Hide_Menu_Button.BeginAnimation(Button.WidthProperty, buttonAnimation);
            Hide_Menu_Button.BeginAnimation(Button.HeightProperty, buttonAnimation);

            //Hide_Menu_Button_Image.ImageSource = new BitmapImage(new Uri(".../.../Images/005.png",UriKind.Relative));
        }

        private void Hide_Menu_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = Hide_Menu_Button.ActualWidth;
            buttonAnimation.To = 30;
            buttonAnimation.Duration = TimeSpan.FromSeconds(0.15);
            Hide_Menu_Button.BeginAnimation(Button.WidthProperty, buttonAnimation);
            Hide_Menu_Button.BeginAnimation(Button.HeightProperty, buttonAnimation);
            //Hide_Menu_Button_Image.ImageSource = new BitmapImage(new Uri(".../.../Images/004.png", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (!firstanim)
            {
                ThicknessAnimation tas = new ThicknessAnimation();
                tas.From = Horisontal_new.Margin;
                tas.To = new Thickness(570, 23, 0, 0);
                tas.Duration = TimeSpan.FromSeconds(0.3);
                this.Horisontal_new.BeginAnimation(Rectangle.MarginProperty, tas);

                ThicknessAnimation tas1 = new ThicknessAnimation();
                tas1.From = Hide_Menu_Button.Margin;
                tas1.To = new Thickness(530, 27, 0, 0);
                tas1.Duration = TimeSpan.FromSeconds(0.29);
                this.Hide_Menu_Button.BeginAnimation(Rectangle.MarginProperty, tas1);
                firstanim = true;
            }
            else
            {
                ThicknessAnimation tas = new ThicknessAnimation();
                tas.From = Horisontal_new.Margin;
                tas.To = new Thickness(800, 25, -230, 0);
                tas.Duration = TimeSpan.FromSeconds(0.3);
                this.Horisontal_new.BeginAnimation(Rectangle.MarginProperty, tas);

                ThicknessAnimation tas1 = new ThicknessAnimation();
                tas1.From = Hide_Menu_Button.Margin;
                tas1.To = new Thickness(745, 27, 0, 0);
                tas1.Duration = TimeSpan.FromSeconds(0.29);
                this.Hide_Menu_Button.BeginAnimation(Rectangle.MarginProperty, tas1);
                firstanim = false;
            }

        }


        private void PlanetChanged(int number, XmlElement root)
        {
            CurrentSattelite.Source = new BitmapImage(new Uri(@"Images/Satellites/default.png", UriKind.RelativeOrAbsolute));
            SatLittleInfo.Text = "";
            SatMass.Text = "";
            SatName.Text = "";
            SatSize.Text = "";
            SatType.Text = "";
            SatVolume.Text = "";
            BitmapImage buf;
            if(!IsAnyPlanetActive)
            buf = new BitmapImage(new Uri(filesname[number].Substring(7, filesname[number].Length-7), UriKind.RelativeOrAbsolute));
            else buf = new BitmapImage(new Uri(anyfilesname[number].Substring(7, anyfilesname[number].Length-7), UriKind.RelativeOrAbsolute));
            PlanetImage.Source = buf;
            list.Clear();
            CurrentPlanet.Source = PlanetImage.Source;
            Add.Text = "";
            ShortInfo.Text = "";
            foreach (XmlNode xnode in root)
            {
                if (xnode.Attributes != null)
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("number");
                        if (attr != null)
                            if (int.Parse(attr.Value) != number)
                                continue;
                    }
                    else continue;
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "name")
                            Name.Text = childnode.InnerText;
                        if (childnode.Name == "size")
                            Size.Text = childnode.InnerText;
                        if (childnode.Name == "mass")
                            Mass.Text = childnode.InnerText;
                        if (childnode.Name == "volume")
                            Volume.Text = childnode.InnerText;
                        if (childnode.Name == "age")
                            Age.Text = childnode.InnerText;
                        if (childnode.Name == "type")
                            Type.Text = childnode.InnerText;
                        if (childnode.Name == "shortinfo")
                            ShortInfo.Text += childnode.InnerText;
                        if (childnode.Name == "longinfo")
                            LongInfo.Text = childnode.InnerText;
                        if (childnode.Name == "peculiar_properties")
                        {
                            if (childnode.ChildNodes.Count > 0)
                            {
                                foreach (XmlNode _childnode in childnode.ChildNodes)
                                {
                                    Add.Text += $"{_childnode.Attributes.GetNamedItem("name").Value.ToString()}\n {_childnode.InnerText}\n";
                                }
                                Add.Text += "====================\n";
                            }
                        }
                        if (childnode.Name == "additional_feature")
                        {
                            if (childnode.ChildNodes.Count > 0)
                            {
                                foreach (XmlNode _childnode in childnode.ChildNodes)
                                {
                                    ShortInfo.Text += $"{_childnode.Attributes.GetNamedItem("name").Value.ToString()}\n {_childnode.InnerText}\n";
                                }
                                ShortInfo.Text += "====================\n";
                            }
                        }
                        if (childnode.Name == "satellites")
                        {
                            if (childnode.ChildNodes.Count > 0)
                            {
                                foreach (XmlNode _childnode in childnode.ChildNodes)
                                {
                                    list.Add(_childnode.Attributes.GetNamedItem("name").Value.ToString());
                                }
                                Satellites.ItemsSource = list;
                                var item = (TextBlock)Satellites.SelectedValue;
                                try
                                {
                                    SatelliteSet(number, item.Text);
                                }
                                catch (Exception)
                                {

                                }

                            }
                        }
                    }//can i use switch?
                    break;
                }






            }
        }

        private void SatelliteSet(int planetnum, string name)
        {
            foreach (XmlNode xnode in xRootSat)
            {
                if (xnode.Attributes != null)
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("planetnum");
                        if (attr != null)
                            if (int.Parse(attr.Value) != planetnum)
                                continue;
                    }
                }
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Attributes.GetNamedItem("name").Value.ToString() == name)
                    {
                        SatName.Text = childnode.Attributes.GetNamedItem("name").Value.ToString();
                        foreach (XmlNode _childnode in childnode.ChildNodes)
                        {
                            if (_childnode.Name == "imagename")
                                CurrentSattelite.Source = new BitmapImage(new Uri(@"Images/Satellites/" + _childnode.InnerText, UriKind.RelativeOrAbsolute));
                            if (_childnode.Name == "name")
                                SatName.Text = _childnode.InnerText;
                            if (_childnode.Name == "size")
                                SatSize.Text = _childnode.InnerText;
                            if (_childnode.Name == "mass")
                                SatMass.Text = _childnode.InnerText;
                            if (_childnode.Name == "volume")
                                SatVolume.Text = _childnode.InnerText;
                            if (_childnode.Name == "type")
                                SatType.Text = _childnode.InnerText;
                            if (_childnode.Name == "littleinfo")
                                SatLittleInfo.Text = _childnode.InnerText;
                        }
                    }
                }
            }
        }
        private void Leftmovebut_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(SwitchInfoTickLeft);
            timer.Interval = TimeSpan.FromSeconds(0.49);
            timer.Start();
        }

        
        private void Rightmovebut_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(SwitchInfoTickRight);
            timer.Interval = TimeSpan.FromSeconds(0.49);
            timer.Start();
        }

        private void SwitchInfoTickLeft(object sender, EventArgs e)
        {

            currentimagenumber--;
            if (!IsAnyPlanetActive)
            {
                if (currentimagenumber < 0)
                    currentimagenumber = length - 1;


                PlanetChanged(currentimagenumber, xRoot);
            }
            else
            {
                if (currentimagenumber < 0)
                    currentimagenumber = anylength - 1;
                PlanetChanged(currentimagenumber, xRootAny);
            }
            if (currentimagenumber == 0 && !IsAnyPlanetActive) BackgroundSun.Visibility = Visibility.Hidden;
            else BackgroundSun.Visibility = Visibility.Visible;
            timer.Stop();
            timer.Tick -= new EventHandler(SwitchInfoTickLeft);
        }

        private void SwitchInfoTickRight(object sender, EventArgs e)
        {

            currentimagenumber++;
            if (!IsAnyPlanetActive)
            {
                if (currentimagenumber >= length)
                    currentimagenumber = 0;



                PlanetChanged(currentimagenumber, xRoot);
            }
            else
            {
                if (currentimagenumber >= anylength)
                    currentimagenumber = 0;
                PlanetChanged(currentimagenumber, xRootAny);
            }
            if (currentimagenumber == 0 && !IsAnyPlanetActive) BackgroundSun.Visibility = Visibility.Hidden;
            else BackgroundSun.Visibility = Visibility.Visible;
            timer.Stop();
            timer.Tick -= new EventHandler(SwitchInfoTickRight);
        }

        BitmapImage leftSelected = new BitmapImage(new Uri(".../.../Images/Buttons/right_selected.png", UriKind.RelativeOrAbsolute));
        BitmapImage rightSelected = new BitmapImage(new Uri(".../.../Images/Buttons/right.png", UriKind.RelativeOrAbsolute));
        BitmapImage leftOut = new BitmapImage(new Uri(".../.../Images/Buttons/left_selected.png", UriKind.RelativeOrAbsolute));
        BitmapImage rightOut = new BitmapImage(new Uri(".../.../Images/Buttons/left.png", UriKind.RelativeOrAbsolute));

        private void Leftmovebut_MouseEnter(object sender, MouseEventArgs e)
        {
            leftmovebut_image.ImageSource = leftSelected;
        }

        private void Leftmovebut_MouseLeave(object sender, MouseEventArgs e)
        {
            leftmovebut_image.ImageSource = rightSelected;
        }

        private void Rightmovebut_MouseEnter(object sender, MouseEventArgs e)
        {
            rightmovebut_image.ImageSource = leftOut;
        }

        private void Rightmovebut_MouseLeave(object sender, MouseEventArgs e)
        {
            rightmovebut_image.ImageSource = rightOut;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Satellites.SelectedValue;
            try
            {
                SatelliteSet(currentimagenumber, item.ToString());
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!CustomDragMoveIsInButton)
                this.DragMove();
        }

        private void Minimize_MouseEnter(object sender, MouseEventArgs e)
        {
            CustomDragMoveIsInButton = true;
            Minimize.Foreground = Brushes.White;
        }

        private void Minimize_MouseLeave(object sender, MouseEventArgs e)
        {
            CustomDragMoveIsInButton = false;
            Minimize.Foreground = null;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            CustomDragMoveIsInButton = true;
            Exit.Foreground = Brushes.White;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            CustomDragMoveIsInButton = false;
            Exit.Foreground = null;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            currentimagenumber = 0;
            if (!IsAnyPlanetActive)
            {
                AnyPlanetCheckMark.Source = checkmark_on;
                IsAnyPlanetActive = true;
                PlanetChanged(0, xRootAny);
            }
            else
            {
                AnyPlanetCheckMark.Source = checkmark_off;
                IsAnyPlanetActive = false;
                PlanetChanged(0, xRoot);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (!OptionsCheck)
            {
                if (AstronautCheck)
                {
                    AstronautCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(Astronaut, 1, 0);
                    AstronautCheck = false;
                }
                if (AboutCheck)
                {
                    AboutCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(About, 1, 0);
                    AboutCheck = false;
                }
                OptionsCheckMark.Source = checkmark_on;
                OptionsCheck = true;
                ChangingGridVisibility(Options, 0, 1);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (!AboutCheck)
            {
                if (AstronautCheck)
                {
                    AstronautCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(Astronaut, 1, 0);
                    AstronautCheck = false;
                }
                if (OptionsCheck)
                {
                    OptionsCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(Options, 1, 0);
                    OptionsCheck = false;
                }
                AboutCheckMark.Source = checkmark_on;
                AboutCheck = true;
                ChangingGridVisibility(About, 0, 1);
            }
        }

       

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (!AstronautCheck)
            {
                if (AboutCheck)
                {
                    AboutCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(About, 1, 0);
                    AstronautCheck = false;
                }
                if (OptionsCheck)
                {
                    OptionsCheckMark.Source = checkmark_off;
                    ChangingGridVisibility(Options, 1, 0);
                    OptionsCheck = false;
                }
                AstronautCheckMark.Source = checkmark_on;
                AstronautCheck = true;
                ChangingGridVisibility(Astronaut, 0, 1);
            }
        }

        void ChangingGridVisibility(Grid _this, double from, double to)
        {
            DoubleAnimation tas = new DoubleAnimation();
            tas.From = from;
            tas.To = to;
            tas.Duration = TimeSpan.FromSeconds(0.3);
            _this.BeginAnimation(Grid.OpacityProperty, tas);
            if (to == 1)
                Panel.SetZIndex(_this, 1);
            else Panel.SetZIndex(_this, 0);
        }

        private void IconChange_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.ICO)|*.ICO";
            if (fileDialog.ShowDialog() == true)
            {
                this.Icon = new BitmapImage(new Uri(fileDialog.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void BackgroundChange(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files(*.*)|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                Background.ImageSource = new BitmapImage(new Uri(fileDialog.FileName.ToString(), UriKind.RelativeOrAbsolute));
            }
        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {
            Reminding_The_User rtu = new Reminding_The_User(this);
            MainScroll.ScrollToHome();
            //double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            //double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            //rtu.Top = (screenHeight - this.Height) / 0x00000002;
            //rtu.Left = (screenWidth - this.Width) / 0x00000002;

            rtu.Top = Top;
            rtu.Left = Left;
            rtu.Owner = this;
            rtu.Show();
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Автор: Власов Т. С. \n Сделано в 4 часа утра за 10 ч.\n Эта программа не будет продаваться с рекламой, потому что никто не купит это", "About Author",MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        public bool basicisopen = false;
        Basic basic;
        private void Basic_Click(object sender, RoutedEventArgs e)
        {
            if (!basicisopen)
            {
                basic = new Basic(this);
                basic.Show();
                basicisopen = true;
            }
            else basic.Focus();

        }
        public bool videoisopen = false;
        Video video;
        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!videoisopen)
            {
                video = new Video(this);
                video.Show();
                videoisopen = true;
            }
            else video.Focus();
        }
    }
}
