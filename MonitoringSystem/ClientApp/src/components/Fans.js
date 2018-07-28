import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {
    Card,
    Button,
    CardTitle,
    CardText,
    Row,
    Col,
    CardBody
} from "reactstrap";

export default class Fans extends Component {
    render() {
        console.log(this.props.fans)
        return (
            <div>
                <Row>
                    {this
                        .props
                        .fans
                        .map(fan => (
                            <Col sm="3" key={fan.fanId}>
                                <Card body className="text-center">
                                    <img
                                        className="rotating"
                                        width="100%"
                                        src="http://www.frozentechnologies.co.uk/communities/9/004/013/373/239//images/4627651659.png"
                                        alt="Card image cap"/>
                                    <CardBody>
                                        <CardTitle>{fan.fanName}</CardTitle>
                                        <div className="switch-container">
                                            <label>
                                                <input
                                                    ref="switch"
                                                    checked={fan.isOn}
                                                    onChange={() => this._handleChange(fan)}
                                                    className="switch"
                                                    type="checkbox"/>
                                                <div>
                                                    <span>
                                                        <g className="icon icon-toolbar grid-view"/>
                                                    </span>
                                                    <span>
                                                        <g className="icon icon-toolbar ticket-view"/>
                                                    </span>
                                                    <div/>
                                                </div>
                                            </label>
                                        </div>
                                    </CardBody>
                                </Card>
                            </Col>
                        ))}
                </Row>
            </div>
        )
    }
    
    _handleChange(fan) {
      console.log(fan);
      fan.isOn = !fan.isOn;
      const isLoaded = false;
      this.props.updateFanStatus(fan);
    }
}

Fans.propTypes = {
    fansRes: PropTypes.array.isRequired
}