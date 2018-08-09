import React from "react";
import { Route } from "react-router";
import Layout from "../components/Layout";
import Admin from "../components/Admin/Admin";
import AsyncFans from "../containers/AsyncFans";
import AsyncDashboard from "../containers/AsyncDashboard";
import Map from "./../components/Map";
import Sensor from "./../components/Admin/Sensor";
import Dashboard from "../containers/AsyncDashboard";
import Rack from "./../components/Admin/Rack";
import Room from "./../components/Admin/Room";
export default () => (
  <Layout>
    <Route exact path="/" component={AsyncDashboard} />
    <Route path="/map" component={Map} />
    <Route path="/fans" component={AsyncFans} />
    <Route path="/admin" component={Admin} />
    <Route path="/sensor/:sensorId?" component={Sensor} />
    <Route path="/rack/:rackId?" component={Rack} />
    <Route path="/room/:roomId?" component={Room} />
  </Layout>
);
