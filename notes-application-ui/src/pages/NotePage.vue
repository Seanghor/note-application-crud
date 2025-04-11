<template>
  <div class="min-h-screen bg-gray-100 py-10 px-6">
    <div class="max-w-7xl mx-auto">
      <div class="flex flex-row justify-between items-center mb-6">
        <h1
          class="text:4xl md:text:4xl lg:text-4xl font-bold text-center mb-8 text-gray-900 items-center font-serif tracking-tight"
        >
          Your Notes
        </h1>
        <!-- Clear all -->
        <button
          @click="showClearAllModal = true"
          class="rounded bg-pink-500 cursor-pointer text-white px-3 lg:px-5 py-2 lg:py-2 rounded-sm hover:bg-pink-600 transition duration-200 flex items-center space-x-2"
        >
          <Icon icon="grommet-icons:clear-option" class="text-sm md:text-sm lg:text-lg" />
        </button>
      </div>

      <div
        class="flex flex-col md:flex-row lg:md:flex-row justify-between mb-6 space-y-4 md:space-y-0 md:space-x-4"
      >
        <!-- Search Input -->
        <div class="flex space-x-4 w-full md:w-1/3 flex flex-row items-center">
          <label>
            <Icon
              icon="uil:search"
              class="text-lg items-center h-6 md:h-6 lg:h-7 w-6 md:w-6 lg:w-7 text-gray-500"
            />
          </label>
          <input
            v-model="searchQuery"
            @input="searchNotes"
            type="text"
            placeholder="Search notes..."
            class="px-2 py-2 border border-gray-300 rounded-lg w-full focus:outline-none focus:ring-2 focus:ring-blue-500 transition duration-300 placeholder:text-gray-400"
          />
        </div>

        <!-- Sort Dropdown -->
        <div
          class="flex flex-col lg:flex-row items-end md:items-center lg:items-center justify-end space-x-2"
        >
          <label
            for="sortOrder"
            class="invisible md:visible lg:visible md: text-sm md:text-base lg:text-base font-medium"
          >
            <Icon
              v-if="!sortDescending"
              icon="mdi:sort-descending"
              class="text-lg items-center h-6 md:h-6 lg:h-7 w-6 md:w-6 lg:w-7 text-gray-500"
            />
            <Icon
              v-else
              icon="mdi:sort-ascending"
              class="text-lg items-center h-6 md:h-6 lg:h-7 w-6 md:w-6 lg:w-7 text-gray-500"
            />
          </label>
          <select
            id="sortOrder"
            v-model="sortDescending"
            @change="sortNotesAs"
            class="cursor-pointer px-2 py-1 border border-gray-400 rounded-md focus:outline-none focus:ring-pink-500 transition duration-200"
          >
            <option
              v-for="sortOption in SORT_OPTIONS"
              class="text-xs md:text-sm lg:text-sm font-medium cursor-pointer"
              :value="sortOption.value"
              :key="sortOption.label"
            >
              {{ sortOption?.label }}
            </option>
          </select>
        </div>
      </div>

      <div class="pt-10 md:pt-16 pt-20">
        <div class="flex justify-end mb-4">
          <button
            @click="openCreateNoteForm"
            class="bg-pink-500 cursor-pointer text-white px-2 lg:px-4 py-2 lg:py-2 rounded-sm hover:bg-pink-600 transition duration-200 flex items-center space-x-2"
          >
            <span class="text-sm md:text-sm lg:text-lg">Create Note</span>
            <Icon icon="akar-icons:plus" class="text-sm md:text-sm lg:text-lg" />
          </button>
        </div>
        <div>
          <div
            v-if="filteredNotes.length > 0"
            class="overflow-x-auto bg-white shadow-xl rounded-lg border border-gray-200 text-white"
          >
            <table class="min-w-full table-auto">
              <thead class="bg-pink-500 text-base">
                <tr>
                  <th class="py-4 px-6 text-left font-semibold">No.</th>
                  <th class="py-4 px-6 text-left font-semibold">Title</th>
                  <th class="py-4 px-6 text-left font-semibold">Content</th>
                  <th class="py-4 px-6 text-left font-semibold">Created At</th>
                  <th class="py-4 px-6 text-center font-semibold">Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(note, i) in filteredNotes"
                  :key="note.id"
                  :class="{
                    'border-b hover:bg-gray-300 transition duration-200 cursor-pointer': true,
                    'bg-white': i % 2 === 0,
                    'bg-pink-200': i % 2 !== 0,
                  }"
                >
                  <td class="py-3 px-6 text-gray-800">{{ i + 1 }}</td>
                  <td class="py-3 px-6 text-gray-800">{{ note.title }}</td>
                  <td class="py-3 px-6 text-gray-800">{{ note.content }}</td>
                  <td class="py-3 px-6 text-gray-800">
                    {{ formatDate(note.created_At) }}
                  </td>
                  <td
                    class="py-3 flex flex-row px-6 justify-center text-center space-x-4"
                  >
                    <button
                      @click="viewNoteDetail(note)"
                      class="text-blue-500 hover:text-blue-600 transition duration-150 cursor-pointer"
                    >
                      <Icon icon="mdi:eye" class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6" />
                    </button>
                    <button
                      @click="openEditNoteForm(note)"
                      class="text-yellow-500 hover:text-yellow-600 transition duration-150 cursor-pointer"
                    >
                      <Icon
                        icon="mdi:pencil"
                        class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6"
                      />
                    </button>
                    <button
                      @click="deleteOneNote(note.id)"
                      class="text-red-500 hover:text-red-600 transition duration-150 cursor-pointer"
                    >
                      <Icon
                        icon="mdi:trash-can"
                        class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6"
                      />
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <!-- No notes found message -->
          <div v-else class="text-center text-gray-500 mt-8">
            <p>No notes available. Start by creating one!</p>
          </div>
        </div>
      </div>
    </div>

    <NoteForm
      v-if="isFormVisible"
      :note="selectedNote!"
      @close="closeEditForm"
      @save="saveNote"
    />
    <ModalViewDetail
      v-if="isDetailModalVisible"
      :note="selectedNote!"
      @close="closeDetailModal"
    />
    <VerifyModal
      :isVisible="showClearAllModal"
      @close="showClearAllModal = false"
      @confirm="clearAllNotes"
    />

    <button
      @click="handleSignOut"
      class="fixed left-4 bottom-4 rounded bg-gray-500 cursor-pointer text-white px-3 lg:px-5 py-2 lg:py-2 rounded-sm hover:bg-gray-600 transition duration-200 flex items-center space-x-2"
    >
      <label>Sign Out</label>
      <Icon icon="ri:logout-box-r-fill" class="text-sm md:text-sm lg:text-lg" />
    </button>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { Icon } from "@iconify/vue";
import store from "../store/index";
import NoteForm from "../components/NoteForm.vue";
import ModalViewDetail from "../components/ModalViewDetail.vue";
import { formatDate } from "@/utils/format-date";
import { toast } from "vue-sonner";
import { NoteResponse } from "@/@types/note";
import VerifyModal from "@/components/VerifyModal.vue";
import router from "@/router";


// Sorting options
const SORT_OPTIONS = [
  { value: false, label: "Created At (Asc)" },
  { value: true, label: "Created At (Desc)" },
];

// Reactive States
const isFormVisible = ref(false);
const isDetailModalVisible = ref(false);
const showClearAllModal = ref(false);
const selectedNote = ref<NoteResponse | null>(null);

const searchQuery = ref("");
const selectedSort = ref("createdAt");
const sortDescending = ref(false);

// Fetch notes from store
const fetchNotes = () => {
  store.dispatch("fetchNotes");
};

// Handle search query change
const searchNotes = () => {
  store.commit("setSearchQuery", searchQuery.value);
  store.dispatch("fetchNotes");
};

// Handle sort change as
const sortNotesAs = () => {
  store.commit("setDescending", sortDescending.value);
  store.dispatch("fetchNotes");
};

// Filter notes based on search query and sort them
const filteredNotes = computed(() => store.getters.filteredNotes);

// --> Open create note form
const openCreateNoteForm = () => {
  selectedNote.value = null;
  isFormVisible.value = true;
};

// --> Handle form Edit/Create
const openEditNoteForm = (note: NoteResponse) => {
  selectedNote.value = { ...note };
  isFormVisible.value = true;
};
const closeEditForm = () => {
  isFormVisible.value = false;
  selectedNote.value = null;
};

// handle submit form:
const saveNote = async (note: NoteResponse) => {
  if (note.id) {
    await store.dispatch("updateNote", note);
    toast.success("Note updated successfully!");
  } else {
    await store.dispatch("createNote", note);
    toast.success("Note created successfully!");
  }
  fetchNotes();
  closeEditForm();
};

// Delete note by ID
const deleteOneNote = async (id: number) => {
  await store.dispatch("deleteNote", id);
  toast.success("Note deleted successfully!");
  fetchNotes();
};

// ---> Handle view note detail
const viewNoteDetail = (note: NoteResponse) => {
  selectedNote.value = { ...note };
  isDetailModalVisible.value = true;
};
const closeDetailModal = () => {
  isDetailModalVisible.value = false;
  selectedNote.value = null;
};

// ---> Clear all notes ---
const clearAllNotes = async () => {
  await store.dispatch("clearNotes");
  toast.success("All notes have been cleared!");
  showClearAllModal.value = false;
  fetchNotes();
};

const handleSignOut = () => {
  localStorage.removeItem("session");
  router.push("/signin");
  window.location.reload();
};

onMounted(async () => {
  await store.dispatch("fetchNotes");
});
</script>
