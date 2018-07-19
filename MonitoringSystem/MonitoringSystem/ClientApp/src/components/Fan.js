import { bindActionCreators } from "redux";
import React, { Component } from "react";
import { connect } from "react-redux";
import { actionCreators } from "../store/Fans";
import {
  Card,
  Button,
  CardTitle,
  CardText,
  Row,
  Col,
  CardBody
} from "reactstrap";
class Fan extends Component {
  componentWillMount() {
    const isLoaded = true;
    this.props.requestFans(isLoaded);
  }
  componentWillReceiveProps() {
    const isLoaded = true;
    this.props.requestFans(isLoaded);
  }
  render() {
    const isLoaded = true;
    return (
      <div>
        <Row>
          {this.props.fans.items &&
            this.props.fans.items.map(fan => (
              <Col sm="3" key={fan.fanId}>
                <Card body className="text-center">
                  <img
                    className="rotating"
                    width="100%"
                    src="http://www.frozentechnologies.co.uk/communities/9/004/013/373/239//images/4627651659.png"
                    alt="Card image cap"
                  />
                  <CardBody>
                    <CardTitle>{fan.fanName}</CardTitle>
                    <div className="switch-container">
                      <label>
                        <input
                          ref="switch"
                          checked={fan.isOn}
                          onClick={() => this._handleChange(fan)}
                          //onClick={this._handleChange}
                          //onChange={this.props.updateFans(isLoaded, fan)}
                          className="switch"
                          type="checkbox"
                        />
                        <div>
                          <span>
                            <g className="icon icon-toolbar grid-view" />
                          </span>
                          <span>
                            <g className="icon icon-toolbar ticket-view" />
                          </span>
                          <div />
                        </div>
                      </label>
                    </div>
                  </CardBody>
                </Card>
              </Col>
            ))}
        </Row>
      </div>
    );
  }

  _handleChange(fan) {
    console.log("duy");
    console.log(fan);
    fan.isOn = !fan.isOn;
    const isLoaded = false;
    this.props.updateFans(isLoaded, fan);
  }
}
export default connect(
  state => state.fans,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Fan);
