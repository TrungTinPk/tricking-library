﻿<template>
  <div>
    <v-text-field label="Comment"
                  outlined v-model="content"
                  @keydown.ctrl.enter="$emit('send')"
                  clearable></v-text-field>
    <v-btn v-if="$listeners['cancel']" @click="$emit('cancel')">Cancel</v-btn>
    <v-btn :disabled="!content" @click="$emit('send')">{{label}}</v-btn>
  </div>
</template>

<script>
export default {
  name: "comment-input",
  props: {
    label: {
      required: false,
      type: String,
      default: 'send'
    }
  },
  data: () => ({
    content : ""
  })
}
</script>

<style scoped>

</style>
