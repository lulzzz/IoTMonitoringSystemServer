const requestSensorType = "REQUEST_SENSORS";
const receiveSensorType = "RECEIVE_SENSORS";
const initialState = {
  sensor: [],
  isLoading: false,
  statuses: []
};

export const actionCreators = {
  requestSensor: (isLoaded, sensorId) => async (dispatch, getState) => {
    if (isLoaded === getState().sensor.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({
      type: requestSensorType,
      isLoaded,
      sensorId
    });

    loadData(dispatch, isLoaded, sensorId);
  }
};

export const loadData = async (dispatch, isLoaded, sensorId) => {
  const sensor = await await fetch(`api/sensors/getsensor/${sensorId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const statuses = await await fetch(
    `api/statuses/getall?sensorId=${sensorId}`,
    {
      method: "GET"
    }
  ).then(function(response) {
    return response.json();
  });

  dispatch({ type: receiveSensorType, isLoaded, sensor, statuses });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestSensorType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      sensorId: action.sensorId
    };
  }

  if (action.type === receiveSensorType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      sensor: action.sensor,
      statuses: action.statuses
    };
  }

  return state;
};
