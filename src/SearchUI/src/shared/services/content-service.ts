import {httpContentClient} from "../../http-client/http-client.ts";
import {FileTypes} from "../types/file-types.ts";
type DownloadFileArgs = {
    filename: string;
    type: FileTypes;
};

type UploadFileArgs = {
    file: Blob;
    type: FileTypes;
};
export const downloadFile = async ({filename, type} : DownloadFileArgs) => {
    try {
        const response = await httpContentClient
            .get(`/api/v1/contents/${filename}/${type}`, {
            responseType: 'blob', // Important for handling binary files
        });

        // Create a URL for the blob
        const fileURL = window.URL.createObjectURL(new Blob([response.data]));

        // Create an anchor element and trigger the download
        const link = document.createElement('a');
        link.href = fileURL;
        link.setAttribute('download', filename); // Set the file name
        document.body.appendChild(link);
        link.click();

        link.remove(); // Clean up
    } catch (error) {
        console.error('Download error:', error);
        throw error; // Rethrow to handle it in the mutation error handling
    }
}

export const uploadFile = async ({file, type }: UploadFileArgs) => {
    const formData = new FormData();
    formData.append('file', file);
    let url = '';
    switch (type) {
        case FileTypes.Law:
            url = '/api/v1/laws/upload';
            break;
        case FileTypes.Contract:
            url = '/api/v1/contracts/upload';
            break;
        default:
            throw new Error('Invalid file type');
    }

    const response = await httpContentClient.post(url, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
    return response.data;
}
