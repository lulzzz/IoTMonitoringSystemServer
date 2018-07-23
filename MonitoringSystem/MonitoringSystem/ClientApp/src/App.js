import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Dashboard from './components/Dashboard';
import Fans from './components/Fans';
import Counter from './components/Counter';
import FetchData from './components/FetchData';

export default () => (
  <Layout>
    {/* <Route exact path='/' component={Home} /> */}
    <Route exact path='/' component={Dashboard} />
    <Route path='/fans' component={Fans} />
    <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
  </Layout>
);
