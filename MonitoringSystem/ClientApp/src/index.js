import "bootstrap/dist/css/bootstrap.min.css";
import "./assets/css/switch.css";
import "./assets/css/fan-animation.css";
import "./index.css";
import "babel-polyfill";

// Styles
// CoreUI Icons Set
//import "@coreui/icons/css/coreui-icons.min.css";
// Import Flag Icons Set
//import "flag-icon-css/css/flag-icon.min.css";
// Import Font Awesome Icons Set
import "font-awesome/css/font-awesome.min.css";
import "react-bootstrap-table/dist/react-bootstrap-table-all.min.css";
// Import Simple Line Icons Set
//import "simple-line-icons/css/simple-line-icons.css";
// Import Main styles for this application
//import "./scss/style.css";

import React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "react-router-redux";
import { createBrowserHistory } from "history";
import configureStore from "./store/configureStore";
import App from "./containers/App";
import registerServiceWorker from "./registerServiceWorker";
import ReactDOM from "react-dom";

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

const rootElement = document.getElementById("root");

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement
);

registerServiceWorker();
