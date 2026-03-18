using LawOfDemeterAssignment.Domain;

namespace LawOfDemeterAssignment.Services;

public sealed class Paperboy
{
    public bool CollectPayment(Customer customer, decimal paymentAmount)
    {
        return customer.Pay(paymentAmount);
    }
}
