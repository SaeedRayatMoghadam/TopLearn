using TopLearn.Core.ViewModels.UserPanel;

namespace TopLearn.Core.Interfaces;

public interface IWalletService
{
    int GetWalletBalance(int userId);
    List<WalletViewModel> GetWalletHistory(int userId);

    void ChargeWallet(int userId, int amount, string description, bool isPaid = false);
}