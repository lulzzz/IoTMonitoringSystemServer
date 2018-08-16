import fetch from "cross-fetch";
import * as dataService from "../services/DataService";

export const requestFansType = "REQUEST_FANS";
export const receiveFansType = "RECEIVE_FANS";
export const updateFanStatusType = "UPDATE_FAN_STATUS";
export const invalidateUpdateFanType = "INVALIDATE_UPDATE_FAN";

// import {     FETCH_PRODUCTS_BEGIN,     FETCH_PRODUCTS_SUCCESS,
// FETCH_PRODUCTS_FAILURE   } from './productActions';

export function updateFanStatus(fan, fans) {
  console.log("updateAction");
  return dispatch => {
    const url = "api/fans/update/" + fan.fanId;
    return dataService.put(url, fan).then(() => dispatch(fetchFans()));
  };
}

function requestFans() {
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
    dispatch(requestFans());
    return dataService
      .get(`/api/fans/getall`)
      .then(json => dispatch(receiveFans(json)));
  };
}

function shouldFetchFans(state) {
  const fans = state.fansReducer.fanList;
  // console.log('should') console.log(state)
  console.log(state);
  if (fans != undefined) {
    return true;
  } else if (fans.isFetching) {
    return false;
  } else {
    console.log("sss");
    return fans.didInvalidate;
  }
}

// export function updateFans (state, fan) {     if (isLoaded ===
// getState().fans.isLoaded) {         // Don't issue a duplicate request (we
// already have or are loading the requested         // data)         return;  }
//     dispatch({type: requestFansType, isLoaded, isLoading: true}); const url =
// `api/fans/update/` + fan.fanId;     const response = await fetch(url, {
// method: "PUT",         headers: {             Accept: "application/json",
//   "Content-Type": "application/json"         },         body:
// JSON.stringify(fan)     });     console.log(response)     const url2 =
// `api/fans/getall`;     const response2 = await fetch(url2);     const fans =
// await response2.json();     dispatch({type: receiveFansType, isLoaded,
// isLoading: false, fans});     return; }

export function fetchFansIfNeeded() {
  return (dispatch, getState) => {
    if (shouldFetchFans(getState())) {
      return dispatch(fetchFans());
    }
  };
}
