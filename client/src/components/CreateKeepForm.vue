<script setup>
import { keepsService } from '@/services/KeepsService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';



const editableKeepData = ref({
  name: '',
  img: '',
  description: ''
})

async function createKeep() {
  try {
    await keepsService.createKeep(editableKeepData.value)

    editableKeepData.value =
    {
      name: '',
      img: '',
      description: ''
    }

    Pop.success("Keep Created")
    Modal.getInstance('#keepForm').hide()
    Modal.getOrCreateInstance('#keepModal').show()
  }
  catch (error) {
    Pop.error(error);
  }
}



</script>


<template>
  <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="keepModalLabel">Add your keep</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createKeep()">
          <div class="modal-body">
            <div class="mb-3">
              <label for="title" class="form-label">Title</label>
              <input v-model="editableKeepData.name" type="text" class="form-control" id="title" placeholder="Title...">
            </div>
            <div class="mb-3">
              <label for="imageURL" class="form-label">Image URL</label>
              <input v-model="editableKeepData.img" type="url" class="form-control" id="imageURL"
                placeholder="Image URL...">
            </div>
            <div class="mb-3">
              <label for="description" class="form-label">Keep Description</label>
              <textarea v-model="editableKeepData.description" class="form-control" id="description" rows="3"
                placeholder="Keep Description..."></textarea>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Create</button>
          </div>
        </form>
      </div>
    </div>
  </div>

</template>


<style lang="scss" scoped></style>