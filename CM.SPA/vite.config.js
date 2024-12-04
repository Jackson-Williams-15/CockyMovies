import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://class.usc547team.info:8080', // API base URL
        changeOrigin: true,
        secure: false,
      },
    },
  },
  optimizeDeps: {
    exclude: ['chunk-FLZU5K7W.js', 'chunk-HLUQYDOY.js'],
  },
});
