import * as signalR from "@aspnet/signalr";
import * as authService from "../services/Authentication";

const requestAccountType = "REQUEST_ACCOUNTS";
const receiveAccountType = "RECEIVE_ACCOUNTS";
const initialState = {
  accounts: [],
  isLoading: false,
  hubConnection: null
};

export const actionCreators = {
  requestAccount: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().account.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

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
      type: requestAccountType,
      isLoaded,
      hubConnection
    });
    loadData(dispatch, isLoaded);
  }
};

export const loadData = async (dispatch, isLoaded) => {
  const accounts = await await fetch(`api/accounts/getall`, {
    method: "GET",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
      Authorization: "Bearer " + authService.getLoggedInUser().access_token
    }
  }).then(function(response) {
    return response.json();
  });

  dispatch({ type: receiveAccountType, isLoaded, accounts });
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestAccountType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      hubConnection: action.hubConnection
    };
  }

  if (action.type === receiveAccountType) {
    return {
      ...state,
      accounts: action.accounts,
      isLoading: false,
      isLoaded: action.isLoaded
    };
  }

  return state;
};
