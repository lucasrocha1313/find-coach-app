<template>
  <form @submit.prevent="submitForm">
    <div class="form-control" :class="{invalid: !firstName.isValid}">
      <label for="firstname">Firstname</label>
      <input type="text" id="firstname" v-model="firstName.value" @blur="clearValidity('firstName')">
    </div>
    <div class="form-control" :class="{invalid: !lastName.isValid}">
      <label for="lastname">Lastname</label>
      <input type="text" id="lastname" v-model="lastName.value" @blur="clearValidity('lastName')">
    </div>
    <div class="form-control" :class="{invalid: !description.isValid}">
      <label for="description">Description</label>
      <textarea id="description" rows="5" v-model="description.value" @blur="clearValidity('description')"></textarea>
    </div>
    <div class="form-control" :class="{invalid: !rate.isValid}">
      <label for="rate">Hourly rate</label>
      <input type="number" id="rate" v-model.number="rate.value" @blur="clearValidity('rate')">
    </div>
    <div class="form-control" :class="{invalid: !areasSelected.isValid}">
      <h3>Areas of Expertise</h3>
      <div v-for="area in areas" :key="area.id">
        <input type="checkbox" :id="area.id" :value="area.id" v-model="areasSelected.value" @blur="clearValidity('areasSelected')">
        <label :for="area.id">{{area.title}}</label>
      </div>
    </div>
    <p v-if="!isValid">Please fix the above errors and submit again</p>
    <base-button>Register</base-button>
  </form>
</template>

<script>
import {mapAreas} from "@/utils/mapAreasValue";
import BaseButton from "@/components/ui/BaseButton.vue";

export default {
  name: "CoachForm",
  emits: ['register-coach'],
  components: {BaseButton},
  data() {
    return {
      firstName: {
        value: '',
        isValid: true
      },
      lastName: {
        value: '',
        isValid: true
      },
      description: {
        value: '',
        isValid: true
      },
      rate: {
        value: null,
        isValid: true
      },
      areasSelected: {
        value: [],
        isValid: true
      },
      isValid: true
    }
  },
  computed: {
    areas() {
      return Object.keys(mapAreas).map(k => mapAreas[k])
    }
  },
  methods: {
    clearValidity(input) {
      this.isValid = true
      this[input].isValid = true
    },
    isFormValid() {
      if(this.firstName.value === '') {
        this.isValid = false
        this.firstName.isValid = false
      }
      if(this.lastName.value === '') {
        this.isValid = false
        this.lastName.isValid = false
      }
      if(this.description.value === '') {
        this.isValid = false
        this.description.isValid = false
      }
      if(!this.rate.value || this.rate.value <= 0) {
        this.isValid = false
        this.rate.isValid = false
      }
      if(this.areasSelected.value.length === 0) {
        this.areasSelected.isValid = false
        this.isValid = false
      }
    },
    submitForm() {
      this.isFormValid()
      if(!this.isValid) {
        return
      }

      const formData = {
        first: this.firstName.value,
        last: this.lastName.value,
        desc: this.description.value,
        rate: this.rate.value,
        areas: this.areasSelected.value
      }

      this.$emit('register-coach', formData)
    }
  }
}
</script>

<style scoped>
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input[type='checkbox'] + label {
  font-weight: normal;
  display: inline;
  margin: 0 0 0 0.5rem;
}

input,
textarea {
  display: block;
  width: 100%;
  border: 1px solid #ccc;
  font: inherit;
}

input:focus,
textarea:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

input[type='checkbox'] {
  display: inline;
  width: auto;
  border: none;
}

input[type='checkbox']:focus {
  outline: #3d008d solid 1px;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}

.invalid label {
  color: red;
}

.invalid input,
.invalid textarea {
  border: 1px solid red;
}
</style>