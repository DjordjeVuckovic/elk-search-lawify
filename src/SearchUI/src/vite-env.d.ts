/// <reference types="vite/client" />
interface ImportMetaEnv {
    readonly VITE_SEARCH_BASE_URL: string
    readonly VITE_CONTENT_BASE_URL: string
    readonly VITE_GOOGLE_MAP_KEY: string
    // more env variables...
}

interface ImportMeta {
    readonly env: ImportMetaEnv
}