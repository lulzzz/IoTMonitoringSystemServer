import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Rack";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import { Row } from "reactstrap";
import RangeFilter from "./RangeFilter";

function getCustomFilter(filterHandler, customFilterParameters) {
  return <RangeFilter filterHandler={filterHandler} />;
}

class Rack extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const rackId = parseInt(this.props.match.params.rackId, 10) || 0;
    const isLoaded = false;
    this.props.requestRack(isLoaded, rackId);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const rackId = parseInt(this.props.match.params.rackId, 10) || 0;
    const isLoaded = false;
    this.props.requestRack(isLoaded, rackId);
  }

  render() {
    console.log(this.props.rack);
    return (
      <div>
        <h1>
          <b>Rack Name: </b>
          {this.props.rack.rackName}
        </h1>
        <h2>
          <b>Sensor Code: </b>
          {this.props.rack.sensor === undefined
            ? ""
            : this.props.rack.sensor.sensorCode}
        </h2>
        <h2>
          <b>Sensor Name: </b>
          {this.props.rack.sensor === undefined
            ? ""
            : this.props.rack.sensor.sensorName}
        </h2>
        <h2>
          <b>Rack Code: </b>
          {this.props.rack.rackCode}
        </h2>
        <h2>
          <b>Location: </b>
          {this.props.rack.location}
        </h2>
        <h2>
          <b>Room Name: </b>
          {this.props.rack.roomName}
        </h2>
        <Row>
          <div className="col-md-7 col-xs-12">
            {this.renderStatusTable(this.props)}
          </div>
          <div className="col-md-5 col-xs-12">
            {this.renderLogTable(this.props)}
          </div>
        </Row>
      </div>
    );
  }

  renderStatusTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 10, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 5, // the pagination bar size.
      prePage: "Prev", // Previous page button text
      nextPage: "Next", // Next page button text
      firstPage: "First", // First page button text
      lastPage: "Last", // Last page button text
      paginationShowsTotal: this.renderShowsTotal, // Accept bool or function
      paginationPosition: "bottom", // default is bottom, top and both is all available
      hideSizePerPage: true
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.statuses.items}
          striped
          hover
          condensed
          pagination={true}
          exportCSV
          //remote={true}
          options={options}
        >
          <TableHeaderColumn
            dataField="statusId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Status Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="dateTime"
            filter={{
              type: "CustomFilter",
              getElement: getCustomFilter,
              customFilterParameters: {}
            }}
          >
            DateTime
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="temperatureValue"
            filter={{
              type: "NumberFilter",
              delay: 1000,
              numberComparators: ["=", ">", "<="]
            }}
          >
            Temperature Value
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="humidityValue"
            filter={{
              type: "NumberFilter",
              delay: 1000,
              numberComparators: ["=", ">", "<="]
            }}
          >
            Humidity Value
          </TableHeaderColumn>
        </BootstrapTable>
      </div>
    );
  }

  renderLogTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 5, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 5, // the pagination bar size.
      prePage: "Prev", // Previous page button text
      nextPage: "Next", // Next page button text
      firstPage: "First", // First page button text
      lastPage: "Last", // Last page button text
      paginationShowsTotal: this.renderShowsTotal, // Accept bool or function
      paginationPosition: "bottom", // default is bottom, top and both is all available
      hideSizePerPage: true
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.rack.logs}
          striped
          hover
          condensed
          pagination={true}
          exportCSV
          //remote={true}
          options={options}
        >
          <TableHeaderColumn
            dataField="logId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Log Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="dateTime"
            filter={{
              type: "CustomFilter",
              getElement: getCustomFilter,
              customFilterParameters: {}
            }}
          >
            DateTime
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="description"
            filter={{
              type: "TextFilter"
            }}
          >
            Description
          </TableHeaderColumn>
        </BootstrapTable>
      </div>
    );
  }
}

export default connect(
  state => state.rack,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Rack);
