<template>
  <div class="fixed inset-0 bg-gray-500 bg-opacity-50 flex justify-center items-center">
    <div
      class="bg-gradient-to-r from-blue-50 to-white p-8 rounded-2xl w-[600px] max-w-3xl shadow-xl border-2 border-blue-300"
    >
      <h2
        class="text-xl md:text-3xl lg:text-3xl font-semibold mb-6 text-center text-gray-900 font-serif tracking-tight"
      >
        {{ data.id ? "Edit Order" : "Create Order" }}
      </h2>

      <form @submit.prevent="handleSubmit">
        <div class="mb-6">
          <label for="title" class="block text-sm font-medium text-pink-800"
            >Customer</label
          >
          <input
            id="title"
            v-model="data.customerName"
            type="text"
            placeholder="Enter title"
            class="w-full p-4 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-lg font-medium placeholder:text-gray-400 transition duration-200"
            required
          />
        </div>

        <div class="mb-6">
          <label for="content" class="block text-sm font-medium text-pink-800"
            >Order At</label
          >
          <input
            id="title"
            v-model="data.orderDate"
            type="text"
            placeholder="Enter title"
            class="w-full p-4 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-lg font-medium placeholder:text-gray-400 transition duration-200"
            required
          />
        </div>

        <div class="mb-6">
          <label for="content" class="block text-sm font-medium text-pink-800"
            >Status</label
          >
          <input
            id="title"
            v-model="data.status"
            type="text"
            placeholder="Enter title"
            class="w-full p-4 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-lg font-medium placeholder:text-gray-400 transition duration-200"
            required
          />
        </div>

        <div class="mb-6">
          <label for="content" class="block text-sm font-medium text-pink-800"
            >Amount</label
          >
          <input
            id="title"
            v-model="data.totalAmount"
            type="number"
            placeholder="Enter title"
            class="w-full p-4 mt-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-lg font-medium placeholder:text-gray-400 transition duration-200"
            required
          />
        </div>
        <!-- Action Section -->
        <div class="flex justify-between items-center mt-4">
          <button
            type="button"
            @click="closeForm"
            class="px-6 py-3 bg-red-600 text-white rounded-sm hover:bg-red-700 focus:outline-none cursor-pointer transition duration-200 shadow-md"
          >
            Cancel
          </button>

          <button
            type="submit"
            class="px-6 py-3 bg-pink-600 text-white rounded-sm hover:bg-pink-700 focus:outline-none cursor-pointer transition duration-200 shadow-md"
          >
            Save
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from "vue";
import { Order } from "@/store/orders";

const props = defineProps({
  order: {
    type: Object,
    required: true,
  },
});

const data = ref({ ...props.order });

const emit = defineEmits<{
  (e: "close"): void;
  (e: "submit", order: Order): void;
}>();

const handleSubmit = () => {
  emit("submit", data.value as Order);
};

const closeForm = () => {
  emit("close");
};
</script>
