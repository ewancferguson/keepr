<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '../services/AuthService.js';
import { Account } from '@/models/Account.js';
import AccountForm from './AccountForm.vue';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
}
async function logout() {
  AuthService.logout()
}

</script>

<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" class="bg-dark border-0 selectable no-select" data-bs-toggle="dropdown">
          <div v-if="account?.picture || identity?.picture">
            <img :src="account?.picture || identity?.picture" alt="account photo" height="40" class="rounded" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div data-bs-toggle="modal" data-bs-target="#editAccountModal"
              class="list-group-item dropdown-item list-group-item-action">
              Edit Account
            </div>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-if="account">
      <AccountForm />
    </div>
  </span>

</template>

<style lang="scss" scoped></style>
