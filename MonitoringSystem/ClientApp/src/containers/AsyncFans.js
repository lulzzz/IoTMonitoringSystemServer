import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {connect} from 'react-redux'
import {fetchFansIfNeeded} from '../actions/FansActions'
import Fans from '../components/Fans'
import LoadingIcon from '../assets/img/loading-animation2.gif'

class AsyncFans extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const {dispatch} = this.props
        dispatch(fetchFansIfNeeded())
    }

    render() {
        const {fansArray, isFetching, updateFanStatus} = this.props

        return (
            console.log('2398721983792817'),
            <div>
                {isFetching
                    ? <div className='text-center'><img src={LoadingIcon}/></div>
                    : <div>
                        <Fans fans={fansArray}/>
                    </div>}
            </div>
        )
    }

}

function mapStateToProps(state) {
    console.log(state)
    const {fanList} = state.fansReducer
    const {isFetching, lastUpdated, items: fans} = fanList.fans || {
        isFetching: true,
        items: []
    }
    var fansArray = [];
    const updateFanStatus = state.updateFanStatus
    if (fanList.fans != undefined) {
        fansArray = fanList.fans.items
    }
    
    return {fansArray, isFetching, lastUpdated, updateFanStatus}
}

AsyncFans.propTypes = {
    isFetching: PropTypes.bool.isRequired,
    lastUpdated: PropTypes.number,
    dispatch: PropTypes.func.isRequired
}

export default connect(mapStateToProps)(AsyncFans)