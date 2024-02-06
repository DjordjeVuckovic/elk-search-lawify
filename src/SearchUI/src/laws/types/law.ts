import {Metadata} from "../../shared/types/metadata.ts";

export type Law = {
    id: string;
    content: string;
    highlight?: string;
    fileName: string;
    metadata: Metadata;
}