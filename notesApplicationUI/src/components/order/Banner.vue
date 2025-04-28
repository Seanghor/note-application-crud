<template>
  <div class="flex bg-pink-500 flex-col items-center justify-center w-[400px]">
    <h4>{{ customer }}</h4>
    <div>
      <li>Created At: {{ orderDate }}</li>
      <li>Status: {{ status }}</li>
      <li>totalAmount: {{ totalAmount }}</li>
    </div>
    <div class="mt-6 text-center bg-red-500">
      <button @click="handleClose">Close</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, ref } from "vue";
defineProps({
  customer: String,
  orderDate: String,
  status: String,
  totalAmount: Number,
});

const emit = defineEmits<{
  (e: "closeBanner"): void;
}>();
const handleClose = () => {
  emit("closeBanner");
};
</script>
