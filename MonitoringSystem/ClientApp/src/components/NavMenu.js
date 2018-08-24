import React from "react";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink
} from "reactstrap";
import Logo from "../assets/img/logo_vntt.png";
import * as authService from "./../services/Authentication";

export default class NavMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render() {
    var role = authService.getLoggedInUser().role;
    console.log(role);
    return (
      <div>
        <Navbar expand="md">
          <NavbarBrand href="/">
            <img src={Logo} />
          </NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="ml-auto" navbar>
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
            </Nav>
          </Collapse>
        </Navbar>
      </div>
    );
  }
}
