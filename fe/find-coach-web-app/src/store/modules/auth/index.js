import axios from "axios";

export default {
    state() {
        return {
            userId: null,
            token: null,
            isAuthenticated: false,
            isCoach: false
        }
    },
    getters: {
        userId(state) {
            return state.userId
        },
        token(state) {
            return state.token
        },
        isAuthenticated(state) {
            return state.isAuthenticated
        },
        isCoach(state) {
            return state.isCoach
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

                context.commit('setUser', { ...{isAuthenticated: true},...response.data})
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
                context.commit('setUser', { ...{isAuthenticated: true},...response.data})
            } catch (err) {
                if(err.response.status === 400) {
                    throw new Error(err.response.data)
                }
                throw new Error(`Failed to signup user. Status returned is ${err.response.status}: ${err.response.statusText}`)
            }
        },
        logout(context) {
            context.commit('setUser', {
                token: null,
                userId: null,
                isAuthenticated: false,
                isCoach: false
            })
        }

    },
    mutations: {
        setUser(state, payload) {
            state.token = payload.token
            state.userId = payload.authId
            state.isAuthenticated = payload.isAuthenticated
            state.isCoach = payload.isCoach
        }
    }
}