import React from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import "../assets/css/Map.css";
import map from "../assets/img/Map.png";
// import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import {
  Button,
  Popover,
  PopoverHeader,
  PopoverBody,
  Container,
  Row,
  Col,
  Alert,
  Card,
  CardTitle,
  CardBody,
  CardText
} from "reactstrap";
import { actionCreators } from "../store/Map";
import Plot from "react-plotly.js";
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
    console.log(this.props.item.temperature);
    return (
      <div>
        <Button
          className="censor-btn"
          color={
            this.props.item.temperature === ""
              ? "stop"
              : parseFloat(this.props.item.temperature) < 26
                ? "normal"
                : parseFloat(this.props.item.temperature) < 35
                  ? "warning"
                  : parseFloat(this.props.item.temperature) < 50
                    ? "high-warning"
                    : "danger"
          }
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
            <a
              className="btn btn-primary"
              href={`/sensor/` + this.props.item.sensorId}
            >
              Detail
            </a>
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
      <div>
        <Row>
          <Col xs="12" xl="8">
            <img className="map" src={map} alt="map" />
            {this.props.popovers &&
              this.props.popovers.map((popover, i) => {
                return <PopoverItem key={i} item={popover} id={popover.key} />;
              })}
          </Col>
          <Col xs="12" xl="4">
            <Card className="map-note">
              <CardBody>
                <CardText>
                  <i className="normal" />
                  Ổn định (&lt; 25)
                </CardText>
                <CardText>
                  <i className="warning" />
                  Có nguy cơ (26 - 35)
                </CardText>
                <CardText>
                  <i className="high-warning" />
                  Nguy cơ cao (36 - 50)
                </CardText>
                <CardText>
                  <i className="danger" />
                  Báo động (&gt; 50)
                </CardText>
                <CardText>
                  <i className="stop" />
                  Không hoạt động
                </CardText>
              </CardBody>
            </Card>
          </Col>
        </Row>

        {/* <Row>
          <Col xs="12" lg="12" xl="6">
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
                width: 550,
                height: 500,
                xaxis: {
                  title: "Sensor"
                },
                yaxis: {
                  title: "Temperature"
                },
                font: {
                  family: "Roboto, sans-serif"
                }
              }}
            />
          </Col>

          <Col xs="12" lg="12" xl="6" />
        </Row> */}
      </div>
    );
  }
}

export default connect(
  state => state.map,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(PopoverExampleMulti);
