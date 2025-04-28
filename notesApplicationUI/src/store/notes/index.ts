import { addNote, deleteAllNotes, deleteNoteById, getAllNotes, updateNoteById } from '@/services/note.service';
import { createStore } from 'vuex';

export interface Note {
    id: number;
    title: string;
    content: string;
    created_At: string;
    updated_At: string;
}


const store = createStore({
    // state
    state: {
        notes: [] as Note[],
        searchQuery: '',
        titleFilter: '',
        sortBy: 'Created_At', // Default sorting by created date
        descending: false, // Default sorting in ascending order
    },

    // getters
    getters: {
        filteredNotes: (state) => {
            return state.notes;
        },
    },

    // mutation -->.commit
    mutations: {
        setNotes(state, notes: Note[]) {
            state.notes = notes;
        },
        addNote(state, note: Note) {
            state.notes.push(note);
        },
        updateNote(state, updatedNote: Note) {
            const index = state.notes.findIndex((note) => note.id === updatedNote.id);
            if (index !== -1) {
                state.notes[index] = updatedNote;
            }
        },
        deleteNote(state, noteId: number) {
            state.notes = state.notes.filter((note) => note.id !== noteId);
        },

        clearNotes(state) {
            state.notes = [];
        },

        setSearchQuery(state, query: string) {
            state.searchQuery = query;
        },
        setTitleFilter(state, filter: string) {
            state.titleFilter = filter;
        },
        setSortBy(state, sortBy: string) {
            state.sortBy = sortBy;
        },
        setDescending(state, descending: boolean) {
            state.descending = descending;
        },
    },

    // actions --> dispatch
    actions: {
        async fetchNotes({ commit, state }) {
            try {
                const response = await getAllNotes({
                    search: state.searchQuery,
                    titleFilter: state.titleFilter,
                    sortBy: state.sortBy,
                    descending: state.descending,
                });
                commit('setNotes', response.data);
            } catch (error) {
                console.error('Error fetching notes:', error);
            }
        },

        async createNote({ commit }, note: Note) {
            try {
                const response = await addNote(note);
                commit('addNote', response.data);
            } catch (error) {
                console.error('Error creating note:', error);
            }
        },
        
        async updateNote({ commit }, updatedNote: Note) {
            try {
                const response = await updateNoteById(updatedNote.id, updatedNote);
                commit('updateNote', response.data);
            } catch (error) {
                console.error('Error updating note:', error);
            }
        },
        async deleteNote({ commit }, noteId: number) {
            try {
                await deleteNoteById(noteId);
                commit('deleteNote', noteId);
            } catch (error) {
                console.error('Error deleting note:', error);
            }
        },
        async clearNotes({ commit }) {
            try {
                await deleteAllNotes();
                commit('clearNotes');
            } catch (error) {
                console.error('Error clearing notes:', error);
            }
        },
        // setSearchQuery({ commit }, query: string) {
        //     commit('setSearchQuery', query);
        // },
        // setTitleFilter({ commit }, filter: string) {
        //     commit('setTitleFilter', filter);
        // },
        // setSortBy({ commit }, sortBy: string) {
        //     commit('setSortBy', sortBy);
        // },
        // setDescending({ commit }, descending: boolean) {
        //     commit('setDescending', descending);
        // },
    },
});

export default store;
