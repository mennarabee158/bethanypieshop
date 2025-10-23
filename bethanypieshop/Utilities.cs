using bethanypieshop.HR;
using System.Text;


namespace bethanypieshop
{
    internal class Utilities
    {
        public static string directory = @"C:\Users\Rabee\source\repos\bethanypieshop\bethanypieshop\HR";
        public static string filePath = Path.Combine(directory, "employees.txt");

        internal static void CheckForExistingEmployeeFile()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine("Directory created and ready for saving files.");
            }
            else
            {
                Console.WriteLine("Directory already exists.");
            }

            if (File.Exists(filePath))
            {
                Console.WriteLine("An existing file with Employee data is found.");
            }
            else
            {
                // Create an empty file
                File.Create(filePath).Close();
                Console.WriteLine("File created for saving employee data.");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
        }

        internal static void RegisterEmployee(List<Employee> employees)
        {
            Console.WriteLine("Creating an employee");

            Console.WriteLine("What type of employee do you want to register?");
            Console.WriteLine("1. Employee\n2. Manager\n3. Store manager\n4. Researcher\n5. Junior researcher");
            Console.Write("Your selection: ");
            string employeeType = Console.ReadLine();

            if (employeeType != "1" && employeeType != "2" && employeeType != "3" &&
                employeeType != "4" && employeeType != "5")
            {
                Console.WriteLine("Invalid selection!");
                return;
            }

            Console.Write("Enter the first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the email: ");
            string email = Console.ReadLine();

            Console.Write("Enter the birth day: ");
            DateTime birthDay = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the hourly rate: ");
            string hourlyRate = Console.ReadLine();
            double rate = double.Parse(hourlyRate);

            Employee employee = null;

            switch (employeeType)
            {
                case "1":
                    employee = new Employee(firstName, lastName, email, birthDay, rate);
                    break;
                case "2":
                    employee = new Manager(firstName, lastName, email, birthDay, rate);
                    break;
                case "3":
                    employee = new StoreManager(firstName, lastName, email, birthDay, rate);
                    break;
                case "4":
                    employee = new Researcher(firstName, lastName, email, birthDay, rate);
                    break;
                case "5":
                    employee = new JuniorResearcher(firstName, lastName, email, birthDay, rate);
                    break;
            }

            employees.Add(employee);

            Console.WriteLine("Employee created!\n\n");
        }

        internal static void ViewAllEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].DisplayEmployeeDetails();
            }
        }

        internal static void LoadEmployeeById(List<Employee> employees)
        {
            Console.Write("Enter the Employee ID you want to visualize: ");
            try
            {
                int selectedId = int.Parse(Console.ReadLine());

                Employee selectedEmployee = employees[selectedId];
                selectedEmployee.DisplayEmployeeDetails();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That's not the correct format to enter an ID!\n\n");
                Console.ResetColor();
            }
        }

        internal static void LoadEmployees(List<Employee> employees)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    employees.Clear();

                    // Read the file
                    string[] employeesAsString = File.ReadAllLines(filePath);
                    foreach (var employeeString in employeesAsString)
                    {
                        string[] employeeSplits = employeeString.Split(';');
                        string firstName = employeeSplits[0].Substring(employeeSplits[0].IndexOf(':') + 1);
                        string lastName = employeeSplits[1].Substring(employeeSplits[1].IndexOf(':') + 1);
                        string email = employeeSplits[2].Substring(employeeSplits[2].IndexOf(':') + 1);
                        DateTime birthDay = DateTime.Parse(employeeSplits[3].Substring(employeeSplits[3].IndexOf(':') + 1));
                        double hourlyRate = double.Parse(employeeSplits[4].Substring(employeeSplits[4].IndexOf(':') + 1));
                        string employeeType = employeeSplits[5].Substring(employeeSplits[5].IndexOf(':') + 1);

                        Employee employee = employeeType switch
                        {
                            "1" => new Employee(firstName, lastName, email, birthDay, hourlyRate),
                            "2" => new Manager(firstName, lastName, email, birthDay, hourlyRate),
                            "3" => new StoreManager(firstName, lastName, email, birthDay, hourlyRate),
                            "4" => new Researcher(firstName, lastName, email, birthDay, hourlyRate),
                            "5" => new JuniorResearcher(firstName, lastName, email, birthDay, hourlyRate),
                            _ => null
                        };

                        employees.Add(employee);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Loaded {employees.Count} employees!\n\n");
                }
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is FileNotFoundException || ex is FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error parsing the file or file not found: " + ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        internal static void SaveEmployees(List<Employee> employees)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                string type = GetEmployeeType(employee);

                sb.Append($"firstName:{employee.FirstName};");
                sb.Append($"lastName:{employee.LastName};");
                sb.Append($"email:{employee.Email};");
                sb.Append($"birthDay:{employee.BirthDay.ToShortDateString()};");
                sb.Append($"hourlyRate:{employee.HourlyRate};");
                sb.Append($"type:{type};");
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(filePath, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved employees successfully");
            Console.ResetColor();
        }

        static string GetEmployeeType(Employee employee)
        {
            return employee switch
            {
                Manager => "2",
                StoreManager => "3",
                JuniorResearcher => "5",
                Researcher => "4",
                Employee => "1",
                _ => "0"
            };
        }
    }
}

