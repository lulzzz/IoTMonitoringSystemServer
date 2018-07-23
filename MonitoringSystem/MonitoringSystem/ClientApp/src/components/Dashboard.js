import React, {Component} from "react";
import {connect} from "react-redux";
import Plot from "react-plotly.js";
import {actionCreators} from "../store/Temperatures";
import {bindActionCreators} from "redux";
import 'react-date-range/dist/styles.css'; // main style file
import 'react-date-range/dist/theme/default.css'; // theme css file
import {DateRangePicker} from 'react-date-range';

const xRange = [];

class Dashboard extends Component {
    handleSelect(ranges) {
        console.log(ranges);
        // { 	selection: { 		startDate: [native Date Object], 		endDate: [native Date
        // Object], 	} }
    }
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
        const selectionRange = {
            startDate: new Date(),
            endDate: new Date(),
            key: 'selection'
        }
        return (

            <div>
                <DateRangePicker ranges={[selectionRange]} onChange={this.handleSelect}/>
                <Plot
                    data={this.props.temperatures.items}
                    layout={{
                    width: 600,
                    height: 500,
                    xaxis: {
                        title: 'Thời gian',
                        // range: 
                    },
                    yaxis: {
                        title: 'Nhiệt độ'
                    },
                    title: "Nhiệt Độ Cảm Biến"
                }}/>
            </div>
        );
    }
}
export default connect(state => state.temperatures, dispatch => bindActionCreators(actionCreators, dispatch))(Dashboard);
