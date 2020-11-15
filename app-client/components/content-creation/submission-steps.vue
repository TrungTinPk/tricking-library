<template>
  <v-card>
    <v-card-title>
      Create Submission
      <v-spacer></v-spacer>
      <v-btn icon @click="close">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-card-title>
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
          <v-card class="pt-5">
            <v-file-input accept="video/*" @change="handleFile"></v-file-input>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="pt-5">
            <v-select :items="trickItems" label="Select Trick" outlined v-model="form.trickId"></v-select>
           <v-card-actions>
             <v-spacer></v-spacer>
             <v-btn @click="step++">Next</v-btn>
           </v-card-actions>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-card class="pt-5">
            <v-text-field label="Description" outlined v-model="form.description"></v-text-field>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="step++">Next</v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="4">
          <v-card class="pt-5">
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="save">Save</v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-card>
</template>

<script>
import {mapState, mapGetters, mapActions, mapMutations} from 'vuex'
import {close} from './_shared'

export default {
  name: "submission-steps",
  mixins: [close],
  data: () => ({
    step: 1,
    form : {
      trickId : "",
      video: "",
      description: ""
    }
  }),
  computed: mapGetters('tricks', ['trickItems']),
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
