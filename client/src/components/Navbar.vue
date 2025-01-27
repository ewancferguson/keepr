<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

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
        <button class="btn btn-link nav-link dropdown-toggle" type="button" data-bs-toggle="dropdown">
          Create
        </button>
        <ul class="dropdown-menu">
          <li class="ml-2 selectable">Vault</li>
          <li class="selectable">Keep</li>
        </ul>
      </div>

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

.profile-pic {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
}
</style>
