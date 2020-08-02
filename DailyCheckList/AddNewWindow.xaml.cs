using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DailyCheckList
{
    /// <summary>
    /// Interaction logic for AddNewWindow.xaml
    /// </summary>
    public partial class AddNewWindow : Window
    {
        public AddNewWindow()
        {
            InitializeComponent();
        }
        ObservableCollection<CheckActivity> WindowItems;
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (WindowItems != null)
            {
                WindowItems.Add(new CheckActivity("[ ] "+TaskNameIn.Text,false)); ;
            }
            Close();
        }

        public void SetList(ObservableCollection<CheckActivity> list) { WindowItems = list; }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
