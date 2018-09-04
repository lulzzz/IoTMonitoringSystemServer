import { combineReducers } from "redux";
import {
  requestTemperaturesType,
  receiveTemperaturesType,
  fetchTemperaturesIfNeeded
} from "../../actions/Dashboard/TemperaturesRealtimeActions";

function temperatures(
  state = {
    hubConnection: null,
    isFetching: false,
    items: []
  },
  action
) {
  switch (action.type) {
    case requestTemperaturesType:
      return {
        ...state,
        isFetching: true,
        hubConnection: action.hubConnection
      };
    case receiveTemperaturesType:
      return {
        ...state,
        isFetching: false,
        items: action,
        lastUpdated: action.receivedAt
      };
    default:
      return state;
  }
}

function temperatureList(state = {}, action) {
  switch (action.type) {
    case receiveTemperaturesType:
    case requestTemperaturesType:
      return {
        ...state,
        temperatures: temperatures(state, action)
      };
    default:
      return state;
  }
}

const temperaturesReducer = combineReducers({ temperatureList });

export default temperaturesReducer;
