import {createStore} from "vuex";

import coachesModule from './modules/coaches/index'

const store = createStore({
    modules: {
        coaches: coachesModule
    },
    state() {
        return {
            userId: 'c13'
        }
    },
    getters: {
        userId(state) {
            return state.userId
        }
    }
})

export default store