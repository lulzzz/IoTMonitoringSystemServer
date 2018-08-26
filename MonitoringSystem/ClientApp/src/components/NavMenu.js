import React from "react";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  Button
} from "reactstrap";
import PropTypes from "prop-types";
import Sidebar from "react-sidebar";
import Logo from "../assets/img/logo_vntt.png";
import * as authService from "./../services/Authentication";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const sidebarStyle = {
  padding: "2em 1em"
};

class NavMenu extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      sidebarOpen: false
    };
    this.onSetSidebarOpen = this.onSetSidebarOpen.bind(this);
  }

  onSetSidebarOpen(open) {
    this.setState({ sidebarOpen: open });
  }

  render() {
    var role = authService.getLoggedInUser().role;
    return (
      <Sidebar
        sidebar={
          <Nav className="ml-auto" navbar>
            <NavItem className="sidebar-logo">
              <p>MENU</p>
            </NavItem>
            <div className="sidebar-item">
              <NavItem>
                <NavLink href="/dashboard">Dashboard</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/map">Map</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/fans/">Fan</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/admin/">Admin</NavLink>
              </NavItem>
              {role == "Admin" && (
                <NavItem>
                  <NavLink href="/account/">Account</NavLink>
                </NavItem>
              )}
            </div>
          </Nav>
        }
        open={this.state.sidebarOpen}
        onSetOpen={this.onSetSidebarOpen}
        styles={{ sidebar: { background: "white", width: 250 } }}
      >
        <Navbar expand="md">
          <Button color="link" onClick={() => this.onSetSidebarOpen(true)}>
            <FontAwesomeIcon icon="bars" />
          </Button>
          <NavbarBrand href="/">
            <img src={Logo} />
          </NavbarBrand>
        </Navbar>
      </Sidebar>
    );
  }
}

export default NavMenu;
