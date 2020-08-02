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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DailyCheckList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
            LoadItems();
            ToDoList.ItemsSource = Items;
        }
        public ObservableCollection<CheckActivity> Items { get; set; }
        private XMLDataHandler xml;
        private void LoadItems()
        {
            xml = new XMLDataHandler();
            Items = new ObservableCollection<CheckActivity>();
            List<CheckActivity> list = new List<CheckActivity>();
            xml.ExtractItems(list);
            foreach (var itm in list)
                Items.Add(itm);
        }
        private void Add_New_Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewWindow addNewWindow = new AddNewWindow();
            addNewWindow.SetList(Items);
            addNewWindow.ShowDialog();
        }

        public void AddItem(CheckActivity newItm)
        {
            if(newItm!=null)
            Items.Add(newItm);
        }

        private void UpdateItems()
        {
            List<CheckActivity> itmsToRemove = new List<CheckActivity>();
            List<CheckActivity> itmsToNotChecked = new List<CheckActivity>();
            foreach (var itm in Items)
            {
                if (itm.IsChecked)
                    itmsToRemove.Add(itm);
                else
                    itmsToNotChecked.Add(itm);
            }
            Items.Clear();
            foreach (var itm in itmsToNotChecked)
                Items.Add(itm);
            foreach (var itm in itmsToRemove)
                Items.Add(itm);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;
            if (null== thisButton) return;

            CheckActivity thsActivity = (CheckActivity)thisButton.DataContext;
            if (null == thsActivity) return;
            thsActivity.IsChecked = !thsActivity.IsChecked;

            if (thsActivity.IsChecked)
                thsActivity.Name = "[✓]" + thsActivity.Name.Substring(3);
            else
                thsActivity.Name = "[ ]" + thsActivity.Name.Substring(3);

            UpdateItems();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (xml != null)
            {
                List<CheckActivity> list = new List<CheckActivity>();
                foreach (var itm in Items)
                    list.Add(itm);
                xml.DumpItems(list);
            }
        }
    }
}
