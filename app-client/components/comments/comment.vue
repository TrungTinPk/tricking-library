<template>
  <div>
    <comment-body :comment="comment" @send="send" @load-replies="loadReplies"></comment-body>
    <div class="ml-5">
      <comment-body label="Reply" v-for="r in comment.replies" :comment="r" @send="send" :key="`reply-${r.id}`"></comment-body>
    </div>
  </div>
</template>

<script>
import CommentBody from "@/components/comments/comment-body";
export default {
  name: "comment",
  components: {CommentBody},
  props: {
    comment : {
      required: true,
      type: Object
    }
  },
  data: () => ({
    replies: []
  }),
  methods: {
    send(content) {
      return this.$axios.$post(`/api/comments/${this.comment.id}/replies`, {content})
          .then((comment) => this.replies.push(comment))
    },
    loadReplies() {
      return this.$axios.$get(`/api/comments/${this.comment.id}/replies`)
        .then((replies) => this.replies = replies)
    }
  }
}
</script>

<style scoped>

</style>
