import React, { useState } from "react";
import { Card, Col, DropdownItem, DropdownMenu, DropdownToggle, Row, Table, UncontrolledDropdown } from "reactstrap";
import { WebApiClient } from "../../common/webApiClient";
import { LoanApplicationResponse } from "../../models/loanApplicationResponse";
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import useOverviewStyles from "./overview.styles";

const Overview = () => {
    const [loanApplications, setLoanApplications] = useState<LoanApplicationResponse[]>([]);
    const [error, setError] = useState<string | null>(null);
    const apiClient = WebApiClient();

    const getLoanApplications = () => {
        apiClient.get<LoanApplicationResponse[]>("api/loan-application")
            .then((res) => setLoanApplications(res))
            .catch(e => {
                setError(e.status + " " + e.statusText);
                e.json && e.json().then((json: any) => {
                    setError(e.status + " " + e.statusText + ": " + json);
                });
            });
    }

    const translateDocumentType = (documentType: string) => {
        const result = documentType.replace(/([A-Z])/g, " $1");
        return result.charAt(0).toUpperCase() + result.slice(1);
    }

    const getDocument = (documentId: string) => {
        const a = document.createElement("a");
        a.href = `api/loan-application/attachment/${documentId}`;
        a.click();
    }

    React.useEffect(getLoanApplications, []);

    const { containerStyle } = useOverviewStyles();

    return error ? (
        <div>
            <p>Something went wrong.</p>
            <p style={{ color: "red" }}>{error}</p>
        </div>
    ) : (
        <div style={containerStyle}>
            <div style={{ position: 'relative', width: '100%', height: '100%' }}>
                <Row className="justify-content-md-center align-items-center" style={{ height: "100%" }}>
                    <Col style={{ margin: '0 auto' }}>
                        <Card style={{ padding: "15px" }}>
                            <div>
                                <h5 style={{ width: "70%", display: "inline-block", lineHeight: "1.5" }} >Loan Applications</h5>
                                <Button style={{ width: "15%", float: "right" }} onClick={() => getLoanApplications()}>Refresh</Button>
                            </div>
                            <Table striped responsive hover>
                                <thead>
                                    <tr>
                                        <th>Case No.</th>
                                        <th>Current Step</th>
                                        <th>Submission Date</th>
                                        <th>Organization No.</th>
                                        <th>Name</th>
                                        <th>Amount (kr)</th>
                                        <th>Payment Period (mo.)</th>
                                        <th>Binding Period (mo.)</th>
                                        <th>Interest Rate</th>
                                        <th>Documents</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {loanApplications.map(la =>
                                        <tr key={la.id}>
                                            <th scope="row">{la.caseNo}</th>
                                            <td><i>{la.currentStep}</i></td>
                                            <td>{new Date(la.createdDate).toLocaleDateString()}</td>
                                            <td>{la.applicantOrganizationNo}</td>
                                            <td>{la.applicantFullName}</td>
                                            <td>{la.loanAmount.toLocaleString()}</td>
                                            <td>{la.loanPaymentPeriod}</td>
                                            <td>{la.loanBindingPeriod}</td>
                                            <td>{`${la.loanInterestRate}%`}</td>
                                            <td align="center">
                                                <UncontrolledDropdown>
                                                    <DropdownToggle>
                                                        ...
                                                    </DropdownToggle>
                                                    <DropdownMenu>
                                                        {la.documents.map(d =>
                                                            <DropdownItem key={d.documentType} onClick={() => getDocument(d.id)}>
                                                                {translateDocumentType(d.documentType)}
                                                            </DropdownItem>
                                                        )}
                                                    </DropdownMenu>
                                                </UncontrolledDropdown>
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                        </Card>
                    </Col>
                </Row>
            </div>
        </div>
    );
}
export default Overview;