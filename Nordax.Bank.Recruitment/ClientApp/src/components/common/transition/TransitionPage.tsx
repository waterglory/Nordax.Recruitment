import * as React from "react";
import useTransitionPageStyles from "./transitionPage.styles";
import './transitionAnimation.css';
import {Col, Row} from "reactstrap";

export const TransitionPage = (props: React.PropsWithChildren<{index: number, currentIndex: number}>) => {
    const { containerStyle } = useTransitionPageStyles();

    const relevantStyle = props.currentIndex < props.index ? "hiddenBotStyle" : props.currentIndex > props.index ? "hiddenTopStyle" : "visibleStyle";

    return (
        <div className={relevantStyle} style={containerStyle}>
            <div style={{position: 'relative', width: '100%', height: '100%'}}>
                <Row className="justify-content-md-center align-items-center" style={{height: "100%", padding: "0 15px"}}>
                    <Col style={{margin: '0 auto'}} md="5" className="text-center">
                        {props.children}
                    </Col>
                </Row>
            </div>
        </div>
    );
};
