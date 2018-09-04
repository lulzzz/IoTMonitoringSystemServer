import { combineReducers } from "redux";
import {
  requestHumiditiesType,
  receiveHumiditiesType,
  fetchHumiditiesIfNeeded
} from "../../actions/Dashboard/HumiditiesLogsActions";

function humidities(
  state = {
    hubConnection: null,
    isFetching: false,
    items: []
  },
  action
) {
  switch (action.type) {
    case requestHumiditiesType:
      return {
        ...state,
        isFetching: true,
        hubConnection: action.hubConnection
      };
    case receiveHumiditiesType:
      return {
        ...state,
        isFetching: false,
        items: action.humidities,
        lastUpdated: action.receivedAt
      };
    default:
      return state;
  }
}

function humidityList(state = {}, action) {
  switch (action.type) {
    case receiveHumiditiesType:
    case requestHumiditiesType:
      return {
        ...state,
        humidities: humidities(state, action)
      };
    default:
      return state;
  }
}

const humiditiesReducer = combineReducers({ humidityList });

export default humiditiesReducer;
