using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DienMay
{

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    /// 
    public partial class MainView : Window
    {
        public event EventHandler QuayLai;
        public MainView()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            QuayLai(this, new EventArgs());
        }


    }
}
