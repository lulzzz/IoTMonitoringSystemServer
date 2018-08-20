import * as authService from "./Authentication";

const header = {
  Accept: "application/json",
  "Content-Type": "application/json",
  Authorization: "Bearer " + authService.getLoggedInUser().access_token
};

const headerWithoutBearer = {
  Accept: "application/json",
  "Content-Type": "application/json"
};

export const login = async data => {
  return await fetch("api/accounts/generatetoken", {
    method: "POST",
    headers: headerWithoutBearer,
    body: JSON.stringify(data)
  });
};

export const get = async url => {
  return await fetch(url, {
    method: "GET",
    headers: header
  }).then(function(response) {
    return response.json();
  });
};

export const post = async (url, data) => {
  return await fetch(url, {
    method: "POST",
    headers: header,
    body: JSON.stringify(data)
  }).then(function(response) {
    return response.json();
  });
};

export const put = async (url, data) => {
  return await fetch(url, {
    method: "PUT",
    headers: header,
    body: JSON.stringify(data)
  }).then(function(response) {
    return response.json();
  });
};

export const remove = async url => {
  return await fetch(url, {
    method: "DELETE",
    headers: header
  }).then(function(response) {
    return response.json();
  });
};
