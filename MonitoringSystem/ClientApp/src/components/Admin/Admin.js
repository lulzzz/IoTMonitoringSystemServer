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
          <BootstrapTable
            data={this.props.sensors.items}
            striped
            hover
            condensed
            pagination={true}
            insertRow={true}
            options={options}
          >
            <TableHeaderColumn
              dataField="sensorCode"
              filter={{
                type: "TextFilter",
                delay: 1000
              }}
              isKey
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
      </div>
    );
  }
}

export default connect(
  state => state.admin,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Admin);
