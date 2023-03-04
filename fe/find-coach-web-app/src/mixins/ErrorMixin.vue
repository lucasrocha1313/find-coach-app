<script>
export default {
  name: "ErrorMixin",
  data() {
    return {
      error: null
    }
  },
  methods: {
    handleError() {
      this.error = null
    }
  }
}
</script>