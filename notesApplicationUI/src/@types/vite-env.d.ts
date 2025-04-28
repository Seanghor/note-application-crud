/// <reference types="vite/client" />

interface ImportMetaEnv {
    VITE_API_URL: string;
    // Add other VITE_ variables you are using here
  }
  
  interface ImportMeta {
    readonly env: ImportMetaEnv;
  }
  