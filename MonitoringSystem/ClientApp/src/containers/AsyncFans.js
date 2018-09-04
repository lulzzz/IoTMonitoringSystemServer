import React, { Component } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { fetchFansIfNeeded, updateFanStatus } from "../actions/FansActions";
import Fans from "../components/Fans";
import LoadingIcon from "../assets/img/loading-animation2.gif";
import { Grid } from "semantic-ui-react";

function SvgIcon(props) {
  return (
    <svg
      width="30%"
      height="100%"
      xmlns="http://www.w3.org/2000/svg"
      viewBox="0 0 100 100"
      preserveAspectRatio="xMidYMid"
      class="lds-stripe"
    >
      <defs>
        <pattern
          ng-attr-id="{{config.patid}}"
          patternUnits="userSpaceOnUse"
          x="0"
          y="0"
          width="100"
          height="100"
          id="lds-stripe-patid-bc74de1ad4246"
        >
          <g transform="translate(9.17415 0)">
            <g transform="rotate(20 50 50) scale(1.2)">
              <rect
                x="-20"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="-10"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="0"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="10"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="20"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="30"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="40"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="50"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="60"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="70"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="80"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="90"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c2}}"
                fill="#eeeced"
              />
              <rect
                x="100"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
              <rect
                x="110"
                y="-10"
                width="10"
                height="120"
                ng-attr-fill="{{config.c1}}"
                fill="#45e7ca"
              />
            </g>
            <animateTransform
              attributeName="transform"
              type="translate"
              values="0 0;26 0"
              keyTimes="0;1"
              ng-attr-dur="{{config.speed}}s"
              repeatCount="indefinite"
              dur="1s"
            />
          </g>
        </pattern>
      </defs>
      <rect
        ng-attr-rx="{{config.r}}"
        ng-attr-ry="{{config.r}}"
        ng-attr-x="{{config.x}}"
        ng-attr-y="{{config.y}}"
        ng-attr-stroke="{{config.stroke}}"
        ng-attr-stroke-width="{{config.strokeWidth}}"
        ng-attr-width="{{config.width}}"
        ng-attr-height="{{config.height}}"
        ng-attr-fill="url(#{{config.patid}})"
        rx="10"
        ry="10"
        x="10"
        y="40"
        stroke="#898989"
        stroke-width="3"
        width="80"
        height="20"
        fill="url(#lds-stripe-patid-bc74de1ad4246)"
      />
    </svg>
  );
}

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
        {isFetching && (
          <Grid centered columns={2}>
            <SvgIcon center />
          </Grid>
        )}
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
