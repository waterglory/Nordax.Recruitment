import React from "react";
import './Button.css';

export const Button = (props: React.PropsWithChildren<any>) => {

    return <button className={`nordax_button red`} {...props}>{props.children}</button>;
}