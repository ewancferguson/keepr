



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

  internal void UpdateKeep(Keep keep)
  {
    string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description
      WHERE id = @Id LIMIT 1;";


    int rowsAffected = _db.Execute(sql, keep);

    if (rowsAffected == 0) throw new Exception("UPDATE WAS UNSUCCESSFUL");
    if (rowsAffected > 1) throw new Exception("UPDATE WAS TOO SUCCESSFUL");

  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id = keepId });

    if (rowsAffected == 0) throw new Exception("Nothing was Deleted");
    if (rowsAffected > 1) throw new Exception("Too much was Deleted");
  }

  internal List<Keep> GetUsersKeeps(string userId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE creator_id = @userId;";


    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { userId }).ToList();

    return keeps;


  }

  internal List<Keep> GetMyKeeps(string userId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE creator_id = @userId;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { userId }).ToList();

    return keeps;
  }

  internal void IncrementVisits(Keep keep)
  {
    string sql = @"
    UPDATE keeps
    SET visits = @Visits
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, keep);

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} WERE UPDATED AND THAT IS BAD");

  }
}