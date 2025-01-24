


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
}