import React, { useState } from 'react';
import { Col, Fade, Form, FormGroup, Input, Label } from "reactstrap";
import { nameof } from "../../common/classUtil";
import { IHttpClient } from '../../common/httpClient';
import BindingPeriod from "../../models/bindingPeriod";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import LoanApplicationEvents from './loanApplicationEvents';

export interface LoanData {
    loanAmount: number;
    loanPaymentPeriod: number;
    loanBindingPeriod: number;
    loanInterestRate: number;
}

const Loan = (props: React.PropsWithChildren<{
    data: LoanData,
    events: LoanApplicationEvents,
    apiClient: IHttpClient
}>) => {
    const [bindingPeriodOptions, setBindingPeriodOptions] = useState<Array<BindingPeriod>>([]);
    const [loadError, setLoadError] = useState<null | string>(null);
    const [simulation, setSimulation] = useState<{ firstMonth: number, nextYear: number }>();

    const labelCol = 5;

    React.useEffect(() => {
        props.apiClient.get<Array<BindingPeriod>>('api/options/binding-periods')
            .then((res) => {
                setBindingPeriodOptions(res);
            }).catch(e => {
                setLoadError(e.status + " " + e.statusText);
                e.json().then((json: any) => {
                    setLoadError(e.status + " " + e.statusText + ": " + json);
                });
            });
    }, []);

    const calculateSimulation = (data: LoanData) => {
        // Assumed that 360 yearly days are used

        const principal = data.loanAmount / data.loanPaymentPeriod;
        const dailyRate = data.loanInterestRate / 36000;
        const monthlyRate = Math.pow((1 + dailyRate), 30);
        const firstMonthInterest = monthlyRate * data.loanAmount - data.loanAmount;

        const nextYearLeftAmount = data.loanAmount - 12 * principal;
        const nextYearInterest = monthlyRate * nextYearLeftAmount - nextYearLeftAmount;

        setSimulation({
            firstMonth: Math.floor(principal + firstMonthInterest),
            nextYear: Math.floor(principal + nextYearInterest)
        });
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        calculateSimulation({ ...props.data, [e.target.name]: Number.parseFloat(e.target.value) });
        props.events.onChange(e);
    }

    const handleBindingPeriodChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const bindingPeriod = Number.parseFloat(e.target.value);
        let matchedOption = bindingPeriodOptions.find(o => o.length === bindingPeriod);
        let interestRate = matchedOption ? matchedOption.interestRate : 0;
        props.events.onMultiUpdates(
            [nameof<LoanData>("loanInterestRate"), interestRate],
            [nameof<LoanData>("loanBindingPeriod"), bindingPeriod]);

        const newLoan = { ...props.data };
        newLoan.loanBindingPeriod = bindingPeriod;
        newLoan.loanInterestRate = interestRate;
        calculateSimulation(newLoan);
    }

    const { inputStyle, labelStyle, buttonStyle } = useFormStyles();

    return loadError ? (
        <div>
            <p>Something went wrong with during form loading.</p>
            <p style={{ color: "red" }}>{loadError}</p>
        </div>
    ) : (
        <Form>
            <h4>Loan</h4>
            <p className="nordax_subtitle">Fill in the amount and we will calculate it for you.</p>
            <FormGroup row>
                <Label for="loanAmount" sm={labelCol} style={labelStyle}>Amount</Label>
                <Col sm={12 - labelCol}>
                    <Input style={inputStyle} type="number" name="loanAmount"
                        value={props.data.loanAmount} onChange={handleChange} />
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label for="loanPaymentPeriod" sm={labelCol} style={labelStyle}>Payment period (mo.)</Label>
                <Col sm={12 - labelCol}>
                    <Input style={inputStyle} type="number" name="loanPaymentPeriod"
                        value={props.data.loanPaymentPeriod} onChange={handleChange} />
                </Col>
            </FormGroup>
            {bindingPeriodOptions.length > 0 &&
                <FormGroup row>
                    <Label for="loanBindingPeriod" sm={labelCol} style={labelStyle}>Binding period (mo.)</Label>
                    <Col sm={12 - labelCol}>
                        <Input style={inputStyle} type="select" name="loanBindingPeriod"
                            value={props.data.loanBindingPeriod} onChange={handleBindingPeriodChange}>
                            <option value="0">---</option>
                            {bindingPeriodOptions.map(o =>
                                <option key={o.length} value={o.length}>
                                    {`${o.length} month${o.length > 1 ? 's' : ''} (${o.interestRate}%)`}
                                </option>)}
                        </Input>
                    </Col>
                </FormGroup>}
            <FormGroup row>
                <Label for="loanInterestRate" sm={labelCol} style={labelStyle}>Interest rate</Label>
                <Col sm={12 - labelCol}>
                    <Input style={inputStyle} value={`${props.data.loanInterestRate} %`} disabled />
                </Col>
            </FormGroup>
            {props.data.loanAmount > 0 && props.data.loanPaymentPeriod > 0 && props.data.loanBindingPeriod > 0 &&
                <Fade style={{ transition: 'opacity 0.25s linear' }}>
                    <FormGroup row>
                        <Label sm={labelCol} style={labelStyle}>First payment</Label>
                        <Col sm={12 - labelCol}>
                            <Input style={inputStyle} value={simulation?.firstMonth} disabled />
                        </Col>
                    </FormGroup>
                    {props.data.loanPaymentPeriod > 12 &&

                        <Fade style={{ transition: 'opacity 0.25s linear' }}>
                            <FormGroup row>
                                <Label sm={labelCol} style={labelStyle}>Next year payment</Label>
                                <Col sm={12 - labelCol}>
                                    <Input style={inputStyle} value={simulation?.nextYear} disabled />
                                </Col>
                            </FormGroup>
                        </Fade>}
                    <Button style={buttonStyle} onClick={props.events.next}>{props.events.nextForm}</Button>
                </Fade>}
        </Form>
    );
}

export default Loan;