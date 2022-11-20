import React, { useState } from 'react';
import { Card, CardTitle, Form, FormFeedback, FormText, Input } from "reactstrap";
import { IHttpClient } from '../../common/httpClient';
import { Button } from '../common/button/Button';
import '../common/button/Button.css';
import '../common/common.css';
import { useFormStyles } from "../common/form.styles";
import LoanApplicationEvents from './loanApplicationEvents';

interface Document {
    documentType: string,
    displayName: string,
    description: string,
    elementRef: React.RefObject<HTMLInputElement>
}

const Documents = (props: React.PropsWithChildren<{
    events: LoanApplicationEvents,
    onDocumentUpload: (documentType: string, fileRef: string) => void,
    apiClient: IHttpClient
}>) => {
    const [hasAllDocuments, setHasAllDocuments] = useState(false);
    const [documentsError, setDocumentsError] = useState<any>({});

    const { buttonStyle } = useFormStyles();

    const maxFileSize = 2097152;
    const supportedTypes = ["pdf", "png", "jpg", "jpeg", "gif"];

    // In an ideal word, these list comes from database.
    const documents: Document[] = [
        {
            documentType: "identification",
            displayName: "Identification",
            description: "Please upload id card, passport, or driving license.",
            elementRef: React.createRef<HTMLInputElement>()
        },
        {
            documentType: "bankStatement",
            displayName: "Bank statement",
            description: "Please upload your bank statement for the past 12 months.",
            elementRef: React.createRef<HTMLInputElement>()
        }
    ];

    const handleChange = () =>
        setHasAllDocuments(documents.every(d => {
            const file = d.elementRef
                && d.elementRef.current
                && d.elementRef.current.files
                && d.elementRef.current.files.length
                && d.elementRef.current.files[0];
            if (!file) {
                setDocumentsError({ ...documentsError, [d.documentType]: null });
                return false;
            }

            if (file.size > maxFileSize) {
                setDocumentsError({ ...documentsError, [d.documentType]: `Document max size is 2MB.` });
                return false;
            }
            else {
                const typeStartIndex = file.name.lastIndexOf(".");
                const type = file.name.substring(typeStartIndex + 1);
                if (!supportedTypes.includes(type)) {
                    setDocumentsError({ ...documentsError, [d.documentType]: `We only accept pdf, png, jpg, jpeg, or gif.` });
                    return false;
                }
            }

            setDocumentsError({ ...documentsError, [d.documentType]: null });
            return true;
        }));

    const uploadDocuments = (e: Event) => {
        e.preventDefault();
        Promise.all(documents.map(d => {
            const file = d.elementRef
                && d.elementRef.current
                && d.elementRef.current.files
                && d.elementRef.current.files.length
                && d.elementRef.current.files[0];
            if (!file) return null;
            return props.apiClient.postFile<{ fileRef: string }>(`api/loan-application/attachment/${d.documentType}`, file)
                .then((res) => props.onDocumentUpload(d.documentType, res.fileRef))
                .catch((e) => {
                    e.json().then((json: any) =>
                        setDocumentsError({ ...documentsError, [d.documentType]: json.errorMessage }));
                    throw e;
                });
        })).then(() => props.events.next(e));
    }

    return (
        <Form>
            <h5>Documents</h5>
            <p className="nordax_subtitle">We just want to double check if it's really you.</p>
            <p>Unfortunately, we only accept pdf, png, jpg, jpeg, or gif. Max. 2MB.</p>
            {documents.map(d =>
                <Card body className="mb-1" key={d.documentType}>
                    <CardTitle tag="h6" className="text-left">{d.displayName}</CardTitle>
                    <Input type="file" name={d.documentType} innerRef={d.elementRef} onChange={handleChange}
                        invalid={documentsError[d.documentType]} />
                    <FormFeedback className="text-left">{documentsError[d.documentType]}</FormFeedback>
                    <FormText className="text-left">{d.description}</FormText>
                </Card>)}
            <Button style={buttonStyle} onClick={uploadDocuments} className="mt-2"
                disabled={!hasAllDocuments}>{props.events.nextForm}</Button>
        </Form>
    );
}
export default Documents;