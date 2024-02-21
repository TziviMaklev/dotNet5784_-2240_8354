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
using BlApi;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;

        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList =new ObservableCollection<BO.Engineer> (s_bl?.Engineer.RequestEngineersList()!);
        }
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;
        private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Experience == BO.EngineerExperience.All) ?
              new ObservableCollection<BO.Engineer>(s_bl?.Engineer.RequestEngineersList()!) : new ObservableCollection<BO.Engineer>(s_bl?.Engineer.RequestEngineersList(item => item.Level == Experience)!);
        }
        private void viewEngineer(object sender, RoutedEventArgs e)
        {

            int? selectedEngId = ((sender as ListView)?.SelectedItem as BO.Engineer)?.Id;
            if (selectedEngId is not null)
                new EngineerWindow(selectedEngId.Value).ShowDialog();
        }

        private void AddEngineer(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }
    }

}
