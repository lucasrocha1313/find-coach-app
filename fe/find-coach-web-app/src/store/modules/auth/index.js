import axios from "axios";

export default {
    state() {
        return {
            userId: 13,
            token: null
        }
    },
    getters: {
        userId(state) {
            return state.userId
        },
        token(state) {
            return state.token
        }
    },
    actions: {
        async signup(context, payload) {
            const auth = {
                email: payload.email,
                password: payload.password
            }

            try {
                const response = await axios.post(`${process.env.VUE_APP_API_URL}/auth/signup`, auth )

                context.commit('setUser', response.data)
            } catch (err) {
                if(err.response.status === 400) {
                    throw new Error(err.response.data)
                }
                throw new Error(`Failed to signup user. Status returned is ${err.response.status}: ${err.response.statusText}`)
            }
        },
        async login(context, payload) {
            const auth = {
                email: payload.email,
                password: payload.password
            }

            try {
                const response = await axios.post(`${process.env.VUE_APP_API_URL}/auth/login`, auth )
                context.commit('setUser', response.data)
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
            state.userId = payload.authId
        }
    }
}