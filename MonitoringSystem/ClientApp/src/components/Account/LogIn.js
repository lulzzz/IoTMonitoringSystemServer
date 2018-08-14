import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/LogIn";
import "font-awesome/css/font-awesome.min.css";
import {
  Button,
  Card,
  CardBody,
  CardGroup,
  Col,
  Container,
  Input,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Row
} from "reactstrap";
class LogIn extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      submitted: false
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(e) {
    const { name, value } = e.target;
    this.setState({ [name]: value });
  }

  handleSubmit(e) {
    e.preventDefault();

    this.setState({ submitted: true });
    const { email, password } = this.state;
    console.log(this.state);
    if (email && password) {
      this.props.logIn(email, password);
    }
  }

  componentWillMount() {
    // This method runs when the component is first added to the page
    const isLoaded = false;
    this.props.requestLogIn(isLoaded);
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
    const isLoaded = false;
    this.props.requestLogIn(isLoaded);
  }

  render() {
    return (
      <div className="app flex-row align-items-center">
        <form onSubmit={this.handleSubmit}>
          <Container>
            <Row className="justify-content-center">
              <Col md="8">
                <CardGroup>
                  <Card className="p-4">
                    <CardBody>
                      <h1>Login</h1>
                      <p style={{ color: "red" }}>{this.props.errorMessage}</p>
                      <p className="text-muted">Sign In to your account</p>
                      <p className="text-muted">a@a.com</p>
                      <p className="text-muted">abc@123</p>
                      <InputGroup className="mb-3">
                        <InputGroupAddon addonType="prepend">
                          <InputGroupText>
                            <i className="fa fa-user-secret" />
                          </InputGroupText>
                        </InputGroupAddon>
                        <Input
                          type="email"
                          name="email"
                          placeholder="Email"
                          value={this.state.email}
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
                          name="password"
                          placeholder="Password"
                          value={this.state.password}
                          onChange={this.handleChange}
                        />
                      </InputGroup>
                      <Row>
                        <Col xs="6">
                          <Button color="primary" className="px-4">
                            Login
                          </Button>
                        </Col>
                        <Col xs="6" className="text-right">
                          <Button color="link" className="px-0">
                            Forgot password?
                          </Button>
                        </Col>
                      </Row>
                    </CardBody>
                  </Card>
                  {/*<Card className="text-white bg-primary py-5 d-md-down-none" style={{ width: 44 + "%" }}>
                    <CardBody className="text-center">
                      <div>
                        <h2>Sign up</h2>
                        <p>
                          If you do not have an account, please click on the
                          'Register Now!' button to proceed to register page
                        </p>
                        {/* <Button color="primary" className="mt-3" active>
                          <Link to={`/Register`}>Register Now!</Link>
                        </Button> */}
                  {/*<p>
                          <Link
                            className="btn btn-primary active"
                            to={`/register`}
                          >
                            Register Now!
                          </Link>
                        </p>
                      </div>
                    </CardBody>
                  </Card>*/}
                </CardGroup>
              </Col>
            </Row>
          </Container>
        </form>
      </div>
    );
  }
}

export default connect(
  state => state.login,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(LogIn);
