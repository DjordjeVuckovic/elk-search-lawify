import {Metadata} from "../../shared/types/metadata.ts";

export type ContractHit = {
  governmentName?: string | null;
  governmentLevel?: string | null;
  agencySignatureName?: string | null;
  agencySignatureSurname?: string | null;
  agencySignatureFullName?: string | null;
  highlight?: string | null;
  content?: string | null;
  id: string;
  fileName: string;
  metadata: Metadata;
  governmentSignatureName?: string | null;
  governmentSignatureSurname?: string | null;
  governmentSignatureFullName?: string | null;
  governmentPhone?: string | null;
  governmentEmail?: string | null;
  governmentAddress?: string | null;
  geoLocation?: GeoLocation;
};

export type BasicSearch = {
    search: string;
    searchField: string;
    isPhrase: boolean;
}

export type BoolSearch = {
    boolSearch: string;
}

export type GeoSearchRequest = {
    lat: number;
    lon: number;
    radiusM: number;
}

export type GeoLocation = {
    lat: number;
    lng: number;
}