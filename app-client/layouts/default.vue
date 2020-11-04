<template>
  <v-app dark>
    <v-app-bar app dense>
      <v-toolbar-title>Tricking Library</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn depressed @click="toggleActivity">
        <v-icon>Upload</v-icon>
      </v-btn>
    </v-app-bar>
    <video-upload></video-upload>
    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script>
import { mapMutations } from 'vuex';
import VideoUpload from '../components/video-upload'
export default {
  methods: mapMutations('videos',['toggleActivity']),
  components: {VideoUpload}
}
</script>
