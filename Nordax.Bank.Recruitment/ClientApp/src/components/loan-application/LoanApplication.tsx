import React, { useState } from "react";
import { RegisterLoanApplicationRequest } from "../../models/registerLoanApplicationRequest";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import { useNavigations } from "../common/common.styles";
import { useFormStyles } from "../common/form.styles";
import { TransitionPage } from "../common/transition/TransitionPage";
import Loan from "./Loan";
import OrganizationNo from "./OrganizationNo";
import Applicant from "./Applicant";
import { isOfType } from "../../common/classUtil";
import LoanApplicationEvents from "./loanApplicationEvents";

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

    const setNext = (e: Event) => {
        if (e && e.preventDefault) e.preventDefault();
        if (pageIndex < pages.length - 1)
            setPageIndex(pageIndex + 1)
    }

    const setPrevious = () => {
        if (pageIndex > 0)
            setPageIndex(pageIndex - 1)
    }

    const retry = () => {
        setLoanApplication(initLoanApplication());
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

    const { buttonStyle } = useFormStyles();
    const { navigationButtonStyle, navigationDivStyle } = useNavigations();
    const loanApplicationEvents: LoanApplicationEvents = {
        onChange: handleChange,
        onMultiUpdates: handleMultiUpdates,
        nextForm: "Next",
        next: setNext
    };

    const pages = [
        <div>
            <h5>Nordax&trade; the bank of banks.</h5>
            <p className="nordax_subtitle">Want to receive news regarding <br /> the best loans?</p>
            <p>Submit your details below and we'll be in touch.</p>
            <Button style={buttonStyle} onClick={setNext}>Continue</Button>
        </div>,
        <Applicant data={loanApplication} events={loanApplicationEvents} />,
        <OrganizationNo applicantOrganizationNo={loanApplication.applicantOrganizationNo} events={loanApplicationEvents} />,
        <Loan data={loanApplication} events={loanApplicationEvents} />
    ];

    return (
        <div style={{ height: '100%', position: 'relative' }}>
            {pages.map((p, index) => (
                <TransitionPage index={index} currentIndex={pageIndex} key={index}>
                    {p}
                </TransitionPage>
            ))}
            {pageIndex > 0 && pageIndex < pages.length - 1 ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => setPrevious()}>Go Back</Button>
                </div>
                : null}
            {pageIndex === pages.length - 1 ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => retry()}>Retry</Button>
                </div>
                : null}
        </div>
    );
};

export default LoanApplication;