import { combineReducers } from "redux";
import {
  requestTemperaturesType,
  receiveTemperaturesType
} from "../../actions/Dashboard/TemperaturesLogsActions";

function temperatures(
  state = {
    isFetching: false,
    hubConnection: null,
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
        items: action.temperatures,
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
