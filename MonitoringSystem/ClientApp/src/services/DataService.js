import * as authService from "./Authentication";
import * as constant from "./Constant";

function getHeader() {
  var header = {
    Accept: "application/json",
    "Content-Type": "application/json",
    Authorization: "Bearer " + authService.getLoggedInUser().access_token
  };
  return header;
}

function getHeaderWithoutBearer() {
  var header = {
    Accept: "application/json",
    "Content-Type": "application/json"
  };
  return header;
}
// const header = {
//   Accept: "application/json",
//   "Content-Type": "application/json",
//   Authorization: "Bearer " + authService.getLoggedInUser().access_token
// };

// const headerWithoutBearer = {
//   Accept: "application/json",
//   "Content-Type": "application/json"
// };

export const login = async data => {
  return await fetch(constant.BASE_URL + "api/accounts/generatetoken", {
    method: "POST",
    headers: getHeaderWithoutBearer(),
    body: JSON.stringify(data)
  });
};

export const get = async url => {
  console.log(constant.BASE_URL + url);
  return await fetch(constant.BASE_URL + url, {
    method: "GET",
    headers: getHeader()
  }).then(function(response) {
    return response.json();
  });
};

export const post = async (url, data) => {
  return await fetch(constant.BASE_URL + url, {
    method: "POST",
    headers: getHeader(),
    body: JSON.stringify(data)
  }).then(function(response) {
    return response.json();
  });
};

export const put = async (url, data) => {
  return await fetch(constant.BASE_URL + url, {
    method: "PUT",
    headers: getHeader(),
    body: JSON.stringify(data)
  }).then(function(response) {
    return response.json();
  });
};

export const remove = async url => {
  return await fetch(constant.BASE_URL + url, {
    method: "DELETE",
    headers: getHeader()
  }).then(function(response) {
    return response.json();
  });
};
