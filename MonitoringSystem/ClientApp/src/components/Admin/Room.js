import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Room";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import { Row } from "reactstrap";

class Room extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const roomId = parseInt(this.props.match.params.roomId, 10) || 0;
    const isLoaded = false;
    this.props.requestRoom(isLoaded, roomId);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const roomId = parseInt(this.props.match.params.roomId, 10) || 0;
    const isLoaded = false;
    this.props.requestRoom(isLoaded, roomId);
  }

  render() {
    console.log(this.props.racks);
    return (
      <div>
        <h1>
          <b>Room Name: </b>
          {this.props.room.roomName}
        </h1>
        <h2>
          <b>Room Code: </b>
          {this.props.room.roomCode}
        </h2>
        <Row>
          <div className="col-md-6 col-xs-12">
            {this.renderRackTable(this.props)}
          </div>
          <div className="col-md-6 col-xs-12">
            {this.renderLogTable(this.props)}
            {this.renderSensorTable(this.props)}
          </div>
        </Row>
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
          data={this.props.room.logs}
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
              type: "DateFilter",
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
          data={this.props.racks.items}
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
        </BootstrapTable>
      </div>
    );
  }

  renderSensorTable(props) {
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
      hideSizePerPage: true,
      onAddRow: this.onAddSensorRow,
      onDeleteRow: this.onDeleteSensorRow,
      onCellEdit: this.onSensorCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.sensors.items}
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
            dataField="sensorId"
            hidden={true}
            hiddenOnInsert
            cellEdit={cellEditProp}
            isKey
          >
            Sensor Id
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="sensorCode"
            cellEdit={cellEditProp}
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Sensor Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="sensorName"
            cellEdit={cellEditProp}
            filter={{
              type: "TextFilter"
            }}
          >
            Sensor Name
          </TableHeaderColumn>
        </BootstrapTable>
      </div>
    );
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

  onAddSensorRow = row => {
    this.props.addSensors(row);
  };
  onDeleteSensorRow = row => {
    this.props.deleteSensors(row[0]);
  };
  onSensorCellEdit = (row, fieldName, value) => {
    this.props.updateSensors(row, fieldName, value);
  };
}

export default connect(
  state => state.room,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Room);
