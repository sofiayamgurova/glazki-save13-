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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;

        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public AgentPage()
        {
            InitializeComponent();
            var currentService = YamgurovaGlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentService;

            ComboType.SelectedIndex = 0;
            ComboAgentType.SelectedIndex = 0;
            UpdateAgents();

        }

        private void ChangePage(int direction, int? selectedPage)
        {

            CurrentPageList.Clear();
            CountRecords = TableList.Count;

            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else
            {
                CountPage = CountRecords / 10;
            }

            Boolean Ifupdate = true;

            int min;

            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:

                        if (CurrentPage > 0)
                        {


                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }

                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }

                        break;

                }
            }

            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;

                min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                TBCount.Text = min.ToString();
                TBAllRecords.Text = " из " + CountRecords.ToString();
                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();

            }


        }


        private void UpdateAgents()
        {
            var currentAgents = YamgurovaGlazkiSaveEntities.GetContext().Agent.ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBox_Search.Text.ToLower()) || p.Phone.Replace("-", " ").Replace("(", "").Replace(")", "").Replace(" ", "").Contains(TBox_Search.Text.ToLower()) || p.Email.ToLower().Contains(TBox_Search.Text.ToLower())).ToList();



            currentAgents = currentAgents.ToList();

            if (ComboAgentType.SelectedIndex == 1)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "МФО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 2)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ООО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 3)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ЗАО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 4)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "МКК").ToList();
            }

            if (ComboAgentType.SelectedIndex == 5)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ОАО").ToList();
            }

            if (ComboAgentType.SelectedIndex == 6)
            {
                currentAgents = currentAgents.Where(p => p.AgentType.Title == "ПАО").ToList();
            }

            if (ComboType.SelectedIndex == 1)
            {
                currentAgents = currentAgents.OrderBy(p => p.Title).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Title).ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
            }

            if (ComboType.SelectedIndex == 4)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentAgents = currentAgents.OrderBy(p => p. Discount).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentAgents = currentAgents.OrderByDescending(p => p.Discount).ToList();
            }


            AgentListView.ItemsSource = currentAgents.ToList();

            TableList = currentAgents;

            ChangePage(0, 0);

        }
       
        private void TBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }


        private void ComboAgentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();
        }



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Agent));

        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(1, null);
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(2, null);
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangePage(0, Convert.ToInt32(PageListBox.SelectedItem.ToString()) - 1);
        }
   

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                YamgurovaGlazkiSaveEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = YamgurovaGlazkiSaveEntities.GetContext().Agent.ToList();
            }
            UpdateAgents();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }
       
        public SolidColorBrush FonStyle
        {
            get
            {
                if (Discount > 24)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("LightGreen");

                }
                else
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("White");

                }
            }

        }
    }
}
    }
    }
