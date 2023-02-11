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
                username: data.username,
                firstName: data.first,
                lastName: data.last,
                description: data.desc,
                hourlyRate: data.rate,
                isCoach: true,
                idsAreas: data.areas.map(a => a)
            }
            const result = await axios.put('https://localhost:7129/api/register', coachData)
            context.commit('registerCoach', result.data)
        },
        addCoachesToStore(context, data) {
            context.commit('initCoaches', data)
        }
    },
    getters: {
        async coaches() {
            const result = await axios.get('https://localhost:7129/api/coaches')
            return result.data
        },
        coach: (state) => async (id) => {
            // eslint-disable-next-line no-debugger
            debugger
            if(state.coaches.some(c => c.id === Number(id))) return state.coaches.find(c => c.id === Number(id))
            const result = await axios.get(`https://localhost:7129/api/coaches/${id}`)
            return result.data
        }
    }
}