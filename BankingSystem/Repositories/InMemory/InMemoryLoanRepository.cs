using BankingSystem.Domain.Entities;
using BankingSystem.Repositories.Interfaces;

namespace BankingSystem.Repositories.InMemory;

public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans;
    private int _nextId;

    public InMemoryLoanRepository()
    {
        _loans = new List<Loan>();
        _nextId = 1;
    }

    public void Add(Loan loan)
    {
        loan.LoanId = _nextId++;
        _loans.Add(loan);
    }

    public Loan GetById(int loanId)
    {
        return _loans.FirstOrDefault(l => l.LoanId == loanId);
    }

    public IEnumerable<Loan> GetByCustomerId(int customerId)
    {
        return _loans.Where(l => l.CustomerId == customerId).ToList();
    }

    public IEnumerable<Loan> GetAll()
    {
        return _loans.AsReadOnly();
    }

    public void Update(Loan loan)
    {
        var existing = GetById(loan.LoanId);
        if (existing != null)
        {
            var index = _loans.IndexOf(existing);
            _loans[index] = loan;
        }
    }
}
