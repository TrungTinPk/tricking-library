<template>
  <v-card>
    <v-card-title>
      Create Trick
      <v-spacer></v-spacer>
      <v-btn icon @click="close">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">Trick Information</v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2">Review</v-stepper-step>
      </v-stepper-header>
      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="pt-5">
            <v-text-field label="Name" outlined v-model="form.name"></v-text-field>
            <v-text-field label="Description" outlined v-model="form.description"></v-text-field>
            <v-select :items="difficultyItems" label="Difficulty" outlined v-model="form.difficulty"></v-select>
            <v-select :items="trickItems" label="Prerequisites" outlined v-model="form.prerequisites" multiple chips small-chips deletable-chips></v-select>
            <v-select :items="trickItems" label="Progressions" outlined v-model="form.progressions" multiple chips small-chips deletable-chips></v-select>
            <v-select :items="categoryItems" label="Category" outlined v-model="form.categories" multiple chips small-chips deletable-chips></v-select>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="step++">Next</v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="2">
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
import {mapActions, mapMutations, mapGetters} from 'vuex';
import {close} from './_shared'

export default {
  name: "trick-steps",
  mixins:[close],
  data: () => ({
    step:1,
    form: {
      name: "",
      description: "",
      difficulty: "",
      prerequisites: [],
      progressions: [],
      categories: []
    },
    testData : [
      {text: "demo 1", value: "1"},
      {text: "demo 2", value: "2"},
      {text: "demo 3", value: "3"}
    ]
  }),
  computed: {
    ...mapGetters('tricks', ['trickItems','categoryItems','difficultyItems'])
  },
  methods: {
    ...mapMutations('video-upload', ['reset']),
    ...mapActions('tricks', ['creatTrick']),
    async save() {
      await this.creatTrick({form: this.form});
      // to do close dialog & reset component state
      this.close();
    }
  }
}
</script>

<style scoped>

</style>
