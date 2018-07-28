import React, {Component} from 'react';
import Temperatures from './AsyncTemperatures';
import {Row, Col} from 'reactstrap'

class Dashboard extends Component {
    render() {
        return (
            <Row>
                <Col xs="6">
                    <div className="Dashboard">
                        <Temperatures/>
                    </div>
                </Col>
                <Col xs="6">Độ ẩm</Col>
            </Row>
        );
    }
}
export default Dashboard;