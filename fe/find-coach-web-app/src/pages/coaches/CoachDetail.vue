<template>
  <pulse-loader v-if="loading" :loading="loading" :color="color" :size="size"></pulse-loader>
  <div v-else>
    <section>
      <base-card>
        <h2>{{fullName}}</h2>
        <h3>${{rate}}/hour</h3>
      </base-card>
    </section>
    <section>
      <base-card>
        <header>
          <h2>Interested? Reach out now!</h2>
          <base-button link :to="contactLink">Contact</base-button>
        </header>
        <router-view></router-view>
      </base-card>
    </section>
    <section>
      <base-card>
        <base-badge v-for="area in areas.map(a => a.name)" :key="area" :type="area" :title="area"></base-badge>
        <p>{{ description }}</p>
      </base-card>
    </section>
  </div>
</template>
<script>
import BaseCard from "@/components/ui/BaseCard.vue";
import BaseButton from "@/components/ui/BaseButton.vue";
import BaseBadge from "@/components/ui/BaseBadge.vue";

export default {
  name: "CoachDetail",
  components: {BaseBadge, BaseButton, BaseCard},
  props: ['id'],
  data() {
    return {
      selectedCoach: null,
      loading: false
    }
  },
  computed: {

    fullName() {
      return `${this.selectedCoach.firstName} ${this.selectedCoach.lastName}`
    },
    contactLink() {
      return `${this.$route.path}/contact`
    },
    areas() {
      return this.selectedCoach.areas
    },
    rate() {
      return this.selectedCoach.hourlyRate
    },
    description() {
      return this.selectedCoach.description
    }
  },
  async created() {
    this.loading = true
    this.selectedCoach = await this.$store.getters['coaches/coach'](this.id)
    this.loading = false
  }
}
</script>

<style scoped>

</style>