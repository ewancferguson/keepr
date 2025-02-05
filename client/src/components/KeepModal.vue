<script setup>
import App from '@/App.vue';
import { AppState } from '@/AppState';
import { keepsService } from '@/services/KeepsService';
import { vaultKeepsService } from '@/services/VaultKeepsService';
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref } from 'vue';

const keep = computed(() => AppState.activeKeep);
const vaults = computed(() => AppState.myVaults);
const account = computed(() => AppState.account)

const editableVaultKeepData = ref({
  vaultId: 0,
  keepId: 0,
})

onMounted(() => { });

async function getMyVaults() {
  try {
    await vaultsService.getMyVaults();
  } catch (error) {
    Pop.error(error);
  }
}

async function createVaultKeep() {
  try {
    editableVaultKeepData.value.keepId = keep.value.id;
    await vaultKeepsService.createVaultKeep(editableVaultKeepData.value);
    Pop.success('Keep Vaulted');
  } catch (error) {
    Pop.error(error);
  }
}

function hideModal() {
  Modal.getInstance('#keepModal').hide();
}

async function deleteKeep(keepId) {
  try {
    Modal.getInstance('#keepModal').hide();
    await keepsService.deleteKeep(keepId);
    Pop.toast("Keep Deleted");
  } catch (error) {
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
                <img :src="keep.img || 'https://via.placeholder.com/150'" alt="Keep Image" class="rounded w-100" />
              </div>
              <div class="col-12 col-md-6 d-flex flex-column justify-content-between text-center">
                <div class="mb-2">
                  <span class="me-3">
                    <i class="mdi mdi-heart"></i> {{ keep.kept || 0 }}
                  </span>
                  <span class="me-3">
                    <i class="mdi mdi-eye"></i> {{ keep.views || 0 }}
                  </span>
                </div>
                <div class="flex-grow-1 d-flex flex-column justify-content-center">
                  <h2 id="modalTitle" class="fw-bold mb-3">{{ keep.name }}</h2>
                  <p class="text-muted">{{ keep.description }}</p>
                </div>
                <div class="modal-footer border-0 d-flex flex-column align-items-center">
                  <form @submit.prevent="createVaultKeep()" v-if="vaults" class="w-100">
                    <div class="mb-2">
                      <label v-if="account" for="vaultSelect" class="text-muted">Select Vault:</label>
                      <select v-if="account" v-model="editableVaultKeepData.vaultId" id="vaultSelect"
                        class="form-select">
                        <option value="" disabled>Select a vault</option>
                        <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                      </select>
                    </div>
                    <div class="d-flex justify-content-center gap-2">
                      <button v-if="account" type="submit" class="btn btn-outline-primary">Vault</button>
                      <button v-if="keep.creatorId == account?.id" @click="deleteKeep(keep.id)" type="button"
                        class="btn btn-outline-danger">Delete</button>
                    </div>
                    <div class="d-flex align-items-center justify-content-center mt-3">
                      <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                        <img @click="hideModal()" :src="keep.creator?.picture" alt="User" class="rounded-circle me-2"
                          style="width: 32px; height: 32px;" />
                      </RouterLink>
                      <span class="fw-bold">{{ keep.creator?.name }}</span>
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
  max-width: 90%;
}

@media (max-width: 576px) {
  .modal-dialog {
    max-width: 100%;
    margin: 0;
  }

  .modal-body {
    padding: 2rem;
  }

  .modal-footer {
    padding: 1rem 0;
  }

  .modal-content {
    border-radius: 0;
  }
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
