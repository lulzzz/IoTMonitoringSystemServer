import React, { Component } from "react";
import { connect } from "react-redux";
import { actionCreators } from "../store/Fans";
import { bindActionCreators } from "redux";

class Fan extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = true;
    this.props.requestFans(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    console.log("componentWillReceiveProps");
    const isLoaded = true;
    this.props.requestFans(isLoaded);
  }

  render() {
    return (
      <div>
        {this.props.fans.items &&
          this.props.fans.items.map(item => (
            <p key={item.fanId}>{item.fanName}</p>
          ))}
      </div>
    );
  }
}
export default connect(
  state => state.fans,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Fan);
