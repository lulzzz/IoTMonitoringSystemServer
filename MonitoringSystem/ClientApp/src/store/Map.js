import * as dataService from "../services/DataService";
import * as signalR from "@aspnet/signalr";
import * as authService from "../services/Authentication";
import { push } from "react-router-redux";

const requestMapsType = "REQUEST_SENSORS";
const receiveMapsType = "RECEIVE_SENSORS";
const initialState = {
  popovers: [],
  isLoading: false,
  latestHumidity: [],
  latestTemperature: [],
  hubConnection: null
};

export const actionCreators = {
  requestMaps: isLoaded => async (dispatch, getState) => {
    //check if user dont log in
    if (!authService.isUserAuthenticated()) {
      dispatch(push("/"));
    } else {
      if (isLoaded === getState().admin.isLoaded) {
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
        type: requestMapsType,
        isLoaded,
        hubConnection
      });

      loadData(dispatch, isLoaded);
    }
  }
};

export const loadData = async (dispatch, isLoaded) => {
  const sensors = await dataService.get(`api/sensors/getall`);

  var popovers = [];
  for (let sensor of sensors.items) {
    if (sensor.racks !== null && sensor.racks.length !== 0) {
      const rack = await dataService.get(
        `api/racks/getrack/${sensor.racks[0]}`
      );

      popovers.push({
        key: rack.location,
        sensorId: sensor.sensorId ? sensor.sensorId : "",
        placement: sensor.latestStatus ? sensor.latestStatus.placement : "top",
        text: sensor.latestStatus
          ? sensor.latestStatus.text
          : sensor.sensorCode,
        sensor: sensor.latestStatus
          ? sensor.latestStatus.sensor
          : sensor.sensorName,
        temperature: sensor.latestStatus ? sensor.latestStatus.temperature : "",
        humidity: sensor.latestStatus ? sensor.latestStatus.humidity : ""
      });
    }
  }
  popovers = popovers.reverse();

  const latestHumidity = await dataService.get(`api/plots/getlatesthumidity`);

  const latestTemperature = await dataService.get(
    `api/plots/getlatesttemperature`
  );

  dispatch({
    type: receiveMapsType,
    isLoaded,
    popovers,
    latestHumidity,
    latestTemperature
  });
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestMapsType) {
    return {
      ...state,
      isLoading: true,
      isLoaded: action.isLoaded,
      hubConnection: action.hubConnection
    };
  }

  if (action.type === receiveMapsType) {
    return {
      ...state,
      popovers: action.popovers,
      isLoading: false,
      isLoaded: action.isLoaded,
      latestHumidity: action.latestHumidity,
      latestTemperature: action.latestTemperature
    };
  }

  return state;
};
