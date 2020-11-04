<template>
  <v-dialog :value="active" @change="" persistent width="500">
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">Select Type</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step :complete="step > 2" step="2">Upload Video</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="step > 3">Trick Information</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4">Review</v-stepper-step>
      </v-stepper-header>
      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mb-12">
            <v-card-actions>
              <v-btn outlined rounded text @click="setType(uploadType.TRICK)">
                Trick
              </v-btn>
              <v-btn outlined rounded text  @click="setType(uploadType.SUBMISSION)">
                Submission
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mb-12">
            <v-file-input accept="video/*" @change="handleFile"></v-file-input>
          </v-card>
        </v-stepper-content>

        <v-stepper-content step="3">
          <v-card class="mb-12" color="grey lighten-1">
            <v-card-text>
              <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
            </v-card-text>
            <v-card-actions>
              <v-btn @click="saveTrick">Save Tricks</v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>

        <v-stepper-content step="4">
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
    <v-card>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" text @click="toggleActivity">Close</v-btn>
      </v-card-actions>
    </v-card>

  </v-dialog>
</template>

<script>
import {mapState, mapActions, mapMutations} from 'vuex'
import {UPLOAD_TYPE} from '@/data/enum';
export default {
  name: "video-upload",
  data: () => ({
    trickName: ""
  }),
  computed: {
    ...mapState('video-upload', ["uploadPromise","active","step"]),
    uploadType() {
      return {...UPLOAD_TYPE}
    }
  },
  methods: {
    ...mapMutations('video-upload', ["reset","toggleActivity","setType"]),
    ...mapActions('video-upload', ['startVideoUpload','creatTrick']),
    async handleFile(file) {
      if (!file) return;
      const form = new FormData();
      form.append("video", file);

      this.startVideoUpload(form);
    },
    async saveTrick() {
      if (!this.uploadPromise) {
        console.log("Upload Video Null");
        return
      }
      const video = this.uploadPromise;

      await this.creatTrick({trick: {name: this.trickName, video}});
      this.trickName = "";
      this.reset();
    }
  }
}
</script>

<style scoped>

</style>
