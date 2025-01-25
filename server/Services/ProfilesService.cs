
namespace keepr.Services;


public class ProfilesService
{

  public ProfilesService(ProfilesRepository repository)
  {
    _repository = repository;
  }

  private readonly ProfilesRepository _repository;

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _repository.GetProfileById(profileId);
    if (profile == null) throw new Exception("INVALID PROFILE ID DOESNT EXIST");
    return profile;
  }
}



