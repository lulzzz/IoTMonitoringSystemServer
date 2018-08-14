import * as authService from "../services/Authentication";

const requestTemperaturesType = "REQUEST_WEATHER_FORECASTS";
const receiveTemperaturesType = "RECEIVE_WEATHER_FORECASTS";
const initialState = {
  temperatures: [],
  isLoading: false
};

export const actionCreators = {
  requestTemperatures: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().temperatures.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({ type: requestTemperaturesType, isLoaded });

    const temperatures = await fetch(`api/plots/temperature/getall`, {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + authService.getLoggedInUser().access_token
      }
    }).then(function(response) {
      return response.json();
    });

    dispatch({ type: receiveTemperaturesType, isLoaded, temperatures });
    return;
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestTemperaturesType) {
    return {
      ...state,
      isLoaded: action.isLoaded,
      isLoading: true
    };
  }

  if (action.type === receiveTemperaturesType) {
    return {
      ...state,
      isLoaded: action.isLoaded,
      temperatures: action.temperatures,
      isLoading: false
    };
  }

  return state;
};
