import React, {Component} from 'react';
import Temperatures from './AsyncTemperatures';
import Humidities from './AsyncHumidities';
import {Row, Col} from 'reactstrap'

class Dashboard extends Component {
    render() {
        return (
            <div className="">
                <Row>
                    <Col xs="6">
                        <Temperatures/>
                    </Col>
                    <Col xs="6">
                        <Humidities/>
                    </Col>
                </Row>
            </div>
        );
    }
}
export default Dashboard;