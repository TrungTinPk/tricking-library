<template>
  <div v-if="submissions">
    <div v-for="s in submissions">
      {{s.id}} - {{s.trickId}} -{{s.description}}
      <video controls :src="`http://localhost:5000/api/videos/${s.video}`"></video>
    </div>
  </div>
</template>

<script>
import {mapState} from 'vuex';
export default {
  computed: mapState('submission',['submissions']),
  async fetch(){
    const trickId = this.$route.params.trick;

    await this.$store.dispatch("submission/fetchSubmissionsForTrick", {trickId}, {root : true});
  }
}
</script>

<style scoped>

</style>
