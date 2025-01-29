<script setup>
import { AppState } from '@/AppState';
import { keepsService } from '@/services/KeepsService';
import { vaultKeepsService } from '@/services/VaultKeepsService';
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref } from 'vue';

const keep = computed(() => AppState.activeKeep);
const vaults = computed(() => AppState.myVaults);



const editableVaultKeepData = ref({
  vaultId: 0,
  keepId: 0,
})


onMounted(() => {

});

async function getMyVaults() {
  try {
    await vaultsService.getMyVaults();
  } catch (error) {
    Pop.error(error);
  }
}


async function createVaultKeep() {
  try {

    editableVaultKeepData.value.keepId = keep.value.id
    await vaultKeepsService.createVaultKeep(editableVaultKeepData.value)
    Pop.success('Keep Vaulted')
  }
  catch (error) {
    Pop.error(error);
  }



}



async function deleteKeep(keepId) {
  try {
    Modal.getInstance('#keepModal').hide()
    await keepsService.deleteKeep(keepId)
    Pop.toast("Keep Deleted")
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>

<template>
  <div v-if="keep" class="modal fade" id="keepModal" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-dialog-centered modal-lg custom-modal-width" role="document">
      <div class="modal-content border-0 rounded-3 shadow">
        <div class="modal-body p-4">
          <div class="container-fluid">
            <div class="row">
              <div class="col-12 col-md-6 mb-3 mb-md-0 text-center">
                <img :src="keep.img || 'https://via.placeholder.com/150'" alt="Keep Image" class="rounded"
                  style="width: 100%; height: auto; object-fit: cover;" />
              </div>
              <div class="col-12 col-md-6 d-flex flex-column justify-content-between">
                <div class="ms-md-4 mb-3">
                  <div class="d-flex align-items-center mt-3">
                    <span class="me-3">
                      <i class="bi bi-eye"></i> {{ keep.kept || 0 }}
                    </span>
                    <span class="me-3">
                      <i class="bi bi-heart"></i> {{ keep.visits || 0 }}
                    </span>
                  </div>
                </div>
                <div>
                  <h2 id="modalTitle" class="fw-bold mb-3">{{ keep.name }}</h2>
                  <p class="text-muted">{{ keep.description }}</p>
                </div>
                <div class="modal-footer border-0 d-flex flex-column align-items-stretch">
                  <form @submit.prevent="createVaultKeep()" v-if="vaults"
                    class="d-flex flex-column align-items-stretch">
                    <div class="d-flex align-items-center mb-2">
                      <label for="vaultSelect" class="me-2 text-muted">Select Vault:</label>
                      <select v-model="editableVaultKeepData.vaultId" id="vaultSelect"
                        class="form-select form-select-sm">
                        <option value="" disabled>Select a vault</option>
                        <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                      </select>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                      <div class="d-flex align-items-center">
                        <button type="submit" class="btn btn-outline-primary btn-sm me-2">Vault</button>
                        <button @click="deleteKeep(keep.id)" type="button"
                          class="btn btn-outline-danger btn-sm">Delete</button>
                      </div>
                      <div class="d-flex align-items-center">
                        <img :src="keep.creator?.picture || 'https://via.placeholder.com/32'" alt="User"
                          class="rounded-circle" style="width: 32px; height: 32px;" />
                        <span class="ms-2 fw-bold">{{ keep.creator?.name }}</span>
                      </div>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style scoped>
.modal {
  animation: fadeIn 0.3s ease-in-out;
}

.modal-dialog-centered {
  display: flex;
  justify-content: center;
  align-items: center;
}

.custom-modal-width {
  max-width: 80%;
}

@keyframes fadeIn {
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
}
</style>
