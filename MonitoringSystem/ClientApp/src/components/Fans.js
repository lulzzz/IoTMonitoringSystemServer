import React, { Component } from "react";
import PropTypes from "prop-types";
import { Card, CardTitle, CardBody } from "reactstrap";
import fanImg from "../assets/img/fanImg.png";
import { Grid } from "semantic-ui-react";

export default class Fans extends Component {
  render() {
    const { fans, onChange } = this.props;
    console.log(fans);
    return (
      <Grid>
        <Grid.Row columns={6}>
          {fans.map(fan => (
            <Grid.Column key={fan.fanId}>
              <Card className="text-center">
                <div style={{ padding: 10 }}>
                  <img
                    className={"rotating-" + fan.isOn}
                    width="100%"
                    id={fan.fanId}
                    src="http://mygreenhomedesign.com/wp-content/uploads/2016/04/MGH-Fan-Icon.png"
                    alt="Card cap"
                  />
                </div>
                <CardBody>
                  <CardTitle>{fan.fanName}</CardTitle>
                  <div className="switch-container">
                    <label>
                      <input
                        ref="switch"
                        checked={fan.isOn}
                        onChange={() => onChange(fan, fans)}
                        className="switch"
                        type="checkbox"
                      />
                      <div>
                        <span>
                          <i className="icon icon-toolbar grid-view" />
                        </span>
                        <span>
                          <i className="icon icon-toolbar ticket-view" />
                        </span>
                        <div />
                      </div>
                    </label>
                  </div>
                </CardBody>
              </Card>
            </Grid.Column>
          ))}
        </Grid.Row>
      </Grid>
    );
  }
}

Fans.propTypes = {
  fans: PropTypes.array.isRequired
};
