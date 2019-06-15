<template>
    <footer> <br/>
        <input v-if="update" class="input input-group-sm" type="text" v-model="body" placeholder="Update task..." />
        <input  class="input input-group-sm" v-else type="text" v-model="body" placeholder="Add a task..." />
        <button v-if="update" id="updateBtn" @click="updateTask" class="btn btn-info"> Update </button>
        <button v-else id="AddBtn" @click="addTask" class="btn btn-light">+ Add </button>
    </footer>
</template>

<script>
export default {
  name: 'NewTask',
  props : {
      ListId : String,
      ListName: String,
      update : Boolean,
      updateId : String,
      previousContent: String
  },
  data() {
      return {
        body: this.previousContent
      }
  },
    methods: {
        addTask () {
            this.$emit('taskAdded', this.body);
            this.body = '';
        },
        updateTask () {
            this.$emit('updateTaskWithNewBody', this.body, this.updateId);
            this.body = '';
        }
    },watch: {
        previousContent(value) {
            this.body = value;
        }
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
    footer {
        margin-bottom: 10px;
    }

    #AddBtn {
        float: right;
    }
</style>
