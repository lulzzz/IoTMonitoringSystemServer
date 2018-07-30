import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {connect} from 'react-redux'
import {fetchTemperaturesIfNeeded} from '../actions/TemperaturesActions'
import Temperatures from '../components/Temperatures'
import LoadingIcon from '../assets/img/loading-animation2.gif'

class AsyncTemperatures extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const {dispatch} = this.props
        dispatch(fetchTemperaturesIfNeeded())
    }

    render() {
        const {temperaturesArray, isFetching} = this.props
        return (

            <div>
                {/* {isFetching && <h2>Loading...</h2>}
                {temperaturesArray !== undefined &&} */}
                {isFetching
                    ? <div className='text-center'><img src={LoadingIcon}/></div>
                    : <div>
                        <Temperatures temperatures={temperaturesArray}/>
                    </div>}
                <div id="temperatures"></div>
            </div>
        )
    }

}

function mapStateToProps(state) {
    const {temperatureList} = state.temperaturesReducer
    const {isFetching, lastUpdated, items: temperatures} = temperatureList.temperatures || {
        isFetching: true,
        items: []
    }
    var temperaturesArray = [];
    if (temperatureList.temperatures != undefined) {
        temperaturesArray = temperatureList.temperatures.items
    }
    return {temperaturesArray, isFetching, lastUpdated}
}

AsyncTemperatures.propTypes = {
    temperatures: PropTypes.array.isRequired,
    isFetching: PropTypes.bool.isRequired,
    lastUpdated: PropTypes.number,
    dispatch: PropTypes.func.isRequired
}

export default connect(mapStateToProps)(AsyncTemperatures)