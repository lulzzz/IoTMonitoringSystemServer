import React from "react";
import {
  // Collapse,
  // Navbar,
  // NavbarToggler,
  // NavbarBrand,
  // Nav,
  // NavItem,
  // NavLink,  
} from "reactstrap";
import {Button, Navbar, NavItem, SideNav, SideNavItem} from 'react-materialize'
// import SideNav, { Toggle, Nav, NavItem, NavIcon, NavText } from '@trendmicro/react-sidenav';

// Be sure to include styles at some point, probably during your bootstraping
import '@trendmicro/react-sidenav/dist/react-sidenav.css';
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
    
      <Navbar style={{background: 'linear-gradient(to right, #0cebeb, #20e3b2, #29ffc6)'}}>
        <SideNav
          trigger={<li><Button className="fa fa-bars btn-flat"></Button></li>}
          options={{ closeOnClick: true }}
        >
          <SideNavItem userView
            user={{
              background: 'https://hdqwalls.com/wallpapers/blur-background-6z.jpg',
              image: 'https://cdn2.iconfinder.com/data/icons/ios-7-icons/50/user_male2-512.png',
              name: 'VNTT',
              email: 'vntt@gmail.com'
            }}
          />
          <SideNavItem href='/dashboard'>DASHBOARD</SideNavItem>
          <SideNavItem href='/fans'>FAN</SideNavItem>
          <SideNavItem href='/map'>MAP</SideNavItem>
          <SideNavItem href='/admin'>ADMIN</SideNavItem>
          <SideNavItem href='/account'>ACCOUNT</SideNavItem>
          {/* <SideNavItem divider /> */}          
        </SideNav>
        <NavItem><img style={{height: "30px"}} src="http://www.ranklogos.com/wp-content/uploads/2015/10/Vntt-Logo.png"/></NavItem>
        {/* <NavItem onClick={() => console.log('test click')}>Getting started</NavItem>
        <NavItem href='components.html'>Components</NavItem> */}
        {/* <button data-activates="sidenav_0" class="btn show-on-large">Sidebar</button> */}
        
    </Navbar>
    );
  }
}

export default NavMenu;
