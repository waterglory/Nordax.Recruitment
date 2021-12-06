import * as React from "react";
import {Container} from 'reactstrap';
import {Footer} from "./Footer";

export const Layout = (props: any) => {
    return (
        <div className="main">
            <Container>
                { props.children }
                <Footer />
            </Container>
        </div>
    );
}