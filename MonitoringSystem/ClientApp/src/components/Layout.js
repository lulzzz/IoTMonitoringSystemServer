import React, { Component } from "react";
// import NavMenu from "./NavMenu";
// import { Button } from "semantic-ui-react";
// import { Input, Menu } from "semantic-ui-react";
import Logo from "../assets/img/logo.png";

import {
  Button,
  Header,
  Icon,
  Image,
  Menu,
  Segment,
  Sidebar,
  Container,
  Input
} from "semantic-ui-react";
import "../assets/css/Navbar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

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
        {/* <a href="/">
                <Button>
                  <i className="fas fa-chart-pie" />
                  DASHBOARD
                </Button>
              </a>
              <a href="/map">
                <Button>
                  <i className="fas fa-map" />
                  MAP
                </Button>
              </a>
              <a href="/fans">
                <Button>
                  <i className="fas fa-paper-plane" />
                  FAN
                </Button>
              </a>
              <a href="/admin">
                <Button>
                  <i className="fas fa-toolbox" />
                  ADMIN
                </Button>
              </a>
              <a href="/account">
                <Button>
                  <i className="fas fa-user" />
                  ACCOUNT
                </Button>
              </a> */}
        <Menu className="navbar" secondary>
          <Menu.Item>
            <img src={Logo} style={{ width: 100 }} />
          </Menu.Item>
          <Menu.Item
            name="Dashboard"
            active={activeItem === "Dashboard"}
            href="/"
          />
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
