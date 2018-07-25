const requestSensorsType = "REQUEST_SENSORS";
const receiveSensorsType = "RECEIVE_SENSORS";
const initialState = { sensors: [], isLoading: false };

export const actionCreators = {
  requestSensors: isLoaded => async (dispatch, getState) => {
    console.log(isLoaded);
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
