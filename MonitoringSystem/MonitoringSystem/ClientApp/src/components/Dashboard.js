import React, {Component} from "react";
import {connect} from "react-redux";
import Plot from "react-plotly.js";
import {actionCreators} from "../store/Temperatures";
import {bindActionCreators} from "redux";

class Dashboard extends Component {
    componentWillMount() {
        // This method runs when the component is first added to the page
        const isLoaded = true;

        this
            .props
            .requestTemperatures(isLoaded);
    }

    componentWillReceiveProps(nextProps) {
        // This method runs when incoming props (e.g., route params) change
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
                    title: "Nhiệt Độ Cảm Biến"
                }}/>
            </div>
        );
    }
}
export default connect(state => state.temperatures, dispatch => bindActionCreators(actionCreators, dispatch))(Dashboard);
