<script setup>
import { AppState } from '@/AppState';
import KeepModal from '@/components/KeepModal.vue';
import { router } from '@/router';
import { api } from '@/services/AxiosService';
import { keepsService } from '@/services/KeepsService';
import { vaultKeepsService } from '@/services/VaultKeepsService';
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, watch } from 'vue';
import { useRoute } from 'vue-router';


const vault = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.vaultedkeeps)
const account = computed(() => AppState.account)
const route = useRoute()
watch(route, () => {
  getVaultById()
  getVaultedkeeps()
}, { immediate: true })

async function getVaultById() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  }
  catch (error) {
    Pop.error(error);
    router.push({ name: 'Home' })
  }
}

async function getVaultedkeeps() {
  try {
    const vaultId = route.params.vaultId
    await vaultKeepsService.getVaultedKeeps(vaultId)

  }
  catch (error) {
    Pop.error(error);
  }
}


async function getKeepById(keepId) {
  try {
    await keepsService.getKeepById(keepId)
    Modal.getOrCreateInstance('#keepModal').show()
  }
  catch (error) {
    Pop.error(error);
  }
}


async function deleteVault(vaultId) {
  try {
    const yes = await Pop.confirm('Are You sure you want to delete this Vault?')
    if (!yes) return
    await vaultsService.deleteVault(vaultId)
    router.push({ name: 'Account' })
    Pop.toast('Vault Deleted')
  }
  catch (error) {
    Pop.error(error);
  }
}


async function deleteVaultKeep(vaultKeepId) {
  try {
    await vaultKeepsService.deleteVaultKeep(vaultKeepId)
    Pop.toast("Keep removed from Vault")
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>


<template>
  <div v-if="vault" class="d-flex justify-content-center mt-5">
    <div class="card text-white text-center bg-dark"
      :style="{ width: '100%', maxWidth: '500px', height: '200px', background: `url(${vault.img}) center/cover no-repeat` }">
      <div class="card-img-overlay d-flex flex-column justify-content-center">
        <h3 class="card-title">{{ vault.name }}</h3>
      </div>
    </div>
  </div>

  <div class="text-center my-3">
    <button v-if="vault.creatorId == account.id" @click="deleteVault(vault.id)" class="btn btn-danger btn-sm">Delete
      Vault</button>
  </div>

  <p class="text-center mt-3">{{ keeps.length }} keeps</p>

  <div v-if="keeps" class="container mt-4">
    <div class="row g-3">
      <div v-for="keep in keeps" :key="keep.id" class="col-md-4 col-sm-6" @click="getKeepById(keep.id)">
        <div class="position-relative border rounded shadow-sm overflow-hidden">
          <img :src="keep.img" :alt="keep.img" class="img-fluid rounded">
          <div class="position-absolute top-0 start-0 p-2">
            <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
              <img onclick="event.stopPropagation()" :src="keep.creator.picture" :title="keep.creator.name"
                alt="Profile Picture" class="rounded-circle border border-light" width="40" height="40">
            </RouterLink>
          </div>
          <div class="position-absolute bottom-0 w-100 bg-dark bg-opacity-50 text-white p-2 text-center">
            <span class="fw-bold">{{ keep.name }}</span>
          </div>
          <!-- X Button -->
          <button v-if="vault.creatorId == account.id" onclick="event.stopPropagation()" @click="deleteVaultKeep(keep)"
            class="btn btn-sm btn-danger position-absolute top-0 end-0 m-2 rounded-circle">
            Remove
          </button>
        </div>
      </div>
    </div>
  </div>

  <KeepModal />
</template>




<style lang="scss" scoped>
.masonry-container {
  columns: 200px;
  gap: 16px;

  >* {
    break-inside: avoid;
    display: inline-block;
    cursor: pointer;
  }
}

.keep-item {
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
  transition: transform 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    transform: translateY(-10px);
    box-shadow: 0 6px 15px rgba(0, 0, 0, 0.4);
  }
}

.picture-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  border-radius: 12px;
}

.keep-wrapper {
  position: relative;
  border-radius: 12px;
}

.keep-overlay {
  position: absolute;
  bottom: 15px;
  left: 15px;
  display: flex;
  align-items: center;
  gap: 8px;
}

.pfp {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
  border: 2px solid white;
}

.keep-name {
  color: white;
  font-weight: bold;
  text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.5);
  font-size: 14px;
  text-transform: uppercase;
}


.overflow-hidden {
  overflow: hidden;
}


/* From Uiverse.io by vinodjangid07 */
.button {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  background-color: rgb(20, 20, 20);
  border: none;
  font-weight: 600;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
  cursor: pointer;
  transition-duration: .3s;
  overflow: hidden;
  position: relative;
}

.svgIcon {
  width: 12px;
  transition-duration: .3s;
}

.svgIcon path {
  fill: white;
}

.button:hover {
  width: 140px;
  border-radius: 50px;
  transition-duration: .3s;
  background-color: rgb(255, 69, 69);
  align-items: center;
}

.button:hover .svgIcon {
  width: 50px;
  transition-duration: .3s;
  transform: translateY(60%);
}

.button::before {
  position: absolute;
  top: -20px;
  content: "Delete";
  color: white;
  transition-duration: .3s;
  font-size: 2px;
}

.button:hover::before {
  font-size: 13px;
  opacity: 1;
  transform: translateY(30px);
  transition-duration: .3s;
}
</style>