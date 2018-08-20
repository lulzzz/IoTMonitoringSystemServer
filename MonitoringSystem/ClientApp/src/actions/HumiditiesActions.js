import * as dataService from "../services/DataService";

export const requestHumiditiesType = "REQUEST_TEMPERATURES";
export const receiveHumiditiesType = "RECEIVE_TEMPERATURES";

function requestHumidities() {
  return { type: requestHumiditiesType };
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
    dispatch(requestHumidities());
    return dataService
      .get(`/api/plots/humidity/getall`)
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
  return (dispatch, getState) => {
    if (shouldFetchHumidities(getState())) {
      return dispatch(fetchHumidities());
    }
  };
}
