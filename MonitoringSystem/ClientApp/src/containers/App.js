﻿import React, { Component } from "react";
import { Route } from "react-router";
import Layout from "../components/Layout";
import Admin from "../components/Admin/Admin";
import AsyncFans from "../containers/AsyncFans";
// import AsyncDashboard from "../containers/AsyncDashboard";
import AsyncHumiditiesLogs from "./Dashboard/AsyncHumiditiesLogs";
import AsyncHumiditiesRealtime from "./Dashboard/AsyncHumiditiesRealtime";
import AsyncTemperaturesLogs from "./Dashboard/AsyncTemperaturesLogs";
import AsyncTemperaturesRealtime from "./Dashboard/AsyncTemperaturesRealtime";
import Map from "./../components/Map";
import Sensor from "./../components/Admin/Sensor";
import Rack from "./../components/Admin/Rack";
import Room from "./../components/Admin/Room";
import LogIn from "../components/Account/LogIn";
import Register from "../components/Account/Register";
import Account from "../components/Account/Account";
// import { library } from "@fortawesome/fontawesome-svg-core";
// import { faBars, faSquare } from "@fortawesome/free-solid-svg-icons";

// library.add(faBars, faSquare);
const App = () => {
  return (
    <Layout>
      <Route exact path="/" component={LogIn} />
      {/* <Route path="/dashboard" component={AsyncDashboard} /> */}
      <Route path="/templog" component={AsyncTemperaturesLogs} />
      <Route path="/temprt" component={AsyncTemperaturesRealtime} />
      <Route path="/humilog" component={AsyncHumiditiesLogs} />
      <Route path="/humirt" component={AsyncHumiditiesRealtime} />
      <Route path="/map" component={Map} />
      <Route path="/fans" component={AsyncFans} />
      <Route path="/admin" component={Admin} />
      <Route path="/sensor/:sensorId?" component={Sensor} />
      <Route path="/rack/:rackId?" component={Rack} />
      <Route path="/room/:roomId?" component={Room} />
      <Route path="/register" component={Register} />
      <Route path="/account" component={Account} />
    </Layout>
  );
};
export default App;
