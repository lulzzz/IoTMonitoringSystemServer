// import React, { Component } from "react";
// import { bindActionCreators } from "redux";
// import { connect } from "react-redux";
// import '../components/Map.css';
// import map from '../assets/img/Map.png'
// import logo from '../assets/img/logo_vntt.png';
// import { Button, Popover, PopoverHeader, PopoverBody } from 'reactstrap';


// class Map extends React.Component {
//     constructor(props) {
//         super(props);

//         this.toggle = this.toggle.bind(this);
//         this.state = {
//             popoverOpen: false
//         };
//     }

//     toggle() {
//         this.setState({
//             popoverOpen: !this.state.popoverOpen
//         });
//     }

//     render() {
//         return (
//             <span>
//                 <div className="container">
//                     <img className="map" src={map} style={{ maxWidth: '200%', maxHeight: 'auto' }} />
//                     {/* row 1 */}
//                     <div className="row">
//                         <div>
//                             <Popover placement="bottom" isOpen={this.state.popoverOpen} target="sensor1-1" toggle={this.toggle}>
//                                 <PopoverHeader>Temporary</PopoverHeader>
//                                 <PopoverBody>
//                                     <table>
//                                         <thread>
//                                             <th>Date</th>
//                                             <th>Temp. (C)</th>
//                                         </thread>
//                                     </table>
//                                 </PopoverBody>
//                             </Popover>
//                         </div>


//                         <Button button type="button" className="btn" id="sensor1-1" onClick={this.toggle}>S1.1 </Button>
//                         <Button button type="button" className="btn" id="sensor1-2" onClick={this.toggle}>S1.2 </Button>





//                         <button type="button" className="btn" id="sensor1-3" >S1.3</button>
//                         <button type="button" className="btn" id="sensor1-4">S1.4</button>

//                         <button type="button" className="btn" id="sensor1-5">S1.5</button>
//                         <button type="button" className="btn" id="sensor1-6" >S1.6</button>
//                         <button type="button" className="btn" id="sensor1-7">S1.7</button>

//                         <button type="button" className="btn" id="sensor1-8">S1.8</button>
//                         <button type="button" className="btn" id="sensor1-9" >S1.9</button>

//                     </div>
//                     {/* row 2  */}
//                     <div className="row">
//                         <button type="button" className="btn" id="sensor2-1">S2.1</button>
//                         <button type="button" className="btn" id="sensor2-2" >S2.2</button>
//                         <button type="button" className="btn" id="sensor2-3">S2.3</button>

//                         <button type="button" className="btn" id="sensor2-4">S2.4</button>
//                         <button type="button" className="btn" id="sensor2-5" >S2.5</button>
//                         <button type="button" className="btn" id="sensor2-6">S2.6</button>

//                         <button type="button" className="btn" id="sensor2-7">S2.7</button>
//                         <button type="button" className="btn" id="sensor2-8" >S2.8</button>
//                     </div>
//                     {/* row 3 */}

//                     <div className="row">
//                         <button type="button" className="btn" id="sensor3-1">S3.1</button>
//                         <button type="button" className="btn" id="sensor3-2" >S3.2</button>
//                         <button type="button" className="btn" id="sensor3-3">S3.3</button>

//                         <button type="button" className="btn" id="sensor3-4">S3.4</button>
//                         <button type="button" className="btn" id="sensor3-5" >S3.5</button>
//                         <button type="button" className="btn" id="sensor3-6">S3.6</button>

//                         <button className="btn" id="sensor3-7">S3.7</button>
//                     </div>

//                     {/* row 4 */}
//                     <div className="row">
//                         <button type="button" className="btn" id="sensor4-1">S4.1</button>
//                         <button type="button" className="btn" id="sensor4-2" >S4.2</button>
//                         <button type="button" className="btn" id="sensor4-3">S4.3</button>

//                         <button type="button" className="btn" id="sensor4-4">S4.4</button>
//                         <button type="button" className="btn" id="sensor4-5" >S4.5</button>
//                         <button type="button" className="btn" id="sensor4-6">S4.6</button>

//                         <button type="button" className="btn" id="sensor4-7">S4.7</button>
//                         <button type="button" className="btn" id="sensor4-8">S4.8</button>
//                     </div>
//                     {/* row 5 */}
//                     <div className="row">
//                         <button type="button" className="btn" id="sensor5-1">S5.1</button>
//                         <button type="button" className="btn" id="sensor5-2" >S5.2</button>
//                         <button type="button" className="btn" id="sensor5-3">S5.3</button>

//                         <button type="button" className="btn" id="sensor5-4">S5.4</button>
//                         <button type="button" className="btn" id="sensor5-5" >S5.5</button>
//                         <button type="button" className="btn" id="sensor5-6">S5.6</button>

//                         <button type="button" className="btn" id="sensor5-7">S5.7</button>
//                         <button type="button" className="btn" id="sensor5-8" >S5.8</button>
//                         <button type="button" className="btn" id="sensor5-9">S5.9</button>
//                     </div>
//                     {/* row 6 */}
//                     <div className="row">
//                         <button type="button" className="btn" id="sensor6-1">S6.1</button>
//                         <button type="button" className="btn" id="sensor6-2" >S6.2</button>
//                         <button type="button" className="btn" id="sensor6-3">S6.3</button>

//                         <button type="button" className="btn" id="sensor6-4">S6.4</button>
//                         <button type="button" className="btn" id="sensor6-5" >S6.5</button>
//                         <button type="button" className="btn" id="sensor6-6">S6.6</button>

//                         <button className="btn" id="sensor6-7">S6.7</button>


//                     </div>

//                     {/* note  */}
//                     <button className="btn" id="sensor-safe">SAFE</button>

//                     <button className="btn" id="sensor-danger">DANGER</button>

//                 </div>


//             </span >
//         );

//     }
// }

// class PopoverExampleMulti extends React.Component {
//     constructor(props) {
//       super(props);

//       this.state = {
//         popovers: [
//           {
//             placement: 'top',
//             text: 'Top'
//           },
//           {
//             placement: 'bottom',
//             text: 'Bottom'
//           },
//           {
//             placement: 'left',
//             text: 'Left'
//           },
//           {
//             placement: 'right',
//             text: 'Right'
//           }
//         ]
//       };
//     }

//     render() {
//       return (

//         <div>
//         { this.state.popovers.map((popover, i) => {
//           return <Map key={i} item={popover} id={i} />;
//         })}
//       </div>
//       );
//     }
//   }

// export default Map;








import React, { Component } from "react";
import { Button, Popover, PopoverHeader, PopoverBody } from 'reactstrap';
import { actionCreators } from "../store/Temperatures";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
class PopoverItem extends React.Component {

  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      popoverOpen: false
    };
  }

  toggle() {
    this.setState({
      popoverOpen: !this.state.popoverOpen
    });
  }

  render() {
    return (
      <span>
        <Button className="mr-1" color="secondary" id={'Popover-' + this.props.id} onClick={this.toggle}>
          {this.props.item.text}
        </Button>
        <Popover placement={this.props.item.placement} isOpen={this.state.popoverOpen} target={'Popover-' + this.props.id} toggle={this.toggle}>
          <PopoverHeader>{this.props.item.value}</PopoverHeader>
          <PopoverBody>Sed posuere consectetur est at lobortis. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</PopoverBody>
        </Popover>
      </span>
    );
  }
}

class PopoverExampleMulti extends React.Component {
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
  constructor(props) {
    super(props);

    
    this.state = {
      popovers: [
        {
          placement: 'top',
          text: 'Top',
          value: 'Duy'
        },
        {
          placement: 'bottom',
          text: 'Bottom',
          value: 'Pu'
        },
        {
          placement: 'left',
          text: 'Left',
          value: 'Hoa'
        },
        {
          placement: 'right',
          text: 'Right',
          value: 'Hau'
        } 
      ]
    };
  }

  render() {
    return (
      <div>
        {this.state.popovers.map((popover, i) => {
          return <PopoverItem key={i} item={popover} id={i} />;
        })}
      </div>
    );
  }
}

export default connect(state => state.temperatures, dispatch => bindActionCreators(actionCreators, dispatch))(PopoverExampleMulti);