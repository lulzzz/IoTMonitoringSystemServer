import React from "react";
import { Route } from "react-router";
import Layout from "../components/Layout";
import Admin from "../components/Admin/Admin";

import AsyncFans from "../containers/AsyncFans";
import AsyncDashboard from "../containers/AsyncDashboard";

export default () => (
  <Layout>
    <Route exact path="/" component={AsyncDashboard} />
    <Route path="/fans" component={AsyncFans} />
    <Route path="/admin" component={Admin} />
  </Layout>
);
