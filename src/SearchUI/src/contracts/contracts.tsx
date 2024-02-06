import useContractStore from "./store/contracts-store.ts";
import {useForm} from "react-hook-form";
import {useMutation, useQuery} from "react-query";
import {downloadFile, uploadFile} from "../shared/services/content-service.ts";
import {AxiosError} from "axios";
import {toastError, toastSuccess} from "../shared/toast/toast.ts";
import {FileTypes} from "../shared/types/file-types.ts";
import {Loader} from "../shared/ui/loader/loader.tsx";
import {ErrorPage} from "../shared/ui/error/error-page.tsx";
import {FaCloudUploadAlt, FaSearch} from "react-icons/fa";
import {DocumentCard} from "../shared/ui/file-card/document-card.tsx";
import contractIcon from "../assets/contract.png";
import {highlightLightShort} from "../shared/utils/search.ts";
import {basicSearchContracts, boolSearchContracts, fetchContracts} from "./contract-service.ts";
import {useNavigate} from "react-router-dom";
import {Button} from "../shared/ui/button/button.tsx";
import {useState} from "react";
import {BasicSearch, BoolSearch} from "./types/contract.ts";

export const Contracts = () => {
    const {setContract} = useContractStore();
    const navigate = useNavigate();
    const {register, handleSubmit} = useForm<BasicSearch>();
    const {register: registerBool, handleSubmit: handleSubmitBool} = useForm<BoolSearch>();

    const [searchParams, setSearchParams]
        = useState<BasicSearch>({search: '', searchField: '', isPhrase: false});
    const [searchBoolParams, setSearchBoolParams] = useState('');
    const [searchType, setSearchType] = useState('initial');

    const contractsInitialQuery =
        useQuery(['contracts', 'initial'], () => fetchContracts(), {
            enabled: searchType === 'initial',
        });

    const basicSearchQuery = useQuery(['contracts', 'firstSearch', searchParams], () => basicSearchContracts(searchParams), {
        enabled: searchType === 'basicSearch',
    });

    const boolSearchQuery = useQuery(['contracts', 'boolSearch', searchParams], () => boolSearchContracts(searchBoolParams), {
        enabled: searchType === 'boolSearch',
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
            mutate({file: file, type: FileTypes.Contract});
        }
    };
    const onBasicSearchSubmit = (data: BasicSearch) => {
        setSearchParams(data);
        setSearchType('basicSearch');
    };

    const onBoolSearchSubmit = async (data: BoolSearch) => {
        setSearchBoolParams(data.boolSearch)
        setSearchType('boolSearch');
    };
    if (contractsInitialQuery.isLoading || basicSearchQuery.isLoading || boolSearchQuery.isLoading) return <Loader/>
    if (contractsInitialQuery.isError || boolSearchQuery.isError || basicSearchQuery.isError) return <ErrorPage/>
    const contracts = (searchType === 'boolSearch' ? boolSearchQuery.data :
        searchType === 'basicSearch' ? basicSearchQuery.data :
            contractsInitialQuery.data) ?? [];
    return (
        <div className={'padding-base'}>
            <div className={'flex-col-start g-5'}>
                    <form className={'flex-start g-5 input-group'} onSubmit={handleSubmit(onBasicSearchSubmit)}>
                        <input className={'input-base'}
                               {...register('search')}
                               placeholder="Search contracts"/>
                        <select
                            {...register('searchField')}
                            className={'select-base'}>
                            <option value="content">Content</option>
                            <option value="governmentName">Government Name</option>
                            <option value="governmentLevel">Government Level</option>
                            <option value="governmentSignatureName">Government Signature Name</option>
                            <option value="governmentSignatureSurname">Government Signature Surname</option>
                            <option value="agencySignatureFullName">Government Signature FullName</option>
                            <option value="agencySignatureName">Agency Signature Name</option>
                            <option value="agencySignatureSurname">Agency Signature Surname</option>
                            <option value="agencySignatureFullName">Agency Signature FullName</option>
                            <option value="governmentPhone">Government Phone</option>
                            <option value="governmentEmail">Government Email</option>
                            <option value="governmentAddress">Government Address</option>
                        </select>
                        <label htmlFor={'phrase'} className={'main-text'}>Is Phrase?</label>
                        <input id={'phrase'} {...register('isPhrase')} type={"checkbox"}/>
                        <Button type={'submit'}>Search <FaSearch/></Button>
                    </form>
                <div className={'flex-start g-1'} onSubmit={handleSubmitBool(onBoolSearchSubmit)}>
                    <form className={'flex-start g-5 input-group'}>
                        <input className={'input-large'}
                               {...registerBool('boolSearch')}
                               placeholder="Bool Search contracts"/>
                        <Button type={'submit'}>Search Bool <FaSearch/></Button>
                    </form>
                    <div className={'btn-primary'}>
                        Upload New
                        <input type="file" onChange={handleFileChange} disabled={isLoading}/>
                        <FaCloudUploadAlt size={24}/>
                    </div>
                </div>
            </div>
            <div className={'grid-3-c'}>
                {
                    contracts.map(contract =>
                        <DocumentCard fileName={contract.fileName}
                                      fileType={FileTypes.Contract}
                                      imageRef={contractIcon}
                                      key={contract.id}
                                      shortHighlight={highlightLightShort(contract.highlight!)}
                                      onDownloadClick={() => {
                                          downloadMutation.mutate({
                                              filename: contract.fileName,
                                              type: FileTypes.Law
                                          })
                                      }}
                                      onViewClick={() => {
                                          setContract(contract);
                                          navigate('/contracts/item')
                                      }}
                        />
                    )
                }
            </div>
        </div>
    );
};
