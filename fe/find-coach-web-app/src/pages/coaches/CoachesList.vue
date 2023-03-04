<template>
  <div>
    <base-dialog :show="!!error" title="A error occurred" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <section>
      <coach-filter @change-filter="setFilters"/>
    </section>
    <section>
      <base-card>
        <div class="controls">
          <base-button mode="outline" @click="getCoaches">Refresh</base-button>
          <base-button link to="/register">Register as a coach</base-button>
        </div>
        <div>
          <div v-if="isLoading">
            <base-spinner></base-spinner>
          </div>
          <ul v-else-if="this.coaches.length > 0">
            <coach-item v-for="coach in filteredCoaches" :key="coach.id"
                        :id="coach.id"
                        :first-name="coach.firstName"
                        :last-name="coach.lastName"
                        :areas="coach.areas.map(a => a.name.toLowerCase())"
                        :rate="coach.hourlyRate"
            />
          </ul>
          <h3 v-else>No coaches found</h3>
        </div>
      </base-card>
    </section>
  </div>
</template>

<script>
import CoachItem from "@/components/coaches/CoachItem.vue";
import BaseCard from "@/components/ui/BaseCard.vue";
import BaseButton from "@/components/ui/BaseButton.vue";
import CoachFilter from "@/components/coaches/CoachFilter.vue";
import BaseSpinner from "@/components/ui/BaseSpinner.vue";
import BaseDialog from "@/components/ui/BaseDialog.vue";
import ErrorMixin from "@/mixins/ErrorMixin.vue";

export default {
  name: "CoachesList",
  components: {BaseDialog, BaseSpinner, CoachFilter, BaseButton, BaseCard, CoachItem},
  mixins: [ErrorMixin],
  data(){
    return {
      activeFilters:[],
      isLoading: false,
      coaches: []
    }
  },
  async created() {
    await this.getCoaches()
  },
  computed: {
    filteredCoaches() {
      return this.coaches.filter(c => this.activeFilters.length === 0 || c.areas.some(a => this.activeFilters.map(a => a.toLowerCase()).includes(a.name.toLowerCase())))
    }
  },
  methods: {
    setFilters(updatedFilters) {
      this.activeFilters = updatedFilters
    },
    async getCoaches() {
      this.isLoading = true
      try {
        this.coaches =  await this.$store.getters['coaches/coaches']
        this.$store.dispatch('coaches/addCoachesToStore', this.coaches)
      } catch (error) {
        this.error = error.message || 'Something went wrong!'
      }

      this.isLoading = false
    }
  }
}
</script>

<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: space-between;
}
</style>