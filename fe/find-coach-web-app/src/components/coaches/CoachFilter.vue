<template>
  <base-card>
    <h2>Find your coach by area</h2>
    <span v-for="area in filters" :key="area.id" class="filter-option">
      <input type="checkbox" v-model="selected" :value="area.id" :id="area.id" @change="changeSelection">
      <label :for="area.id">{{area.name}}</label>
    </span>
  </base-card>
</template>

<script>
import BaseCard from "@/components/ui/BaseCard.vue";
import {mapAreas} from "@/utils/mapAreasValue";

export default {
  name: "CoachFilter",
  emits: ['change-filter'],
  components: {BaseCard},
  data() {
    return {
      filters: [mapAreas.frontend, mapAreas.backend, mapAreas.career],
      selected: []
    }
  },
  methods: {
    changeSelection() {
      this.$emit('change-filter', this.selected)
    }
  }
}
</script>

<style scoped>
h2 {
  margin: 0.5rem 0;
}

.filter-option {
  margin-right: 1rem;
}

.filter-option label,
.filter-option input {
  vertical-align: middle;
}

.filter-option label {
  margin-left: 0.25rem;
}

.filter-option.active label {
  font-weight: bold;
}
</style>