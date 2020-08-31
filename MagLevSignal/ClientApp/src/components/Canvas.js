import React, { Component } from 'react';
import './Canvas.css';

export class Canvas extends Component {
    static displayName = Simulator.name;

    constructor(props) {
        super(props);
        //this.state = { result: "", loading: true };
    }

    componentDidMount() {
        //this.startSimulator();
    }

    render() {
        return (
            <div id="container">
                <canvas id="imgCanvas">
                    Your browser does not support the HTML5 canvas tag.
                </canvas>
                <div id="overlay">
                    <label for="shipColorPicker">Ship Color:</label>
                    <input class="color" id="shipColorPicker" />
                    <input type="button" id="playButton" value="Play" />
                </div>
            </div>
        );
    }
}
