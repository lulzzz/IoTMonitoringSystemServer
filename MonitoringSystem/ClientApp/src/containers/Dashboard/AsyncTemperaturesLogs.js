import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchTemperaturesIfNeeded } from "../../actions/Dashboard/TemperaturesLogsActions";
import Temperatures from "../../components/Dashboard/TemperaturesLogs";
import LoadingIcon from "../../assets/img/loading-animation2.gif";

class AsyncTemperaturesLogs extends Component {
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
            <Temperatures temperatures={temperaturesArray} />
          </div>
        )}
        <div id="temperatures" />
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { temperatureList } = state.temperaturesLogsReducer;
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

AsyncTemperaturesLogs.propTypes = {
  //temperatures: PropTypes.array.isRequired,
  isFetching: PropTypes.bool.isRequired,
  lastUpdated: PropTypes.number,
  dispatch: PropTypes.func.isRequired
};

export default connect(mapStateToProps)(AsyncTemperaturesLogs);
