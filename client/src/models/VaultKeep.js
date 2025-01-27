export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}