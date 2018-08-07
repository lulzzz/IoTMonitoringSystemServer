import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Sensor";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import { Row } from "reactstrap";

class Sensor extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const sensorId = parseInt(this.props.match.params.sensorId, 10) || 0;
    const isLoaded = false;
    this.props.requestSensor(isLoaded, sensorId);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const sensorId = parseInt(this.props.match.params.sensorId, 10) || 0;
    const isLoaded = false;
    this.props.requestSensor(isLoaded, sensorId);
  }

  render() {
    return (
      <div>
        <h1>
          <b>Sensor {this.props.sensorId}</b>
        </h1>
        <Row>
          {this.renderStatusTable(this.props)}
          {this.renderLogTable(this.props)}
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
              type: "TextFilter",
              delay: 1000
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
          data={this.props.sensor.logs}
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
              type: "TextFilter",
              delay: 1000
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
  state => state.sensor,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Sensor);
