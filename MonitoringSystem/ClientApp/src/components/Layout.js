import React from "react";
import NavMenu from "./NavMenu";
import { Container } from "reactstrap";

export default props => (
  <div>
    <div>
      <NavMenu />
    </div>
    <div className="body">
      <Container className="content">{props.children}</Container>
    </div>
  </div>
);

