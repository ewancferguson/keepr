

namespace keepr.Services;


public class VaultKeepsService
{

  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;

  }

  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    Vault vault = _vaultsService.GetVaultById(vaultKeep.VaultId, userId);
    if (vault.CreatorId != userId) throw new Exception("You cant add things to someone elses vault");
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

