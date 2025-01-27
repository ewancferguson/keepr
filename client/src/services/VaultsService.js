import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Vault } from "@/models/Vault.js"
import { AppState } from "@/AppState.js"

class VaultsService {
  async createVault(vaultData) {
    const response = await api.post('api/vaults', vaultData)
    const vault = new Vault(response.data)
    AppState.myVaults.push(vault)
  }
  async getMyVaults() {
    const response = await api.get("account/vaults")
    logger.log("my vaults", response.data)
    const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
    AppState.myVaults = vaults

  }

}

export const vaultsService = new VaultsService