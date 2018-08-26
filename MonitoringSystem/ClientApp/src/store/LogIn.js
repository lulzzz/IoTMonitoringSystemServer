import * as dataService from "../services/DataService";
import * as authService from "../services/Authentication";
import { push } from "react-router-redux";

const requestLogInType = "REQUEST_LOGIN";
const receiveLogInType = "RECEIVE_LOGIN";
const initialState = {
  isLoading: false,
  errorMessage: []
};

export const actionCreators = {
  requestLogIn: isLoaded => async (dispatch, getState) => {
    //check if user logged in
    if (authService.isUserAuthenticated()) {
      dispatch(push("/dashboard"));
    } else {
      if (isLoaded === getState().login.isLoaded) {
        // Don't issue a duplicate request (we already have or are loading the requested
        // data)
        return;
      }

      dispatch({
        type: requestLogInType,
        isLoaded
      });
      loadData(dispatch, isLoaded);
    }
  },

  logIn: (email, password) => async (dispatch, getState) => {
    const data = {
      email: email,
      password: password
    };

    var res = await dataService.login(data);

    const jsonRespone = await res.json();

    if (jsonRespone && jsonRespone.access_token) {
      localStorage.removeItem("CURRENT_USER");
      localStorage.setItem("CURRENT_USER", JSON.stringify(jsonRespone));
      dispatch(push("/dashboard"));
    } else {
      const isLoaded = getState().login.isLoaded;
      const errorMessage = "You have entered an invalid username or password";
      loadData(dispatch, isLoaded, errorMessage);
    }
  }
};

export const loadData = async (dispatch, isLoaded, errorMessage) => {
  dispatch({
    type: receiveLogInType,
    isLoaded,
    errorMessage
  });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestLogInType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded
    };
  }

  if (action.type === receiveLogInType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      errorMessage: action.errorMessage
    };
  }

  return state;
};
