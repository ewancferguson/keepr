


namespace keepr.Repositories;

public class VaultsRepository
{


  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
      INSERT INTO
      vaults(name, description, is_private, img, creator_id)
      VALUES(@Name, @Description, @IsPrivate, @Img, @CreatorId);
      
      SELECT
      vaults.*,
      accounts.*
      FROM vaults
      JOIN accounts ON vaults.creator_id = accounts.id
      WHERE vaults.id = LAST_INSERT_ID();";


    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, vaultData).SingleOrDefault();

    return vault;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON vaults.creator_id = accounts.id
    WHERE vaults.id = @vaultId;";


    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { vaultId }).SingleOrDefault();
    return vault;
  }

  internal void UpdateVault(Vault vault)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    is_private = @IsPrivate
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, vault);

    if (rowsAffected == 0) throw new Exception("UPDATE WAS UNSUCCESSFUL");
    if (rowsAffected > 1) throw new Exception("UPDATE WAS TOO SUCCESSFUL");
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id = vaultId });

    if (rowsAffected == 0) throw new Exception("Nothing was Deleted");
    if (rowsAffected > 1) throw new Exception("Too much was Deleted");
  }

  internal List<VaultedKeep> GetVaultedKeeps(int vaultId)
  {
    string sql = @"
    SELECT 
    vault_keeps.*,
    keeps.*,
    vaults.*,
    accounts.*
    FROM vault_keeps
    JOIN keeps ON vault_keeps.keep_id = keeps.id
    JOIN vaults ON vault_keeps.vault_id = vaults.id
    JOIN accounts ON keeps.creator_id = accounts.id 
    WHERE vault_keeps.vault_id = @vaultId";


    List<VaultedKeep> vaultedKeeps = _db.Query(sql, (VaultKeep vaultKeep, VaultedKeep keep, Vault vault, Profile account) =>
    {
      keep.VaultKeepId = vaultKeep.Id;
      keep.Creator = account;
      return keep;
    }, new { vaultId }).ToList();

    return vaultedKeeps;


  }

  internal List<Vault> GetUsersVaults(string profileId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON vaults.creator_id = accounts.id
    WHERE creator_id = @profileId;";

    List<Vault> vaults = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { profileId }).ToList();

    return vaults;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON vaults.creator_id = accounts.id
    WHERE creator_id = @userId;";

    List<Vault> vaults = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { userId }).ToList();

    return vaults;
  }
}