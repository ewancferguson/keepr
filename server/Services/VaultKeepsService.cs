

namespace keepr.Services;


public class VaultKeepsService
{

  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
  }

  private readonly VaultKeepsRepository _repository;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }


  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null) throw new Exception("Doesn't exist");
    return vaultKeep;

  }

  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId) throw new Exception(" NOT YOUR VAULTKEEP PAL");

    _repository.DeleteVaultKeep(vaultKeepId);

    return "No longer vaulted";
  }
}

