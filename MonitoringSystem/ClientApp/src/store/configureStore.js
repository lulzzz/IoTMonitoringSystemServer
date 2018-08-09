import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { routerReducer, routerMiddleware } from "react-router-redux";
import * as Counter from "./Counter";
import * as WeatherForecasts from "./WeatherForecasts";
import * as Temperatures from "./Temperatures";
import * as Admin from "./Admin";
import * as Sensor from "./Sensor";
import * as Rack from "./Rack";
import * as Room from "./Room";
import * as Map from "./Map";
import fansReducer from "../reducers/FansReducers";
import temperaturesReducer from "../reducers/TemperaturesReducers";
import humiditiesReducer from "../reducers/HumiditiesReducers";
import { reducer as toastrReducer } from "react-redux-toastr";

export default function configureStore(history, initialState) {
  const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
    temperatures: Temperatures.reducer,
    admin: Admin.reducer,
    sensor: Sensor.reducer,
    rack: Rack.reducer,
    room: Room.reducer,
    map: Map.reducer,
    fansReducer,
    temperaturesReducer,
    humiditiesReducer,
    toastr: toastrReducer
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
