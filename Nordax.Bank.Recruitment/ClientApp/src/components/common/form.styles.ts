import * as React from "react";

export const useFormStyles = () => {
    const buttonStyle: React.CSSProperties = {
        width: "42%",
        marginLeft: "5px",
        marginRight: "5px",
    }

    const inputStyle: React.CSSProperties = {
        width: "59%",
        margin: "0 auto",
        minWidth: "230px"
    }

    const selectStyle: React.CSSProperties = {
        width: "65%",
        margin: "0 auto",
        minWidth: "280px",
    }

    return {
        buttonStyle, inputStyle, selectStyle
    };
};