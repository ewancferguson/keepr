<script setup>
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';




const editableVaultData = ref({
  name: '',
  img: '',
  description: '',
  isPrivate: false
})

async function createVault() {
  try {
    await vaultsService.createVault(editableVaultData.value)

    editableVaultData.value =
    {
      name: '',
      img: '',
      description: '',
      isPrivate: false
    }
    Pop.success("Vault Created")
    Modal.getInstance('#vaultModal').hide()
  }
  catch (error) {
    Pop.error(error);
  }
}


</script>


<template>

  <!-- Modal -->
  <div class="modal fade" id="vaultModal" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="vaultModalLabel">Add your vault</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>
        <form @submit.prevent="createVault()">
          <div class="modal-body">
            <div class="mb-3">
              <label for="vaultTitle" class="form-label visually-hidden">Title</label>
              <input v-model="editableVaultData.name" type="text" class="form-control" id="vaultTitle"
                placeholder="Title...">
            </div>
            <div class="mb-3">
              <label for="vaultURL" class="form-label visually-hidden">URL</label>
              <input v-model="editableVaultData.img" type="url" class="form-control" id="vaultURL" placeholder="URL...">
            </div>
            <div class="mb-3">
              <label for="vaultDescription" class="form-label visually-hidden">Description</label>
              <input v-model="editableVaultData.description" type="text" class="form-control" id="vaultDescription"
                placeholder="Description...">
            </div>
            <div class="form-check mb-3">
              <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input" id="vaultPrivate">
              <label class="form-check-label" for="vaultPrivate">Make Vault Private?</label>
              <small class="d-block text-muted">Private Vaults can only be seen by you</small>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create Vault</button>
          </div>
        </form>
      </div>
    </div>
  </div>


</template>


<style lang="scss" scoped></style>