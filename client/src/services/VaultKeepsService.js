import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { VaultKeep } from "@/models/VaultKeep.js"
import { AppState } from "@/AppState.js"
import { VaultedKeep } from "@/models/VaultedKeep.js"

class VaultKeepsService {
  async getVaultedKeeps(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    const vaultkeeps = response.data.map(vaultKeepPOJO => new VaultedKeep(vaultKeepPOJO))
    AppState.vaultedkeeps = vaultkeeps
  }
  async createVaultKeep(editableFavoriteData) {
    const response = await api.post('api/vaultkeeps', editableFavoriteData)
    logger.log("vault keep", response.data)

  }

}


export const vaultKeepsService = new VaultKeepsService