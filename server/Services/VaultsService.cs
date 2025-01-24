

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

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetVaultById(vaultId);

    if (vault == null) throw new Exception($"INVALID VAULT ID: {vaultId}");

    return vault;
  }

  internal Vault UpdateVault(int vaultId, string userId, Vault vaultUpdateData)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId) throw new Exception("NOT YOUR VAULT PAL");

    vault.Name = vaultUpdateData.Name ?? vault.Name;
    vault.IsPrivate = vaultUpdateData.IsPrivate;


    _repository.UpdateVault(vault);
    return vault;


  }

  internal string DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId) throw new Exception("THATS NOT UR VAULT BRO");
    _repository.DeleteVault(vaultId);
    return "Delete the vault";

  }
}