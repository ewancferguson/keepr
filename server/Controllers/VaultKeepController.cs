namespace keepr.Controllers;
[ApiController]
[Route("api/vaultkeeps")]


public class VaultKeepsController : ControllerBase
{
  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
  }

  private readonly VaultKeepsService _vaultKeepsService;

  private readonly Auth0Provider _auth0Provider;



  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo.Id);
      return Ok(vaultKeep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}