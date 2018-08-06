import * as signalR from "@aspnet/signalr";

const requestAdminType = "REQUEST_SENSORS";
const receiveAdminType = "RECEIVE_SENSORS";
const initialState = {
  sensors: [],
  racks: [],
  rooms: [],
  fans: [],
  isLoading: false,
  hubConnection: null,
  trueFalseFormatter: []
};

export const actionCreators = {
  requestAdmin: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().admin.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    var trueFalseFormatter = [
      { id: true, formatter: "On" },
      { id: false, formatter: "Off" }
    ];

    var hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/hub")
      .build();

    hubConnection.on("LoadData", () => {
      loadData(dispatch, isLoaded);
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
      type: requestAdminType,
      isLoaded,
      hubConnection,
      trueFalseFormatter
    });
    loadData(dispatch, isLoaded);
  },

  addRacks: data => async (dispatch, getState) => {
    console.log("addRacks");

    delete data.rackId;
    await fetch(`api/racks/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  addRooms: data => async (dispatch, getState) => {
    console.log("addRooms");

    delete data.roomId;
    await fetch(`api/rooms/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  addSensors: data => async (dispatch, getState) => {
    console.log("addSensors");

    delete data.sensorId;
    await fetch(`api/sensors/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  addFans: data => async (dispatch, getState) => {
    console.log("addFans");

    delete data.fanId;
    await fetch(`api/fans/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  updateRacks: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRacks");
    var rackId = data.rackId;
    data[fieldName] = value;

    data = {
      rackId: data.rackId,
      rackCode: data.rackCode,
      rackName: data.rackName
    };

    console.log(data);
    var res = await fetch(`api/racks/update/` + rackId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  updateRooms: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRooms");
    var roomId = data.roomId;
    data[fieldName] = value;

    data = {
      roomCode: data.roomCode,
      roomName: data.roomName
    };

    console.log(data);
    var res = await fetch(`api/rooms/update/` + roomId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch);
  },

  updateSensors: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateSensors");
    var sensorId = data.sensorId;
    data[fieldName] = value;

    data = {
      roomName: data.roomName,
      sensorCode: data.sensorCode,
      sensorName: data.sensorName
    };

    console.log(data);
    var res = await fetch(`api/sensors/update/` + sensorId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });
    console.log(res);

    loadData(dispatch);
  },

  updateFans: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateFans");
    var fanId = data.fanId;
    data[fieldName] = value;

    data = {
      fanCode: data.fanCode,
      FanName: data.fanName,
      isOn: data.isOn,
      capacity: data.capacity,
      roomName: data.roomName
    };

    console.log(data);
    var res = await fetch(`api/fans/update/` + fanId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });
    console.log(res);

    loadData(dispatch);
  },

  deleteRacks: rackId => async (dispatch, getState) => {
    await fetch(`api/racks/delete/` + rackId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });

    loadData(dispatch);
  },

  deleteRooms: roomId => async (dispatch, getState) => {
    await fetch(`api/rooms/delete/` + roomId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });

    loadData(dispatch);
  },

  deleteSensors: sensorId => async (dispatch, getState) => {
    await fetch(`api/sensors/delete/` + sensorId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });

    loadData(dispatch);
  },

  deleteSensors: fanId => async (dispatch, getState) => {
    await fetch(`api/fans/delete/` + fanId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });

    loadData(dispatch);
  }
};

export const loadData = async (dispatch, isLoaded) => {
  const sensors = await await fetch(`api/sensors/getall`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const rooms = await fetch(`api/rooms/getall`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const racks = await fetch(`api/racks/getall`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const fans = await fetch(`api/fans/getall`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  dispatch({ type: receiveAdminType, sensors, isLoaded, rooms, racks, fans });
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestAdminType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      hubConnection: action.hubConnection,
      trueFalseFormatter: action.trueFalseFormatter
    };
  }

  if (action.type === receiveAdminType) {
    return {
      ...state,
      sensors: action.sensors,
      isLoading: false,
      isLoaded: action.isLoaded,
      rooms: action.rooms,
      racks: action.racks,
      fans: action.fans
    };
  }

  return state;
};
