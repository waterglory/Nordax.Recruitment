

export const useColors = () => {
    const red = "#f4134a";

    return { red }
}

export const useNavigations = () => {
    const navigationDivStyle: React.CSSProperties = {
        position: "absolute",
        bottom: "22px",
        left: "50%"
    };

    const navigationButtonStyle: React.CSSProperties = {
        position: "relative",
        left: "-50%",
        width: "125px"
    };

    return {
        navigationDivStyle,
        navigationButtonStyle
    };
}
