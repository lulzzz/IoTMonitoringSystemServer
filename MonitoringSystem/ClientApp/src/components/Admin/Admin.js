import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/Admin";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import "react-bootstrap-table/dist/react-bootstrap-table-all.min.css";
import { Row } from "reactstrap";
import {} from "../../assets/css/table.css";

class Admin extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestAdmin(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const isLoaded = false;
    this.props.requestAdmin(isLoaded);
  }

  render() {
    return (
      <div className="animated fadeIn">
        <Row>{this.renderSensorsTable(this.props)}</Row>
        <Row>{this.renderRacksTable(this.props)}</Row>
        <Row>{this.renderRoomsTable(this.props)}</Row>
        <Row>{this.renderFansTable(this.props)}</Row>
      </div>
    );
  }

  renderSensorsTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 10, // which size per page you want to locate as default
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
      <div className="table admin-table">
        <BootstrapTable
          data={this.props.sensors.items}
          striped
          condensed
          pagination={true}
          insertRow
          deleteRow
          exportCSV
          selectRow={{
            mode: "checkbox",
            columnWidth: "40px",
            clickToSelect: false
          }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn dataField="sensorId" hidden hiddenOnInsert isKey>
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

          <TableHeaderColumn
            editable={false}
            dataFormat={this.sensorDetailButton}
          />
        </BootstrapTable>
      </div>
    );
  }

  renderRacksTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 10, // which size per page you want to locate as default
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
      <div className="table">
        <BootstrapTable
          data={this.props.racks.items}
          striped
          condensed
          pagination={true}
          insertRow
          deleteRow
          exportCSV
          selectRow={{
            mode: "checkbox",
            columnWidth: "40px",
            clickToSelect: true
          }}
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
            dataField="location"
            filter={{
              type: "TextFilter",
              delay: 1000
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

          <TableHeaderColumn
            editable={false}
            dataFormat={this.rackDetailButton}
          />
        </BootstrapTable>
      </div>
    );
  }

  renderRoomsTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 10, // which size per page you want to locate as default
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
      <div className="table">
        <BootstrapTable
          data={this.props.rooms.items}
          striped
          condensed
          pagination={true}
          insertRow
          deleteRow
          exportCSV
          selectRow={{
            mode: "checkbox",
            columnWidth: "40px",
            clickToSelect: true
          }}
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

          <TableHeaderColumn
            editable={false}
            dataFormat={this.roomDetailButton}
          />
        </BootstrapTable>
      </div>
    );
  }

  renderFansTable(props) {
    const options = {
      page: 1, // which page you want to show as default
      sizePerPage: 10, // which size per page you want to locate as default
      pageStartIndex: 1, // where to start counting the pages
      paginationSize: 3, // the pagination bar size.
      prePage: "Prev", // Previous page button text
      nextPage: "Next", // Next page button text
      firstPage: "First", // First page button text
      lastPage: "Last", // Last page button text
      paginationShowsTotal: this.renderShowsTotal, // Accept bool or function
      paginationPosition: "bottom", // default is bottom, top and both is all available
      hideSizePerPage: true,
      onAddRow: this.onAddFanRow,
      onDeleteRow: this.onDeleteFanRow,
      onCellEdit: this.onFanCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.fans.items}
          striped
          condensed
          pagination={true}
          insertRow
          deleteRow
          exportCSV
          selectRow={{
            mode: "checkbox",
            columnWidth: "40px",
            clickToSelect: true
          }}
          //remote={true}
          cellEdit={cellEditProp}
          options={options}
        >
          <TableHeaderColumn
            dataField="fanId"
            hidden={true}
            hiddenOnInsert
            isKey
          >
            Fan Id
          </TableHeaderColumn>
          <TableHeaderColumn
            dataField="fanCode"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Fan Code
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="fanName"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Fan Name
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="capacity"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Capacity
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="isOn"
            dataFormat={this.showIsOnFormat}
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
            editable={{
              type: "select",
              options: {
                values: this.props.trueFalseFormatter,
                textKey: "formatter",
                valueKey: "id"
              }
            }}
          >
            Is On
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

  showIsOnFormat(cell, row) {
    if (row.isOn) {
      return "On";
    } else {
      return "Off";
    }
  }

  sensorDetailButton(cell, row, enumObject, rowIndex) {
    let theEditButton = (
      <Link className="btn btn-primary" to={`/sensor/${row.sensorId}`}>
        Detail
      </Link>
    );
    return theEditButton;
  }

  rackDetailButton(cell, row, enumObject, rowIndex) {
    let theEditButton = (
      <Link className="btn btn-primary" to={`/rack/${row.rackId}`}>
        Detail
      </Link>
    );
    return theEditButton;
  }

  roomDetailButton(cell, row, enumObject, rowIndex) {
    let theEditButton = (
      <Link className="btn btn-primary" to={`/room/${row.roomId}`}>
        Detail
      </Link>
    );
    return theEditButton;
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

  onAddFanRow = row => {
    this.props.addFans(row);
  };
  onDeleteFanRow = row => {
    this.props.deleteFans(row[0]);
  };
  onFanCellEdit = (row, fieldName, value) => {
    this.props.updateFans(row, fieldName, value);
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
