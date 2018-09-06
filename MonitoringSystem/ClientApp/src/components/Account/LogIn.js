import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { actionCreators } from "../../store/LogIn";
import "font-awesome/css/font-awesome.min.css";
import { Button, Form, Card, Input, Icon } from "semantic-ui-react";

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

    if (email && password) {
      this.props.logIn(email, password);
    }
  }
  hau() {
    alert(document.getElementById("hau").value());
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
        <div className="login-card">
          <Card centered>
            <Card.Content>
              <Card.Header content="LOGIN" />
              <Card.Description>
                <Form onSubmit={this.handleSubmit}>
                  <Form.Field>
                    <Input
                      icon={<Icon name="mail" inverted circular link />}
                      placeholder={"Username..."}
                      name="email"
                      value={this.state.email}
                      onChange={this.handleChange}
                    />
                  </Form.Field>
                  <Form.Field>
                    <Input
                      type="password"
                      icon={<Icon name="key" inverted circular link />}
                      placeholder="Password..."
                      name="password"
                      value={this.state.password}
                      onChange={this.handleChange}
                    />
                  </Form.Field>
                  <br />
                  <div className="ui two buttons">
                    <Button className="login-button" basic color="green">
                      Login
                    </Button>
                    <Button className="forgot-button" basic color="red">
                      Forgot Password?
                    </Button>
                  </div>
                </Form>
              </Card.Description>
            </Card.Content>
          </Card>
        </div>

        {/* <form onSubmit={this.handleSubmit}>
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
                </CardGroup>
              </Col>
            </Row>
          </Container>
        </form> */}
      </div>
    );
  }
}

export default connect(
  state => state.login,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(LogIn);
