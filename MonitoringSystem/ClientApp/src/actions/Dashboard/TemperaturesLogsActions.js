import * as dataService from "../../services/DataService";
import * as authService from "../../services/Authentication";
import * as signalR from "@aspnet/signalr";
import { push } from "react-router-redux";

export const requestTemperaturesType = "REQUEST_TEMPERATURES";
export const receiveTemperaturesType = "RECEIVE_TEMPERATURES";

function requestTemperatures(dispatch) {
  var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

  hubConnection.on("LoadData", () => {
    dataService
      .get(`/api/plots/temperature/getall`)
      .then(json => dispatch(receiveTemperatures(json)));
  });

  hubConnection
    .start()
    .then(() => {
      console.log("Hub connection started");
    })
    .catch(err => {
      console.log("Error while establishing connection");
    });

  return { type: requestTemperaturesType };
}

function receiveTemperatures(json) {
  return {
    type: receiveTemperaturesType,
    temperatures: json.items,
    receivedAt: Date.now()
  };
}

function fetchTemperatures() {
  return dispatch => {
    dispatch(requestTemperatures(dispatch));
    return dataService
      .get(`api/plots/temperature/getall`)
      .then(json => dispatch(receiveTemperatures(json)));
  };
}

function shouldFetchTemperatures(state) {
  const temperatures = state.temperatureList;
  if (!temperatures) {
    return true;
  } else if (temperatures.isFetching) {
    return false;
  } else {
    return false;
  }
}

export function fetchTemperaturesIfNeeded() {
  //check if user dont log in
  if (!authService.isUserAuthenticated() || authService.isExpired()) {
    authService.clearLocalStorage();
    return dispatch => {
      dispatch(push("/"));
    };
  } else {
    return (dispatch, getState) => {
      if (shouldFetchTemperatures(getState())) {
        return dispatch(fetchTemperatures());
      }
    };
  }
}
