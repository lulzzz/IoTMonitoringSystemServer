import React, {Component} from "react";
import {connect} from "react-redux";
import {actionCreators} from "../store/Fans";
import {bindActionCreators} from "redux";

class Fan extends Component {
    componentWillMount() {
        // This method runs when the component is first added to the page
        const isLoaded = true;

        this
            .props
            .requestFans(isLoaded);
    }

    componentWillReceiveProps(nextProps) {
        // This method runs when incoming props (e.g., route params) change
        const isLoaded = true;
        this
            .props
            .requestFans(isLoaded);
        console.log(this.props.fans);
    }

    render() {
        return (
            <div>
            aa
                {/* {this.props.fans.items.map((fan) =>
                    <li key={fan.fanId}>
                        {fan.fanName}
                    </li>
                )} */}
            </div>
        );
    }
}
export default connect(state => state.fans, dispatch => bindActionCreators(actionCreators, dispatch))(Fan);
