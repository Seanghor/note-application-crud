<template>
  <div
    class="fixed inset-0 bg-gray-500 bg-opacity-50 flex justify-center items-center z-50"
  >
    <div class="bg-white p-8 rounded-lg w-96 shadow-lg">
      <h2 class="text:lg lg:text-2xl font-semibold text-center mb-6 text-gray-800">
        Are you sure you want to delete all Order?
      </h2>

      <div
        class="flex justify-between mt-6 md:mt-10 lg:mt-10 text-xs md:text-sm lg:text-lg"
      >
        <button
          @click="handleClose"
          class="px-6 py-2 text-red-500 hover:bg-gray-100 rounded-md transition duration-200 cursor-pointer"
        >
          Cancel
        </button>
        <button
          @click="handleConfirm"
          class="px-6 py-2 bg-pink-600 text-white rounded-md hover:bg-pink-700 transition duration-200 cursor-pointer"
        >
          Yes, Clear All
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from "vue";
    const emit = defineEmits<{
    (e: "close"): void;
    (e: "confirm"): void;
  }>();

  const handleClose = () => {
    emit("close");
  };
  const handleConfirm = () => {
    emit("confirm");
  };
</script>
