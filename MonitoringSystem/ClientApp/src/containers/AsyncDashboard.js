import React, { Component } from "react";
import Temperatures from "./AsyncTemperatures";
import Humidities from "./AsyncHumidities";
// import { Row, Col, Breadcrumb, BreadcrumbItem } from "reactstrap";
import { Grid } from "semantic-ui-react";

class Dashboard extends Component {
  render() {
    return (
      <div className="">
        {/* <Breadcrumb>
                    <BreadcrumbItem>DASHBOARD</BreadcrumbItem>
                </Breadcrumb> */}
        <Grid divided="vertically">
          <Grid.Row>
            <Grid.Column mobile={16} computer={8}>
              <Temperatures />
            </Grid.Column>
            <Grid.Column mobile={16} computer={8}>
              <Humidities />
            </Grid.Column>
          </Grid.Row>
        </Grid>
        {/* <Row>
          <Col xs="12" lg="12" xl="6">
            <Temperatures />
          </Col>
          <Col xs="12" lg="12" xl="6">
            <Humidities />
          </Col>
        </Row> */}
      </div>
    );
  }
}
export default Dashboard;
