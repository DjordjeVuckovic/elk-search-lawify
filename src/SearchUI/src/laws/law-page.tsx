import useLawStore from "./store/laws-store.ts";
import './laws.scss';
import {MetadataView} from "../shared/ui/metadata/metadata-view.tsx";
import {useNavigate} from "react-router-dom";
import {highlightColored} from "../shared/utils/search.ts";
import {Button} from "../shared/ui/button/button.tsx";
import {IoIosArrowBack} from "react-icons/io";
export const LawPage = () => {
    const { law} = useLawStore();
    const navigate = useNavigate();
    return (
        <div
            className={'padding-base flex-col-start main-text'}
        >
            <div className={'flex-space g-1'}>
                <h1 className={'primary-text'}>Law View</h1>
                <Button onClick={() => navigate('/laws')}>Back <IoIosArrowBack /></Button>
            </div>
            <h1><span className={'primary-text'}>FileName: </span> {law?.fileName}</h1>
            <span className={'primary-text'}>Highlight: </span>
            <h2 dangerouslySetInnerHTML={{__html: highlightColored(law.highlight!) ?? ""}}/>
            <span className={'primary-text'}>Content: </span>
            <p>{law?.content}</p>
            <MetadataView
                fileName={law.metadata.fileName}
                author={law.metadata.author}
                createdAt={law.metadata.createdAt}
                title={law.metadata.title}
            />
        </div>
    );
};
