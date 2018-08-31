import React, { Component } from "react";
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
import Logo from "../assets/img/logo_vntt.png";
import * as authService from "./../services/Authentication";

class NavMenu extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = { activeItem: "home", visible: false };
  }

  handleContextRef = contextRef => this.setState({ contextRef });
  handleItemClick = (e, { name }) => this.setState({ activeItem: name });
  handleButtonClick = () => this.setState({ visible: !this.state.visible });
  handleSidebarHide = () => this.setState({ visible: false });

  render() {
    const { activeItem, visible } = this.state;
    const role = authService.getLoggedInUser().role;

    return <div />;
  }
}

export default NavMenu;
