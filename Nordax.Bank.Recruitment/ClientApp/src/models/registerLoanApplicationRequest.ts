export interface RegisterLoanApplicationRequest {
    applicantOrganizationNo: string;
    applicantFirstName: string;
    applicantSurname: string;
    applicantPhoneNo: string;
    applicantEmail: string;
    applicantAddress: string;
    applicantIsPoliticallyExposed: boolean;

    loanAmount: number;
    loanBindingPeriod: number;
    loanInterestRate: number;

    documents: RegisterLoanApplicationDocumentRequest[];
}

export interface RegisterLoanApplicationDocumentRequest {
    documentType: string;
    fileRef: string;
}