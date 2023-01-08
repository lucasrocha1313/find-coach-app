<template>
  <section>
    <base-card>
      <h2>Register as a coach now!</h2>
      <coach-form @register-coach="registerCoach"/>
    </base-card>
  </section>
</template>

<script>
import CoachForm from "@/components/coaches/CoachForm.vue";
import BaseCard from "@/components/ui/BaseCard.vue";

export default {
  name: "CoachRegistration",
  components: {BaseCard, CoachForm},
  methods: {
    registerCoach(data) {
      this.$store.dispatch('coaches/registerCoach', data)
      this.$router.replace('/coaches')
    }
  }
}
</script>

<style scoped>

</style>