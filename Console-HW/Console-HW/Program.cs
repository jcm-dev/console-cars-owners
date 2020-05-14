using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console_HW
{
    class Program
    {
        struct CarOwners
        {
            public string custName;
            public string custPhone;
            public string custEmail;
            public string custModel;
            public string carManufacturer;
            public string carPrice;
        }

        //read cars
        private static bool ReadCars(string fileName, List<Cars> cars)
        {
            const int CAR_MANUFACTURER = 0;
            const int CAR_MODEL = 1;
            const int CAR_PRICE = 2;
            char[] delim = { ',' };

            try
            {
                StreamReader inputFile = File.OpenText(fileName);

                while (!inputFile.EndOfStream)
                {
                    string line = inputFile.ReadLine();

                    string[] tokens = line.Split(delim);
                    string manufacturer = tokens[CAR_MANUFACTURER];
                    string model = tokens[CAR_MODEL];
                    string price = tokens[CAR_PRICE];

                    Cars car = new Cars(model);
                    car.Manufacturer = manufacturer;
                    car.Price = price;


                    cars.Add(car);
                }
                inputFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("DEBUG: ");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //read customer
        private static bool ReadCustomer(string fileName, List<Customers> customers)
        {
            const int CUST_NAME = 0;
            const int CUST_PHONE = 1;
            const int CUST_EMAIL = 2;
            const int CUST_MODEL = 3;
            char[] delim = { ',' };

            try
            {
                StreamReader inputFile = File.OpenText(fileName);

                while (!inputFile.EndOfStream)
                {
                    string line = inputFile.ReadLine();

                    string[] tokens = line.Split(delim);
                    string name = tokens[CUST_NAME];
                    string phone = tokens[CUST_PHONE];
                    string email = tokens[CUST_EMAIL];
                    string model = tokens[CUST_MODEL];

                    Customers cust = new Customers(name);
                    cust.Phone = phone;
                    cust.Email = email;
                    cust.Model = model;

                    customers.Add(cust);
                }
                inputFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("DEBUG: ");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //save customer
        private static void WriteCustomer(CarOwners[]carsOwners, string fileName)
        {
            try
            {
                StreamWriter outputFile = File.CreateText(fileName);
                foreach(CarOwners owner in carsOwners)
                {
                    outputFile.WriteLine(owner.custName 
                        + "," + owner.custPhone 
                        + "," + owner.custEmail 
                        + "," + owner.custModel
                        + "," + owner.carManufacturer
                        + "," + owner.carPrice);
                }
                outputFile.Close();
            }
            catch
            {
                Console.WriteLine("Error writing file");
            }
        }

        //assign car info to customer with the same model
        private static void AssignMethod(List<Cars>carList, List<Customers>customerList, ref CarOwners[]carsOwners)
        {
            for (int i = 0; i < customerList.Count; i++)
            {
                for (int carCount = 0; carCount < carList.Count; carCount++)
                {
                    if (customerList[i].Model == carList[carCount].Model)
                    {
                        //assign customer and car info to struct
                        CarOwners ownerStruct = new CarOwners();
                        ownerStruct.custName = customerList[i].Name;
                        ownerStruct.custEmail = customerList[i].Email;
                        ownerStruct.custPhone = customerList[i].Phone;
                        ownerStruct.custModel = customerList[i].Model;
                        ownerStruct.carManufacturer = carList[carCount].Manufacturer;
                        ownerStruct.carPrice = carList[carCount].Price;

                        //assign struct to list
                        carsOwners[i] = ownerStruct;
                    }
                }
            }
        }

        //display owners and their car information
        private static void DisplayCustomerCars(CarOwners[]carsOwners)
        {
            Console.WriteLine("\n" + "Customers and their cars: ");
            foreach (CarOwners c in carsOwners)
            {
                Console.WriteLine(c.custName + " " + c.custEmail + " " + c.custPhone + " " + c.carManufacturer + " " + c.custModel);
            }
        }

        private static void LinqSearchDisplay(CarOwners[]carsOwners)
        {
            Console.Write("\n" + "Search for Customer to find their car price:");
            string searchName = Console.ReadLine();

            //search using linq
            var foundName = carsOwners.Where(o => o.custName.StartsWith(searchName));

            foreach (CarOwners c in foundName)
            {
                Console.WriteLine("\n" + "Customer Price Search Results: " + c.custName + " " + c.carPrice);
            }
        }

        static void Main(string[] args)
        {
            const int NUMBER_OWNERS = 5;
            CarOwners[] carsOwners = new CarOwners[NUMBER_OWNERS];

            //lists of cars and owners
            List<Cars> carList = new List<Cars>();
            List<Customers> customerList = new List<Customers>();


            if (args.Length == 1 && args[0].Length > 0)
            {
                //read cars into list
                Console.WriteLine("\n" + "Types of cars:");
                if (ReadCars(args[0], carList))
                {
                    //read cars to user
                    foreach (Cars car in carList)
                    {
                        Console.WriteLine(car.Model + " " + car.Manufacturer + " " + car.Price);
                    }
                }
                else
                {
                    Console.WriteLine("Error reading file;");
                }
                //read customer
                Console.Write("\n" + "Enter customers.csv to load customer list: ");
                string custList = Console.ReadLine();
                Console.WriteLine("\n" + "Customers: ");
                if (ReadCustomer(custList, customerList))
                {
                    foreach (Customers cust in customerList)
                    {
                        Console.WriteLine(cust.Name + " " + cust.Phone + " " + cust.Email + " " + cust.Model);
                    }
                }
                else
                {
                    Console.WriteLine("Error reading file;");
                }

                //match and assign customers to their model information
                AssignMethod(carList, customerList, ref carsOwners);

                //save customer and their cars
                WriteCustomer(carsOwners, "Result.csv");

                //display joined array of customers and their cars
                DisplayCustomerCars(carsOwners);

                //search for car owner and display car price
                LinqSearchDisplay(carsOwners);  
            }
            else
            {
                Console.WriteLine("Please enter file name/path");
            }
            }
        }
    }
