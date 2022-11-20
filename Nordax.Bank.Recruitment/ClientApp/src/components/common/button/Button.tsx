import React from "react";
import './Button.css';

export const Button = (props: React.PropsWithChildren<any>) => {

    const { className, ...propsWithoutClass } = props;

    return <button className={`nordax_button red ${props.className}`} {...propsWithoutClass}>{props.children}</button>;
}