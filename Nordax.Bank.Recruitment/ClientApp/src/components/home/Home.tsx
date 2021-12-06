import * as React from "react";
import {Col, Container, Fade, Row} from 'reactstrap';
import {useHomeStyles, useSocialMediaStyles} from "./home.styles";
import {Logo} from "../common/logo/Logo";
import {Button} from "../common/button/Button";

export const Home = () => {
    const { buttonStyle } = useHomeStyles();
    const { iconStyle, linkStyle } = useSocialMediaStyles();

    return (
        <Fade style={{transition: 'opacity 0.25s linear'}}>
            <Container>
                <Row className="justify-content-md-center align-items-center" style={{ height: '90%' }}>
                    <Col md="5" className="text-center">
                        <Logo />

                        <p>A Bank of banks  </p>
                        <div style={{ marginTop: '25px' }}>
                            <Button style={buttonStyle} onClick={() => window.location.href = "/loan-application"}>Try Nordax</Button>
                            <Button style={buttonStyle} onClick={() => window.location.href = "/signup"}>Subscribe</Button>
                        </div>
                        <div style={{ margin: 'auto', marginTop: '25px', width: '80%' }}>
                            <a href={"https://twitter.com/nordaxb"} style={linkStyle}><img src={"icons/twitter.svg"} alt={"twitter"} style={iconStyle}/></a>
                            <a href={"https://medium.com/@nordax"} style={linkStyle}><img src={"icons/medium.svg"} alt={"medium"} style={iconStyle}/></a>
                            <a href={"https://www.linkedin.com/company/nordax-finans"} style={linkStyle}><img src={"icons/linkedin.svg"} alt={"linkedin"} style={iconStyle}/></a>
                        </div>
                    </Col>
                </Row>
            </Container>
        </Fade>
    );
};