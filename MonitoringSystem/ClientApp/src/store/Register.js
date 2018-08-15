import { push } from "react-router-redux";
import * as authService from "../services/Authentication";

const requestRegisterType = "REQUEST_REGISTER";
const receiveRegisterType = "RECEIVE_REGISTER";
const initialState = {
  isLoading: false,
  errorMessage: []
};

export const actionCreators = {
  requestRegister: isLoaded => async (dispatch, getState) => {
    if (isLoaded === getState().register.isLoaded) {
      // Don't issue a duplicate request (we already have or are loading the requested
      // data)
      return;
    }

    dispatch({
      type: requestRegisterType,
      isLoaded
    });

    const errorMessage = getState().register.errorMessage;
    loadData(dispatch, isLoaded, errorMessage);
  },

  register: user => async (dispatch, getState) => {
    var errorMessage = "";
    if (user.password.localeCompare(user.confirmPassword) != 0) {
      errorMessage = "Password does not match the confirm password.";
    } else {
      var res = await fetch(`api/accounts/register`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + authService.getLoggedInUser().access_token
        },
        body: JSON.stringify(user)
      });
      console.log(res);
      if (res.status == 200) {
        dispatch(push("/login"));
      } else {
        errorMessage = "Could not register. Some error were happen";
      }
    }
    console.log(errorMessage);
    const isLoaded = getState().login.isLoaded;
    loadData(dispatch, isLoaded, errorMessage);
  }
};

export const loadData = async (dispatch, isLoaded, errorMessage) => {
  dispatch({
    type: receiveRegisterType,
    isLoaded,
    errorMessage
  });
};

export const reducer = (state, action) => {
  state = state || initialState;
  if (action.type === requestRegisterType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded
    };
  }

  if (action.type === receiveRegisterType) {
    return {
      ...state,
      isLoading: false,
      isLoaded: action.isLoaded,
      errorMessage: action.errorMessage
    };
  }

  return state;
};
