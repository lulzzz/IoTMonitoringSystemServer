import React from 'react';
import { Route } from 'react-router';
import Layout from '../components/Layout';
import Dashboard from '../components/Dashboard';
import Fan from '../components/Fan';
import Counter from '../components/Counter';
import FetchData from '../components/FetchData';
import AsyncFans from '../containers/AsyncFans'

export default () => (
  <Layout>
    {/* <Route exact path='/' component={Home} /> */}
    <Route exact path='/' component={Dashboard} />
    <Route path='/fans' component={AsyncFans} />
    <Route path='/fan' component={Fan} />
    <Route path='/counter' component={Counter} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
  </Layout>
);
