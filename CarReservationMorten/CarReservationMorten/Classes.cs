using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CarReservationMorten
{
    public class Employee
    {
        private string EmailDomain { get; set; } = "@ssa-nordjylland";
        private string FirstName { get; set; }
        private int Id { get; set; }
        private string Initials { get; set; }
        private string LastName { get; set; }
        private string PhoneNumber { get; set; }

        public Employee(int id, string firstName, string lastName, string phoneNumber, string initials)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Initials = initials; 
        }

        public override string ToString()
        {
            return $"{this.Initials} - {this.FirstName} - {this.LastName} - {this.PhoneNumber} - {this.Initials + this.EmailDomain}";
        }
    }

    public class Car
    {
        private int Id { get; set; }
        private bool IsAvailable { get; set; }
        private string LicensePlate { get; set; }
        private string Make { get; set; }
        private string Model { get; set; }
        private int ProductionYear { get; set; }

        public Car(int id, string licensePlate, int productionYear, string make, string model, bool isAvailable)
        {
            this.Id = id;
            this.LicensePlate = licensePlate;
            this.ProductionYear = productionYear;
            this.Make = make;
            this.Model = model;
            this.IsAvailable = isAvailable;
        }

        public Car(string licensePlate, int productionYear, string make, string model)
        {
            this.LicensePlate = licensePlate;
            this.ProductionYear = productionYear;
            this.Make = make;
            this.Model = model;
        }

        public bool getBookingStatus()
        {
            return this.IsAvailable;
        }

        public override string ToString()
        {
            return $"{this.Make} - {this.Model} - {this.LicensePlate} - {this.ProductionYear} - {this.IsAvailable.ToString()}";
        }
    }

    public class Access
    {
        //sql connection string to the database
        SqlConnection myConnection = new SqlConnection(@"Data Source = CV-PC-T-41\SQLEXPRESS; Initial Catalog = CarReservation; Integrated Security = True");
        SqlCommand myCommand = null;
        SqlDataReader myReader = null;

        // gets all the employees from the database
        public List<Employee> getAllEmployees()
        {
            //List that gets filled with all the employees before being returned
            List<Employee> employees = new List<Employee>();

            //simple sql command. gets all columns from table
            string sql = @"SELECT * FROM Employee";
            myCommand = new SqlCommand(sql, myConnection);

            try
            {
                myConnection.Open();
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    // creates an employee object with the data gotten from the database
                    Employee e = new Employee(Convert.ToInt32(myReader["ID"]),
                        myReader["FirstName"].ToString(),
                        myReader["LastName"].ToString(),
                        myReader["PhoneNumber"].ToString(),
                        myReader["Initials"].ToString());

                    //adds the newly create employee to the list
                    employees.Add(e);
                }

                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public List<Car> getAllCars()
        {
            List<Car> cars = new List<Car>();

            string sql = @"SELECT * FROM Cars";
            myCommand = new SqlCommand(sql, myConnection);

            try
            {
                myConnection.Open();
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    // 0 equals occupied 1 equals free
                    bool isavailable;
                    if (Convert.ToInt32(myReader["IsAvailable"]) == 1) isavailable = true;
                    else isavailable = false;

                        Car c = new Car(Convert.ToInt32(myReader["ID"]),
                        myReader["LicensePlate"].ToString(),
                        Convert.ToInt32(myReader["ProductionYear"]),
                        myReader["Make"].ToString(),
                        myReader["Model"].ToString(),
                        isavailable);

                    cars.Add(c);
                }
                return cars;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
