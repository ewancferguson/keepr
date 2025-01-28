<script setup>
import { AppState } from '@/AppState';
import KeepModal from '@/components/KeepModal.vue';
import { keepsService } from '@/services/KeepsService';
import { vaultKeepsService } from '@/services/VaultKeepsService';
import { vaultsService } from '@/services/VaultsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, watch } from 'vue';
import { useRoute } from 'vue-router';


const vault = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.vaultedkeeps)
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


</script>


<template>

  <div v-if="vault" class="d-flex justify-content-center mt-5">
    <div class="card text-white text-center bg-dark"
      :style="{ width: '100%', maxWidth: '500px', height: '200px', background: `url(${vault.img}) center/cover no-repeat` }">
      <div class="card-img-overlay d-flex flex-column justify-content-center">
        <h3 class="card-title">{{ vault.name }}</h3>
      </div>
    </div>
    <div class="text-center">
    </div>
  </div>

  <p class="text-center mt-3">{{ keeps.length }} keeps</p>

  <div v-if="keeps" class="container mt-4">
    <div class="masonry-container">
      <div @click="getKeepById(keep.id)" v-for="keep in keeps" :key="keep.id" class="mb-3 keep-item">
        <div class="keep-wrapper">
          <img :src="keep.img" :alt="keep.img" class="picture-img rounded">
          <div class="keep-overlay">
            <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
              <img onclick="event.stopPropagation()" :src="keep.creator.picture" :title="keep.creator.name"
                alt="Profile Picture" class="pfp">
            </RouterLink>
            <span class="keep-name">{{ keep.name }}</span>
          </div>
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
</style>