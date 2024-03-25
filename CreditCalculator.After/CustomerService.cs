namespace CreditCalculator.After;

// Issues:
// - Move validation logic to a separate class
// - Create a method to calculate age
// - Move company and credit check logic to a separate class
// - Change company type to an enum
// ...


public class CustomerService
{
    private readonly CompanyRepository _companyRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly CustomerCreditServiceClient _creditService;

    public CustomerService(CompanyRepository companyRepository, CustomerRepository customerRepository, CustomerCreditServiceClient creditService)
    {
        _companyRepository = companyRepository;
        _customerRepository = customerRepository;
        _creditService = creditService;
    }

    public bool AddCustomer(
        string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth,
        int companyId)
    {

        if (!CustomerValidator.Validate(firstName, lastName, email, dateOfBirth))
        {
            return false;
        }

        var company = _companyRepository.GetById(companyId);

        var customer = new Customer
        {
            Company = company,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

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

        if (customer.HasCreditLimit && customer.CreditLimit < 500)
        {
            return false;
        }

        _customerRepository.AddCustomer(customer);

        return true;
    }
}