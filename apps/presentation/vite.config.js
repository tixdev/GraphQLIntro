import { resolve } from 'path';
import { defineConfig } from 'vite';

export default defineConfig({
  server: {
    host: true, // Listen on all addresses (0.0.0.0)
    port: 3000,
    allowedHosts: true, // Allow ngrok or other tunnel hostnames
    proxy: {
      '/graphql': 'http://localhost:5095',
    },
  },
  build: {
    rollupOptions: {
      input: {
        main: resolve(__dirname, 'index.html'),
        remote: resolve(__dirname, 'remote.html'),
      },
    },
  },
});
