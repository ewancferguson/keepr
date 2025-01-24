


namespace keepr.Services;


public class KeepsService
{

  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  private readonly KeepsRepository _repository;
  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetKeepById(keepId);

    if (keep == null) throw new Exception($"INVALID KEEP ID : {keepId}");

    return keep;
  }

  internal Keep UpdateKeep(int keepId, string userId, Keep keepUpdateData)
  {
    Keep keep = GetKeepById(keepId);

    if (keep.CreatorId != userId) throw new Exception("INVALID NOT YOUR KEEP PAL");

    _repository.UpdateKeep(keep);
    return keep;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId) throw new Exception("NOT YOUR KEEP BUCKO");
    _repository.DeleteKeep(keepId);
    return "Keep Deleted";
  }
}