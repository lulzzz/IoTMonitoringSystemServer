import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchHumiditiesIfNeeded } from "../../actions/Dashboard/HumiditiesRealtimeActions";
import HumiditiesRealtime from "../../components/Dashboard/HumiditiesRealtime";
import LoadingIcon from "../../assets/img/loading-animation2.gif";

class AsyncHumiditiesRealtime extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    const { dispatch } = this.props;
    dispatch(fetchHumiditiesIfNeeded());
  }

  render() {
    const { humiditiesArray, isFetching } = this.props;
    return (
      <div>
        {isFetching ? (
          <div className="text-center">
            <img src={LoadingIcon} />
          </div>
        ) : (
          <div>
            <HumiditiesRealtime humidities={humiditiesArray} />
          </div>
        )}
        <div id="humiditiesRt" />
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { humidityList } = state.humiditiesRealtimeReducer;
  const {
    isFetching,
    lastUpdated,
    items: humidities
  } = humidityList.humidities || {
    isFetching: true,
    items: []
  };
  var humiditiesArray = [];
  if (humidityList.humidities != undefined) {
    humiditiesArray = humidityList.humidities.items;
  }
  return { humiditiesArray, isFetching, lastUpdated };
}

AsyncHumiditiesRealtime.propTypes = {
  //humidities: PropTypes.array.isRequired,
  isFetching: PropTypes.bool.isRequired,
  lastUpdated: PropTypes.number,
  dispatch: PropTypes.func.isRequired
};

export default connect(mapStateToProps)(AsyncHumiditiesRealtime);
