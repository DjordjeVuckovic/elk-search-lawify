import {MetadataView} from "../shared/ui/metadata/metadata-view.tsx";
import {useNavigate} from "react-router-dom";
import {highlightColored} from "../shared/utils/search.ts";
import {Button} from "../shared/ui/button/button.tsx";
import {IoIosArrowBack} from "react-icons/io";
import useContractStore from "./store/contracts-store.ts";

export const ContractPage = () => {
    const { contract} = useContractStore();
    const navigate = useNavigate();
    return (
        <div
            className={'padding-base flex-col-start main-text'}
        >
            <div className={'flex-space g-1'}>
                <h1 className={'primary-text'}>Law View</h1>
                <Button onClick={() => navigate('/contracts')}>Back <IoIosArrowBack/></Button>
            </div>
            <h1><span className={'primary-text'}>FileName: </span> {contract?.fileName}</h1>
            <span className={'primary-text'}>Highlight: </span>
            <h2 dangerouslySetInnerHTML={{__html: highlightColored(contract.highlight!) ?? ""}}/>
            <span className={'primary-text'}>Content: </span>
            <p>{contract?.content}</p>
            <p><span className={'primary-text-small'}>Government Name: </span>{contract?.governmentName}</p>
            <p><span className={'primary-text-small'}>Government Level: </span>{contract?.governmentLevel}</p>
            <p><span className={'primary-text-small'}>Agency Signature Name: </span>{contract?.agencySignatureName}</p>
            <p><span className={'primary-text-small'}>Agency Signature Surname: </span>{contract?.agencySignatureSurname}</p>
            <p><span className={'primary-text-small'}>Agency Signature Full Name: </span>{contract?.agencySignatureFullName}
            </p>
            <p><span className={'primary-text-small'}>Government Signature Name: </span>{contract?.governmentSignatureName}
            </p>
            <p><span
                className={'primary-text-small'}>Government Signature Surname: </span>{contract?.governmentSignatureSurname}
            </p>
            <p><span
                className={'primary-text-small'}>Government Signature Full Name: </span>{contract?.governmentSignatureFullName}
            </p>
            <p><span className={'primary-text-small'}>Government Phone: </span>{contract?.governmentPhone}</p>
            <p><span className={'primary-text-small'}>Government Email: </span>{contract?.governmentEmail}</p>
            <p><span className={'primary-text-small'}>Government Address: </span>{contract?.governmentAddress}</p>
            <MetadataView
                fileName={contract.metadata.fileName}
                author={contract.metadata.author}
                createdAt={contract.metadata.createdAt}
                title={contract.metadata.title}
            />
        </div>
    );
};
