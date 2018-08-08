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
    return (
      <div>
        <Navbar color="light" light expand="md">
          <NavbarBrand href="/">
            <img src={Logo} />
          </NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="ml-auto" navbar>
              <NavItem>
                <NavLink href="/">Dashboard</NavLink>
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
            </Nav>
          </Collapse>
        </Navbar>
      </div>
    );
  }
}
