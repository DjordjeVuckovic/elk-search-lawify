import {Law} from "./types/law.ts";
import {httpSearchClient} from "../http-client/http-client.ts";

export const fetchLaws = async (): Promise<Law[]> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-laws`);
    return response.data;
}

export const searchLaws = async (searchInput: string): Promise<Law[]> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-laws/search?content=${searchInput}`);
    return response.data;
}