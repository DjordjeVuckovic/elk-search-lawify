import {Link} from "react-router-dom";

export const NavButton = ({title,link}: {title: string,link: string}) => {
    return (
        <button className={'btn-primary'}>
            <Link className={'button-link'} to={link}>{title}</Link>
        </button>
    );
};
