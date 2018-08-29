import * as signalR from "@aspnet/signalr";
import * as dataService from "../services/DataService";
import * as authService from "../services/Authentication";
import { push } from "react-router-redux";

const requestAdminType = "REQUEST_ADMINS";
const receiveAdminType = "RECEIVE_ADMINS";
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
    //check if user dont log in
    if (!authService.isUserAuthenticated()) {
      dispatch(push("/"));
    } else {
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
    }
  },

  addRacks: data => async (dispatch, getState) => {
    console.log("addRacks");

    delete data.rackId;
    await dataService.post(`api/racks/add`, data);
    loadData(dispatch);
  },

  addRooms: data => async (dispatch, getState) => {
    console.log("addRooms");

    delete data.roomId;
    await dataService.post(`api/rooms/add`, data);

    loadData(dispatch);
  },

  addSensors: data => async (dispatch, getState) => {
    console.log("addSensors");

    delete data.sensorId;
    await dataService.post(`api/sensors/add`, data);

    loadData(dispatch);
  },

  addFans: data => async (dispatch, getState) => {
    console.log("addFans");

    delete data.fanId;
    await dataService.post(`api/fans/add`, data);

    loadData(dispatch);
  },

  updateRacks: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRacks");
    var rackId = data.rackId;
    data[fieldName] = value;

    data = {
      rackId: data.rackId,
      rackCode: data.rackCode,
      rackName: data.rackName,
      location: data.location
    };

    console.log(data);
    await dataService.put(`api/racks/update/` + rackId, data);

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
    await dataService.put(`api/rooms/update/` + roomId, data);

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

    await dataService.put(`api/sensors/update/` + sensorId, data);

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
    await dataService.put(`api/fans/update/` + fanId, data);

    loadData(dispatch);
  },

  deleteRacks: rackId => async (dispatch, getState) => {
    await dataService.remove(`api/racks/delete/` + rackId);

    loadData(dispatch);
  },

  deleteRooms: roomId => async (dispatch, getState) => {
    await dataService.remove(`api/rooms/delete/` + roomId);

    loadData(dispatch);
  },

  deleteSensors: sensorId => async (dispatch, getState) => {
    await dataService.remove(`api/sensors/delete/` + sensorId);

    loadData(dispatch);
  },

  deleteFans: fanId => async (dispatch, getState) => {
    console.log('delete fan');
    console.log(fanId);
    await dataService.remove(`api/fans/delete/` + fanId);

    loadData(dispatch);
  }
};

export const loadData = async (dispatch, isLoaded) => {
  const sensors = await dataService.get(`api/sensors/getall`);

  const rooms = await dataService.get(`api/rooms/getall`);

  const racks = await dataService.get(`api/racks/getall`);

  racks.items.sort(function(rack1, rack2) {
    // if (rack1.roomName > rack2.roomName) {
    //   return -1;
    // }
    // if (rack1.roomName < rack2.roomName) {
    //   return 1;
    // }

    if (
      parseInt(rack1.rackCode.substring(1, rack1.rackCode.length)) >
      parseInt(rack2.rackCode.substring(1, rack2.rackCode.length))
    ) {
      return -1;
    }
    if (
      parseInt(rack1.rackCode.substring(1, rack1.rackCode.length)) <
      parseInt(rack2.rackCode.substring(1, rack2.rackCode.length))
    ) {
      return 1;
    }
  });

  const fans = await dataService.get(`api/fans/getall`);

  dispatch({
    type: receiveAdminType,
    sensors,
    isLoaded,
    rooms,
    racks,
    fans
  });
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
