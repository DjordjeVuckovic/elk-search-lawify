import {httpSearchClient} from "../http-client/http-client.ts";
import {BasicSearch, ContractHit, GeoSearchRequest} from "./types/contract.ts";

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

export const geoSearch = async (geoSearch: GeoSearchRequest): Promise<ContractHit[]> => {
    try {
        const response =
            await httpSearchClient
                .get(`/api/v1/serbian-contracts/geospatial-search?lat=${geoSearch.lat}&lon=${geoSearch.lon}&radiusM=${geoSearch.radiusM}`);
        return response.data;
    }
    catch (error) {
        console.error(error);
        return []
    }
}