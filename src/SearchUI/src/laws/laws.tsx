import {Loader} from "../shared/ui/loader/loader.tsx";
import {useMutation, useQuery} from "react-query";
import {fetchLaws, searchLaws} from "./laws-service.ts";
import {ErrorPage} from "../shared/ui/error/error-page.tsx";
import {DocumentCard} from "../shared/ui/file-card/document-card.tsx";
import lawIcon from '../assets/law.png';
import {FileTypes} from "../shared/types/file-types.ts";
import {highlightLightShort} from "../shared/utils/search.ts";
import useLawStore from "./store/laws-store.ts";
import {useNavigate} from "react-router-dom";
import {useForm} from "react-hook-form";
import {downloadFile, uploadFile} from "../shared/services/content-service.ts";
import {AxiosError} from "axios";
import {toastError, toastSuccess} from "../shared/toast/toast.ts";
import {FaCloudUploadAlt} from "react-icons/fa";

export const Laws = () => {
    const {setLaw} = useLawStore();
    const navigate = useNavigate();
    const {register, handleSubmit, watch} = useForm();
    const search = watch('search');
    const allLawsQuery = useQuery(['laws', search], () => search ? searchLaws(search) : fetchLaws(), {
        keepPreviousData: true
    });

    const downloadMutation = useMutation(downloadFile, {
        onError: (error: AxiosError) => {
            console.log(error)
            toastError('Error while downloading file')
        },
        onSuccess: () => {
            toastSuccess('You are successfully downloaded file!')
        }
    });

    const {mutate, isLoading} = useMutation(uploadFile, {
        onSuccess: () => {
            toastSuccess('File uploaded successfully!');
        },
        onError: (error) => {
            console.error('Upload error:', error);
            toastError('Error while uploading file')
        },
    });
    const handleFileChange = (event: any) => {
        const file = event.target.files[0];
        if (file) {
            mutate({file: file, type: FileTypes.Law});
        }
    };
    const onSubmit = async () => {
        await allLawsQuery.refetch();
    };
    if (allLawsQuery.isLoading) return <Loader/>
    if (allLawsQuery.isError) return <ErrorPage/>
    return (
        <div className={'padding-base'}>
            <div className={'flex-space'}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <input className={'input-base'}
                           {...register('search')} placeholder="Search laws"/>
                </form>
                <div className={'btn-primary'}>
                    Upload New
                    <input type="file" onChange={handleFileChange} disabled={isLoading}/>
                    <FaCloudUploadAlt size={24}/>
                </div>
            </div>
            <div className={'grid-3-c'}>
                {
                    allLawsQuery.data?.map(law =>
                        <DocumentCard fileName={law.fileName}
                                      fileType={FileTypes.Law}
                                      imageRef={lawIcon}
                                      key={law.id}
                                      shortHighlight={highlightLightShort(law.highlight)}
                                      onDownloadClick={() => {
                                      downloadMutation.mutate({
                                          filename: law.fileName,
                                          type: FileTypes.Law
                                      })
                                  }}
                                      onViewClick={() => {
                                      setLaw(law);
                                      navigate('/laws/item')
                                  }}
                        />
                    )
                }
            </div>
        </div>
    );
};
