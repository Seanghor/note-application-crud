

// import { Order } from "@/store/orders"
// const noteDatas = [
//     {
//         id: 1,
//         customerName: "Seanghor",
//         orderDate: "2023-10-01",
//         status: "Pending",
//         totalAmount: 100.00,
//     },
//     {
//         id: 2,
//         customerName: "Sokha",
//         orderDate: "2023-10-02",
//         status: "Completed",
//         totalAmount: 200.00,
//     },
//     {
//         id: 3,
//         customerName: "Sophal",
//         orderDate: "2023-10-03",
//         status: "Cancelled",
//         totalAmount: 150.00,
//     },
//     {
//         id: 4,
//         customerName: "Chenda",
//         orderDate: "2023-10-04",
//         status: "Pending",
//         totalAmount: 300.00,
//     },
//     {
//         id: 5,
//         customerName: "Rathana",
//         orderDate: "2023-10-05",
//         status: "Completed",
//         totalAmount: 250.00,
//     }
// ]

// export const getAllOrdersService = async () => {
//     return noteDatas as Order[];
// }

// export const getOneOrderService = async (id: number) => {
//     return noteDatas.find((order) => order.id === id) as Order;
// }

// export const addOrderService = async (order: Order) => {
//     noteDatas.push({
//         ...order,
//         id: noteDatas.length + 1,
//     });
//     return {
//         ...order,
//         id: noteDatas.length, 
//     } as Order;
// }

// export const updateOrderByIdService = async (id: number, order: Order) => {
//     const index = noteDatas.findIndex((o) => o.id === id);
//     if (index !== -1) {
//         noteDatas[index] = order;
//     }
//     return order;
// }

// export const deleteOrderByIdService = async (id: number) => {
//     const index = noteDatas.findIndex((o) => o.id === id);
//     if (index !== -1) {
//         noteDatas.splice(index, 1);
//     }
//     return id;
// }

// export const deleteAllOrdersService = async () => {
//     noteDatas.length = 0;
//     return noteDatas;
// }