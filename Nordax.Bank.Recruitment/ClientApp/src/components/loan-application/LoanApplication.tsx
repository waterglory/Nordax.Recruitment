import React, { useState } from "react";
import { TransitionPage } from "../common/transition/TransitionPage";
import { Button } from '../common/button/Button';
import '../common/button/Button.css'
import { useFormStyles } from "../common/form.styles";
import { useNavigations } from "../common/common.styles";
import {
    RegisterLoanApplicationRequest,
    RegisterLoanApplicationDocumentRequest
} from "../../models/registerLoanApplicationRequest";
import Loan from "./Loan";

const LoanApplication = () => {
    const [pageIndex, setPageIndex] = useState(0);
    const [loanApplication, setLoanApplication] = useState({
        applicantOrganizationNo: "",
        applicantFirstName: "",
        applicantSurname: "",
        applicantPhoneNo: "",
        applicantEmail: "",
        applicantAddress: "",
        applicantIsPoliticallyExposed: false,

        loanAmount: 0,
        loanBindingPeriod: 0,
        loanInterestRate: 0,

        documents: []
    } as RegisterLoanApplicationRequest);

    const setNext = () => {
        if (pageIndex < pages.length - 1)
            setPageIndex(pageIndex + 1)
    }

    const setPrevious = () => {
        if (pageIndex > 0)
            setPageIndex(pageIndex - 1)
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        let val: string | number | boolean;
        if (e.target.type === "number")
            val = e.target.value ? Number.parseFloat(e.target.value) : 0;
        else if (e.target.type === "checkbox")
            val = e.target.value ? true : false;
        else
            val = e.target.value;
        setLoanApplication({ ...loanApplication, [e.target.name]: val });
    }

    const { buttonStyle } = useFormStyles();
    const { navigationButtonStyle, navigationDivStyle } = useNavigations();

    const pages = [
        <div>
            <h5>Nordax&trade; the bank of banks.</h5>
            <p className="nordax_subtitle">Want to receive news regarding <br /> the best loans?</p>
            <p>Submit your details below and we'll be in touch.</p>
            <Button style={buttonStyle} onClick={() => setNext()}>Continue</Button>
        </div>,
        <Loan data={loanApplication} onChange={handleChange} nextForm="Next" next={setNext} />
    ];

    return (
        <div style={{ height: '100%', position: 'relative' }}>
            {pages.map((p, index) => (
                <TransitionPage index={index} currentIndex={pageIndex} key={index}>
                    {p}
                </TransitionPage>
            ))}
            {pageIndex > 0 && pageIndex < pages.length - 2 ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => setPrevious()}>Go Back</Button>
                </div>
                : null}
            {pageIndex === pages.length - 1 ?
                <div style={navigationDivStyle}>
                    <Button style={navigationButtonStyle} onClick={() => setPageIndex(0)}>Retry</Button>
                </div>
                : null}
        </div>
    );
};

export default LoanApplication;