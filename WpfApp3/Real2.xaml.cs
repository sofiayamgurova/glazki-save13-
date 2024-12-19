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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Real2.xaml
    /// </summary>
    public partial class Real2 : Page
    {
        Agent currentAgent;
        public Real2(Agent SelectedAgent)
        {
            InitializeComponent();
            currentAgent = SelectedAgent;

            var currentSales = YamgurovaGlazkiSaveEntities.GetContext().ProductSale.ToList();

            if (SelectedAgent.ID != 0)
            {
                currentSales = currentSales.Where(p => p.AgentID == SelectedAgent.ID).ToList();
            }
            SalesListView.ItemsSource = currentSales;
            SalesListView.Items.Refresh();

            DeleteSale.Visibility = Visibility.Collapsed;
        }
        private void UpdateSales()
        {
            var currentSales = YamgurovaGlazkiSaveEntities.GetContext().ProductSale.ToList();

            if (currentAgent.ID != 0)
            {
                currentSales = currentSales.Where(p => p.Agent.ID == currentAgent.ID).ToList();
            }
            SalesListView.ItemsSource = currentSales;
            SalesListView.Items.Refresh();
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Real(currentAgent));
            UpdateSales();
            SalesListView.Items.Refresh();

        }

        private void DeleteSale_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> SelectedSales = SalesListView.SelectedItems.Cast<ProductSale>().ToList();

            foreach (ProductSale currentSales in SelectedSales)
            {
                YamgurovaGlazkiSaveEntities.GetContext().ProductSale.Remove(currentSales);
            }
            YamgurovaGlazkiSaveEntities.GetContext().SaveChanges();
            UpdateSales();
        }

        private void SalesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalesListView.SelectedItems.Count == 0)
                DeleteSale.Visibility = Visibility.Collapsed;
            if (SalesListView.SelectedItems.Count > 0)
                DeleteSale.Visibility = Visibility.Visible;
            UpdateSales();
            SalesListView.Items.Refresh();
        }
    }
}
