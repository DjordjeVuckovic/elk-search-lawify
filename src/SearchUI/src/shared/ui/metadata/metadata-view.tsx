import {Metadata} from "../../types/metadata.ts";

export const MetadataView = (props: Metadata) => {
    return (
        <div className={'flex-col-start'}>
            <span className={'primary-text'}>Metadata: </span>
            <span> <span className={'primary-text-small'}>FileName: </span> {props.fileName}</span>
            <span> <span className={'primary-text-small'}>Author: </span> {props.author}</span>
            <span> <span className={'primary-text-small'}>CreatedAt: </span> {props.createdAt?.toString()}</span>
            <span> <span className={'primary-text-small'}>Title: </span> {props.title ?? "Missing"}</span>
        </div>
    );
};
