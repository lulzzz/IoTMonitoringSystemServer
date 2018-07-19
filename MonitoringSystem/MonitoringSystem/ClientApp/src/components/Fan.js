import {bindActionCreators} from 'redux';
import React, {Component} from
"react";
import {connect} from "react-redux";
import {actionCreators} from
"../store/Fans";
import {
    Card,
    Button,
    CardTitle,
    CardText,
    Row,
    Col,
    CardBody
} from "reactstrap";
class Fan extends Component
{
    componentWillMount() {
        const isLoaded = true;
        this
            .props
            .requestFans(isLoaded);
    }
    componentWillReceiveProps() {
        const isLoaded = true;
        this
            .props
            .requestFans(isLoaded);
    }
    render() {
        return (
            <div>
                <Row>
                    {this.props.fans.items && this
                        .props
                        .fans
                        .items
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
                                                    onChange={this._handleChange}
                                                    className="switch"
                                                    type="checkbox"/>
                                                <div>
                                                    <span>
                                                        <g className="icon icon-toolbar grid-view"></g>
                                                    </span>
                                                    <span>
                                                        <g className="icon icon-toolbar ticket-view"></g>
                                                    </span>
                                                    <div></div>
                                                </div>
                                            </label>
                                        </div>
                                    </CardBody>
                                </Card>
                            </Col>
                        ))}
                </Row>
            </div>
        );
    }
    _handleChange() {
        this.setState({
            isChecked: !this.state.isChecked
        });
    }
}
export
default connect(state => state.fans, dispatch => bindActionCreators(actionCreators, dispatch))(Fan);
// import React, {Component} from 'react' import PropTypes from 'prop-types'
// import {connect} from 'react-redux' import {fetchFansIfNeeded} from
// '../store/actions' import Fans from '../components/Fans' class Fan extends
// Component {     constructor(props) {         super(props) this.handleChange =
// this             .handleChange             .bind(this)
// this.handleRefreshClick = this             .handleRefreshClick   .bind(this)
// }     componentDidMount() {         const {dispatch} = this.props
// dispatch(fetchFansIfNeeded())     } componentDidUpdate(prevProps) { if
// (this.props !== prevProps) {       const {dispatch} = this.props
// dispatch(fetchFansIfNeeded())         }     } handleChange(nextSubreddit) {
// this .props             .dispatch()     this             .props
// .dispatch(fetchFansIfNeeded(nextSubreddit))     }     handleRefreshClick(e) {
//         e.preventDefault()         const {dispatch, selectedSubreddit} =
// this.props         dispatch() dispatch(fetchFansIfNeeded(selectedSubreddit))
// }     render() { const {fans, isFetching, lastUpdated} = this.props
// console.log(); return (             <div>                 {/* <p>
// {lastUpdated && <span>                     Last updated at {new
// Date(lastUpdated).toLocaleTimeString()}. {' '}                     </span>}
// {!isFetching && <button onClick={this.handleRefreshClick}> Refresh </button>}
// </p> */} {isFetching && fans.length === 0 && <h2>Loading...</h2>} {/*
// {!isFetching && fans.length === 0 && <h2>Empty.</h2>} */}  {fans.length > 0
// && <div style={{        opacity: isFetching ? 0.5  : 1                 }}>
// <Fans fans={fans}/> </div>} <div>aaa</div> </div>         )     } }
// Fan.propTypes = { fans: PropTypes.array.isRequired,     isFetching:
// PropTypes.bool.isRequired, lastUpdated: PropTypes.number,     dispatch:
// PropTypes.func.isRequired } function mapStateToProps(state) {     const
// {fanList} = state     const {isFetching, lastUpdated, items: fans} = fanList
// || {         isFetching: true,         items: []     }     return {fans,
// isFetching, lastUpdated} } export default connect(mapStateToProps)(Fan)