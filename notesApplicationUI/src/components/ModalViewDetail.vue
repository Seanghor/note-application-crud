<template>
  <div class="fixed inset-0 bg-gray-500 bg-opacity-50 flex justify-center items-center">
    <div
      class="bg-gradient-to-r from-blue-100 via-white to-blue-100 px-7 py-8 rounded-2xl w-[600px] max-w-3xl shadow-2xl transform transition-all scale-95 hover:scale-100 border-2 border-gray-300"
    >
      <h2
        class="text-xl md:text-3xl lg:text-3xl font-serif font-semibold mb-6 text-center text-gray-900 font-sans tracking-tight"
      >
        Note Details
      </h2>
      <div class="space-y-6 text-gray-700 max-h-72 overflow-y-auto text-sm md:text-sm lg:text-lg">
        <div >
          <p><span class="text-pink-600 font-medium">Title:</span> {{ note.title }}</p>
        </div>
        <div >
          <p><span class="text-pink-600 font-medium">Content:</span></p>
          <p class="whitespace-pre-line break-words border-1 rounded h-20 p-1">
            {{ note.content }}
          </p>
        </div>
        <div >
          <p>
            <span class="text-pink-600 font-medium">Created At:</span>
            {{ formatDate(note.created_At) }}
          </p>
        </div>
        <div >
          <p v-if="note.updated_At">
            <span class="text-pink-600 font-medium">Updated At:</span>
            {{ formatDate(note.updated_At) }}
          </p>
          <p v-else>
            <span class="text-pink-600 font-medium">Updated At:</span>
            Non
          </p>
        </div>
      </div>

      <!-- Close Button -->
      <div class="mt-6 text-center">
        <button
          @click="closeModal"
          class="bg-pink-600 text-white w-full px-8 py-3 rounded-sm hover:bg-pink-700 cursor-pointer focus:outline-none focus:ring-4 focus:ring-blue-300 transition duration-300 font-medium shadow-lg"
        >
          Close
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from "vue";
import { formatDate } from "@/utils/format-date";

const props = defineProps({
  note: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits();

const closeModal = () => {
  emit("close");
};
</script>

<style scoped></style>
