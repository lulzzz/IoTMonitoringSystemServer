const requestSensorsType = "REQUEST_SENSORS";
const receiveSensorsType = "RECEIVE_SENSORS";
const addSensorsType = "ADD_SENSORS";
const initialState = { sensors: [], isLoading: false };

export const actionCreators = {
  requestSensors: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().admin.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({ type: requestSensorsType, isLoaded });

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();

    dispatch({ type: receiveSensorsType, sensors, isLoaded });
  },

  addSensors: data => async (dispatch, getState) => {
    console.log("addSensors");
    // data = {
    //   roomName: data.roomName,
    //   sensorCode: data.sensorCode,
    //   sensorName: data.sensorName
    // };
    delete data.sensorId;
    await fetch(`api/sensors/add`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();

    dispatch({ type: receiveSensorsType, sensors });
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

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();

    dispatch({ type: receiveSensorsType, sensors });
  },

  deleteSensors: sensorId => async (dispatch, getState) => {
    await fetch(`api/sensors/delete/` + sensorId, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      }
    });

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();

    dispatch({ type: receiveSensorsType, sensors });
  }
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
      isLoaded: action.isLoaded
    };
  }

  return state;
};
