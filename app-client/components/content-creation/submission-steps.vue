<template>
  <v-stepper v-model="step">
    <v-stepper-header>
      <v-stepper-step :complete="step > 1" step="1">Upload Video</v-stepper-step>
      <v-divider></v-divider>
      <v-stepper-step :complete="step > 2" step="2">Select Trick</v-stepper-step>
      <v-divider></v-divider>
      <v-stepper-step :complete="step > 3" step="3">Submission</v-stepper-step>
      <v-divider></v-divider>
      <v-stepper-step step="4">Review</v-stepper-step>
    </v-stepper-header>
    <v-stepper-items>
      <v-stepper-content step="1">
        <v-file-input accept="video/*" @change="handleFile"></v-file-input>
      </v-stepper-content>
      <v-stepper-content step="2">
        <v-select :items="trickItems" label="Select Trick" outlined v-model="form.trickId"></v-select>
        <v-btn @click="step++">Next</v-btn>
      </v-stepper-content>
      <v-stepper-content step="3">
        <v-text-field label="Description" outlined v-model="form.description"></v-text-field>
      </v-stepper-content>
      <v-stepper-content step="4">
        <v-card class="mb-12" color="grey lighten-1" height="200px">
          <v-card-actions>
            <v-btn @click="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-stepper-content>
    </v-stepper-items>
  </v-stepper>
</template>

<script>
import {mapState, mapGetters, mapActions, mapMutations} from 'vuex'

const initState = () => ({
  step: 1,
  form : {
    trickId : "",
    video: "",
    description: ""
  }
})

export default {
  name: "submission-steps",
  data: initState,
  computed: {
    ...mapGetters('tricks', ['trickItems']),
    ...mapState('video-upload', ['active'])
  },
  watch: {
    'active' : (newValue) => {
      if (!newValue){
        Object.assign(this.$data,initState());
      }
    }
  },
  methods: {
    ...mapMutations('video-upload', ['hide']),
    ...mapActions('video-upload', ['startVideoUpload','createSubmission']),
    async handleFile(file) {
      if (!file) return;
      const form = new FormData();
      form.append("video", file);

      this.startVideoUpload({form});
      this.step++;
    },
    async save() {
      this.createSubmission({form: this.form});
      this.hide();
    }
  }
}
</script>

<style scoped>

</style>
