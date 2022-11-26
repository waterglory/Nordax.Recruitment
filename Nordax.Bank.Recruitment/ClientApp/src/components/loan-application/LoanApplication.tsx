import React, { useRef, useState } from "react";
import { isOfType } from "../../common/classUtil";
import { WebApiClient } from "../../common/webApiClient";
import { RegisterLoanApplicationRequest } from "../../models/registerLoanApplicationRequest";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import { useNavigations } from "../common/common.styles";
import { useFormStyles } from "../common/form.styles";
import { TransitionPage } from "../common/transition/TransitionPage";
import LoanApplicationEvents from "./loanApplicationEvents";
import Resettable from "./resettable";
import Applicant from "./Applicant";
import Documents from "./Documents";
import Loan from "./Loan";
import OrganizationNo from "./OrganizationNo";
import Submit from "./Submit";

const LoanApplication = () => {
    const initLoanApplication = (): RegisterLoanApplicationRequest => ({
        applicantOrganizationNo: "",
        applicantFirstName: "",
        applicantSurname: "",
        applicantPhoneNo: "",
        applicantEmail: "",
        applicantAddress: "",
        applicantIncomeLevel: "",
        applicantIsPoliticallyExposed: false,

        loanAmount: 0,
        loanPaymentPeriod: 0,
        loanBindingPeriod: 0,
        loanInterestRate: 0,

        documents: []
    })

    const [pageIndex, setPageIndex] = useState(0);
    const [loanApplication, setLoanApplication] = useState(initLoanApplication());
    const [canRetry, setCanRetry] = useState(false);
    const apiClient = WebApiClient();

    const loanRef = useRef<Resettable>(null);
    const organizationNoRef = useRef<Resettable>(null);
    const documentsRef = useRef<Resettable>(null);
    const submitRef = useRef<Resettable>(null);
    const resettableRefs = [loanRef, organizationNoRef, documentsRef, submitRef];

    const setNext = (e: Event) => {
        if (e && e.preventDefault) e.preventDefault();
        if (pageIndex < pages.length - 1)
            setPageIndex(pageIndex + 1);
    }

    const setPrevious = () => {
        if (pageIndex > 0)
            setPageIndex(pageIndex - 1);
        submitRef.current?.reset();
    }

    const retry = () => {
        setCanRetry(false);
        setLoanApplication(initLoanApplication());
        resettableRefs.forEach(r => r.current?.reset());
        setPageIndex(0);
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        let val: string | number | boolean;
        if (isOfType(loanApplication, e.target.name, "number"))
            val = e.target.value ? Number.parseFloat(e.target.value) : 0;
        else if (isOfType(loanApplication, e.target.name, "boolean"))
            val = e.target.checked;
        else
            val = e.target.value;
        setLoanApplication({ ...loanApplication, [e.target.name]: val });
    }

    const handleMultiUpdates = (...updates: [string, any][]) => {
        let newState = { ...loanApplication };
        for (const update of updates)
            newState[update[0]] = update[1];
        setLoanApplication(newState);
    }

    const onDocumentUpload = (documentType: string, fileRef: string) => {
        // No need to use setLoanApplication, since it will never re-render the component.
        const docs = loanApplication.documents;
        const existingDocumentIndex = docs.findIndex(d => d.documentType == documentType);
        if (existingDocumentIndex > -1) docs.splice(existingDocumentIndex, 1);
        docs.push({ documentType, fileRef });
    }

    const { buttonStyle } = useFormStyles();
    const { navigationButtonStyle, navigationDivStyle } = useNavigations();
    const loanApplicationEvents: LoanApplicationEvents = {
        onChange: handleChange,
        onMultiUpdates: handleMultiUpdates,
        nextForm: "Next",
        next: setNext,
        onEndOfFlow: () => setCanRetry(true)
    };

    const pages = [
        <div>
            <h5>Nordax&trade; the bank of banks.</h5>
            <p className="nordax_subtitle">Want to receive news regarding <br /> the best loans?</p>
            <p>Submit your details below and we'll be in touch.</p>
            <Button style={buttonStyle} onClick={setNext}>Continue</Button>
        </div>,
        <Loan data={loanApplication} events={loanApplicationEvents} apiClient={apiClient} ref={loanRef} />,
        <OrganizationNo applicantOrganizationNo={loanApplication.applicantOrganizationNo} events={loanApplicationEvents} apiClient={apiClient} ref={organizationNoRef} />,
        <Applicant data={loanApplication} events={loanApplicationEvents} />,
        <Documents events={loanApplicationEvents} onDocumentUpload={onDocumentUpload} apiClient={apiClient} ref={documentsRef} />,
        <Submit data={loanApplication} events={loanApplicationEvents} apiClient={apiClient} ref={submitRef} />
    ];

    return (
        <div style={{ height: '100%', position: 'relative' }}>
            {pages.map((p, index) => (
                <TransitionPage index={index} currentIndex={pageIndex} key={index}>
                    {p}
                </TransitionPage>
            ))}
            {pageIndex > 0 && !canRetry ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => setPrevious()}>Go Back</Button>
                </div>
                : null}
            {canRetry ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => retry()}>Retry</Button>
                </div>
                : null}
        </div>
    );
};

export default LoanApplication;