<template>
  <div>
    <v-btn v-for="m in items" :to="`/moderation/${m.id}/${m.type}/${m.target}`" :key="m.id">
      {{m.target}}
    </v-btn>
  </div>
</template>

<script>
export default {
  data: () => ({
    items : []
  }),
  async fetch() {
    this.items = await this.$axios.$get('/api/moderation-items')
  }
}
</script>

<style scoped>

</style>
