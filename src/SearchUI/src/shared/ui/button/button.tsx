import "./button.scss"
import {ReactNode} from "react";
export interface ButtonProps {
    disabled?: boolean
    children: ReactNode,
    onClick?: () => void;
    type?: 'button' | 'submit' | 'reset';
}
export const Button = ({disabled = false,onClick,children, type = "button"} : ButtonProps) => {
    return (
        <>
        <button type={type}
                className="button"
                onClick={onClick}
                disabled={disabled}>{children}</button>
        </>
    );
};
