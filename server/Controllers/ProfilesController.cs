namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{


  public ProfilesController(Auth0Provider auth0Provider, ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
  {

    _auth0Provider = auth0Provider;

    _profilesService = profilesService;

    _keepsService = keepsService;

    _vaultsService = vaultsService;
  }


  private readonly Auth0Provider _auth0Provider;

  private readonly ProfilesService _profilesService;
  private readonly KeepsService _keepsService;

  private readonly VaultsService _vaultsService;


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

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetUsersVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetUsersVaults(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}