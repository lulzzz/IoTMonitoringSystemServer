import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../../store/Account";
import { BootstrapTable, TableHeaderColumn } from "react-bootstrap-table";
import "react-bootstrap-table/dist/react-bootstrap-table-all.min.css";
import { Row } from "reactstrap";

class Account extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestAccount(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const isLoaded = false;
    this.props.requestAccount(isLoaded);
  }

  render() {
    console.log(this.props.accounts);
    return (
      <div className="animated fadeIn">
        <Row>{this.renderAccountsTable(this.props)}</Row>
      </div>
    );
  }

  renderAccountsTable(props) {
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
      onAddRow: this.onAddAccountRow,
      onDeleteRow: this.onDeleteAccountRow,
      onCellEdit: this.onAccountCellEdit
    };
    const cellEditProp = {
      mode: "click"
    };
    return (
      <div className="table">
        <BootstrapTable
          data={this.props.accounts}
          striped
          hover
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
          {/* <TableHeaderColumn dataField="id" hidden={true} hiddenOnInsert isKey>
            Id
          </TableHeaderColumn> */}

          <TableHeaderColumn
            isKey
            dataField="email"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Email
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="phoneNumber"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Phone Number
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="fullName"
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Full Name
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="createdOn"
            hiddenOnInsert
            editable={false}
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Created On
          </TableHeaderColumn>

          <TableHeaderColumn
            dataField="updatedOn"
            hiddenOnInsert
            editable={false}
            filter={{
              type: "TextFilter",
              delay: 1000
            }}
          >
            Updated On
          </TableHeaderColumn>

          <TableHeaderColumn dataField="password" hidden={true}>
            Password
          </TableHeaderColumn>
        </BootstrapTable>
      </div>
    );
  }

  onAddAccountRow = row => {
    this.props.addAccount(row);
  };
  onDeleteAccountRow = row => {
    this.props.deleteAccount(row[0]);
  };
  onAccountCellEdit = (row, fieldName, value) => {
    this.props.updateAccount(row, fieldName, value);
  };
}

export default connect(
  state => state.account,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Account);
