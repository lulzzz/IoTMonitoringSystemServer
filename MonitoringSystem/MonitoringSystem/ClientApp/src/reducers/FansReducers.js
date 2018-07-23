import {combineReducers} from 'redux'
import {requestFansType, receiveFansType, updateFanStatusType} from '../actions/FansActions'

function updateFanStatus(state = 'reactjs', action) {
    switch (action.type) {
        case updateFanStatusType:
            return action.fan
        default:
            return state
    }
}

function fans(state = {
    isFetching: false,
    items: []
}, action) {
    console.log(action)
    switch (action.type) {
        case updateFanStatusType:
            return Object.assign({}, state)
        case requestFansType:
            return Object.assign({}, state, {isFetching: true})
        case receiveFansType:
            console.log('receive2')
            return Object.assign({}, state, {
                isFetching: false,
                items: action.fans,
                lastUpdated: action.receivedAt
            })
        default:
            return state
    }
    console.log(state)
}

function fanList(state = {}, action) {

    switch (action.type) {
        case updateFanStatusType:
        case receiveFansType:
        case requestFansType:
            return Object.assign({}, state, {
                [action]: fans(state, action)
            })
        default:
            return state
    }
}

const fansReducer = combineReducers({fanList, updateFanStatus})

export default fansReducer