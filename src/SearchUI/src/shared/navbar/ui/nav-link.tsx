import {Link} from "react-router-dom";
import "./nav-link.scss"
import {ReactNode} from "react";

export const NavLink = ({children,url}: {children: ReactNode,url: string}) => {
    return (
        <Link className={'link button-as-link'} to={url}>{children}</Link>
    );
};
