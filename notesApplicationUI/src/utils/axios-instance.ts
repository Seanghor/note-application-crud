
import axios from "axios";
import { toast } from "vue-sonner";

// };

const instance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 36000,
  headers: {
    "X-Custom-Header": "foobar",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS",
    "Access-Control-Allow-Headers":
      "Content-Type, Authorization, Content-Length, X-Requested-With",
    "Content-Type": "application/json",
  },
});

// Response interceptor for API calls
instance.interceptors.response.use(
  (response) => {
    return response;
  },
  async function (error) {
    try {
      if(error.code === "ERR_NETWORK") {
        toast.error("NETWORK ERROR")
      }
      if (error.code === "ECONNABORTED") {
        // Handle timeout
        alert("Request timeout. Please try again");
        window.location.reload();
      }  
      if (error.response.status >= 400) {
        toast.error(error.response.data.error, {
          description: error.response.data.message,
        });
      }
    } catch (e) {
      console.log("error", e);
      // window.location.reload();
    }
    return Promise.reject(error);
  },
);

export default instance;
