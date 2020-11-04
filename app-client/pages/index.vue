<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <v-stepper v-model="step">
        <v-stepper-header>
          <v-stepper-step :complete="step > 1" step="1">Upload Video</v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step :complete="step > 2" step="2">Trick Information</v-stepper-step>
          <v-divider></v-divider>
          <v-stepper-step step="3">Confirmation</v-stepper-step>
        </v-stepper-header>
        <v-stepper-items>
          <v-stepper-content step="1">
            <v-card class="mb-12" color="grey lighten-1" height="200px">
              <v-file-input accept="video/*" @change="handleFile"></v-file-input>
            </v-card>
          </v-stepper-content>

          <v-stepper-content step="2">
            <v-card class="mb-12" color="grey lighten-1" height="200px">
              <v-card-text>
                <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
              </v-card-text>
              <v-card-actions>
                <v-btn @click="saveTrick">Save Tricks</v-btn>
                <v-btn @click="resetTricks">Reset Tricks</v-btn>
              </v-card-actions>
            </v-card>
          </v-stepper-content>

          <v-stepper-content step="3">
            <v-card class="mb-12" color="grey lighten-1" height="200px">
              <div v-if="tricks">
                <p v-for="trick in tricks">
                  <video controls :src="`http://localhost:5000/api/video/${trick.video}`"></video>
                  <v-card-title>{{ trick.name }}</v-card-title>
                </p>
              </div>
              <v-card-actions>
                <v-spacer/>
              </v-card-actions>
            </v-card>
          </v-stepper-content>
        </v-stepper-items>
      </v-stepper>
    </v-col>
  </v-row>
</template>

<script>
import {mapState, mapActions, mapMutations} from 'vuex'

export default {
  components: {},
  data: () => ({
    trickName: "",
    step: 1

  }),
  computed: {
    ...mapState('tricks', ["tricks"]),
    ...mapState('videos', ["uploadPromise"])
  },
  methods: {
    ...mapMutations('tricks', {
      resetTricks: 'reset'
    }),
    ...mapMutations('videos', {
      resetVideos: 'reset'
    }),
    ...mapActions('tricks', ['creatTrick']),
    ...mapActions('videos', ['startVideoUpload']),
    async handleFile(file) {
      if (!file) return;
      const form = new FormData();
      form.append("video", file);

      this.startVideoUpload(form);
      this.step++;
    },
    async saveTrick() {
      if (!this.uploadPromise){
        console.log("Upload Video Null");
        return
      }
      const video = this.uploadPromise;

      await this.creatTrick({trick: {name: this.trickName,video}});
      this.trickName = "";
      this.step++;
      this.resetVideos();
    },

  }
}
</script>
