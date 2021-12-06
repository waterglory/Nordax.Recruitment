import * as React from "react";

export const useSurveyPageStyles = () => {
    const containerStyle: React.CSSProperties = {
        position: 'absolute',
        height: '100%',
        width: '100%',
    };

    const contentStyle: React.CSSProperties = {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignContent: 'center',
        height: '100%',
        margin: '0 auto',
    };

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
        containerStyle, contentStyle, buttonStyle, inputStyle, selectStyle
    };
};