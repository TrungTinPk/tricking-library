<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <div class="text-center">
        <logo />
        <vuetify-logo />
      </div>
      <v-card>
        <v-card-title class="headline">
          {{message}}
        </v-card-title>
        <v-card-text>
          <div v-if="tricks">
            <p v-for="trick in tricks">
              {{trick.name}}
            </p>
          </div>
          <div>
            <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
          </div>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn @click="reset">reset</v-btn>
          <v-btn @click="saveTrick">Save Tricks</v-btn>
          <v-btn @click="resetTricks">Reset Tricks</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import Logo from '~/components/Logo.vue'
import VuetifyLogo from '~/components/VuetifyLogo.vue'
import {mapState, mapActions, mapMutations} from 'vuex'

export default {
  components: {
    Logo,
    VuetifyLogo
  },
  data: () => ({
    trickName: "",

  }),
  computed: {
    ...mapState({
      message: state => state.message
    }),
    ...mapState('tricks', {
      tricks: state => state.tricks
    })
  },
  methods: {
    ...mapMutations([
      'reset'
    ]),
    ...mapMutations('tricks',{
      resetTricks: 'reset'
    }),
    ...mapActions('tricks',['creatTrick']),
    async saveTrick() {
      await this.creatTrick({trick: {name: this.trickName}});
      this.trickName = "";
    }

  }
}
</script>
