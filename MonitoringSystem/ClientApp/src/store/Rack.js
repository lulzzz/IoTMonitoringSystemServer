import * as signalR from "@aspnet/signalr";

const requestRackType = "REQUEST_RACKS";
const receiveRackType = "RECEIVE_RACKS";
const initialState = {
  rack: [],
  isLoading: false,
  statuses: [],
  racks: [],
  hubConnection: null
};

export const actionCreators = {
  requestRack: (isLoaded, rackId) => async (dispatch, getState) => {
    if (isLoaded === getState().rack.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    var hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/hub")
      .build();

    hubConnection.on("LoadData", () => {
      loadData(dispatch, rackId, isLoaded);
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
      type: requestRackType,
      isLoaded,
      hubConnection,
      rackId
    });

    loadData(dispatch, rackId, isLoaded);
  }
};

export const loadData = async (dispatch, rackId, isLoaded) => {
  const rack = await await fetch(`api/racks/getrack/${rackId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  const statuses = await fetch(`api/statuses/getall?rackId=${rackId}`, {
    method: "GET"
  }).then(function(response) {
    return response.json();
  });

  dispatch({
    type: receiveRackType,
    isLoaded,
    rack,
    statuses
  });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestRackType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      rackId: action.rackId,
      hubConnection: action.hubConnection
    };
  }

  if (action.type === receiveRackType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      statuses: action.statuses,
      rack: action.rack
    };
  }

  return state;
};
