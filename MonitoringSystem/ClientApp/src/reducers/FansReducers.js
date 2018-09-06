import { combineReducers } from "redux";
import {
  requestFansType,
  receiveFansType,
  updateFanStatusType
} from "../actions/FansActions";

function updatedFanStatus(state = "reactjs", action) {
  switch (action.type) {
    case updateFanStatusType:
      return action.fans;
    default:
      return state;
  }
}

function fans(
  state = {
    isFetching: false,
    hubConnection: null,
    items: []
  },
  action
) {
  switch (action.type) {
    case updateFanStatusType:
      return Object.assign({}, state);
    case requestFansType:
      return Object.assign({}, state, {
        isFetching: true,
        hubConnection: action.hubConnection
      });
    case receiveFansType:
      return Object.assign({}, state, {
        isFetching: false,
        items: action.fans,
        lastUpdated: action.receivedAt
      });
    default:
      return state;
  }
}

function fanList(state = {}, action) {
  switch (action.type) {
    case updateFanStatusType:
    case receiveFansType:
    case requestFansType:
      return {
        ...state,
        fans: fans(state, action)
      };
    default:
      return state;
  }
}

const fansReducer = combineReducers({ fanList, updatedFanStatus });

export default fansReducer;
