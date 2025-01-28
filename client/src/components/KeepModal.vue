<script setup>
import { AppState } from '@/AppState';
import { vaultKeepsService } from '@/services/VaultKeepsService';
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
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


</script>

<template>
  <div v-if="keep" class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="modalTitle"
    aria-hidden="true">
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
                <div class="modal-footer border-0 d-flex justify-content-between px-4">
                  <form @submit.prevent="createVaultKeep()" v-if="vaults" class="d-flex align-items-center">
                    <label for="vaultSelect" class="me-2 text-muted">Select Vault:</label>
                    <select v-model="editableVaultKeepData.vaultId" id="vaultSelect" class="form-select form-select-sm">
                      <option value="" disabled>Select a vault</option>
                      <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                    </select>
                    <div class="d-flex align-items-center">
                      <button type="submit" class="btn btn-outline-primary btn-sm me-2">save</button>
                      <img :src="keep.creator?.picture || 'https://via.placeholder.com/32'" alt="User"
                        class="rounded-circle" style="width: 32px; height: 32px;" />
                      <span class="ms-2 fw-bold">{{ keep.creator?.name }}</span>
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
