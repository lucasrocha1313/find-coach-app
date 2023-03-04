<template>
  <div>
    <base-dialog :show="!!error" title="A error occurred" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>
    <base-dialog :show="isLoading" title="Authenticating..." fixed>
      <base-spinner></base-spinner>
    </base-dialog>
    <base-card>
      <form @submit.prevent="submitForm">
        <div class="form-control">
          <label for="email">E-Mail</label>
          <input type="email" id="email" v-model.trim="email">
        </div>
        <div class="form-control">
          <label for="password">Password</label>
          <input type="password" id="password" v-model.trim="password">
        </div>
        <p v-if="!isFormValid">Please submit a valid email and password (password must have more than 6 characters</p>
        <base-button>{{submitButtonCaption}}</base-button>
        <base-button type="button" mode="flat" @click="switchAuthMode">{{switchModeButtonCaption}}</base-button>
      </form>
    </base-card>
  </div>
</template>

<script>
import BaseButton from "@/components/ui/BaseButton.vue";
import BaseCard from "@/components/ui/BaseCard.vue";
import BaseDialog from "@/components/ui/BaseDialog.vue";
import BaseSpinner from "@/components/ui/BaseSpinner.vue";
import ErrorMixin from "@/mixins/ErrorMixin.vue";

export default {
  name: "UserAuth",
  mixins: [ErrorMixin],
  components: {BaseSpinner, BaseCard, BaseButton, BaseDialog},
  data() {
    return {
      email: '',
      password: '',
      mode:'login',
      isLoading: false
    }
  },
  methods: {
    async submitForm(){
      if(!this.isFormValid) return
      this.isLoading = true
      try{
        const payload = {
          email: this.email,
          password: this.password
        }
        if(this.mode === 'login') {
          await this.$store.dispatch('login', payload)
        } else {
          await this.$store.dispatch('signup', payload)
          //TODO change backend to log the user in after signup
          this.mode = 'login'
        }
      }catch (error) {
        this.error = error.message || 'Something went wrong!'
      }

      this.isLoading = false
    },
    switchAuthMode(){
      this.mode = this.mode==='login' ? 'signup' : 'login'
    }
  },
  computed: {
    isEmailValid() {
      return String(this.email)
          .toLowerCase()
          .match(
              /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
          )
    },
    isFormValid() {
      return (this.isEmailValid && this.password.length > 6);
    },
    submitButtonCaption() {
      return  this.mode==='login' ? 'Login' : 'Signup'
    },
    switchModeButtonCaption() {
      return  this.mode==='login' ? 'Signup instead' : 'Login instead'
    }
  }
}
</script>

<style scoped>
form {
  margin: 1rem;
  padding: 1rem;
}

.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  margin-bottom: 0.5rem;
  display: block;
}

input,
textarea {
  display: block;
  width: 100%;
  font: inherit;
  border: 1px solid #ccc;
  padding: 0.15rem;
}

input:focus,
textarea:focus {
  border-color: #3d008d;
  background-color: #faf6ff;
  outline: none;
}
</style>