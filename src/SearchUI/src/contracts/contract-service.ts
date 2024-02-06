import {httpSearchClient} from "../http-client/http-client.ts";
import {BasicSearch, ContractHit} from "./types/contract.ts";

export const fetchContracts = async (): Promise<ContractHit[]> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-contracts`);
    return response.data;
}

export const basicSearchContracts = async (searchInput: BasicSearch): Promise<ContractHit[]> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-contracts/basic-search?field=${searchInput.searchField}&value=${searchInput.search}&isPhrase=${searchInput.isPhrase}`);
    return response.data;
}

export const boolSearchContracts = async (searchInput: string): Promise<ContractHit[]> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-contracts/bool-search?query=${searchInput}`);
    return response.data;
}

export const geoSearch = async (id: string): Promise<ContractHit> => {
    const response =
        await httpSearchClient.get(`/api/v1/serbian-contracts/${id}`);
    return response.data;
}