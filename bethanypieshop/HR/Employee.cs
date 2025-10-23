using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bethanypieshop.HR
{
    public class Employee
    {
        private string firstName;
        private string lastName;
        private string email;

        private int numberOfHoursWorked;
        private double wage;
        private double? hourlyRate;

        private DateTime birthDay;
        private const int minimalHoursWorkedUnit = 1;

        public static double taxRate = 0.15;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        public int NumberOfHoursWorked
        {
            get { return numberOfHoursWorked; }
            protected set
            {
                numberOfHoursWorked = value;
            }
        }

        public double Wage
        {
            get { return wage; }
            private set
            {
                wage = value;
            }
        }

        public double? HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (hourlyRate < 0)
                {
                    hourlyRate = 0;
                }
                else
                {
                    hourlyRate = value;

                }
            }
        }

        public DateTime BirthDay
        {
            get { return birthDay; }
            set
            {
                birthDay = value;
            }
        }

        public Employee(string firstName, string lastName, string email, DateTime birthDay)
            : this(firstName, lastName, email, birthDay, 0)
        {
        }

        public Employee(string firstName, string lastName, string email, DateTime birthDay, double? hourlyRate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDay = birthDay;
            HourlyRate = hourlyRate ?? 10;

        }

        public void DisplayEmployeeDetails()
        {
            Console.WriteLine($"\nFirst name: \t{FirstName}\nLast name: \t{LastName}\nEmail: \t\t{Email}\nBirthday: \t{BirthDay.ToShortDateString()}\n");
        }
        public void PerformWork()
        {
            PerformWork(minimalHoursWorkedUnit);
        }

        public void PerformWork(int numberOfHours)
        {
            NumberOfHoursWorked += numberOfHours;
            NumberOfHoursWorked++;

            Console.WriteLine($"{FirstName} {LastName} has worked for {numberOfHours} hour(s)!");
        }
        public int CalculateBonus(int bonus)
        {

            if (NumberOfHoursWorked > 10)
                bonus *= 2;

            Console.WriteLine($"The employee got a bonus of {bonus}");
            return bonus;
        }







    }
}
