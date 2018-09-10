import React, { Component } from "react";
// import NavMenu from "./NavMenu";
// import { Button } from "semantic-ui-react";
// import { Input, Menu } from "semantic-ui-react";
import Logo from "../assets/img/logo.png";

import { Menu, Dropdown } from "semantic-ui-react";
import "../assets/css/Navbar.css";
import * as authService from "../services/Authentication";
import { isUserAuthenticated } from "./../services/Authentication";

export default class Layout extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = { activeItem: "home", visible: false };
  }
  handleContextRef = contextRef => this.setState({ contextRef });
  handleItemClick = (e, { name }) => this.setState({ activeItem: name });
  handleButtonClick = () => this.setState({ visible: !this.state.visible });

  handleSidebarHide = () => this.setState({ visible: false });
  render() {
    const { activeItem, visible, contextRef } = this.state;
    return (
      <div>
        <Menu className="navbar" secondary>
          <Menu.Item>
            <img src={Logo} style={{ width: 100 }} />
          </Menu.Item>
          {/* <Menu.Item
            name="Dashboard"
            active={activeItem === "Dashboard"}
            href="/"
          /> */}
          <Dropdown item text="Dashboard">
            <Dropdown.Menu>
              <Dropdown item text="Logs">
                <Dropdown.Menu className="sub-menu">
                  <Dropdown.Item href="/templog">
                    Temperature Logs
                  </Dropdown.Item>
                  <Dropdown.Item href="/humilog">Humidity Logs</Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
              <Dropdown item text="Realtime">
                <Dropdown.Menu className="sub-menu">
                  <Dropdown.Item href="/temprt">
                    Temperature Realtime
                  </Dropdown.Item>
                  <Dropdown.Item href="/humirt">
                    Humidity Realtime
                  </Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
            </Dropdown.Menu>
          </Dropdown>
          <Menu.Item name="Map" active={activeItem === "Map"} href="/Map" />
          <Menu.Item name="Fan" active={activeItem === "Fan"} href="/Fans" />
          <Menu.Item
            name="Admin"
            active={activeItem === "Admin"}
            href="/Admin"
          />
          <Menu.Item
            name="Account"
            active={activeItem === "Account"}
            href="/Account"
          />
          {authService.isUserAuthenticated() && (
            <Menu.Item name="Log Out" onClick={authService.logOut} href="/" />
          )}

          <Menu.Menu position="right">
            <Menu.Item>
              {/* <Input icon="search" placeholder="Search..." /> */}
            </Menu.Item>
          </Menu.Menu>
        </Menu>
        {/* </Sticky> */}
        <div className="body">
          <div className="body-content">{this.props.children}</div>
        </div>
      </div>
    );
  }
}
