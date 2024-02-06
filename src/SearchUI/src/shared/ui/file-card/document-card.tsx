import {Button} from "../button/button.tsx";

export type FileCardProps = {
    fileName: string;
    fileType: string;
    imageRef: string;
    shortHighlight: string;
    onDownloadClick: () => void;
    onViewClick: () => void;
}
import './document-card.scss';
import {FaArrowDown, FaStreetView} from "react-icons/fa";
export const DocumentCard = (props : FileCardProps) => {
    console.log(props)
    return (
        <div className={'file-card padding-base mg-t-1'}>
            <img src={props.imageRef} alt={'i'}/>
            <h3>{props.fileName}</h3>
            <h4>{props.fileType}</h4>
            <div dangerouslySetInnerHTML={{ __html: props.shortHighlight }} />
            <div className={'flex-start g-5'}>
                <Button onClick={props.onDownloadClick}>Download <FaArrowDown/> </Button>
                <Button onClick={props.onViewClick}>View <FaStreetView/> </Button>
            </div>
        </div>
    );
};
