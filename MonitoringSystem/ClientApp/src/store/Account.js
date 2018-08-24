import * as signalR from "@aspnet/signalr";
import * as dataService from "../services/DataService";

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
  },

  addAccount: data => async (dispatch, getState) => {
    console.log("addAccount");
    delete data.id;
    delete data.createdOn;
    delete data.updatedOn;

    const res = await dataService.post(`api/accounts/register`, data);
    const isLoaded = getState().account.isLoaded;
    loadData(dispatch, isLoaded);
  },

  updateAccount: (data, fieldName, value) => async (dispatch, getState) => {
    console.log("updateRacks");

    data[fieldName] = value;

    data = {
      email: data.email,
      phoneNumber: data.phoneNumber,
      fullName: data.fullName
    };
    console.log(data);
    const isLoaded = getState().account.isLoaded;
    var res = await dataService.put(`api/accounts/update/` + data.email, data);
    console.log(res);
    loadData(dispatch, isLoaded);
  },

  deleteAccount: email => async (dispatch, getState) => {
    console.log("deleteAccount");
    await dataService.remove(`api/accounts/delete/` + email);
    const isLoaded = getState().account.isLoaded;
    loadData(dispatch, isLoaded);
  }
};

export const loadData = async (dispatch, isLoaded) => {
  const accounts = await dataService.get(`api/accounts/getall`);

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
