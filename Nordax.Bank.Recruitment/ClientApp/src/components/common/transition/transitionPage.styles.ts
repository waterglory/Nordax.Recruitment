import * as React from "react";

const useTransitionPageStyles = () => {
    const containerStyle: React.CSSProperties = {
        position: 'absolute',
        height: '100%',
        width: '100%',
    };

    return {
        containerStyle
    };
};

export default useTransitionPageStyles;