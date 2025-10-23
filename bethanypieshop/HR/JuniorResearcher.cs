using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bethanypieshop.HR
{
    internal class JuniorResearcher : Employee
    {
        public JuniorResearcher(string firstName, string lastName, string email, DateTime birthDay, double? hourlyRate) : base(firstName, lastName, email, birthDay, hourlyRate)
        {
           
        }

    }
}
