import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchFansIfNeeded, updateFanStatus } from "../actions/FansActions";
import Fans from "../components/Fans";
import LoadingIcon from "../assets/img/loading-animation2.gif";

class AsyncFans extends Component {
  constructor(props) {
    super(props);
    this._handleChange = this._handleChange.bind(this);
  }

  componentDidMount() {
    const { dispatch } = this.props;
    dispatch(fetchFansIfNeeded());
  }

  componentDidUpdate(prevProps) {
    if (this.props.fans !== prevProps.fans) {
      const { dispatch } = this.props;
      dispatch(fetchFansIfNeeded());
    }
  }

  _handleChange(fan, fans) {
    console.log(fans);
    fan.isOn = !fan.isOn;
    this.props.dispatch(updateFanStatus(fan, fans));
    // this.props.dispatch(fetchFansIfNeeded());
  }

  render() {
    const { fansArray, isFetching } = this.props;
    // console.log(this.props)
    return (
      <div>
        {isFetching && <img src={LoadingIcon} alt="loading-icon" />}
        {fansArray !== undefined && (
          <Fans fans={fansArray} onChange={this._handleChange} />
        )}
      </div>
    );
  }
}

AsyncFans.propTypes = {
  isFetching: PropTypes.bool.isRequired,
  lastUpdated: PropTypes.number,
  dispatch: PropTypes.func.isRequired
};

function mapStateToProps(state) {
  // console.log
  const { fanList } = state.fansReducer;
  const { isFetching, lastUpdated, items: fans } = fanList.fans || {
    isFetching: true,
    items: []
  };
  var fansArray = [];
  if (fanList.fans !== undefined) {
    fansArray = fanList.fans.items;
  }

  return { fansArray, isFetching, lastUpdated };
}

export default connect(mapStateToProps)(AsyncFans);
