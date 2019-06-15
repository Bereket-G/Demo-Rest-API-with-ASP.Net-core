<template>
    <li> <a v-on:click.self="updateTask">{{body}} &nbsp; </a> <input type="checkbox" v-model="status" /> &nbsp;
        <b style="float: right;" @click="deleteTask">X</b>
    </li>
</template>

<script>
export default {
  name: 'Task',
  props : {
      id: String,
      body: String,
      _status: Boolean
  },
  data() {
      return {
        status: this._status
      }
  },
    methods: {
        deleteTask () {
            if(confirm('Are you sure you want to delete this task?')){
                this.$emit('deleteTask', this.id);
            }

        },
        updateTask (){
            this.$emit('updateTask', this.id, this.status);
        }
    },
    watch: {
        status(value){
            this.$emit('updateStatusTask', this.id, this.body, value);
        }
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

</style>
