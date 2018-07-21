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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DienMay
{
    /// <summary>
    /// Interaction logic for MainChoVay.xaml
    /// </summary>
    public partial class MainChoVay : UserControl
    {
        private MainView mainView;

        public MainChoVay()
        {
            InitializeComponent();
        }

        public MainChoVay(MainView mainView)
        {
            this.mainView = mainView;
        }
    }
}
