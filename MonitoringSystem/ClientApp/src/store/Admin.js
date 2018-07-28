const requestSensorsType = "REQUEST_SENSORS";
const receiveSensorsType = "RECEIVE_SENSORS";
const addSensorsType = "ADD_SENSORS";
const initialState = { sensors: [], racks: [], rooms: [], isLoading: false };

export const actionCreators = {
  requestSensors: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().admin.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({ type: requestSensorsType, isLoaded });
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

  dispatch({ type: receiveSensorsType, sensors, isLoaded, rooms, racks });
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestSensorsType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded
    };
  }

  if (action.type === receiveSensorsType) {
    return {
      ...state,
      sensors: action.sensors,
      isLoading: false,
      isLoaded: action.isLoaded,
      rooms: action.rooms,
      racks: action.racks
    };
  }

  return state;
};
