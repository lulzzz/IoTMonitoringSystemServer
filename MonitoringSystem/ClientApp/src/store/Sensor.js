import * as signalR from "@aspnet/signalr";
import * as dataService from "../services/DataService";

const requestSensorType = "REQUEST_SENSORS";
const receiveSensorType = "RECEIVE_SENSORS";
const initialState = {
  sensor: [],
  isLoading: false,
  statuses: [],
  racks: [],
  rooms: [],
  hubConnection: null
};

export const actionCreators = {
  requestSensor: (isLoaded, sensorId) => async (dispatch, getState) => {
    if (isLoaded === getState().sensor.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    var hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/hub")
      .build();

    hubConnection.on("LoadData", () => {
      loadData(dispatch, sensorId, isLoaded);
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
  },

  addRacks: data => async (dispatch, getState) => {
    console.log("addRacks");
    const sensorId = getState().sensor.sensorId;
    data.sensorId = sensorId;
    delete data.rackId;
    const res = await dataService.post(`api/racks/add`, data);

    loadData(dispatch, sensorId);
  },

  updateRacks: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRacks");
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
    console.log("deleteRacks");
    await dataService.remove(`api/racks/delete/` + rackId);

    const sensorId = getState().sensor.sensorId;
    loadData(dispatch, sensorId);
  }
};

export const loadData = async (dispatch, sensorId, isLoaded) => {
  const sensor = await dataService.get(`api/sensors/getsensor/${sensorId}`);

  const statuses = await dataService.get(
    `api/statuses/getall?sensorId=${sensorId}`
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
