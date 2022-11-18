import React, { useState } from 'react';
import { Button } from '../common/button/Button';
import '../common/button/Button.css'
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import { Input, Form, FormGroup, Label, Col } from "reactstrap";
import { useNavigations } from "../common/common.styles";

export interface LoanData {
    loanAmount: number;
    loanBindingPeriod: number;
    loanInterestRate: number
}

const { inputStyle, labelStyle } = useFormStyles();

const Loan = (props: React.PropsWithChildren<{
    data: LoanData,
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void,
    nextForm: string,
    next: () => void
}>) => {
    const [interestRate, setInterestRate] = useState(0);

    const { buttonStyle } = useFormStyles();

    return (
        <Form>
            <h4>Loan</h4>
            <p className="nordax_subtitle">Fill in the amount and we will calculate it for you.</p>
            <FormGroup row>
                <Label for="loanAmount" sm={4} style={labelStyle}>Amount</Label>
                <Col sm={8}>
                    <Input style={inputStyle} type="number" name="loanAmount" placeholder="Tap to start writing.."
                        value={props.data.loanAmount} onChange={props.onChange} />
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label for="loanBindingPeriod" sm={4} style={labelStyle}>Binding period</Label>
                <Col sm={8}>
                    <Input style={inputStyle} type="select" name="loanBindingPeriod"
                        value={props.data.loanBindingPeriod} onChange={props.onChange}>
                        <option value="0">---</option>
                        <option value="3">3 months (1%)</option>
                        <option value="6">6 months (1.5%)</option>
                    </Input>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label for="loanInterestRate" sm={4} style={labelStyle}>Interest rate</Label>
                <Col sm={8}>
                    <Input style={inputStyle} value={`${interestRate * 100} %`} disabled />
                </Col>
            </FormGroup>
            {props.data.loanAmount > 0 && props.data.loanBindingPeriod > 0 &&
                <Button style={buttonStyle} onClick={props.next}>{props.nextForm}</Button>}
        </Form>
    );
}

export default Loan;