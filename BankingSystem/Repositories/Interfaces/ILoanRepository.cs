using BankingSystem.Domain.Entities;

namespace BankingSystem.Repositories.Interfaces;

public interface ILoanRepository
{
    Loan GetById(int loanId);
    IEnumerable<Loan> GetByCustomerId(int customerId);
    IEnumerable<Loan> GetAll();
    void Add(Loan loan);
    void Update(Loan loan);
}
