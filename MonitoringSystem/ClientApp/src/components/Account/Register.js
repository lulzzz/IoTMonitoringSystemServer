import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/Register";
import "font-awesome/css/font-awesome.min.css";
import {
  Button,
  Card,
  CardBody,
  CardFooter,
  Col,
  Container,
  Input,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Row
} from "reactstrap";

class Register extends Component {
  constructor(props) {
    super(props);

    this.state = {
      user: {
        email: "",
        password: "",
        confirmPassword: ""
      },
      submitted: false
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    const { name, value } = event.target;
    const { user } = this.state;
    this.setState({
      user: {
        ...user,
        [name]: value
      }
    });
  }

  handleSubmit(event) {
    event.preventDefault();
    this.setState({ submitted: true });
    const { user } = this.state;
    if (user.email && user.password && user.confirmPassword) {
      this.props.register(user);
    }
  }

  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestRegister(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const isLoaded = false;
    this.props.requestRegister(isLoaded);
  }

  render() {
    return (
      <div className="app flex-row align-items-center">
        <form onSubmit={this.handleSubmit}>
          <Container>
            <Row className="justify-content-center">
              <Col md="6">
                <Card className="mx-4">
                  <CardBody className="p-4">
                    <h1>Register</h1>
                    <p className="text-muted">Create your account</p>
                    <p style={{ color: "red" }}>{this.props.errorMessage}</p>
                    <InputGroup className="mb-3">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>@</InputGroupText>
                      </InputGroupAddon>
                      <Input
                        type="text"
                        placeholder="Email"
                        name="email"
                        value={this.state.user.email}
                        onChange={this.handleChange}
                      />
                    </InputGroup>
                    <InputGroup className="mb-3">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="fa fa-unlock-alt" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        type="password"
                        placeholder="Password"
                        name="password"
                        value={this.state.user.password}
                        onChange={this.handleChange}
                      />
                    </InputGroup>
                    <InputGroup className="mb-4">
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>
                          <i className="fa fa-unlock-alt" />
                        </InputGroupText>
                      </InputGroupAddon>
                      <Input
                        type="password"
                        placeholder="Confirm password"
                        name="confirmPassword"
                        value={this.state.user.confirmPassword}
                        onChange={this.handleChange}
                      />
                    </InputGroup>
                    <Button color="success" block>
                      Create Account
                    </Button>
                  </CardBody>
                  <CardFooter className="p-4">
                    <Row>
                      <Col xs="12" sm="6">
                        <Button
                          className="fa fa-facebook-f"
                          style={{
                            color: "white",
                            backgroundColor: "#30497C"
                          }}
                          block
                        >
                          <span> facebook</span>
                        </Button>
                      </Col>
                      <Col xs="12" sm="6">
                        <Button
                          className="fa fa-twitter"
                          style={{
                            color: "white",
                            backgroundColor: "#0090C7"
                          }}
                          block
                        >
                          <span> twitter</span>
                        </Button>
                      </Col>
                    </Row>
                  </CardFooter>
                </Card>
              </Col>
            </Row>
          </Container>
        </form>
      </div>
    );
  }
}

export default connect(
  state => state.register,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Register);
