namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  private Account GetAccount(string accountId)
  {
    Account account = _repo.GetById(accountId);
    if (account == null)
    {
      throw new Exception("Invalid Account Id");
    }
    return account;
  }

  internal Account GetOrCreateAccount(Account userInfo)
  {
    Account account = _repo.GetById(userInfo.Id);
    if (account == null)
    {
      return _repo.Create(userInfo);
    }
    return account;
  }

  internal Account Edit(Account editData, string accountId)
  {
    Account original = GetAccount(accountId);
    original.Name = editData.Name ?? editData.Name;
    original.Picture = editData.Picture ?? editData.Picture;
    return _repo.Edit(original);
  }

  internal Account UpdateAccount(Account userInfo, Account accountUpdateData)
  {
    userInfo.Name = accountUpdateData.Name ?? userInfo.Name;
    userInfo.Picture = accountUpdateData.Picture ?? userInfo.Picture;
    userInfo.CoverImg = accountUpdateData.CoverImg ?? userInfo.CoverImg;

    _repo.UpdateAccount(userInfo);
    return userInfo;
  }
}
