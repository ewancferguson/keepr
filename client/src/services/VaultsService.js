import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Vault } from "@/models/Vault.js"
import { AppState } from "@/AppState.js"

class VaultsService {
  async deleteVault(vaultId) {
    const response = await api.delete(`api/vaults/${vaultId}`)
    const vaultIndex = AppState.myVaults.findIndex(vault => vault.id == vaultId)
    AppState.myVaults.splice(vaultIndex, 1)

  }
  async getVaultById(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}`)
    const vault = new Vault(response.data)
    AppState.activeVault = vault
  }
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