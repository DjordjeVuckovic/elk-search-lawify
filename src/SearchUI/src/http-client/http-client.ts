import axios from 'axios';
const S_BASE_URL = import.meta.env.VITE_SEARCH_BASE_URL
const C_BASE_URL = import.meta.env.VITE_CONTENT_BASE_URL
export const httpContentClient = axios.create({
    baseURL: C_BASE_URL,
});

export const httpSearchClient = axios.create({
    baseURL: S_BASE_URL,
});