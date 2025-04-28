import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Signup from './pages/SignupPage.vue';  
import Signin from './pages/SigninPage.vue';   
import Notes from './pages/NotePage.vue';  
import OrderPage from './pages/OrderPage.vue';

// Define your routes
const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/signin',  // Redirect to sign in or sign up page
  },
  {
    path: '/signup',
    name: 'Signup',
    component: Signup,
  },
  {
    path: '/signin',
    name: 'Signin',
    component: Signin,
  },
  {
    path: '/notes',
    name: 'Notes',
    component: Notes,
    meta: {
      requiresAuth: true, 
    },
  },
  {
    path: '/orders',
    name: 'Orders',
    component: OrderPage,
    meta: {
      requiresAuth: true, 
    },
  },
];

// --> instance
const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem('session');  

  if (to.meta.requiresAuth && !isAuthenticated) {
    next({ name: 'Signin' });
  } else {
    next();  
  }
});

export default router;
