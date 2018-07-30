import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {connect} from 'react-redux'
import {fetchHumiditiesIfNeeded} from '../actions/HumiditiesActions'
import Humidities from '../components/Humidities'
import LoadingIcon from '../assets/img/loading-animation2.gif'

class AsyncHumidities extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const {dispatch} = this.props
        dispatch(fetchHumiditiesIfNeeded())
    }

    render() {
        const {humiditiesArray, isFetching} = this.props
        return (

            <div>
                {isFetching
                    ? <div className='text-center'><img src={LoadingIcon}/></div>
                    : <div>
                        <Humidities humidities={humiditiesArray}/>
                    </div>}
                <div id="humidities"></div>
            </div>
        )
    }

}

function mapStateToProps(state) {
    const {humidityList} = state.humiditiesReducer
    const {isFetching, lastUpdated, items: humidities} = humidityList.humidities || {
        isFetching: true,
        items: []
    }
    var humiditiesArray = [];
    if (humidityList.humidities != undefined) {
        humiditiesArray = humidityList.humidities.items
    }
    return {humiditiesArray, isFetching, lastUpdated}
}

AsyncHumidities.propTypes = {
    humidities: PropTypes.array.isRequired,
    isFetching: PropTypes.bool.isRequired,
    lastUpdated: PropTypes.number,
    dispatch: PropTypes.func.isRequired
}

export default connect(mapStateToProps)(AsyncHumidities)