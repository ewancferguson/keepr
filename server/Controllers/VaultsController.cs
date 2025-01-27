namespace keepr.Controllers;


[ApiController]
[Route("api/{controller}")]


public class VaultsController : ControllerBase
{
  public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, KeepsService keepsService)
  {
    _vaultsService = vaultsService;
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
  }

  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;



  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize]
  [HttpPut("{vaultId}")]

  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault updatedVault = _vaultsService.UpdateVault(vaultId, userInfo.Id, vaultUpdateData);
      return Ok(updatedVault);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.DeleteVault(vaultId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultedKeep>>> GetVaultedKeeps(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<VaultedKeep> vaultedKeeps = _vaultsService.GetVaultedKeeps(vaultId, userInfo?.Id);
      return Ok(vaultedKeeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
