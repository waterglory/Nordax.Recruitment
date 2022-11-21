import React, { forwardRef, Ref, useImperativeHandle, useState } from 'react';
import { Form, FormGroup, Input, Label } from "reactstrap";
import { IHttpClient } from '../../common/httpClient';
import { RegisterLoanApplicationRequest } from "../../models/registerLoanApplicationRequest";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import Resettable from "./resettable";

const Submit = forwardRef((props: React.PropsWithChildren<{
    data: RegisterLoanApplicationRequest,
    apiClient: IHttpClient
}>, ref: Ref<Resettable>) => {
    const [agreeTerms, setAgreeTerms] = useState(false);
    const [correctInfo, setCorrectInfo] = useState(false);
    const [caseNo, setCaseNo] = useState<string | null>(null);
    const [existing, setExisting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const submitApplication = (e: Event) => {
        e.preventDefault();
        props.apiClient.post<{ caseNo: string }>("api/loan-application", props.data)
            .then((res) => {
                setCaseNo(res.caseNo);
                setExisting(false);
            })
            .catch((e) => {
                switch (e.status) {
                    case 409:
                        e.json().then((json: any) => {
                            setCaseNo(json.caseNo);
                            setExisting(true);
                        });
                        break;
                    default:
                        setError(e.status + " " + e.statusText);
                        e.json().then((json: any) => {
                            setError(e.status + " " + e.statusText + ": " + json);
                        });
                        break;
                }
            });
    }

    useImperativeHandle(ref, () => ({
        reset: () => {
            setAgreeTerms(false);
            setCorrectInfo(false);
            setCaseNo(null);
            setExisting(false);
            setError(null);
        }
    }));

    const { buttonStyle, labelStyle } = useFormStyles();

    const renderCaseNo = () => (
        <div>
            {
                existing ? (
                    <>
                        <p>You already have an ongoing application.</p>
                        <p className="nordax_subtitle">Please wait for decision or withdraw your application <br />before submitting a new one.</p>
                    </>
                ) : (
                    <>
                        <p>Application submitted successfully.</p>
                        <p className="nordax_subtitle">Please note the case no. and feel free to contact us if there are further questions.</p>
                    </>
                )
            }
            <h5>Case no.: {caseNo}</h5>
        </div>
    )

    const renderSubmitForm = () => (
        <Form>
            <FormGroup className="text-left" check>
                <Input type="checkbox" name="agreeTerms" onChange={e => setAgreeTerms(e.target.checked)} checked={agreeTerms} />
                <Label for="agreeTerms" style={labelStyle} check>By submitting this application, I agree to Nordax's terms for personal data processing.</Label>
            </FormGroup>
            <FormGroup className="text-left" check>
                <Input type="checkbox" name="correctInfo" onChange={e => setCorrectInfo(e.target.checked)} checked={correctInfo} />
                <Label for="correctInfo" style={labelStyle} check>
                    I hereby declare that the information to be submitted are correct to the extend of my knowledge.
                </Label>
            </FormGroup>
            <Button style={buttonStyle} onClick={submitApplication} className="mt-3"
                disabled={!agreeTerms || !correctInfo} >Submit loan application</Button>
        </Form>
    )

    return error ? (
        <div>
            <p>Something went wrong.</p>
            <p style={{ color: "red" }}>{error}</p>
        </div>
    ) : caseNo ? renderCaseNo() : renderSubmitForm()
})
export default Submit;