


namespace keepr.Repositories;


public class VaultKeepsRepository
{

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO
    vault_keeps(vault_id, keep_id, creator_id)
    VALUES(@VaultId, @KeepId, @CreatorId);
    
    
    SELECT * FROM vault_keeps WHERE id = LAST_INSERT_ID();";


    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
    return vaultKeep;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = "SELECT * FROM vault_keeps WHERE id = @vaultKeepID;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();

    return vaultKeep;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vault_keeps WHERE id = @vaultKeepId";

    int rowsAffected = _db.Execute(sql, new { vaultKeepId });

    switch (rowsAffected)
    {
      case 1: return;

      case 0: throw new Exception("NO ROWS UPDATED");

      default: throw new Exception($"{rowsAffected} ROWS WERE UPDATED AND THAT IS BAD");
    }

  }
}