import React, { Component } from "react";
import PropTypes from "prop-types";
import Plotly from "plotly.js/dist/plotly-cartesian";

import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";
import { DateRangePicker } from "react-date-range";
import { format, addDays } from "date-fns";
import { Button, Collapse } from "reactstrap";

var data = [];
var layoutUpdate = {
  xaxis: {
    title: "Time"
  },
  yaxis: {
    title: "Humidity"
  },
  title: "Humidity sensor",
  updatemenus: [
    {
      buttons: [
        {
          label: "Show",
          method: "restyle",
          args: ["visible", true]
        },
        {
          label: "Hide",
          method: "restyle",
          args: ["visible", "legendonly"]
        }
      ]
    }
  ]
};
var otherSettings = {
  modeBarButtonsToAdd: [
    {
      name: "show",
      click: gd => {
        Plotly.restyle(gd, "visible", true);
      }
    },
    {
      name: "hide",
      click: gd => {
        Plotly.restyle(gd, "visible", "legendonly");
      }
    }
  ]
};

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
    this.toggle = this.toggle.bind(this);
    this.state = {
      collapse: false,
      xRange: [],
      plotlyGraph: {
        data: [],
        range: []
      },
      dateRangePicker: {
        selection: {
          startDate: new Date(),
          endDate: new Date(),
          key: "selection"
        }
      }
    };
  }

  toggle() {
    this.setState({
      collapse: !this.state.collapse
    });
  }

  async handleRangeChange(which, payload) {
    await this.setState({
      [which]: {
        ...this.state[which],
        ...payload
      }
    });

    if (this.props.humidities != undefined) {
      data = this.props.humidities.items;
    }

    var layout = {
      xaxis: {
        range: [
          formatStartDateDisplay(
            this.state.dateRangePicker.selection.startDate
          ),
          formatEndDateDisplay(this.state.dateRangePicker.selection.endDate)
        ]
      }
    };

    Plotly.update("humidities", data, layout);
  }

  render() {
    // console.log(this.props.humidities != undefined)
    if (
      this.props.humidities != undefined &&
      this.props.humidities.length !== 0
    ) {
      data = this.props.humidities;
      Plotly.newPlot("humidities", data, layoutUpdate, otherSettings);
    }
    return (
      <div>
        <Button
          color="primary"
          onClick={this.toggle}
          style={{
            marginBottom: "1rem"
          }}
        >
          Date Picker
        </Button>
        <Collapse isOpen={this.state.collapse}>
          <DateRangePicker
            style={{
              width: "100%"
            }}
            onChange={this.handleRangeChange.bind(this, "dateRangePicker")}
            showSelectionPreview={true}
            ranges={[this.state.dateRangePicker.selection]}
            moveRangeOnFirstSelection={false}
          />
        </Collapse>
      </div>
    );
  }
}

Humidities.propTypes = {
  humiditiesRes: PropTypes.array.isRequired
};
