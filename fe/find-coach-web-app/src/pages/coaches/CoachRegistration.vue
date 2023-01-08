<template>
  <section>
    <base-card>
      <h2>Register as a coach now!</h2>
      <coach-form/>
    </base-card>
  </section>
</template>

<script>
import CoachForm from "@/components/coaches/CoachForm.vue";
import BaseCard from "@/components/ui/BaseCard.vue";

export default {
  name: "CoachRegistration",
  components: {BaseCard, CoachForm}
}
</script>

<style scoped>

</style>