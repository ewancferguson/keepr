<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import CreateKeepForm from './CreateKeepForm.vue';
import CreateVaultForm from './CreateVaultForm.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light navbar-custom">
    <div class="container-fluid">
      <router-link class="navbar-brand nav-link" :to="{ name: 'Home' }">Home</router-link>

      <div class="dropdown">
        <button class="btn btn-outline-primary dropdown-toggle px-3 py-2" type="button" data-bs-toggle="dropdown">
          <i class="bi bi-plus-circle me-2"></i>Create
        </button>
        <ul class="dropdown-menu shadow-sm rounded-3">
          <li>
            <a class="dropdown-item py-2" data-bs-toggle="modal" data-bs-target="#vaultModal">
              <i class="bi bi-folder-fill me-2 text-primary"></i>Vault
            </a>
          </li>
          <li>
            <a class="dropdown-item py-2" data-bs-toggle="modal" data-bs-target="#keepForm">
              <i class="bi bi-bookmark-fill me-2 text-success"></i>Keep
            </a>
          </li>
        </ul>
      </div>

      <CreateVaultForm />
      <CreateKeepForm />
      <div class="mx-auto">
        <span class="brand-logo">the keepr co.</span>
      </div>

      <div class="ms-auto d-flex align-items-center">
        <Login />
      </div>
    </div>
  </nav>
</template>

<style>
.navbar-custom {
  background-color: #f8f0e6;
  padding: 10px 20px;
}

.brand-logo {
  font-family: 'Courier New', Courier, monospace;
  font-size: 1.2rem;
  border: 1px solid #000;
  padding: 5px 10px;
}

.nav-link {
  color: #8c5b5b !important;
}

.dropdown-menu {
  background-color: #ffffff;
  border: 1px solid rgba(0, 0, 0, 0.125);
  padding: 0.5rem 0;
  min-width: 160px;
}

.dropdown-item {
  font-size: 0.9rem;
  color: #6c757d;
  transition: all 0.2s;
}

.dropdown-item:hover {
  background-color: #f1f1f1;
  color: #495057;
}

.dropdown-item i {
  font-size: 1rem;
}
</style>
