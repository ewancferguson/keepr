<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import KeepModal from '@/components/KeepModal.vue';

const account = computed(() => AppState.account)
const vaults = computed(() => AppState.myVaults)
const keeps = computed(() => AppState.myKeeps)




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
  <div v-if="account" class="container py-4">
    <section class="row">
      <img class="cover-img img-fluid p-2" :src="account.coverImg" alt="">
    </section>
    <section class="row mx-3 py-4">
      <div class="d-flex justify-content-center">
        <img class="creator-img rounded-circle border border-white shadow" :src="account.picture" alt="">
      </div>
    </section>
    <div class="text-center mt-5">
      <h2 class="fw-bold">{{ account.name }}</h2>
      <p class="text-muted">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
    </div>
    <div v-if="vaults" class="mt-4">
      <h3 class="fw-bold">Vaults</h3>
      <div class="row g-3">
        <div v-for="vault in vaults" :key="vault.id" class="col-6 col-sm-4 col-md-3">
          <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">
            <div class="vault-card position-relative overflow-hidden rounded shadow-sm">
              <img :src="vault.img" class="w-100 rounded">
              <div class="position-absolute bottom-0 w-100 bg-dark bg-opacity-50 text-white text-center py-1">
                <span class="fw-bold">{{ vault.name }}</span>
              </div>
              <div v-if="vault.isPrivate == true" class="lock-icon position-absolute bottom-0 end-0 m-2">
                <i class="mdi mdi-lock text-white bg-dark bg-opacity-75 p-1 rounded-circle"></i>
              </div>
            </div>
          </RouterLink>
        </div>
      </div>
    </div>

    <!-- Keeps Section -->
    <div v-if="keeps" class="container mt-4">
      <h3 class="fw-bold">Keeps</h3>
      <div class="masonry-container">
        <div @click="getKeepById(keep.id)" v-for="keep in keeps" :key="keep.id" class="mb-3 keep-item">
          <div class="keep-wrapper">
            <img :src="keep.img" :alt="keep.img" class="picture-img rounded">
            <div class="keep-overlay">
              <img :src="keep.creator.picture" :title="keep.creator.name" alt="Profile Picture" class="pfp">
              <span class="keep-name">{{ keep.name }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Keep Modal -->
    <KeepModal />
  </div>
</template>

<style scoped lang="scss">
.cover-img {
  width: 100%;
  height: 15rem;
  object-fit: cover;
}

.creator-img {
  height: 10rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  margin-top: -15em;
  position: relative;
  top: 8em;
}

.vault-card {
  cursor: pointer;
  max-height: 200px;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
  transition: transform 0.3s ease, box-shadow 0.3s ease;

  &:hover {
    transform: translateY(-10px);
    box-shadow: 0 6px 15px rgba(0, 0, 0, 0.4);
  }
}

.lock-icon {
  font-size: 1.2rem;
  display: flex;
  justify-content: center;
  align-items: center;
}

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
