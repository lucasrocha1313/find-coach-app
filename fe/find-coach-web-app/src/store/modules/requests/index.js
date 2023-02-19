import axios from "axios";

export default {
    namespaced: true,
    state() {
        return {
            requests: []
        }
    },
    mutations: {
        addRequest(state, payload) {
            state.requests.push(payload)
        }
    },
    actions: {
        async contactCoach(context, payload) {
            const newRequest = {
                coachId: payload.coachId,
                userEmail: payload.email,
                message: payload.message
            }

            const response = await axios.post(`${process.env.VUE_APP_API_URL}/requests`, newRequest)
            if(response.status !== 200) {
                throw new Error(`Failed to post requests to coach with status ${response.status}: ${response.statusText}`)
            }
            context.commit('addRequest', response.data)
        }
    },
    getters: {
        async requests(state, _, _2, rootGetters) {
            const response = await axios.get(`${process.env.VUE_APP_API_URL}/requests/${rootGetters.userId}`)
            if(response.status !== 200) {
                throw new Error(`Failed to fetch requests to coach with status ${response.status}: ${response.statusText}`)
            }
            return response.data
        }
    }
}