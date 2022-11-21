export interface LoanApplicationDocumentResponse {
    id: string;
    documentType: string
}

export interface LoanApplicationResponse {
    id: string;
    caseNo: string;
    currentStep: string;
    createdDate: Date;
    applicantOrganizationNo: string;
    applicantFullName: string;
    loanAmount: number;
    loanPaymentPeriod: number;
    loanBindingPeriod: number;
    loanInterestRate: number;
    documents: LoanApplicationDocumentResponse[];
}