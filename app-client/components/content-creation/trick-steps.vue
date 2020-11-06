<template>
  <v-stepper v-model="step">
    <v-stepper-header>
      <v-stepper-step :complete="step > 1" step="1">Trick Information</v-stepper-step>
      <v-divider></v-divider>
      <v-stepper-step step="5">Review</v-stepper-step>
    </v-stepper-header>
    <v-stepper-items>
      <v-stepper-content step="1">
        <v-text-field label="Trick Name" outlined v-model="form.name"></v-text-field>
        <v-btn @click="step++">Next</v-btn>
      </v-stepper-content>
      <v-stepper-content step="2">
        <v-btn @click="save">Save</v-btn>
      </v-stepper-content>
    </v-stepper-items>
  </v-stepper>
</template>

<script>
import {mapActions, mapMutations, mapState} from 'vuex'

const initState = () => ({
  step:1,
  form: {
    name: ""
  }
});
export default {
  name: "trick-steps",
  data: initState,
  computed: mapState('video-upload', ['active']),
  watch: {
    'active' : (newValue) => {
      if (!newValue){
        Object.assign(this.$data,initState());
      }
    }
  },
  methods: {
    ...mapMutations('video-upload', ['reset']),
    ...mapActions('tricks', ['creatTrick']),
    async save() {
      await this.creatTrick({form: this.form});
      // to do close dialog & reset component state
      this.reset();
      Object.assign(this.$data,initState());
    }
  }
}
</script>

<style scoped>

</style>
