


namespace keepr.Repositories;


public class KeepsRepository
{
  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO
    keeps(name, description, img, creator_id)
    VALUES(@Name, @Description, @Img, @CreatorId);
    
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE keeps.id = LAST_INSERT_ID();";

    Keep keep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).SingleOrDefault();

    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
      SELECT
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON keeps.creator_id = accounts.id
      WHERE keeps.id = @keepId;";

    Keep keep = _db.Query(sql, (Keep keep, Profile account) =>
  {
    keep.Creator = account;
    return keep;
  }, new { keepId }).SingleOrDefault();

    return keep;
  }
}