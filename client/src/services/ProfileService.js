import { Profile } from "@/models/Profile.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"
import { Vault } from "@/models/Vault.js"

class ProfileService {
  async getUsersVaults(profileId) {
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
    AppState.userVaults = vaults
  }
  async getUsersKeeps(profileId) {
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
    AppState.userKeeps = keeps
  }
  async getProfileById(profileId) {
    const response = await api.get(`api/profiles/${profileId}`)
    const profile = new Profile(response.data)
    AppState.activeProfile = profile
  }

}


export const profileService = new ProfileService