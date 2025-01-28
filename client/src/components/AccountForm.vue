<script setup>
import { AppState } from '@/AppState';
import { computed, ref } from 'vue';

const account = computed(() => AppState.account)

const editableAccountData = ref({
  name: account.value.name,
  picture: account.value.picture,
  coverImg: account.value.coverImg,
})



async function updateAccount() {

}




</script>


<template>
  <div class="modal fade" id="editAccountModal" tabindex="-1" aria-labelledby="editAccountLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="editAccountLabel">Edit Your Account</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="updateAccount">
          <div class="modal-body">
            <div class="mb-3">
              <label for="accountName" class="form-label">Name</label>
              <input v-model="editableAccountData.name" type="text" class="form-control" id="accountName"
                placeholder="Your Name...">
            </div>
            <div class="mb-3">
              <label for="profilePicture" class="form-label">Profile Picture URL</label>
              <input v-model="editableAccountData.picture" type="url" class="form-control" id="profilePicture"
                placeholder="Profile Picture URL...">
            </div>
            <div class="mb-3">
              <label for="coverPicture" class="form-label">Cover Picture URL</label>
              <input v-model="editableAccountData.coverImg" type="url" class="form-control" id="coverPicture"
                placeholder="Cover Picture URL...">
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Save Changes</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>