import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { routerReducer, routerMiddleware } from "react-router-redux";
import * as Admin from "./Admin";
import * as Sensor from "./Sensor";
import * as Rack from "./Rack";
import * as Room from "./Room";
import * as Map from "./Map";
import * as LogIn from "./LogIn";
import * as Register from "./Register";
import * as Account from "./Account";
import fansReducer from "../reducers/FansReducers";
import temperaturesLogsReducer from "../reducers/Dashboard/TemperaturesLogsReducers";
import humiditiesLogsReducer from "../reducers/Dashboard/HumiditiesLogsReducers";
import humiditiesRealtimeReducer from "../reducers/Dashboard/HumiditiesRealtimeReducers";
import temperaturesRealtimeReducer from "../reducers/Dashboard/TemperaturesRealtimeReducers";

export default function configureStore(history, initialState) {
  const reducers = {
    admin: Admin.reducer,
    sensor: Sensor.reducer,
    rack: Rack.reducer,
    room: Room.reducer,
    map: Map.reducer,
    fansReducer,
    temperaturesLogsReducer,
    humiditiesLogsReducer,
    humiditiesRealtimeReducer,
    temperaturesRealtimeReducer,
    login: LogIn.reducer,
    register: Register.reducer,
    account: Account.reducer
  };

  const middleware = [thunk, routerMiddleware(history)];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === "development";
  if (
    isDevelopment &&
    typeof window !== "undefined" &&
    window.devToolsExtension
  ) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(
      applyMiddleware(...middleware),
      ...enhancers
    )
  );
}
