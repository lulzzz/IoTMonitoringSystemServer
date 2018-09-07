import * as dataService from "../services/DataService";
import * as authService from "../services/Authentication";
import { push } from "react-router-redux";
import * as signalR from "@aspnet/signalr";

export const requestFansType = "REQUEST_FANS";
export const receiveFansType = "RECEIVE_FANS";
export const updateFanStatusType = "UPDATE_FAN_STATUS";
export const invalidateUpdateFanType = "INVALIDATE_UPDATE_FAN";

// import {     FETCH_PRODUCTS_BEGIN,     FETCH_PRODUCTS_SUCCESS,
// FETCH_PRODUCTS_FAILURE   } from './productActions';

export function updateFanStatus(fan, fans) {
  return dispatch => {
    const url = "api/fans/update/" + fan.fanId;
    return dataService.put(url, fan).then(() => dispatch(fetchFans()));
  };
}

function requestFans(dispatch) {
  var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .build();

  hubConnection.on("LoadData", () => {
    dataService
      .get(`/api/fans/getall`)
      .then(json => dispatch(receiveFans(json)));
  });

  hubConnection
    .start()
    .then(() => {
      console.log("Hub connection started");
    })
    .catch(err => {
      console.log("Error while establishing connection");
    });

  return { type: requestFansType };
}

function receiveFans(json) {
  return {
    type: receiveFansType,
    fans: json.items,
    receivedAt: Date.now()
  };
}

function fetchFans() {
  return dispatch => {
    dispatch(requestFans(dispatch));
    return dataService
      .get(`api/fans/getall`)
      .then(json => dispatch(receiveFans(json)));
  };
}

function shouldFetchFans(state) {
  const fans = state.fansReducer.fanList;
  if (fans !== undefined) {
    return true;
  } else if (fans.isFetching) {
    return false;
  } else {
    return fans.didInvalidate;
  }
}

export function fetchFansIfNeeded() {
  //check if user dont log in
  if (!authService.isUserAuthenticated() || authService.isExpired()) {
    authService.clearLocalStorage();
    return dispatch => {
      dispatch(push("/"));
    };
  } else {
    return (dispatch, getState) => {
      if (shouldFetchFans(getState())) {
        return dispatch(fetchFans());
      }
    };
  }
}
