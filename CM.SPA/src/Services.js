import axios from 'axios';

const api = axios.create({
  baseURL: '/api',  // automatically proxy through Vite
});

export default api;
