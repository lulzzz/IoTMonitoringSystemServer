import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Sensor";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import { Row } from "reactstrap";
import RangeFilter from "./RangeFilter";

function getCustomFilter(filterHandler, customFilterParameters) {
  return <RangeFilter filterHandler={filterHandler} />;
}

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
          <b>Sensor Name: </b>
          {this.props.sensor.sensorName}
        </h1>
        <h2>
          <b>Sensor Code: </b>
          {this.props.sensor.sensorCode}
        </h2>
        <h2>
          <b>Room Name: </b>
          {this.props.sensor.roomName}
        </h2>
        <Row>
          <div className="col-md-7 col-xs-12">
            {this.renderStatusTable(this.props)}
          </div>
          <div className="col-md-5 col-xs-12">
            {this.renderLogTable(this.props)}
            {this.renderRackTable(this.props)}
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
              numberComparators: ["=", ">", "<"]
            }}
          >
            Temperature Value
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="humidityValue"
            filter={{
              type: "NumberFilter",
              delay: 1000,
              numberComparators: ["=", ">", "<"]
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

  renderRackTable(props) {
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
      hideSizePerPage: true,
      onAddRow: this.onAddRackRow,
      onDeleteRow: this.onDeleteRackRow,
      onCellEdit: this.onRackCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.racks}
          striped
          hover
          condensed
          pagination={true}
          insertRow
          deleteRow
          exportCSV
          selectRow={{ mode: "radio" }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn
            dataField="rackId"
            hidden={true}
            hiddenOnInsert
            cellEdit={cellEditProp}
            isKey
          >
            Rack Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="rackCode"
            cellEdit={cellEditProp}
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Rack Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="rackName"
            cellEdit={cellEditProp}
            filter={{
              type: "TextFilter"
            }}
          >
            Rack Name
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="location"
            cellEdit={cellEditProp}
            filter={{
              type: "TextFilter"
            }}
          >
            Location
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="roomId"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
            dataFormat={this.showRoomName}
            editable={{
              type: "select",
              options: {
                values: this.props.rooms.items,
                textKey: "roomName",
                valueKey: "roomId"
              }
            }}
          >
            Room Name
          </TableHeaderColumn>
        </BootstrapTable>
      </div>
    );
  }

  showRoomName(cell, row) {
    return row.roomName;
  }

  onAddRackRow = row => {
    this.props.addRacks(row);
  };
  onDeleteRackRow = row => {
    this.props.deleteRacks(row[0]);
  };
  onRackCellEdit = (row, fieldName, value) => {
    this.props.updateRacks(row, fieldName, value);
  };
}

export default connect(
  state => state.sensor,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Sensor);
