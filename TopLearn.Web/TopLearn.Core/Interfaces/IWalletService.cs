using TopLearn.Core.DTOs.UserPanel;

namespace TopLearn.Core.Interfaces;

public interface IWalletService
{
    int GetUserId(string username);
    int GetUserWalletBalance(string username);
    List<UserTransactionsViewModel> GetUserTransactions(string username);

    void ChargeWallet(string username, int amount, bool isPayed=false);
}