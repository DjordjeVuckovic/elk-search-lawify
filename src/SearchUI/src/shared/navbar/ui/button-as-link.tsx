import './nav-link.scss'
import {ReactNode} from "react";
export const ButtonAsLink = ({children, onClick} : { children: ReactNode, onClick: () => void}) => {
    return (
        <button className={'link button-as-link'}
                onClick={() => onClick()}>
            {children}
        </button>
    );
};
