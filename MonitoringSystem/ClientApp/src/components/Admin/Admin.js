import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/Admin";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
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
      hideSizePerPage: true // You can hide the dropdown for sizePerPage
      // alwaysShowAllBtns: true // Always show next and previous button
      // withFirstAndLast: false > Hide the going to First and Last page button
    };

    return (
      <div className="animated fadeIn">
        <Row>
          <Col>
            <Card>
              <CardHeader>
                <i className="fa fa-align-justify" /> SENSOR
              </CardHeader>
              <CardBody>
                {this.props.sensors.items && renderSensorsTable(this.props)}
                <nav>
                  <Pagination>
                    <PaginationItem>
                      <PaginationLink previous tag="button">
                        Prev
                      </PaginationLink>
                    </PaginationItem>
                    <PaginationItem active>
                      <PaginationLink tag="button">1</PaginationLink>
                    </PaginationItem>
                    <PaginationItem>
                      <PaginationLink tag="button">2</PaginationLink>
                    </PaginationItem>
                    <PaginationItem>
                      <PaginationLink tag="button">3</PaginationLink>
                    </PaginationItem>
                    <PaginationItem>
                      <PaginationLink tag="button">4</PaginationLink>
                    </PaginationItem>
                    <PaginationItem>
                      <PaginationLink next tag="button">
                        Next
                      </PaginationLink>
                    </PaginationItem>
                  </Pagination>
                </nav>
              </CardBody>
            </Card>
          </Col>
        </Row>

        <Row>
          <BootstrapTable
            data={this.props.sensors.items}
            striped
            hover
            condensed
            pagination={true}
            options={options}
          >
            <TableHeaderColumn dataField="sensorCode" isKey>
              Sensor Code
            </TableHeaderColumn>
            <TableHeaderColumn dataField="sensorName">
              Sensor Name
            </TableHeaderColumn>
            <TableHeaderColumn dataField="room" dataFormat={showRoomName}>
              Room Name
            </TableHeaderColumn>
          </BootstrapTable>
        </Row>
      </div>
    );
  }
}

function showRoomName(cell, row) {
  return cell.roomName;
}

function renderSensorsTable(props) {
  return (
    <Table hover bordered striped responsive size="sm">
      <thead>
        <tr>
          <th>SensorCode</th>
          <th>SensorName</th>
          <th>Room</th>
          <th>Rack</th>
        </tr>
      </thead>
      <tbody>
        {props.sensors.items.map(sensor => (
          <tr key={sensor.sensorId}>
            <td>{sensor.sensorCode}</td>
            <td>{sensor.sensorName}</td>
            <td>{sensor.room.roomName}</td>
            <td>{sensor.rackNames.map(name => <p>{name}</p>)}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}

export default connect(
  state => state.admin,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Admin);
