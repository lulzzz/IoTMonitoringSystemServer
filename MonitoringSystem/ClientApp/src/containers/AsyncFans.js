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
        const {fansArray, isFetching} = this.props
        // console.log(this.props)
        return (
            <div>
                {isFetching && fansArray.length === 0 && <img src={LoadingIcon}/>}
                {fansArray !== undefined && <Fans fans={fansArray}/>}                
            </div>
        )
    }

}

function mapStateToProps(state) {
    // console.log
    const {fanList} = state.fansReducer
    const {isFetching, lastUpdated, items: fans} = fanList || {
        isFetching: true,
        items: []
    }
    var fansArray = [];
    if(fanList.fans != undefined){
        fansArray = fanList.fans.items
    }
    
    // console.log(fanList.fans)

    return { fansArray, isFetching, lastUpdated}
}

AsyncFans.propTypes = {
    isFetching: PropTypes.bool.isRequired,
    lastUpdated: PropTypes.number,
    dispatch: PropTypes.func.isRequired
}

export default connect(mapStateToProps)(AsyncFans)