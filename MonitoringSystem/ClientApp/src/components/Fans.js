import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {Card, CardTitle, Row, Col, CardBody} from "reactstrap";

export default class Fans extends Component {

    render() {
        const {fans, onChange} = this.props
        console.log(fans)
        return (
            <div>
                <Row>
                    {fans.map(fan => (
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
                                                onChange={() => onChange(fan, fans)}
                                                className="switch"
                                                type="checkbox"/>
                                            <div>
                                                <span>
                                                    <i className="icon icon-toolbar grid-view"/>
                                                </span>
                                                <span>
                                                    <i className="icon icon-toolbar ticket-view"/>
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

}

Fans.propTypes = {
    fans: PropTypes.array.isRequired
}