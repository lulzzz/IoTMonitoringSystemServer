import * as dataService from "../../services/DataService";
import * as signalR from "@aspnet/signalr";
import * as authService from "../../services/Authentication";
import { push } from "react-router-redux";
import humiditiesReducer from "../../reducers/Dashboard/HumiditiesLogsReducers";

export const requestHumiditiesType = "REQUEST_HUMIDITIES";
export const receiveHumiditiesType = "RECEIVE_HUMIDITIES";

function requestHumidities(dispatch) {
  var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

  hubConnection.on("LoadData", () => {
    dataService
      .get(`/api/plots/humidity/getall`)
      .then(json => dispatch(receiveHumidities(json)));
  });

  hubConnection
    .start()
    .then(() => {
      console.log("Hub connection started");
    })
    .catch(err => {
      console.log("Error while establishing connection");
    });

  return { type: requestHumiditiesType, hubConnection: hubConnection };
}

function receiveHumidities(json) {
  return {
    type: receiveHumiditiesType,
    humidities: json.items,
    receivedAt: Date.now()
  };
}

function fetchHumidities() {
  return dispatch => {
    dispatch(requestHumidities(dispatch));
    return dataService
      .get(`api/plots/humidity/getall`)
      .then(json => dispatch(receiveHumidities(json)));
  };
}

function shouldFetchHumidities(state) {
  const humidities = state.humidityList;
  if (!humidities) {
    return true;
  } else if (humidities.isFetching) {
    return false;
  } else {
    return false;
  }
}

export function fetchHumiditiesIfNeeded() {
  //check if user dont log in
  if (!authService.isUserAuthenticated() || authService.isExpired()) {
    authService.clearLocalStorage();
    return dispatch => {
      dispatch(push("/"));
    };
  } else {
    return (dispatch, getState) => {
      if (shouldFetchHumidities(getState())) {
        return dispatch(fetchHumidities());
      }
    };
  }
}
