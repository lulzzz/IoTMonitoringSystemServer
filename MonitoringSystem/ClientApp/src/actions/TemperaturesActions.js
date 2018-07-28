import fetch from 'cross-fetch'

export const requestTemperaturesType = "REQUEST_TEMPERATURES";
export const receiveTemperaturesType = "RECEIVE_TEMPERATURES";

function requestTemperatures() {
    return {type: requestTemperaturesType}
}

function receiveTemperatures(json) {
    return {
        type: receiveTemperaturesType,
        temperatures: json.items,
        receivedAt: Date.now()
    }
}

function fetchTemperatures() {
    return dispatch => {
        dispatch(requestTemperatures())
        return fetch(`/api/plots/temperature/getall`)
            .then(response => response.json())
            .then(json => dispatch(receiveTemperatures(json)))
    }
}

function shouldFetchTemperatures(state) {
    const temperatures = state.temperatureList
    if (!temperatures) {
        return true
    } else if (temperatures.isFetching) {
        return false
    } else {
        return false
    }
}

export function fetchTemperaturesIfNeeded() {
    return (dispatch, getState) => {
        if (shouldFetchTemperatures(getState(),)) {
            return dispatch(fetchTemperatures())
        }
    }
}