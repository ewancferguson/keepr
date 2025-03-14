

namespace keepr.Services;

public class VaultsService
{

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  private readonly VaultsRepository _repository;

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {

    Vault vault = _repository.GetVaultById(vaultId);

    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("This vault doesnt exist pal");

    if (vault == null) throw new Exception($"INVALID VAULT ID: {vaultId}");

    return vault;
  }

  internal Vault UpdateVault(int vaultId, string userId, Vault vaultUpdateData)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("NOT YOUR VAULT PAL");

    vault.Name = vaultUpdateData.Name ?? vault.Name;
    vault.IsPrivate = vaultUpdateData.IsPrivate;


    _repository.UpdateVault(vault);
    return vault;


  }

  internal string DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("THATS NOT UR VAULT BRO");
    _repository.DeleteVault(vaultId);
    return "Delete the vault";

  }

  internal List<VaultedKeep> GetVaultedKeeps(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("This vault doesnt exist pal");


    List<VaultedKeep> vaultedKeep = _repository.GetVaultedKeeps(vaultId);

    return vaultedKeep;

  }

  internal List<Vault> GetUsersVaults(string profileId, string userId)
  {

    List<Vault> vaults = _repository.GetUsersVaults(profileId);
    return vaults.FindAll(vault => vault.CreatorId == userId || vault.IsPrivate == false);

  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> vaults = _repository.GetMyVaults(userId);
    return vaults;
  }
}