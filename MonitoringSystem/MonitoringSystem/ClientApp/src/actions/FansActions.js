import fetch from 'cross-fetch'

export const requestFansType = "REQUEST_FANS";
export const receiveFansType = "RECEIVE_FANS";
export const updateFanStatusType = "UPDATE_FAN_STATUS";

export function updateFanStatus(fan) {
    const url = `api/fans/update/` + fan.fanId;
    console.log('url')
    // const response = await fetch(url, {
    //     method: "PUT",
    //     headers: {
    //         Accept: "application/json",
    //         "Content-Type": "application/json"
    //     },
    //     body: JSON.stringify(fan)
    // });

    // console.log(response)

    // const url2 = `api/fans/getall`;
    // const response2 = await fetch(url2);
    // const fans = await response2.json();
    // dispatch({type: receiveFansType, isLoaded, isLoading: false, fans});
    return;
    // return {
    //   type: updateFanStatusType,
    //   fan
    // }
  }

function requestFans() {
    return {type: requestFansType}
}

function receiveFans(json) {
    return {
        type: receiveFansType,
        fans: json.items,
        receivedAt: Date.now()
    }
}

function fetchFans() {
    return dispatch => {
        dispatch(requestFans())
        return fetch(`/api/fans/getall`)
            .then(response => response.json())
            .then(json => dispatch(receiveFans(json)))
    }
}

function shouldFetchFans(state) {
    const fans = state.fanList
    console.log('should')
    console.log(state)
    if (!fans) {
        return true
    } else if (fans.isFetching) {
        return false
    } else {
        console.log('error')
        return false
    }
}

// export function updateFans (state, fan) {
//     if (isLoaded === getState().fans.isLoaded) {
//         // Don't issue a duplicate request (we already have or are loading the requested
//         // data)
//         return;
//     }

//     dispatch({type: requestFansType, isLoaded, isLoading: true});
//     const url = `api/fans/update/` + fan.fanId;

//     const response = await fetch(url, {
//         method: "PUT",
//         headers: {
//             Accept: "application/json",
//             "Content-Type": "application/json"
//         },
//         body: JSON.stringify(fan)
//     });
//     console.log(response)

//     const url2 = `api/fans/getall`;
//     const response2 = await fetch(url2);
//     const fans = await response2.json();
//     dispatch({type: receiveFansType, isLoaded, isLoading: false, fans});
//     return;
// }

export function fetchFansIfNeeded() {
    return (dispatch, getState) => {
        if (shouldFetchFans(getState(),)) {
            return dispatch(fetchFans())
        }
    }
}