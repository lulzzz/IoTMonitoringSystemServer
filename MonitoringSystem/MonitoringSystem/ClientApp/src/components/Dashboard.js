import React, {Component} from "react";
import {connect} from "react-redux";
import Plot from "react-plotly.js";
import {actionCreators} from "../store/Temperatures";
import {bindActionCreators} from "redux";

class Dashboard extends Component {
    componentWillMount() {
        const isLoaded = true;
        this
            .props
            .requestTemperatures(isLoaded);
    }

    componentWillReceiveProps(nextProps) {
        const isLoaded = true;
        this
            .props
            .requestTemperatures(isLoaded);
    }
    render() {
        return (
            <div>
                <Plot
                    data={this.props.temperatures.items}
                    layout={{
                    height: 500,
                    width: 1000,
                    title: "Nhiệt Độ Cảm Biến",
                    yaxis: {
                        title: 'Nhiệt độ'
                    },
                    xaxis: {
                        title: 'Thời gian'
                    }
                }}/>

            </div>
        );
    }
}
export default connect(state => state.temperatures, dispatch => bindActionCreators(actionCreators, dispatch))(Dashboard);
