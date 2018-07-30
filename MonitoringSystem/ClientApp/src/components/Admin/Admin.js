import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/Admin";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import "react-bootstrap-table/dist/react-bootstrap-table-all.min.css";
import {
  Badge,
  Card,
  CardBody,
  CardHeader,
  Col,
  Pagination,
  PaginationItem,
  PaginationLink,
  Row,
  Table
} from "reactstrap";

class Admin extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestSensors(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const isLoaded = false;
    this.props.requestSensors(isLoaded);
  }

  render() {
    return (
      <div className="animated fadeIn">
        {this.renderSensorsTable(this.props)}
        {this.renderRacksTable(this.props)}
        {this.renderRoomsTable(this.props)}
      </div>
    );
  }

  renderSensorsTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 5, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 3, // the pagination bar size.
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
      <Row className="table">
        <BootstrapTable
          data={this.props.sensors.items}
          striped
          hover
          condensed
          pagination={true}
          insertRow
          deleteRow
          selectRow={{ mode: "radio" }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn
            dataField="sensorId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Sensor Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="sensorCode"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Sensor Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="sensorName"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Sensor Name
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
      </Row>
    );
  }

  renderRacksTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 5, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 3, // the pagination bar size.
      prePage: "Prev", // Previous page button text
      nextPage: "Next", // Next page button text
      firstPage: "First", // First page button text
      lastPage: "Last", // Last page button text
      paginationShowsTotal: this.renderShowsTotal, // Accept bool or function
      paginationPosition: "bottom", // default is bottom, top and both is all available
      hideSizePerPage: true,
      onAddRow: this.onAddRackRow,
      onDeleteRow: this.onDeleteRackRow,
      onCellEdit: this.onRackrCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <Row className="table">
        <BootstrapTable
          data={this.props.racks.items}
          striped
          hover
          condensed
          pagination={true}
          insertRow
          deleteRow
          selectRow={{ mode: "radio" }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn
            dataField="rackId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Rack Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="rackCode"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Rack Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="rackName"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Rack Name
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
      </Row>
    );
  }

  renderRoomsTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 5, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 3, // the pagination bar size.
      prePage: "Prev", // Previous page button text
      nextPage: "Next", // Next page button text
      firstPage: "First", // First page button text
      lastPage: "Last", // Last page button text
      paginationShowsTotal: this.renderShowsTotal, // Accept bool or function
      paginationPosition: "bottom", // default is bottom, top and both is all available
      hideSizePerPage: true,
      onAddRow: this.onAddRoomRow,
      onDeleteRow: this.onDeleteRoomRow,
      onCellEdit: this.onRoomCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <Row className="table">
        <BootstrapTable
          data={this.props.rooms.items}
          striped
          hover
          condensed
          pagination={true}
          insertRow
          deleteRow
          selectRow={{ mode: "radio" }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn
            dataField="roomId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Room Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="roomCode"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Room Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="roomName"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Room Name
          </TableHeaderColumn>
        </BootstrapTable>
      </Row>
    );
  }

  showRoomName(cell, row) {
    return row.roomName;
  }

  onAddSensorRow = row => {
    this.props.addSensors(row);
  };
  onDeleteSensorRow = row => {
    this.props.deleteSensors(row[0]);
  };
  onSensorCellEdit = (row, fieldName, value) => {
    this.props.updateSensors(row, fieldName, value);
  };

  onAddRoomRow = row => {
    this.props.addRooms(row);
  };
  onDeleteRoomRow = row => {
    this.props.deleteRooms(row[0]);
  };
  onRoomCellEdit = (row, fieldName, value) => {
    this.props.updateRooms(row, fieldName, value);
  };

  onAddRackRow = row => {
    this.props.addRacks(row);
  };
  onDeleteRackRow = row => {
    this.props.deleteRacks(row[0]);
  };
  onRackCellEdit = (row, fieldName, value) => {
    this.props.updateRacks(row, fieldName, value);
  };
  // handleSave(data) {
  //   this.props.addSensors(data);
  // }
  // handleDelete(data) {
  //   this.props.deleteSensors(data[0]);
  // }
}

export default connect(
  state => state.admin,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Admin);
