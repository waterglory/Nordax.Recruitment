import Personnummer from "personnummer";
import React, { useState } from 'react';
import { FormFeedback, Input } from "reactstrap";
import { WebApiClient } from "../../common/webApiClient";
import GetCustomerDataResponse from "../../models/getCustomerDataResponse";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import LoanApplicationEvents from "./loanApplicationEvents";

const OrganizationNo = (props: React.PropsWithChildren<{
    applicantOrganizationNo: string,
    events: LoanApplicationEvents
}>) => {
    const [validOrgNo, setValidOrgNo] = useState(true);
    const [existingCaseNo, setExistingCaseNo] = useState<string | null>(null);
    const [error, setError] = useState<string | null>(null);

    const labelCol = 5;

    const handleOrganizationNoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        let isValid = true;
        if (e.target.value)
            isValid = Personnummer.valid(e.target.value);
        setValidOrgNo(isValid);

        props.events.onChange(e);
    };

    const signWithBankId = (e: Event) => {
        WebApiClient().get<GetCustomerDataResponse>(`api/loan-application/customer/${props.applicantOrganizationNo}`)
            .then((res) => {
                props.events.onMultiUpdates(
                    ["applicantFirstName", res.firstName],
                    ["applicantSurname", res.surname],
                    ["applicantPhoneNo", res.phoneNo],
                    ["applicantEmail", res.email],
                    ["applicantAddress", res.address],
                    ["applicantIncomeLevel", res.incomeLevel],
                    ["applicantIsPoliticallyExposed", res.isPoliticallyExposed],
                );
                props.events.next(e);
            }).catch(e => {
                switch (e.status) {
                    case 409:
                        e.json().then((json: any) => {
                            setExistingCaseNo(json.caseNo);
                        });
                        break;
                    case 404:
                        //do nothing, customer data simply not exists
                        props.events.next(e);
                        break;
                    default:
                        setError(e.status + " " + e.statusText);
                        e.json().then((json: any) => {
                            setError(e.status + " " + e.statusText + ": " + json);
                        });
                        break;
                }
            });
    };

    const renderExistingCaseNo = () => (
        <div>
            <p>You already have an ongoing application.</p>
            <p className="nordax_subtitle">Please wait for decision or withdraw your application <br />before submitting a new one.</p>
            <h5>Case no.: {existingCaseNo}</h5>
        </div>
    )

    const { inputStyle, buttonStyle } = useFormStyles();

    const renderOrganizationNo = () => (
        <div>
            <p>Provide your organization number</p>
            <Input style={inputStyle} type="text" name="applicantOrganizationNo" placeholder={"Tap to start writing.."}
                value={props.applicantOrganizationNo} onChange={handleOrganizationNoChange} invalid={!validOrgNo} />
            <FormFeedback>
                Oh noes! That is not a valid Swedish SSN
            </FormFeedback>
            <Button style={buttonStyle} className="mt-3" onClick={signWithBankId} disabled={!props.applicantOrganizationNo || !validOrgNo}>Sign with bank id</Button>
        </div>
    )

    return error ? (
        <div>
            <p>Something went wrong.</p>
            <p style={{ color: "red" }}>{error}</p>
        </div>
    ) : (existingCaseNo ? renderExistingCaseNo() : renderOrganizationNo());
}
export default OrganizationNo;