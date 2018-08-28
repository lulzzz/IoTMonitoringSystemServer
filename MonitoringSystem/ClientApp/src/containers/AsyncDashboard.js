import React, {Component} from 'react';
import Temperatures from './AsyncTemperatures';
import Humidities from './AsyncHumidities';
import {Row, Col,Breadcrumb,BreadcrumbItem} from 'reactstrap'

class Dashboard extends Component {
    render() {
        return (
            <div className="">
                {/* <Breadcrumb>
                    <BreadcrumbItem>DASHBOARD</BreadcrumbItem>
                </Breadcrumb> */}
                <Row>
                    <Col xs="12" lg="12" xl="6">
                        <Temperatures/>
                    </Col>
                    <Col xs="12" lg="12" xl="6">
                        <Humidities/>
                    </Col>
                </Row>
            </div>
        );
    }
}
export default Dashboard;