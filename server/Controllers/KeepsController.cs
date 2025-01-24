using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace keepr.Controllers;

[ApiController]
[Route("api/keeps")]

public class KeepsController : ControllerBase
{
  public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
  {
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
  }

  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;


  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetKeepById(keepId);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
