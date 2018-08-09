const requestMapsType = "REQUEST_SENSORS";
const receiveMapsType = "RECEIVE_SENSORS";
const initialState = {
  popovers: [],
  isLoading: false
};

export const actionCreators = {
  requestMaps: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().admin.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({
      type: requestMapsType,
      isLoaded
    });

    const url = `api/sensors/getall`;
    const response = await fetch(url);
    const sensors = await response.json();
    var popovers = [];
    for (let sensor of sensors.items) {
      const rack = await await fetch(`api/racks/getrack/${sensor.racks[0]}`, {
        method: "GET"
      }).then(function(response) {
        return response.json();
      });
      popovers.push({
        key: rack.location,
        placement: sensor.latestStatus ? sensor.latestStatus.placement : "top",
        text: sensor.latestStatus
          ? sensor.latestStatus.text
          : sensor.sensorCode,
        sensor: sensor.latestStatus
          ? sensor.latestStatus.sensor
          : sensor.sensorName,
        temperature: sensor.latestStatus ? sensor.latestStatus.temperature : "",
        humidity: sensor.latestStatus ? sensor.latestStatus.humidity : ""
      });
    }
    popovers = popovers.reverse();
    dispatch({
      type: receiveMapsType,
      isLoaded,
      popovers
    });
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
