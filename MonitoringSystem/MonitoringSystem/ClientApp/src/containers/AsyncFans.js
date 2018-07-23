import React, {Component} from 'react'
import PropTypes from 'prop-types'
import {connect} from 'react-redux'
import {fetchFansIfNeeded} from '../actions/FansActions'
import Fans from '../components/Fans'

class AsyncFans extends Component {
    constructor(props) {
        super(props)
        // this.handleChange = this     .handleChange     .bind(this)
        // this.handleRefreshClick = this     .handleRefreshClick     .bind(this)
    }

    componentDidMount() {
        const {dispatch} = this.props
        dispatch(fetchFansIfNeeded())
        console.log('didMount')
        console.log(this.props)
        // console.log(this.props)
    }

    // componentDidUpdate(prevProps) {     if (this.props.selectedSubreddit !==
    // prevProps.selectedSubreddit) {         const {dispatch, selectedSubreddit} =
    // this.props         dispatch(fetchPostsIfNeeded(selectedSubreddit))     } }
    // handleChange(nextSubreddit) {     this         .props
    // .dispatch(selectSubreddit(nextSubreddit))     this         .props
    // .dispatch(fetchPostsIfNeeded(nextSubreddit)) } handleRefreshClick(e) {
    // e.preventDefault()     const {dispatch, selectedSubreddit} = this.props
    // dispatch(invalidateSubreddit(selectedSubreddit))
    // dispatch(fetchPostsIfNeeded(selectedSubreddit)) }

    render() {
        console.log("render")
        console.log(this.props.fansRes)
        const {fans, isFetching, lastUpdated, fansRes} = this.props
        return (
            <div>

                <p>
                    {lastUpdated && <span>
                        Last updated at {new Date(lastUpdated).toLocaleTimeString()}. {' '}
                    </span>}
                    {/* {!isFetching && <button onClick={this.handleRefreshClick}>
                        Refresh
                    </button>} */}
                </p>
                {isFetching && fansRes.length === 0 && <h2>Loading...</h2>}
                {/* 
                {!isFetching && fans.length === 0 && <h2>Empty.</h2>}
                {fans.length > 0 && <div
                    style={{
                    opacity: isFetching
                        ? 0.5
                        : 1
                }}>
                    <Fans fans={fans}/>
                </div>} */}
                <Fans fansRes={fansRes}/>
            </div>
        )
    }

}

AsyncFans.propTypes = {
    fans: PropTypes.array.isRequired,
    isFetching: PropTypes.bool.isRequired,
    lastUpdated: PropTypes.number,
    dispatch: PropTypes.func.isRequired
}


function mapStateToProps(state) {

    const {fanList} = state
    const {isFetching, lastUpdated, items: fans} = fanList || {
        isFetching: true,
        items: []
    }
    const fansRes = Object.values(state.fans)[0];

    return {fansRes, isFetching, lastUpdated}
}

export default connect(mapStateToProps)(AsyncFans)