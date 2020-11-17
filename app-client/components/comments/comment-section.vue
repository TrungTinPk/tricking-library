<template>
  <div>
    <comment-input label="comment" @send="(c) => $emit('send',c)" />

    <v-divider />
    <comment-body v-for="comment in comments" :comment="comment" :key="comment.id"></comment-body>
  </div>
</template>

<script>
import CommentInput from "@/components/comments/comment-input";
import CommentBody from "@/components/comments/comment-body";
export default {
  name: "comment-section",
  components: {CommentBody, Comment, CommentInput},
  props: {
    comments: {
      required: true,
      type: Array
    }
  }
}
</script>

<style scoped>

</style>
