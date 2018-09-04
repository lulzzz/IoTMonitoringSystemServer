import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchTemperaturesIfNeeded } from "../../actions/Dashboard/TemperaturesRealtimeActions";
import TemperaturesRealtime from "../../components/Dashboard/TemperaturesRealtime";
import LoadingIcon from "../../assets/img/loading-animation2.gif";

class AsyncTemperaturesRealtime extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    const { dispatch } = this.props;
    dispatch(fetchTemperaturesIfNeeded());
  }

  render() {
    const { temperaturesArray, isFetching } = this.props;
    return (
      <div>
        {isFetching ? (
          <div className="text-center">
            <img src={LoadingIcon} />
          </div>
        ) : (
          <div>
            <TemperaturesRealtime temperatures={temperaturesArray} />
          </div>
        )}
        <div id="temperaturesRt" />
      </div>
    );
  }
}

function mapStateToProps(state) {
  console.log(state);
  const { temperatureList } = state.temperaturesRealtimeReducer;
  const {
    isFetching,
    lastUpdated,
    items: temperatures
  } = temperatureList.temperatures || {
    isFetching: true,
    items: []
  };
  var temperaturesArray = [];
  if (temperatureList.temperatures != undefined) {
    temperaturesArray = temperatureList.temperatures.items;
  }
  return { temperaturesArray, isFetching, lastUpdated };
}

AsyncTemperaturesRealtime.propTypes = {
  //temperatures: PropTypes.array.isRequired,
  isFetching: PropTypes.bool.isRequired,
  lastUpdated: PropTypes.number,
  dispatch: PropTypes.func.isRequired
};

export default connect(mapStateToProps)(AsyncTemperaturesRealtime);
