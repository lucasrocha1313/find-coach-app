import axios from "axios";

export default {
    state() {
        return {
            userId: 13
        }
    },
    getters: {
        userId(state) {
            return state.userId
        }
    },
    actions: {
        async signup(context, payload) {
            const auth = {
                email: payload.email,
                password: payload.password
            }

            try {
                //TODO login and return token
                await axios.post(`${process.env.VUE_APP_API_URL}/auth/signup`, auth )
            } catch (err) {
                if(err.response.status === 400) {
                    throw new Error(err.response.data)
                }
                throw new Error(`Failed to signup user. Status returned is ${err.response.status}: ${err.response.statusText}`)
            }
        }
    },
    mutations: {
        setUser(state, payload) {
            state.token = payload.token
            state.authId = payload.authId
        }
    }
}