import React from "react";
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/home/Home';
import LoanApplication from './components/loan-application/LoanApplication';
import Overview from './components/overview/Overview';
import './custom.css'
import {Signup} from "./components/signUp/Signup";
import {Unsubscribe} from "./components/unsubscribe/Unsubscribe";

export const App = () => {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/signup' component={Signup} />
            <Route exact path='/unsubscribe/:userId' component={Unsubscribe} />
            <Route exact path='/loan-application' component={LoanApplication} />
            <Route exact path='/overview' component={Overview} />
        </Layout>
    );
};