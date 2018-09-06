import React, { Component } from "react";
import DateRangePicker from "react-bootstrap-daterangepicker";
// you will need the css that comes with bootstrap@3. if you are using
// a tool like webpack, you can do the following:
import "bootstrap/dist/css/bootstrap.css";
// you will also need the css that comes with bootstrap-daterangepicker
import "bootstrap-daterangepicker/daterangepicker.css";

class RangeFilter extends Component {
  constructor(props) {
    super(props);
    this.state = {
      startDate: "",
      endDate: "",
      dates: null
    };
    this.isFiltered = this.isFiltered.bind(this);
    this.handleEvent = this.handleEvent.bind(this);
  }

  filter() {
    if (this.state.startDate === "" && this.state.endDate === "") {
      this.props.filterHandler();
    } else {
      this.props.filterHandler({ callback: this.isFiltered });
    }
  }

  isFiltered(targetValue) {
    if (this.state.startDate !== "" && this.state.endDate !== "") {
      const date = new Date(targetValue);
      return date >= this.state.startDate && date <= this.state.endDate;
    }
  }

  handleEvent(event, picker) {
    this.setState({
      startDate: picker.startDate._d,
      endDate: picker.endDate._d
    });

    this.filter();
  }
  render() {
    return (
      <div>
        <DateRangePicker
          startDate={new Date()}
          endDate={new Date()}
          onApply={this.handleEvent}
        >
          <button className="btn btn-success">Open Date Range</button>
        </DateRangePicker>
      </div>
    );
  }
}
export default RangeFilter;
