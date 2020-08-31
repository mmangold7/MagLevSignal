import React, { Component } from 'react';

export class Simulator extends Component {
  static displayName = Simulator.name;

  constructor(props) {
      super(props);
      this.state = { result: "", loading: true };
  }

  componentDidMount() {
    this.startSimulator();
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.state.result;

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        Simulator Started: {contents}
      </div>
    );
  }

    async startSimulator() {
    const response = await fetch('simulator');
    const data = await response.json();
    this.setState({ result: data.success.toString(), loading: false });
  }
}
