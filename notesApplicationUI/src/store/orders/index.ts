// import { addOrderService, deleteOrderByIdService, getAllOrdersService, updateOrderByIdService } from '@/services/order.service';
import { createStore } from 'vuex';

export interface Order {
    id: number;
    customerName: string;
    orderDate: string;
    status: string;
    totalAmount: number;
}

const orderDatas = [
    {
        id: 1,
        customerName: "Seanghor",
        orderDate: "2023-10-01",
        status: "Pending",
        totalAmount: 100.00,
    },
    {
        id: 2,
        customerName: "Sokha",
        orderDate: "2023-10-02",
        status: "Completed",
        totalAmount: 200.00,
    },
    {
        id: 3,
        customerName: "Sophal",
        orderDate: "2023-10-03",
        status: "Cancelled",
        totalAmount: 150.00,
    },
    {
        id: 4,
        customerName: "Chenda",
        orderDate: "2023-10-04",
        status: "Pending",
        totalAmount: 300.00,
    },
    {
        id: 5,
        customerName: "Rathana",
        orderDate: "2023-10-05",
        status: "Completed",
        totalAmount: 250.00,
    }
]

const orderStore = createStore({
    // state
    state: {
        orders: orderDatas as Order[],
    },

    // getters
    getters: {
        allOrders: (state) => {
            return state.orders;
        },
        orderById: (state) => (id: number) => {
            return state.orders.find((order) => order.id === id);
        },
    },

    // mutations
    mutations: {
        setOrders(state, orders: Order[]) {
            state.orders = orders;
        },
        addOrder(state, order: Order) {
            // state.orders.push(order);
            state.orders = [...state.orders, order];
        },
        updateOrder(state, updatedOrder: Order) {
            const index = state.orders.findIndex((order) => order.id === updatedOrder.id);
            if (index !== -1) {
                state.orders[index] = updatedOrder;
            }
        },
        deleteOrder(state, orderId: number) {
            state.orders = state.orders.filter((order) => order.id !== orderId);
        },
    },

    // actions --> .dispatch  : API call here
    actions: {
        async fetchOrders({ commit }) {
            const response = this.state.orders;
            commit('setOrders', response);
        },

        async addOrder({ commit }, order: Order) {
            const orderData = {
                ...order,
                id: this.state.orders.length + 1,
            }
            commit('addOrder', orderData);
        },

        async updateOrder({ commit }, updatedOrder: Order) {
            // Simulate an API call to update an order
            const newOrder = await this.state.orders.find((order) => order.id === updatedOrder.id);
            commit('updateOrder', { ...newOrder, ...updatedOrder });
        },

        async deleteOrder({ commit }, orderId: number) {
            const id = this.state.orders.find((order) => order.id === orderId)?.id;
            commit('deleteOrder', id);
        },

        async clearData({ commit }) {
            commit('setOrders', []);
        },
    }
})

export default orderStore;