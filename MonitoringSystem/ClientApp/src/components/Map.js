import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import "../assets/css/Map.css";
import map from "../assets/img/Map.png";
import {
  Button,
  Popover,
  PopoverHeader,
  PopoverBody,
  Container,
  Row,
  Col,
  Alert
} from "reactstrap";
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

  showAlert() {
    <Alert color="warning">This is a warning alert — check it out!</Alert>;
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
                  type: "scatter",
                  x: this.props.latestTemperature.x,
                  y: this.props.latestTemperature.y,

                  marker: {
                    color: "green"
                  }
                }
              ]}
              layout={{
                width: 600,
                height: 500,
                xaxis: {
                  title: "Sensor"
                },
                yaxis: {
                  title: "Temperature"
                },
                title: "Real-time temperature"
              }}
            />
          </Col>

          <Col xs="6">
            <Plot
              data={[
                {
                  type: "scatter",
                  x: this.props.latestHumidity.x,
                  y: this.props.latestHumidity.y
                }
              ]}
              layout={{
                width: 600,
                height: 500,
                xaxis: {
                  title: "Sensor"
                },
                yaxis: {
                  title: "Humidity"
                },
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
