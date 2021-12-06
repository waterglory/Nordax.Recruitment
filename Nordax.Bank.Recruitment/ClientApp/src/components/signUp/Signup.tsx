import React, {useState} from "react";
import {SurveyPage} from "./SurveyPage";
import {Input} from "reactstrap";
import {NewSubscriberRequest} from "../../models/newSubscriberRequest";
import {Button} from '../common/button/Button';
import {WebApiClient} from "../../common/webApiClient";
import '../common/button/Button.css'
import {useSurveyPageStyles} from "./surveyPage.styles";


export const Signup = () => {
    const [pageIndex, setPageIndex] = useState(0);
    const [formData, setFormData] = useState({name: "", email: "", feedback: ""} as NewSubscriberRequest);
    const [submitError, setSubmitError] = useState<null | string>(null);
    const [id, setId] = useState<null | string>(null);
    const apiClient = WebApiClient();

    const setNext = () => {
        if (pageIndex < pages.length - 1)
            setPageIndex(pageIndex + 1)
    }

    const setPrevious = () => {
        if (pageIndex > 0)
            setPageIndex(pageIndex - 1)
    }

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const onSubmit = () => {
        apiClient.post<{subscriberId: string}>('api/subscriber', formData)
            .then((res) => {
                setId(res.subscriberId);
                setPageIndex(4);
            }).catch(e => {
                setSubmitError(e.status + " " + e.statusText);
                e.json().then((json: any) => {
                    setSubmitError(e.status + " " + e.statusText + ": "  + json);
                });
                setPageIndex(5);
        });
    };

    const validateEmail = (email: string) => {
        return /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/.test(email)
    }

    const {buttonStyle, inputStyle} = useSurveyPageStyles();

    const pages = [
        <SurveyPage index={0} currentIndex={pageIndex} key={0}>
            <div>
                <h5>Nordax™ the bank of banks.</h5>
                <p style={{color: "gray"}}>Want to receive news regarding <br/> the best loans?</p>
                <p>Submit your details below and we'll be in touch.</p>
                <Button style={buttonStyle} onClick={() => setNext()}>Continue</Button>
            </div>
        </SurveyPage>,
        <SurveyPage index={1} currentIndex={pageIndex} key={1}>
            <div>
                <p>What's your name?</p>
                <p style={{color: "gray"}}>Nicknames are welcomed, obvs.</p>
                <Input style={inputStyle} type={"text"} name={"name"} placeholder={"Tap to start writing.."} value={formData.name} onChange={handleChange}/>
                <Button  style={{...{marginTop: "18px"}, ...buttonStyle}} onClick={() => setNext()} disabled={!formData.name || formData.name.length === 0}>Next</Button>
            </div>
        </SurveyPage>,
        <SurveyPage index={2} currentIndex={pageIndex} key={2}>
            <div>
                <p>What's your email?</p>
                <p style={{color: "gray"}}>We promise, we'll send you fun stuff only.</p>
                <Input style={inputStyle} type={"email"} name={"email"} placeholder={"Tap to start writing.."} value={formData.email} onChange={handleChange}/>
                <Button style={{...{marginTop: "18px"}, ...buttonStyle}} onClick={() => {if(validateEmail(formData.email)) setNext()}} disabled={(!formData.email || formData.email.length === 0)}>Next</Button>
            </div>
        </SurveyPage>,
        <SurveyPage index={3} currentIndex={pageIndex} key={3}>
            <div>
                <p>By entering your information, you agree to let Nordax store your contact information and update you with news and announcements.</p>
                <p style={{color: "gray"}}>Are you ready to join the conversation?</p>
                <Button style={buttonStyle} onClick={() => onSubmit()}>I'm in!</Button>
            </div>
        </SurveyPage>,
        <SurveyPage index={4} currentIndex={pageIndex} key={4}>
            <div>
                <p><b>We saved you</b> and you are now part of the world of Nordax.<br/>
                An bank of banks.</p>
            </div>
        </SurveyPage>,
        <SurveyPage index={5} currentIndex={pageIndex} key={5}>
            <div>
                <p>Something went wrong with your submission.</p>
                <p style={{color: "red"}}>{submitError}</p>
            </div>
        </SurveyPage>
    ];

    return (
        <div style={{height: '100%', position: 'relative'}}>
            {pages}
            {pageIndex > 0 && pageIndex < 4 ?
                <div style={{position: "absolute", bottom: "22px", left: "50%"}}>
                    <Button style={{position: "relative", left: "-50%", width: "125px"}} color={"gray"} onClick={() => setPrevious()}>Go Back</Button>
                </div>
                : null}
            {pageIndex === 5 ?
                <div style={{position: "absolute", bottom: "22px", left: "50%"}}>
                    <Button style={{position: "relative", left: "-50%", width: "125px"}} color={"gray"} onClick={() => setPageIndex(0)}>Retry</Button>
                </div>
                : null}
        </div>
    )


}