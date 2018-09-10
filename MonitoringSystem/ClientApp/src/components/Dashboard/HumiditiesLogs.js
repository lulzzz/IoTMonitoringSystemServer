import React, { Component } from "react";
import PropTypes from "prop-types";
import Plotly from "plotly.js/dist/plotly-cartesian";

import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";
import { DateRangePicker } from "react-date-range";
import { format, addDays } from "date-fns";
import { Collapse } from "reactstrap";
import { Button, Modal, Input } from "semantic-ui-react";

var data = [];

//icon show on dashboard
var icona = {
  width: 1000,
  height: 1000,
  path:
    "M569.354 231.631C512.969 135.949 407.81 72 288 72 168.14 72 63.004 135.994 6.646 231.631a47.999 47.999 0 0 0 0 48.739C63.031 376.051 168.19 440 288 440c119.86 0 224.996-63.994 281.354-159.631a47.997 47.997 0 0 0 0-48.738zM288 392c-75.162 0-136-60.827-136-136 0-75.162 60.826-136 136-136 75.162 0 136 60.826 136 136 0 75.162-60.826 136-136 136zm104-136c0 57.438-46.562 104-104 104s-104-46.562-104-104c0-17.708 4.431-34.379 12.236-48.973l-.001.032c0 23.651 19.173 42.823 42.824 42.823s42.824-19.173 42.824-42.823c0-23.651-19.173-42.824-42.824-42.824l-.032.001C253.621 156.431 270.292 152 288 152c57.438 0 104 46.562 104 104z",
  transform: "matrix(1.7 0 0 -1.7 0 850)"
};

//icon hide on dashboard
var iconb = {
  width: 1000,
  height: 1000,
  path:
    "M286.693 391.984l32.579 46.542A333.958 333.958 0 0 1 288 440C168.19 440 63.031 376.051 6.646 280.369a47.999 47.999 0 0 1 0-48.739c24.023-40.766 56.913-75.775 96.024-102.537l57.077 81.539C154.736 224.82 152 240.087 152 256c0 74.736 60.135 135.282 134.693 135.984zm282.661-111.615c-31.667 53.737-78.747 97.46-135.175 125.475l.011.015 41.47 59.2c7.6 10.86 4.96 25.82-5.9 33.42l-13.11 9.18c-10.86 7.6-25.82 4.96-33.42-5.9L100.34 46.94c-7.6-10.86-4.96-25.82 5.9-33.42l13.11-9.18c10.86-7.6 25.82-4.96 33.42 5.9l51.038 72.617C230.68 75.776 258.905 72 288 72c119.81 0 224.969 63.949 281.354 159.631a48.002 48.002 0 0 1 0 48.738zM424 256c0-75.174-60.838-136-136-136-17.939 0-35.056 3.473-50.729 9.772l19.299 27.058c25.869-8.171 55.044-6.163 80.4 7.41h-.03c-23.65 0-42.82 19.17-42.82 42.82 0 23.626 19.147 42.82 42.82 42.82 23.65 0 42.82-19.17 42.82-42.82v-.03c18.462 34.49 16.312 77.914-8.25 110.95v.01l19.314 27.061C411.496 321.2 424 290.074 424 256zM262.014 356.727l-77.53-110.757c-5.014 52.387 29.314 98.354 77.53 110.757z",
  transform: "matrix(1.7 0 0 -1.7 0 850)"
};

var otherSettings = {
  modeBarButtonsToAdd: [
    {
      name: "show",
      icon: icona,
      click: gd => {
        Plotly.restyle(gd, "visible", true);
      }
    },
    {
      name: "hide",
      icon: iconb,
      click: gd => {
        Plotly.restyle(gd, "visible", "legendonly");
      }
    }
  ],
  displaylogo: false
};

function formatDate(date, defaultText) {
  if (!date) return defaultText;
  return format(date, "DD-MM-YYYY");
}

function formatStartDateDisplay(date, defaultText) {
  if (!date) return defaultText;
  return format(date, "YYYY-MM-DD 00:00");
}

function formatEndDateDisplay(date, defaultText) {
  if (!date) return defaultText;
  return format(date, "YYYY-MM-DD 23:59");
}

export default class Humidities extends Component {
  constructor(props, context) {
    super(props, context);
    // this.toggle = this.toggle.bind(this);
    this.state = {
      open: false,
      dateRangePicker: {
        selection: {
          startDate: new Date(new Date().setDate(new Date().getDate() - 30)),
          endDate: new Date(),
          key: "selection"
        }
      },
      startDate: formatStartDateDisplay(
        new Date(new Date().setDate(new Date().getDate() - 30))
      ),
      endDate: formatEndDateDisplay(new Date())
    };
  }

  show = dimmer => () =>
    this.setState({
      dimmer,
      open: true,
      dateRangePicker: {
        selection: {
          startDate: this.state.startDate,
          endDate: this.state.endDate,
          key: "selection"
        }
      }
    });
  close = () =>
    this.setState({
      open: false,
      startDate: this.state.dateRangePicker.selection.startDate,
      endDate: this.state.dateRangePicker.selection.endDate
    });

  async handleRangeChange(which, payload) {
    await this.setState({
      [which]: {
        ...this.state[which],
        ...payload
      }
    });

    if (this.props.humidities !== undefined) {
      data = this.props.humidities.items;
    }

    var layout = {
      xaxis: {
        range: [
          formatStartDateDisplay(
            this.state.dateRangePicker.selection.startDate
          ),
          formatEndDateDisplay(this.state.dateRangePicker.selection.endDate)
        ],
        mode: "lines+markers"
      }
    };

    Plotly.update("humidities", data, layout);
  }

  render() {
    const { open, dimmer } = this.state;
    if (
      this.props.humidities !== undefined &&
      this.props.humidities.length !== 0
    ) {
      var layoutUpdate = {
        xaxis: {
          title: "Time",
          range: [this.state.startDate, this.state.endDate]
        },
        yaxis: {
          title: "Humidity"
        },
        title: "Humidity Graph",
        font: {
          family: "Roboto, sans-serif"
        }
      };
      data = this.props.humidities;
      Plotly.newPlot("humidities", data, layoutUpdate, otherSettings);
    }
    return (
      <div>
        <Button
          inverted
          color="blue"
          onClick={this.show(true)}
          style={{
            marginBottom: "1rem"
          }}
        >
          Date Range
        </Button>
        <span className="input-date">
          <Button.Group>
            <Button
              style={{
                opacity: "1 !important;"
              }}
            >
              {formatDate(this.state.dateRangePicker.selection.startDate)}
            </Button>
            <Button.Or text="to" />
            <Button>
              {formatDate(this.state.dateRangePicker.selection.endDate)}
            </Button>
          </Button.Group>
        </span>
        <Modal
          dimmer={dimmer}
          open={open}
          onClose={this.close}
          className="datePicker"
        >
          <Modal.Content>
            <DateRangePicker
              style={{
                width: "100%"
              }}
              onChange={this.handleRangeChange.bind(this, "dateRangePicker")}
              showSelectionPreview={true}
              ranges={[this.state.dateRangePicker.selection]}
              moveRangeOnFirstSelection={false}
            />
          </Modal.Content>
          <Modal.Actions>
            <Button inverted color="green" onClick={this.close}>
              Confirm
            </Button>
          </Modal.Actions>
        </Modal>
        {/* <Collapse isOpen={this.state.collapse}>
          <DateRangePicker
            style={{
              width: "100%"
            }}
            onChange={this.handleRangeChange.bind(this, "dateRangePicker")}
            showSelectionPreview={true}
            ranges={[this.state.dateRangePicker.selection]}
            moveRangeOnFirstSelection={false}
          />
        </Collapse> */}
      </div>
    );
  }
}

Humidities.propTypes = {
  //humiditiesRes: PropTypes.array.isRequired
};
