import { hasIndexer } from "../common/classUtil";

export interface RegisterLoanApplicationRequest extends hasIndexer {
    applicantOrganizationNo: string;
    applicantFirstName: string;
    applicantSurname: string;
    applicantPhoneNo: string;
    applicantEmail: string;
    applicantAddress: string;
    applicantIncomeLevel: string;
    applicantIsPoliticallyExposed: boolean;

    loanAmount: number;
    loanPaymentPeriod: number;
    loanBindingPeriod: number;
    loanInterestRate: number;

    documents: RegisterLoanApplicationDocumentRequest[];
}

export interface RegisterLoanApplicationDocumentRequest {
    documentType: string;
    fileRef: string;
}