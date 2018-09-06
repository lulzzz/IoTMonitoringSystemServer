import * as signalR from "@aspnet/signalr";
import * as dataService from "../services/DataService";
import * as authService from "../services/Authentication";
import { push } from "react-router-redux";

const requestSensorType = "REQUEST_SENSORS";
const receiveSensorType = "RECEIVE_SENSORS";
const statusPageChangeType = "STATUS_PAGE_CHANGE";
const dateRangeFilterChangeType = "DATE_RANGE_FILTER_CHANGE";
const initialState = {
  sensor: [],
  isLoading: false,
  statuses: [],
  racks: [],
  rooms: [],
  hubConnection: null,
  currentStatusPage: 1,
  startDate: null,
  endDate: null
};

export const actionCreators = {
  requestSensor: (isLoaded, sensorId) => async (dispatch, getState) => {
    //check if user dont log in
    if (!authService.isUserAuthenticated() || authService.isExpired()) {
      authService.clearLocalStorage();
      return dispatch => {
        dispatch(push("/"));
      };
    } else {
      if (isLoaded === getState().sensor.isLoaded) {
        // Don't issue a duplicate request (we already have or are loading the requested
        // data)
        return;
      }

      var currentStatusPage = getState().sensor.currentStatusPage;
      var hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

      hubConnection.on("LoadData", () => {
        loadData(dispatch, sensorId, isLoaded, currentStatusPage);
      });

      hubConnection
        .start()
        .then(() => {
          console.log("Hub connection started");
        })
        .catch(err => {
          console.log("Error while establishing connection");
        });

      dispatch({
        type: requestSensorType,
        isLoaded,
        hubConnection,
        sensorId
      });

      loadData(dispatch, sensorId, isLoaded);
    }
  },

  addRacks: data => async (dispatch, getState) => {
    const sensorId = getState().sensor.sensorId;
    data.sensorId = sensorId;
    delete data.rackId;
    const res = await dataService.post(`api/racks/add`, data);

    loadData(dispatch, sensorId);
  },

  updateRacks: (data, fieldName, value) => async (dispatch, getState) => {
    const sensorId = getState().sensor.sensorId;
    var rackId = data.rackId;
    data[fieldName] = value;

    data = {
      rackId: data.rackId,
      rackCode: data.rackCode,
      rackName: data.rackName,
      location: data.location,
      sensorId: sensorId,
      roomId: data.roomId
    };

    var res = await dataService.put(`api/racks/update/` + rackId, data);
    loadData(dispatch, sensorId);
  },

  deleteRacks: rackId => async (dispatch, getState) => {
    await dataService.remove(`api/racks/delete/` + rackId);

    const sensorId = getState().sensor.sensorId;
    loadData(dispatch, sensorId);
  },

  statusPageChange: page => async (dispatch, getState) => {
    var sensorId = getState().sensor.sensorId;
    const statuses = await dataService.get(
      `api/statuses/getall?sensorId=${sensorId}&pageSize=10&page=${page}&sortBy=datetime`
    );

    var currentStatusPage = page;
    var isLoaded = getState().sensor.isLoaded;

    dispatch({
      type: statusPageChangeType,
      isLoaded,
      statuses,
      currentStatusPage
    });
  },

  rangeFilterChange: (startDate, endDate) => async (dispatch, getState) => {
    var sensorId = getState().sensor.sensorId;
    const statuses = await dataService.get(
      `api/statuses/getall?sensorId=${sensorId}&pageSize=10&page=1&sortBy=datetime&startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`
    );

    var currentStatusPage = 1;
    var isLoaded = getState().sensor.isLoaded;

    dispatch({
      type: dateRangeFilterChangeType,
      isLoaded,
      statuses,
      currentStatusPage
    });
  },

  clear: () => async (dispatch, getState) => {
    const sensorId = getState().sensor.sensorId;
    loadData(dispatch, sensorId);
  }
};

export const loadData = async (
  dispatch,
  sensorId,
  isLoaded,
  currentStatusPage
) => {
  const sensor = await dataService.get(`api/sensors/getsensor/${sensorId}`);

  // const statuses = await dataService.get(
  //   `api/statuses/getall?sensorId=${sensorId}`
  // );

  const statuses = await dataService.get(
    `api/statuses/getall?sensorId=${sensorId}&pageSize=10&page=${currentStatusPage}&sortBy=datetime`
  );

  const racks = [];
  for (let element of sensor.racks) {
    var rack = await dataService.get(`api/racks/getrack/${element}`);

    if (!rack.isDeleted) {
      racks.push(rack);
    }
  }

  const rooms = await dataService.get(`api/rooms/getall`);

  dispatch({
    type: receiveSensorType,
    isLoaded,
    sensor,
    statuses,
    racks,
    rooms
  });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestSensorType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      sensorId: action.sensorId,
      hubConnection: action.hubConnection
    };
  }

  if (action.type === statusPageChangeType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      statuses: action.statuses,
      currentStatusPage: action.currentStatusPage
    };
  }

  if (action.type === dateRangeFilterChangeType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      statuses: action.statuses,
      currentStatusPage: action.currentStatusPage
    };
  }

  if (action.type === receiveSensorType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      sensor: action.sensor,
      statuses: action.statuses,
      racks: action.racks,
      rooms: action.rooms
    };
  }

  return state;
};
