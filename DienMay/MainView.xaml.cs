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
            Content.Children.Add(new MainBanTraGop());
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            QuayLai(this, new EventArgs());
        }

        private void banHangTraGop_Click(object sender, RoutedEventArgs e)
        {
            MainBanTraGop main = Content.Children[0] as MainBanTraGop;
            if (main != null)
            {
                // Đang la ban hang tra gop
            }
            else
            {
                Content.Children.Clear();
                Content.Children.Add(new MainBanTraGop());
            }
        }

        private void choVay_Click(object sender, RoutedEventArgs e)
        {
            MainBanTraGop main = Content.Children[0] as MainBanTraGop;
            if (main != null)
            {
                Content.Children.Clear();
                Content.Children.Add(new MainChoVay());
            }
            else
            {

            }
        }
    }
}
