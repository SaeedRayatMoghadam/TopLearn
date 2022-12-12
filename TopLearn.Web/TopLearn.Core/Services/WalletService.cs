using TopLearn.Core.DTOs.UserPanel;
using TopLearn.Core.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Core.Services;

public class WalletService : IWalletService
{
    private readonly TopLearnContext _context;

    public WalletService(TopLearnContext context)
    {
        _context = context;
    }

    public List<UserTransactionsViewModel> GetUserTransactions(string username)
    {
        var userId = GetUserId(username);

        return _context.Transactions.Where(t => t.UserId == userId && t.IsPayed)
            .Select(t => new UserTransactionsViewModel()
            {
                Amount = t.Amount,
                Description = t.Description,
                Type = t.TransactionType,
                CreateDate = t.CreateDate
            }).ToList();
    }

    public int GetUserId(string username)
    {
        return _context.Users.Single(u => u.UserName == username).Id;
    }

    public int GetUserWalletBalance(string username)
    {
        var userId = GetUserId(username);

        var deposit = _context.Transactions
            .Where(t => t.UserId == userId && t.TransactionType == 1 && t.IsPayed)
            .Select(t => t.Amount).ToList();
        var withdraw = _context.Transactions
            .Where(t => t.UserId == userId && t.TransactionType == 2 && t.IsPayed)
            .Select(t => t.Amount).ToList();

        return (deposit.Sum() - withdraw.Sum());
    }

    public void ChargeWallet(string username, int amount,bool isPayed = false)
    {
        var userId = GetUserId(username);

        _context.Transactions.Add(new Transaction()
        {
            UserId = userId,
            Amount = amount,
            CreateDate = DateTime.Now,
            Description = "واریز",
            IsPayed = isPayed,
            TransactionType = 1
        });
        _context.SaveChanges();
    }
}