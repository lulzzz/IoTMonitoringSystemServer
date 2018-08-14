import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import "../assets/css/Map.css";
import map from "../assets/img/Map.png";
import { Button, Popover, PopoverHeader, PopoverBody } from "reactstrap";
import { Container, Row, Col } from "reactstrap";
import { actionCreators } from "../store/Map";
import Plot from "react-plotly.js";
import $ from "jquery";

class PopoverItem extends React.Component {
  constructor(props) {
    super(props);
    this.toggle = this.toggle.bind(this);
    this.state = {
      popoverOpen: false
    };
  }

  toggle() {
    this.setState({
      popoverOpen: !this.state.popoverOpen
    });
  }

  render() {
    return (
      <div>
        <Button
          className="censor-btn"
          color="success"
          id={"sensor-" + this.props.id}
          onClick={this.toggle}
        >
          {this.props.item.text}
        </Button>
        <Popover
          placement={this.props.item.placement}
          isOpen={this.state.popoverOpen}
          target={"sensor-" + this.props.id}
          toggle={this.toggle}
        >
          <PopoverHeader>{this.props.item.sensor}</PopoverHeader>
          <PopoverBody>
            <div>{"Temperature: " + this.props.item.temperature}</div>
            <div>{"Humidity: " + this.props.item.humidity}</div>
          </PopoverBody>
        </Popover>
      </div>
    );
  }
}

class PopoverExampleMulti extends React.Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestMaps(isLoaded);
  }

  // componentWillReceiveProps(nextProps) {
  //   // This method runs when incoming props (e.g., route params) change
  //   const isLoaded = false;
  //   this.props.requestMaps(isLoaded);
  // }

  render() {
    return (
      <Container>
        <Row className="container">
          <Col sm="12" md={{ size: 8, offset: 2 }}>
            <img className="map" src={map} />
            {this.props.popovers &&
              this.props.popovers.map((popover, i) => {
                return <PopoverItem key={i} item={popover} id={popover.key} />;
              })}
          </Col>
        </Row>

        <Row>
          <Col xs="6">
            <Plot
              data={[
                {
                  name: "Sensor",
                  type: "bar",
                  x: [
                    "Sensor1",
                    "Sensor2",
                    "Sensor3",
                    "Sensor4",
                    "Sensor5",
                    "Sensor6",
                    "Sensor7",
                    "Sensor8",
                    "Sensor9",
                    "Sensor10"
                  ],
                  y: [4, 1, 8, 5, 3, 7, 12, 16, 10, 11],
                  marker: {
                    color: "red"
                  }
                }
              ]}
              layout={{
                width: 600,
                height: 500,
                title: "Real-time temperature"
              }}
            />
          </Col>

          <Col xs="6">
            <Plot
              data={[
                {
                  title: "Sensor",
                  type: "bar",
                  x: [
                    "Sensor1",
                    "Sensor2",
                    "Sensor3",
                    "Sensor4",
                    "Sensor5",
                    "Sensor6",
                    "Sensor7",
                    "Sensor8",
                    "Sensor9",
                    "Sensor10"
                  ],
                  y: [1, 3, 6, 9, 6, 5, 8, 9, 11, 16]
                }
              ]}
              layout={{
                width: 600,
                height: 500,
                title: "Real-time humidity"
              }}
            />
          </Col>
        </Row>
      </Container>
    );
  }
}

export default connect(
  state => state.map,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(PopoverExampleMulti);
