using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCalculator.After
{
    internal static class CreditLimitCalculator
    {
        public static bool Calculate(Company company, Customer customer)
        {
            if (company.Type == "VeryImportantClient")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (company.Type == "ImportantClient")
            {
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;

                var creditLimit = _creditService.GetCreditLimit(
                    customer.FirstName,
                    customer.LastName,
                    customer.DateOfBirth);

                creditLimit *= 2;
                customer.CreditLimit = creditLimit;
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;

                var creditLimit = _creditService.GetCreditLimit(
                    customer.FirstName,
                    customer.LastName,
                    customer.DateOfBirth);

                customer.CreditLimit = creditLimit;
            }
        }
    }
}
