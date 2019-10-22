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

namespace ASP_templateatwpf
{


    public partial class test : Window
    {
        public test()
        { 
            InitializeComponent();
            textbox.Visibility = Visibility.Hidden;
            
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.ClearFocus();
            Keyboard.Focus(textbox);
        }

        private void Textbox_KeyDown_1(object sender, KeyEventArgs e)
        {
            text.Text = textbox.Text;
        }
    }
}
