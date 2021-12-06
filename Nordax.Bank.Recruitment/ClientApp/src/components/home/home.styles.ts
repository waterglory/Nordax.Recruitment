import * as React from "react";

export const useHomeStyles = () => {

    const buttonStyle: React.CSSProperties = {
        width: '42%',
        marginLeft: '5px',
        marginRight: '5px',
    };

    return {
        buttonStyle
    };
};

export const useSocialMediaStyles = () => {
    const iconStyle: React.CSSProperties = {
        width: '12%',
    };
    const linkStyle: React.CSSProperties = {
        marginLeft: "4%",
        marginRight: "4%",
    };

    return {
        iconStyle, linkStyle
    };
};