const requestFansType = "REQUEST_FANS";
const receiveFansType = "RECEIVE_FANS";
const initialState = { fans: [], isLoading: false };

export const actionCreators = {
  requestFans: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().fans.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }

    dispatch({ type: requestFansType, isLoaded });

    const url = `api/fans/getall`;
    const response = await fetch(url);
    const fans = await response.json();
    dispatch({
      type: receiveFansType,
      isLoaded,
      fans
    });
    return;
  },

  updateFans: (isLoaded, fan) => async (dispatch, getState) => {
    console.log("updateFans");
    console.log(isLoaded);
    console.log(getState().fans.isLoaded);
    if (isLoaded === getState().fans.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;
    }
    console.log("updateFans2");
    dispatch({ type: requestFansType, isLoaded });
    console.log("updateFans3");
    const url = `api/fans/update/` + fan.fanId;

    const response = await fetch(url, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(fan)
    });
    console.log(response);

    const url2 = `api/fans/getall`;
    const response2 = await fetch(url);
    const fans = await response.json();
    dispatch({
      type: receiveFansType,
      isLoaded,
      fans
    });
    return;
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestFansType) {
    return {
      ...state,
      isLoaded: action.isLoaded,
      isLoading: true
    };
  }

  if (action.type === receiveFansType) {
    return {
      ...state,
      isLoaded: action.isLoaded,
      fans: action.fans,
      isLoading: false
    };
  }

  return state;
};
