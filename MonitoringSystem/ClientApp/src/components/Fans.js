import React, { Component } from "react";
import PropTypes from "prop-types";
import { Card, CardTitle, Row, Col, CardBody } from "reactstrap";
import fanImg from "../assets/img/fanImg.png";
import { Grid } from "semantic-ui-react";

export default class Fans extends Component {
  render() {
    const { fans, onChange } = this.props;
    console.log(fans);
    return (
      <div>
        <Row>
          {fans.map(fan => (
            <Col sm="3" key={fan.fanId}>
              <Card body className="text-center">
                <img
                  className={"rotating-" + fan.isOn}
                  width="100%"
                  id={fan.fanId}
                  src={fanImg}
                  alt="Card cap"
                />
                <CardBody>
                  <CardTitle>{fan.fanName}</CardTitle>
                  <div className="switch-container">
                    <label>
                      <input
                        ref="switch"
                        checked={fan.isOn}
                        onChange={() => onChange(fan, fans)}
                        className="switch"
                        type="checkbox"
                      />
                      <div>
                        <span>
                          <i className="icon icon-toolbar grid-view" />
                        </span>
                        <span>
                          <i className="icon icon-toolbar ticket-view" />
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
}

Fans.propTypes = {
  fans: PropTypes.array.isRequired
};
