using TopLearn.Core.Interfaces;
using TopLearn.Core.ViewModels.UserPanel;
using TopLearn.Data.Context;
using TopLearn.Data.Models.Wallets;

namespace TopLearn.Core.Services;

public class WalletService : IWalletService
{
    private readonly TopLearnDbContext _context;

    public WalletService(TopLearnDbContext context)
    {
        _context = context;
    }

    public int GetWalletBalance(int userId)
    {
        var deposits = _context.Wallets
            .Where(w => w.UserId == userId && w.TypeId == 1)
            .Select(w => w.Amount).ToList();

        var withdraw = _context.Wallets
            .Where(w => w.UserId == userId && w.TypeId == 2)
            .Select(w => w.Amount).ToList();

        return (deposits.Sum() - withdraw.Sum());
    }

    public List<WalletViewModel> GetWalletHistory(int userId)
    {
        return _context.Wallets
            .Where(w => w.UserId == userId && w.IsPaid)
            .Select(w => new WalletViewModel()
            {
                Amount = w.Amount,
                Type = w.TypeId,
                Description = w.Description,
                DateTime = w.CreateDate
            }).ToList();
    }

    public void ChargeWallet(int userId, int amount, string description, bool isPaid = false)
    {
        var wallet = new Wallet()
        {
            Amount = amount,
            UserId = userId,
            Description = description,
            IsPaid = isPaid,
            CreateDate = DateTime.Now,
            TypeId = 1
        };
        _context.Wallets.Add(wallet);
        _context.SaveChanges();
    }
}