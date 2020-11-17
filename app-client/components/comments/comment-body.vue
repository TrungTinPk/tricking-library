<template>
  <div>
    <p v-html="comment.htmlContent"></p>
    <v-btn small v-if="!replying" @click="replying = true">Reply</v-btn>
    <v-btn small v-if="$listeners['load-replies']" @click="$emit('load-replies')">Load Replies</v-btn>
  </div>
  <comment-input v-if="replying" @send="(c) => $emit('send',c)" @cancel="replying = false"></comment-input>
</template>

<script>
import CommentInput from "@/components/comments/comment-input";
export default {
  name: "comment-body",
  components: {CommentInput},
  props: {
    comment : {
      required: true,
      type: Object
    }
  },
  data: () =>({
    replying: false
  })
}
</script>

<style scoped>

</style>
