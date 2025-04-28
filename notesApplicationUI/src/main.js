import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { toast } from 'vue-sonner';

createApp(App)
    .component('toast', toast)
    .use(router)
    .mount('#app')
