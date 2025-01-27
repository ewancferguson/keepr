import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { Keep } from "@/models/Keep.js";
import { AppState } from "@/AppState.js";

class KeepsService {
  async createKeep(keepData) {
    AppState.activeKeep = null
    const response = await api.post('api/keeps', keepData)
    logger.log(keepData)
    const keep = new Keep(response.data)
    AppState.activeKeep = keep
    AppState.keeps.push(keep)
  }
  async getKeepById(keepId) {
    AppState.activeKeep = null
    const response = await api.get(`api/keeps/${keepId}`)
    const keep = new Keep(response.data)
    AppState.activeKeep = keep
  }
  async getAllKeeps() {
    const response = await api.get('api/keeps')
    logger.log(response.data)
    const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
    logger.log(keeps)
    AppState.keeps = keeps;
  }

}


export const keepsService = new KeepsService;