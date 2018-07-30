const requestMapsType = "REQUEST_SENSORS";
const receiveMapsType = "RECEIVE_SENSORS";
const initialState = { popovers: [], isLoading: false };

export const actionCreators = {
  requestMaps: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().admin.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({ type: requestMapsType, isLoaded });

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();

    var popovers = [];
    for (let sensor of sensors.items) {
      popovers.push({
        placement: sensor.latestStatus.placement,
        text: sensor.latestStatus.text,
        sensor: sensor.latestStatus.sensor,
        temperature: sensor.latestStatus.temperature,
        humidity: sensor.latestStatus.humidity
      });
    }
    popovers = popovers.reverse();

    dispatch({ type: receiveMapsType, isLoaded, popovers });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestMapsType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded
    };
  }

  if (action.type === receiveMapsType) {
    return {
      ...state,
      popovers: action.popovers,
      isLoading: false,
      isLoaded: action.isLoaded
    };
  }

  return state;
};
