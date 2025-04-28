<template>
  <h2>Order Page</h2>
  <button
    @click="openCreateOrderForm"
    class="bg-pink-500 cursor-pointer text-white px-2 lg:px-4 py-2 lg:py-2 rounded-sm hover:bg-pink-600 transition duration-200 flex items-center space-x-2"
  >
    <span class="text-sm md:text-sm lg:text-lg">Create Note</span>
    <Icon icon="akar-icons:plus" class="text-sm md:text-sm lg:text-lg" />
  </button>
  <button
    @click="isOpenClearAllModal = true"
    class="mt-4 rounded bg-pink-500 cursor-pointer text-white px-3 lg:px-5 py-2 lg:py-2 rounded-sm hover:bg-pink-600 transition duration-200 flex items-center space-x-2"
  >
    <Icon icon="grommet-icons:clear-option" class="text-sm md:text-sm lg:text-lg" />
  </button>
  <div
    v-if="orders.length > 0"
    class="overflow-x-auto bg-white shadow-xl rounded-lg border border-gray-200 text-white"
  >
    <table class="min-w-full table-auto">
      <thead class="bg-pink-500 text-base">
        <tr>
          <th class="py-4 px-6 text-left font-semibold">No.</th>
          <th class="py-4 px-6 text-left font-semibold">Customer</th>
          <th class="py-4 px-6 text-left font-semibold">Order At</th>
          <th class="py-4 px-6 text-left font-semibold">Status</th>
          <th class="py-4 px-6 text-center font-semibold">Amount</th>
          <th class="py-4 px-6 text-center font-semibold">Options</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(order, i) in orders"
          :key="order.id"
          :class="{
            'border-b hover:bg-gray-300 transition duration-200 cursor-pointer': true,
            'bg-white': i % 2 === 0,
            'bg-pink-200': i % 2 !== 0,
          }"
        >
          <td class="py-3 px-6 text-gray-800">{{ i + 1 }}</td>
          <td class="py-3 px-6 text-gray-800">{{ order.customerName }}</td>
          <td class="py-3 px-6 text-gray-800">{{ order.orderDate }}</td>
          <td class="py-3 px-6 text-gray-800">{{ order.status }}</td>
          <td class="py-3 px-6 text-gray-800">{{ order.totalAmount }}</td>
          <td class="py-3 flex flex-row px-6 justify-center text-center space-x-4">
            <button
              @click="viewOrderDetail(order)"
              class="text-blue-500 hover:text-blue-600 transition duration-150 cursor-pointer"
            >
              <Icon icon="mdi:eye" class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6" />
            </button>
            <button
              @click="openEditOrderForm(order)"
              class="text-yellow-500 hover:text-yellow-600 transition duration-150 cursor-pointer"
            >
              <Icon icon="mdi:pencil" class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6" />
            </button>
            <button
              @click="handleDeleteOneOrder(order.id)"
              class="text-red-500 hover:text-red-600 transition duration-150 cursor-pointer"
            >
              <Icon icon="mdi:trash-can" class="h-5 md:h-6 lg:h-6 w-5 md:w-6 lg:w-6" />
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div v-else>
    <div class="flex flex-col items-center justify-center h-screen bg-gray-100">
      <Icon icon="mdi:alert-circle" class="h-16 w-16 text-gray-500 mb-4" />
      <h2 class="text-xl font-semibold text-gray-700">No Orders Found</h2>
      <p class="text-gray-500">Please check back later.</p>
    </div>
  </div>

  <Banner
    v-if="isOpenViewBanner"
    :customer="seletedOrder?.customerName"
    :orderDate="seletedOrder?.orderDate"
    :status="seletedOrder?.status"
    :totalAmount="seletedOrder?.totalAmount"
    @closeBanner="closeBanner"
  />

  <FormOrder
    v-if="isOpenCreationForm"
    :order="seletedOrder!"
    @close="closeCreationForm"
    @submit="handleSubmit"
  />

  <VerifyOrderModal
    v-if="isOpenClearAllModal"
    @close="isOpenClearAllModal = false"
    @confirm="handleClearAllData"
  />
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { Icon } from "@iconify/vue";
import orderStore, { Order } from "@/store/orders";
import { formatDate } from "@/utils/format-date";
import Banner from "@/components/order/Banner.vue";
import FormOrder from "@/components/order/FormOrder.vue";
import VerifyOrderModal from "@/components/order/VerifyOrderModal.vue";

const orders = ref<Order[]>([]);
const seletedOrder = ref<Order | null>(null);
const isOpenViewBanner = ref(false);
const isOpenCreationForm = ref(false);
const isOpenClearAllModal = ref(false);

const fetchOrders = async () => {
  await orderStore.dispatch("fetchOrders");
  const datas = computed(() => orderStore.state.orders);
  orders.value = datas.value;
};

// --- handle view order detail ---
const viewOrderDetail = (order: Order) => {
  seletedOrder.value = { ...order };
  isOpenViewBanner.value = true;
};
const closeBanner = () => {
  console.log("Close Banner clicked ...");
  seletedOrder.value = null;
  isOpenViewBanner.value = false;
};

// --- handle create note form ---
const openCreateOrderForm = () => {
  console.log("Open Create Form clicked ...");
  seletedOrder.value = null;
  isOpenCreationForm.value = true;
};
const closeCreationForm = () => {
  console.log("Close Form clicked ...");
  seletedOrder.value = null;
  isOpenCreationForm.value = false;
};

// --- handle edit note form ---
const openEditOrderForm = (order: Order) => {
  console.log("Open Edit Form clicked ...", order);
  seletedOrder.value = { ...order };
  isOpenCreationForm.value = true;
};

const handleClearAllData = () => {
  console.log("Clear All Order clicked ...");
  orderStore.dispatch("clearData");
  isOpenClearAllModal.value = false;
  fetchOrders();
}
const handleDeleteOneOrder = async (id: number) => {
  console.log("Delete Order clicked ...", id);
  await orderStore.dispatch("deleteOrder", id);
  await fetchOrders();
};

const handleSubmit = async (order: Order) => {
  if (!order.id) {
    await orderStore.dispatch("addOrder", order);
  } else {
    await orderStore.dispatch("updateOrder", order);
  }
  console.log("Submit Form clicked ...", orders.value);

  await fetchOrders();
  closeCreationForm();
};

onMounted(async () => {
  await fetchOrders();
});
</script>
