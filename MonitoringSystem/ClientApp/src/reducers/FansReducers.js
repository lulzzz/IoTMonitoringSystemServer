import {combineReducers} from 'redux'
import {requestFansType, receiveFansType, updateFanStatusType, invalidateUpdateFanType} from '../actions/FansActions'

function updatedFanStatus(state = '', action) {
    console.log('aaa')
    switch (action.type) {
        case updateFanStatusType:
        console.log(action)
          return {fans: action.fans}
        default:
          return state
      }
}

function fans(state = {
    isFetching: false,
    items: [],
    didInvalidate: false
}, action) {
    switch (action.type) {
        case invalidateUpdateFanType:
            return {
                ...state,
                didInvalidate: true
            }
            // case updateFanStatusType:     return {         ...state,
            // didInvalidate: false     }
        case requestFansType:
            return {
                ...state,
                isFetching: true,
                didInvalidate: false
            }
        case receiveFansType:
            return {
                ...state,
                isFetching: false,
                didInvalidate: false,
                items: action.fans,
                lastUpdated: action.receivedAt
            }
        default:
            return state
    }
}

function fanList( state= {}, action) {
    console.log(state)
    console.log(action)
    switch (action.type) {
        case invalidateUpdateFanType:
        case receiveFansType:
        case requestFansType:
            return {
                ...state,
                fans: fans(state, action),
                action
            }
        default:
            return state
    }
}

const fansReducer = combineReducers({fanList, updatedFanStatus})

export default fansReducer