import React from 'react';
import { Col, Form, FormFeedback, FormGroup, Input, Label, Row } from "reactstrap";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import LoanApplicationEvents from './loanApplicationEvents';

export interface ApplicantData {
    applicantFirstName: string,
    applicantSurname: string,
    applicantPhoneNo: string,
    applicantEmail: string,
    applicantAddress: string,
    applicantIncomeLevel: string,
    applicantIsPoliticallyExposed: boolean,
}

const Applicant = (props: React.PropsWithChildren<{
    data: ApplicantData,
    events: LoanApplicationEvents
}>) => {
    const formIsValid = () =>
        props.data.applicantFirstName
        && props.data.applicantSurname
        && validatePhone()
        && validateEmail();

    const incomeLevelOptions: { value: string, text: string }[] = [
        { value: "35000", text: "0 - 35,000 kr" },
        { value: "70000", text: "35,001 - 70,000 kr" },
        { value: "120000", text: "70,001 - 120,000 kr" },
        { value: "-1", text: "> 120,000 kr" }
    ];

    const validatePhone = () =>
        /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im.test(props.data.applicantPhoneNo);

    const validateEmail = () =>
        /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(props.data.applicantEmail);

    const { labelStyle, buttonStyle } = useFormStyles();

    return (
        <Form>
            <h4>Applicant</h4>
            <p className="nordax_subtitle">Help us know you better.</p>
            <Row>
                <Col sm={6}>
                    <FormGroup className="text-left">
                        <Label for="applicantFirstName" style={labelStyle}>First name</Label>
                        <Input type="text" name="applicantFirstName" invalid={!props.data.applicantFirstName}
                            value={props.data.applicantFirstName} onChange={props.events.onChange} />
                    </FormGroup>
                </Col>
                <Col sm={6}>
                    <FormGroup className="text-left">
                        <Label for="applicantSurname" style={labelStyle}>Surname</Label>
                        <Input type="text" name="applicantSurname" invalid={!props.data.applicantSurname}
                            value={props.data.applicantSurname} onChange={props.events.onChange} />
                    </FormGroup>
                </Col>
            </Row>
            <Row>
                <Col sm={6}>
                    <FormGroup className="text-left">
                        <Label for="applicantPhoneNo" style={labelStyle}>Phone no.</Label>
                        <Input type="text" name="applicantPhoneNo" invalid={!validatePhone()}
                            value={props.data.applicantPhoneNo} onChange={props.events.onChange} />
                        <FormFeedback>
                            Please provide a valid phone no.
                        </FormFeedback>
                    </FormGroup>
                </Col>
                <Col sm={6}>
                    <FormGroup className="text-left">
                        <Label for="applicantEmail" style={labelStyle}>Email</Label>
                        <Input type="text" name="applicantEmail" invalid={!validateEmail()}
                            value={props.data.applicantEmail} onChange={props.events.onChange} />
                        <FormFeedback>
                            Please provide a valid email address.
                        </FormFeedback>
                    </FormGroup>
                </Col>
            </Row>
            <FormGroup className="text-left">
                <Label for="applicantAddress" style={labelStyle}>Address</Label>
                <Input type="textarea" name="applicantAddress"
                    value={props.data.applicantAddress} onChange={props.events.onChange} />
            </FormGroup>
            <FormGroup className="text-left">
                <Label for="applicantIncomeLevel" style={labelStyle}>Monthly income level</Label>
                {
                    incomeLevelOptions.map((opt, index) =>
                        <FormGroup className="text-left" check key={opt.value}>
                            <Input type="radio" name="applicantIncomeLevel" checked={props.data.applicantIncomeLevel === opt.value}
                                onChange={props.events.onChange} value={opt.value} />
                            <Label style={labelStyle} check>{opt.text}</Label>
                        </FormGroup>
                    )
                }
            </FormGroup>
            <FormGroup className="text-left" check>
                <Input type="checkbox" name="applicantIsPoliticallyExposed"
                    onChange={props.events.onChange} checked={props.data.applicantIsPoliticallyExposed} />
                <Label for="applicantIsPoliticallyExposed" style={labelStyle} check>I am a politically exposed person</Label>
            </FormGroup>
            <Button style={buttonStyle} onClick={props.events.next} className="mt-3"
                disabled={!formIsValid()} >{props.events.nextForm}</Button>
        </Form >
    );
}
export default Applicant;