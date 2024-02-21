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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get;
        private StateOfWindow engWindowState;
        public static readonly DependencyProperty engineerProperty = 
            DependencyProperty.Register("currentEngineer", typeof(BO.Engineer), typeof(EngineerWindow));

        public BO.Engineer currentEngineer
        {
            get { return (BO.Engineer)GetValue(engineerProperty); }
            set { SetValue(engineerProperty, value); }
        }
        int Id = 0;
        public EngineerWindow(int id=0)
        {
            InitializeComponent();
            if (id == 0)
            {
                currentEngineer=new BO.Engineer();
                engWindowState = StateOfWindow.Add;
            }
            else
            {
                try
                {
                    engWindowState = StateOfWindow.Update;
                    currentEngineer = s_bl.Engineer.RequestEngineerDetails(id);
                }catch 
                {
                    MessageBox.Show("A system error occurred. Please try again.");
                }
            }
        }

        private void sendEngineer(object sender, RoutedEventArgs e)
        {
            try
            {
                if(engWindowState==StateOfWindow.Add)
                {
                    s_bl.Engineer.AddEngineer(currentEngineer);
                    MessageBox.Show("adding engineer success");
                }
                else
                {
                    s_bl.Engineer.UpdateEngineerDetails(currentEngineer);
                    MessageBox.Show("updating engineer success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
