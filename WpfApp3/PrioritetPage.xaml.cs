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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для PrioritetPage.xaml
    /// </summary>
    public partial class PrioritetPage : Window
    {
        private Agent currentAgent = new Agent();
        public PrioritetPage(int max)
        {
            InitializeComponent();
            PriorityBox.Text = max.ToString();
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriorityBox.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введите новый приоритет для агента");
            }
        }

        private void CloseBut_Click(object sender, RoutedEventArgs e)
        {
            PriorityBox.Text = "";
            Close();
        }

        private void PriorityBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
