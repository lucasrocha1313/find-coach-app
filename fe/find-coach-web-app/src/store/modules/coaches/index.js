import axios from "axios"
export default {
    namespaced: true,
    state() {
        return {
            coaches: []
        }
    },
    mutations: {
        registerCoach(state, payload) {
            state.coaches.push(payload)
        },
        initCoaches(state, payload) {
            state.coaches = []
            state.coaches = payload
        }
    },
    actions: {
        async registerCoach(context, data) {
            const coachData = {
                authId: data.authId,
                username: data.username,
                firstName: data.first,
                lastName: data.last,
                description: data.desc,
                hourlyRate: data.rate,
                isCoach: true,
                idsAreas: data.areas.map(a => a)
            }
            try {
                const result = await axios.put(`${process.env.VUE_APP_API_URL}/register`, coachData,
                    {
                        headers: { Authorization: `Bearer ${data.token}` }
                    })
                // eslint-disable-next-line no-debugger
                debugger
                context.commit('registerCoach', result.data)
            } catch (error) {
                // eslint-disable-next-line no-debugger
                debugger
                if(error.response.status === 401) {
                    context.rootState.isAuthenticated = false
                    throw new Error(`User is not authorized to register coach`)
                }

                throw new Error(`Failed to register coach with status ${error.response.status}: ${error.response.statusText}`)
            }

        },
        addCoachesToStore(context, data) {
            context.commit('initCoaches', data)
        }
    },
    getters: {
        async coaches() {
            const result = await axios.get(`${process.env.VUE_APP_API_URL}/coaches`)
            if(result.status !== 200) {
                throw new Error(`Failed to fetch coaches with status ${result.status}: ${result.statusText}`)
            }
            return result.data
        },
        coach: (state) => async (id) => {
            if(state.coaches.some(c => c.id === Number(id))) return state.coaches.find(c => c.id === Number(id))
            const result = await axios.get(`${process.env.APP_API_URL}/coaches/${id}`)
            if(result.status !== 200) {
                throw new Error(`Failed to fetch coach with status ${result.status}: ${result.statusText}`)
            }
            return result.data
        }
    }
}