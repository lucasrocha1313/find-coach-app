<template>
  <div>
    <base-dialog :show="!!error" title="A error occurred" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <section v-if="!error">
      <base-card>
        <header>
          <h2>Request received</h2>
        </header>
        <div  v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <ul v-else-if="requests?.length > 0">
          <request-item v-for="req in requests" :key="req.id" :email="req.userEmail" :message="req.message"/>
        </ul>
        <h3 v-else>You haven't received any request yet!</h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import BaseCard from "@/components/ui/BaseCard.vue";
import RequestItem from "@/components/requests/RequestItem.vue";
import BaseSpinner from "@/components/ui/BaseSpinner.vue";
import ErrorMixin from "@/mixins/ErrorMixin.vue";
import BaseDialog from "@/components/ui/BaseDialog.vue";

export default {
  name: "RequestReceived",
  components: {BaseDialog, BaseSpinner, RequestItem, BaseCard},
  mixins: [ErrorMixin],
  data() {
    return {
      isLoading: false,
      requestsLoaded: []
    }
  },
  async mounted() {
    await this.receivedRequests()
  },
  methods: {
    async receivedRequests() {
      this.isLoading=true
      try{
        this.requestsLoaded =  await this.$store.getters['requests/requests']
      } catch (err) {
        this.error = err.message || 'Something went wrong!'
      }

      this.isLoading=false
    }
  },
  computed: {
    requests() {
      return this.requestsLoaded
    }
  }
}
</script>

<style scoped>
header {
  text-align: center;
}
ul {
  list-style: none;
  margin: 2rem auto;
  padding: 0;
  max-width: 30rem;
}
h3 {
  text-align: center;
}
</style>