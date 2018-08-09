import * as signalR from "@aspnet/signalr";

const requestRoomType = "REQUEST_ROOMS";
const receiveRoomType = "RECEIVE_ROOMS";
const initialState = {
  room: [],
  isLoading: false,
  racks: [],
  sensors: [],
  hubConnection: null
};

export const actionCreators = {
  requestRoom: (isLoaded, roomId) => async (dispatch, getState) => {
    if (isLoaded === getState().room.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    var hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/hub")
      .build();

    hubConnection.on("LoadData", () => {
      loadData(dispatch, roomId, isLoaded);
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
      type: requestRoomType,
      isLoaded,
      hubConnection,
      roomId
    });

    loadData(dispatch, roomId, isLoaded);
  },

  addRacks: data => async (dispatch, getState) => {
    console.log("addRacks");
    const roomId = getState().room.roomId;
    data.roomId = roomId;
    delete data.rackId;
    await fetch(`api/racks/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch, roomId);
  },

  updateRacks: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRacks");
    const roomId = getState().room.roomId;
    var rackId = data.rackId;
    data[fieldName] = value;

    data = {
      rackCode: data.rackCode,
      rackName: data.rackName,
      location: data.location,
      sensorId: data.sensorId,
      roomId: roomId
    };

    var res = await fetch(`api/racks/update/` + rackId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch, roomId);
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
    const roomId = getState().room.roomId;
    loadData(dispatch, roomId);
  },

  addSensors: data => async (dispatch, getState) => {
    console.log("addSensors");
    const roomId = getState().room.roomId;
    data.roomId = roomId;
    delete data.sensorId;
    await fetch(`api/sensors/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch, roomId);
  },

  updateSensors: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateSensors");
    const roomId = getState().room.roomId;
    var sensorId = data.sensorId;
    data[fieldName] = value;

    data = {
      sensorCode: data.sensorCode,
      sensorName: data.sensorName,
      roomId: roomId
    };

    var res = await fetch(`api/sensors/update/` + sensorId, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    loadData(dispatch, roomId);
  },

  deleteSensors: sensorId => async (dispatch, getState) => {
    console.log("deleteSensors");
    await fetch(`api/sensors/delete/` + sensorId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });
    const roomId = getState().room.roomId;
    loadData(dispatch, roomId);
  }
};

export const loadData = async (dispatch, roomId, isLoaded) => {
  const room = await await fetch(`api/rooms/getroom/${roomId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const racks = await fetch(`api/racks/getall?roomId=${roomId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });
  racks.items.sort(function(rack1, rack2) {
    if (rack1.roomName > rack2.roomName) return -1;
    if (rack1.roomName < rack2.roomName) return 1;

    if (
      parseInt(rack1.rackCode.substring(1, rack1.rackCode.length)) >
      parseInt(rack2.rackCode.substring(1, rack1.rackCode.length))
    )
      return -1;
    if (
      parseInt(rack1.rackCode.substring(1, rack1.rackCode.length)) <
      parseInt(rack2.rackCode.substring(1, rack1.rackCode.length))
    )
      return 1;
  });

  const sensors = await fetch(`api/sensors/getall?roomId=${roomId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  dispatch({
    type: receiveRoomType,
    isLoaded,
    room,
    racks,
    sensors
  });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestRoomType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      roomId: action.roomId,
      hubConnection: action.hubConnection
    };
  }

  if (action.type === receiveRoomType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      room: action.room,
      racks: action.racks,
      sensors: action.sensors
    };
  }

  return state;
};
