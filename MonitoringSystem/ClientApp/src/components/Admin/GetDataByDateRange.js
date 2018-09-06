import React, { Component } from "react";
import DateRangePicker from "react-bootstrap-daterangepicker";
// you will need the css that comes with bootstrap@3. if you are using
// a tool like webpack, you can do the following:
import "bootstrap/dist/css/bootstrap.css";
// you will also need the css that comes with bootstrap-daterangepicker
import "bootstrap-daterangepicker/daterangepicker.css";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Sensor";

class GetDataByDateRange extends Component {
  constructor(props) {
    super(props);
    this.handleEvent = this.handleEvent.bind(this);
  }

  handleEvent(event, picker) {
    this.props.rangeFilterChange(picker.startDate._d, picker.endDate._d);
  }
  render() {
    return (
      <div>
        <DateRangePicker
          startDate={new Date()}
          endDate={new Date()}
          onApply={this.handleEvent}
        >
          <button className="btn btn-primary">Open Date Range</button>
        </DateRangePicker>
      </div>
    );
  }
}
export default connect(
  state => state.sensor,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(GetDataByDateRange);
