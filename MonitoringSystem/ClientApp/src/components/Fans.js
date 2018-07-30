import {connect} from 'react-redux'
import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {updateFanStatus, fetchFansIfNeeded, invalidateUpdateFan} from '../actions/FansActions'
import {
    Card,
    Button,
    CardTitle,
    CardText,
    Row,
    Col,
    CardBody
} from "reactstrap";

class Fans extends Component {

    render() {

        var isEmpty = this.props.fans
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
        fan.isOn = !fan.isOn;
        const {dispatch, fans} = this.props
        // dispatch(invalidateUpdateFan(fan, fans))
        dispatch(updateFanStatus(fan, fans))
        // await this
        //     .props
        //     .dispatch(updateFanStatus(fan, fans))
        //   const isLoaded = false;   this.props.updateFanStatus(fan);
    }

}

Fans.propTypes = {
    fansRes: PropTypes.array.isRequired
}

export default connect()(Fans)