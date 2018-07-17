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
    console.log(this.fans)
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
