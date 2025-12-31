using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomerSearch
{
    private readonly DatabaseContext db;

    public CustomerSearch(DatabaseContext databaseContext)
    {
        db = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }


    public List<Customer> SearchByCountry(string country)
    {
        return SearchCustomers(c => c.Country.Contains(country));
    }

    public List<Customer> SearchByCompanyName(string companyName)
    {
        return SearchCustomers(c => c.CompanyName.Contains(companyName));
    }

    public List<Customer> SearchByContactName(string contactName)
    {
        return SearchCustomers(c => c.ContactName.Contains(contactName));
    }


    public string ExportToCsv(IEnumerable<Customer> customers)
    {
        if (customers == null)
        {
            throw new ArgumentNullException(nameof(customers));
        }

        var csvBuilder = new StringBuilder();

        foreach (Customer customer in customers)
        {
            csvBuilder.AppendLine(
                $"{customer.CustomerID},{customer.CompanyName},{customer.ContactName},{customer.Country}");
        }

        return csvBuilder.ToString();
    }

    private List<Customer> SearchCustomers(Func<Customer, bool> filter)
    {
        return db.Customers
                 .Where(filter)
                 .OrderBy(c => c.CustomerID)
                 .ToList();
    }
}