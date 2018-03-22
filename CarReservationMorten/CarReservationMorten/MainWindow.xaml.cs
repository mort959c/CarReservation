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

namespace CarReservationMorten
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> employees = new List<Employee>();
        List<Car> cars = new List<Car>();
        Access a = new Access();

        public MainWindow()
        {
            InitializeComponent();
            fillCombobox();
        }

        // fills the cboEmployee with all the employees in the database
        public void fillCombobox()
        {
            employees.Clear();
            //instance of the access class used the call the methods from that class
            
            try
            {
                employees = a.getAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("noget gik galt det var ikke muligt at indlæse ansatte fra databasen \n\n" + ex);
            }

            cboEmployee.ItemsSource = employees;
            //preselects a item, to insure that a "legit" value is always chosen
            cboEmployee.SelectedIndex = 0;
        }

        // Fills the datagridCars with all the cars in the databse
        public void fillDataGridCars()
        {

        }

        private void btnNewCar_Click(object sender, RoutedEventArgs e)
        {

        }

        //event handler for btnShowAllCars and btnShowFreeCars
        private void AllAndFreeCarsBtns_Clicked(object sender, RoutedEventArgs e)
        {
            //if the car list contains anything
            if (!cars.Any())
            {
                try
                {
                    cars = a.getAllCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("noget gik galt da der blev forsøgt at hente biler fra databasen \n\n" + ex);
                }
            }

            if (listBoxCars.Items.Count != 0)
            {
                //clears the datagrid
                listBoxCars.Items.Clear();
            }

            // if show all cars was pressed all cars will be showed
            if (sender == btnShowAllCars)
            {
                foreach (Car c in cars)
                {
                    listBoxCars.Items.Add(c);
                }
            }
            // if show free cars was pressed all the cars that is free will be showed
            else
            {
                // checks each car in the list to see if its free if it is free it will get added to the datagrid
                foreach (Car c in cars)
                {
                    if (c.getBookingStatus())
                    {
                        listBoxCars.Items.Add(c);
                    }
                }
            }
        }

        private void cboEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Employee em in employees)
            {
                
            }
        }
    }
}
