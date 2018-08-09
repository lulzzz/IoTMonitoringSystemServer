import * as signalR from "@aspnet/signalr";

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
    const res = await fetch(`api/racks/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

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

    var res = await fetch(`api/racks/update/` + rackId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch, sensorId);
  },

  deleteRacks: rackId => async (dispatch, getState) => {
    console.log("deleteRacks");
    await fetch(`api/racks/delete/` + rackId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });
    const sensorId = getState().sensor.sensorId;
    loadData(dispatch, sensorId);
  }
};

export const loadData = async (dispatch, sensorId, isLoaded) => {
  const sensor = await await fetch(`api/sensors/getsensor/${sensorId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const statuses = await fetch(`api/statuses/getall?sensorId=${sensorId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const racks = [];
  for (let element of sensor.racks) {
    var rack = await fetch(`api/racks/getrack/${element}`, {
      method: "GET"
    }).then(function(response) {
      return response.json();
    });

    if (!rack.isDeleted) {
      racks.push(rack);
    }
  }

  const rooms = await fetch(`api/rooms/getall`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

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
