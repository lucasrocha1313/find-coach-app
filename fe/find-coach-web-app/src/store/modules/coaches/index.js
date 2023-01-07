import {mapAreas} from "@/utils/mapAreasValue";

export default {
    namespaced: true,
    state() {
        return {
            coaches: [
                {
                    id: 'c1',
                    firstName: 'Maximilian',
                    lastName: 'Schwarzmüller',
                    areas: [mapAreas.frontend, mapAreas.backend, mapAreas.career],
                    description:
                        "I'm Maximilian and I've worked as a freelance web developer for years. Let me help you become a developer as well!",
                    hourlyRate: 30
                },
                {
                    id: 'c2',
                    firstName: 'Julie',
                    lastName: 'Jones',
                    areas: [mapAreas.frontend, mapAreas.career],
                    description:
                        'I am Julie and as a senior developer in a big tech company, I can help you get your first job or progress in your current role.',
                    hourlyRate: 30
                },
                {
                    id: 'c3',
                    firstName: 'Monty',
                    lastName: 'Bones',
                    areas: [mapAreas.career],
                    description:
                        'I am a quantic coach',
                    hourlyRate: 30
                }
            ]
        }
    },
    mutations: {},
    actions: {},
    getters: {
        coaches(state) {
            return state.coaches
        },
        hasCoaches(state) {
            return state.coaches && state.coaches.length
        }
    }
}