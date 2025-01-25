namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{


  public ProfilesController(Auth0Provider auth0Provider, ProfilesService profilesService)
  {

    _auth0Provider = auth0Provider;

    _profilesService = profilesService;
  }


  private readonly Auth0Provider _auth0Provider;

  private readonly ProfilesService _profilesService;


  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}