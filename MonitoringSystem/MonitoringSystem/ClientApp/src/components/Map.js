import React, { Component } from "react";
import { connect } from "react-redux";
import '../components/Map.css';
import map from '../assets/img/Map.png';
import logo from '../assets/img/logo_vntt.png';
import ToolTip from 'react-portal-tooltip'

class Map extends Component {
    state = {
        isTooltipActive: false
    }
    showTooltip() {
        this.setState({ isTooltipActive: true })
    }
    hideTooltip() {
        this.setState({ isTooltipActive: false })
    }
    render() {
        return (
            <div className="App">
                <div className="container">
                    <img className="map" src={map} style={{ maxWidth: '200%', maxHeight: 'auto' }} />
                    {/* row 1 */}
                    <div className="row">
                        <button type="button" className="btn" id="sensor1-1" >
                            <p id="text" onMouseEnter={this.showTooltip.bind(this)} onMouseLeave={this.hideTooltip.bind(this)}>S1.1</p>
                            <ToolTip active={this.state.isTooltipActive} position="top" arrow="center" parent="#text">
                                <div>Phu cute dep trai Vo doi!! </div>
                            </ToolTip>
                        </button>

                        <button type="button" className="btn" id="sensor1-2" >
                            <p id="text" onMouseEnter={this.showTooltip.bind(this)} onMouseLeave={this.props.hideTooltip}>S1.2</p>
                            <ToolTip active={this.state.isTooltipActive} position="top" arrow="center" parent="#text">
                                <div>noi dung muon hien thi</div>
                            </ToolTip>
                        </button>
                        <button type="button" className="btn" id="sensor1-3">S1.3</button>

                        <button type="button" className="btn" id="sensor1-4">S1.4</button>
                        <button type="button" className="btn" id="sensor1-5" >S1.5</button>
                        <button type="button" className="btn" id="sensor1-6">S1.6</button>

                        <button type="button" className="btn" id="sensor1-7">S1.7</button>
                        <button type="button" className="btn" id="sensor1-8" >S1.8</button>
                        <button type="button" className="btn" id="sensor1-9">S1.9</button>
                    </div>
                    {/* row 2  */}
                    <div className="row">
                        <button type="button" className="btn" id="sensor2-1">S2.1</button>
                        <button type="button" className="btn" id="sensor2-2" >S2.2</button>
                        <button type="button" className="btn" id="sensor2-3">S2.3</button>

                        <button type="button" className="btn" id="sensor2-4">S2.4</button>
                        <button type="button" className="btn" id="sensor2-5" >S2.5</button>
                        <button type="button" className="btn" id="sensor2-6">S2.6</button>

                        <button type="button" className="btn" id="sensor2-7">S2.7</button>
                        <button type="button" className="btn" id="sensor2-8" >S2.8</button>
                    </div>
                    {/* row 3 */}

                    <div className="row">
                        <button type="button" className="btn" id="sensor3-1">S3.1</button>
                        <button type="button" className="btn" id="sensor3-2" >S3.2</button>
                        <button type="button" className="btn" id="sensor3-3">S3.3</button>

                        <button type="button" className="btn" id="sensor3-4">S3.4</button>
                        <button type="button" className="btn" id="sensor3-5" >S3.5</button>
                        <button type="button" className="btn" id="sensor3-6">S3.6</button>

                        <button className="btn" id="sensor3-7">S3.7</button>
                    </div>

                    {/* row 4 */}
                    <div className="row">
                        <button type="button" className="btn" id="sensor4-1">S4.1</button>
                        <button type="button" className="btn" id="sensor4-2" >S4.2</button>
                        <button type="button" className="btn" id="sensor4-3">S4.3</button>

                        <button type="button" className="btn" id="sensor4-4">S4.4</button>
                        <button type="button" className="btn" id="sensor4-5" >S4.5</button>
                        <button type="button" className="btn" id="sensor4-6">S4.6</button>

                        <button type="button" className="btn" id="sensor4-7">S4.7</button>
                        <button type="button" className="btn" id="sensor4-8">S4.8</button>
                    </div>
                    {/* row 5 */}
                    <div className="row">
                        <button type="button" className="btn" id="sensor5-1">S5.1</button>
                        <button type="button" className="btn" id="sensor5-2" >S5.2</button>
                        <button type="button" className="btn" id="sensor5-3">S5.3</button>

                        <button type="button" className="btn" id="sensor5-4">S5.4</button>
                        <button type="button" className="btn" id="sensor5-5" >S5.5</button>
                        <button type="button" className="btn" id="sensor5-6">S5.6</button>

                        <button type="button" className="btn" id="sensor5-7">S5.7</button>
                        <button type="button" className="btn" id="sensor5-8" >S5.8</button>
                        <button type="button" className="btn" id="sensor5-9">S5.9</button>
                    </div>
                    {/* row 6 */}
                    <div className="row">
                        <button type="button" className="btn" id="sensor6-1">S6.1</button>
                        <button type="button" className="btn" id="sensor6-2" >S6.2</button>
                        <button type="button" className="btn" id="sensor6-3">S6.3</button>

                        <button type="button" className="btn" id="sensor6-4">S6.4</button>
                        <button type="button" className="btn" id="sensor6-5" >S6.5</button>
                        <button type="button" className="btn" id="sensor6-6">S6.6</button>

                       {/* <button className="btn" id="sensor6-7">S6.7</button> */}

                       <button type="button" className="btn" id="sensor6-7">
                            <p id="text" onMouseEnter={this.showTooltip.bind(this)} onMouseLeave={this.hideTooltip.bind(this)}>S6.7</p>
                            <ToolTip active={this.state.isTooltipActive} position="top" arrow="center" parent="#text">
                                <div>Phu cute dep trai Vo doi!! </div>
                            </ToolTip>
                        </button>
                    </div>

                    {/* note  */}
                    <button className="btn">S6.6</button>

                    <button className="btn" >S6.7</button>

                </div>

                {/* <button type="button" className="btn">
                    <p id="text" onMouseEnter={this.showTooltip.bind(this)} onMouseLeave={this.hideTooltip.bind(this)}>.</p>
                    <ToolTip active={this.state.isTooltipActive} position="top" arrow="center" parent="#text">
                        <div>
                            Phu cute dep trai Vo doi!!
                    </div>
                    </ToolTip>
                </button> */}

            </div>
        );
    }



}


export default Map;