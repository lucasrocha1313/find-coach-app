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

            try {
                const response = await axios.post(`${process.env.VUE_APP_API_URL}/requests`, newRequest)

                context.commit('addRequest', response.data)
            } catch (error) {
                if(error.status === 401)
                    throw new Error(`User is not authorized to send requests`)

                throw new Error(`Failed to post requests to coach with status ${error.response.status}: ${error.response.statusText}`)
            }
        }
    },
    getters: {
        async requests(state, _, _2, rootGetters) {
            try {
                const response = await axios.get(`${process.env.VUE_APP_API_URL}/requests/${rootGetters.userId}`, {
                    headers: { Authorization: `Bearer ${rootGetters.token}` }
                })
                return response.data
            } catch (error) {
                if(error.response.status === 401)
                    throw new Error(`User is not authorized to see the requests`)

                throw new Error(`Failed to fetch requests to coach with status ${error.response.status}: ${error.response.statusText}`)
            }

        }
    }
}