<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">

    </v-col>
  </v-row>

</template>

<script>
import {mapState, mapActions, mapMutations} from 'vuex'
import {UPLOAD_TYPE} from '@/data/enum';
export default {
  components: {},
  computed: mapState('tricks', ["tricks"])
}
</script>
