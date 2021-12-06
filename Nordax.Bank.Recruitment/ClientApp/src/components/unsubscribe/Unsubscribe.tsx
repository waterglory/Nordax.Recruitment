import {RouteComponentProps} from "react-router";
import {Col, Container, Fade, Row} from "reactstrap";
import {Logo} from "../common/logo/Logo";
import {Button} from "../common/button/Button";
import * as React from "react";
import {useState} from "react";
import {useHomeStyles, useSocialMediaStyles} from "../home/home.styles";
import {WebApiClient} from "../../common/webApiClient";

export const Unsubscribe = (props: RouteComponentProps<{userId: string}>) => {
    const { buttonStyle } = useHomeStyles();
    const [unsubscribeMessage, setUnsubscribeMessage] = useState<string | null>(null);
    const [submitError, setSubmitError] = useState<string | null>(null);
    const { iconStyle, linkStyle } = useSocialMediaStyles();
    const apiClient = WebApiClient();

    const onSubmit = () => {
        setUnsubscribeMessage(null);
        setSubmitError(null);
        apiClient.delete('api/subscriber/' + props.match.params.userId)
            .then((res) => {
                setUnsubscribeMessage("Unsubscribed successfully")
            }).catch(e => {
                setSubmitError(e.status + " " + e.statusText)
                e.json().then((json: any) => {
                    setSubmitError(e.status + " " + e.statusText + ": "  + json);
                })
            });
    };

    return (
        <Fade style={{transition: 'opacity 0.25s linear'}}>
            <Container>
                <Row className="justify-content-md-center align-items-center" style={{ height: '90%' }}>
                    <Col md="5" className="text-center">
                        <Logo />
                        <p>Unsubscribe from the Nordax mailing list.</p>
                        <p>If you are sure you want to unsubscribe, click the button below.</p>
                        <p style={{color: "red"}}>{submitError}</p>
                        {unsubscribeMessage ? <p>{unsubscribeMessage}</p> :
                            <div style={{ marginTop: '25px' }}>
                                <Button style={buttonStyle} onClick={() => onSubmit()}>Unsubscribe</Button>
                            </div>}
                        <div style={{ margin: 'auto', marginTop: '25px', width: '80%' }}>
                            <a href={"https://twitter.com/nordax"} style={linkStyle}><img src={"icons/twitter.svg"} alt={"twitter"} style={iconStyle}/></a>
                            <a href={"https://medium.com/@nordax"} style={linkStyle}><img src={"icons/medium.svg"} alt={"medium"} style={iconStyle}/></a>
                            <a href={"https://www.linkedin.com/company/nordax"} style={linkStyle}><img src={"icons/linkedin.svg"} alt={"linkedin"} style={iconStyle}/></a>
                        </div>
                    </Col>
                </Row>
            </Container>
        </Fade>
    )
}