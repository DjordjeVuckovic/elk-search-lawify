import {ReactNode} from "react";
import {Outlet} from "react-router-dom";

export const Body = ({children} : { children: ReactNode }) => {
    return (
        <div style={{margin: '0 auto'}}>
            {children}
            <Outlet/>
        </div>
    )
}