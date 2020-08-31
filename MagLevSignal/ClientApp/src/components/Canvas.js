import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr';
import './Canvas.css';

export class Canvas extends Component {
    static displayName = Canvas.name;

    constructor(props) {
        super(props);
        //this.state = { result: "", loading: true };

        let connection = new HubConnectionBuilder().withUrl("/magLevHub").build();
        connection.start().then(function () {
            connection.invoke("GetConnectionId")
                .then(function (connectionId) {
                    //playerId = connectionId;
                });
        }).catch(function (err) {
            return console.error(err.toString());
        });

        //var canvas = document.getElementById("imgCanvas");
        //var canvasWidth = canvas.width = window.innerWidth;
        //var canvasHeight = canvas.height = window.innerHeight;
        //var context = canvas.getContext("2d");

        //var gridSpacing = 200;
        //var gridRadius = 10000;

        connection.on("GameState",
            function (bodies) {
                console.log(bodies);

                ////center view on sun
                //displayOffsetBody = bodies.filter(body => body.name === "sun")[0];

                ////draw black background
                //context.clearRect(0, 0, canvas.width, canvas.height);
                //context.fillStyle = "black";
                //context.fillRect(0, 0, canvas.width, canvas.height);

                ////draw debug info
                ////context.font = "10px Arial";
                ////context.fillText("Hello World", 10, 50);
                //if (debugEnabled) {
                //    drawGrid();
                //}

                ////draw bodies
                //bodies.forEach(body => drawBody(body));

                //if (firstUpdate && displayOffsetBody !== "undefined" && displayOffsetBody !== null) {
                //    //Put things here you only want to happen once
                //    firstUpdate = false;
                //}
            });
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
            </div>
        );
    }

    //drawGrid() {
    //    context.beginPath();
    //    for (var i = -gridRadius / gridSpacing; i <= gridRadius / gridSpacing; i++) {
    //        context.moveTo(i * gridSpacing, -gridRadius);
    //        context.lineTo(i * gridSpacing, gridRadius);
    //        context.moveTo(-gridRadius, i * gridSpacing);
    //        context.lineTo(gridRadius, i * gridSpacing);
    //    }

    //    context.strokeStyle = "white";
    //    context.stroke();
    //}

    //drawBody(body) {
    //    context.beginPath();
    //    if (body.hasOwnProperty("id")) { //if player

    //        var offsetFactor = 0.3;
    //        var nosePosition = {
    //            x: body.position.x + body.radius * (1 + offsetFactor),
    //            y: body.position.y
    //        };
    //        var backRightPosition = {
    //            x: body.position.x - body.radius * (1 - offsetFactor),
    //            y: body.position.y + body.radius
    //        };
    //        var backLeftPosition = {
    //            x: body.position.x - body.radius * (1 - offsetFactor),
    //            y: body.position.y - body.radius
    //        };
    //        var backMiddlePosition = {
    //            x: body.position.x - body.radius * (1 - offsetFactor),
    //            y: body.position.y
    //        };

    //        var forwardBackwardExhaustLength = body.radius * 3;
    //        var frontSideExhaustLength = body.radius * 3;
    //        var backSideExhaustLength = body.radius * 1.5;

    //        //rotate drawing plane to match ship angle
    //        context.save();
    //        context.translate(body.position.x, body.position.y);
    //        context.rotate(body.angle * Math.PI / 180);
    //        context.translate(-body.position.x, -body.position.y);

    //        //draw effects
    //        context.beginPath();
    //        if (body.input.upPressed) {
    //            context.moveTo(backMiddlePosition.x, backMiddlePosition.y);
    //            context.lineTo(backMiddlePosition.x - forwardBackwardExhaustLength, backMiddlePosition.y);
    //        }
    //        if (body.input.downPressed) {
    //            context.moveTo(nosePosition.x, nosePosition.y);
    //            context.lineTo(nosePosition.x + forwardBackwardExhaustLength, nosePosition.y);
    //        }
    //        if (body.input.leftPressed) {
    //            context.moveTo(nosePosition.x, nosePosition.y);
    //            context.lineTo(nosePosition.x, nosePosition.y + frontSideExhaustLength);
    //            context.moveTo(backLeftPosition.x, backLeftPosition.y);
    //            context.lineTo(backLeftPosition.x, backLeftPosition.y - backSideExhaustLength);
    //        }
    //        if (body.input.rightPressed) {
    //            context.moveTo(nosePosition.x, nosePosition.y);
    //            context.lineTo(nosePosition.x, nosePosition.y - frontSideExhaustLength);
    //            context.moveTo(backRightPosition.x, backRightPosition.y);
    //            context.lineTo(backRightPosition.x, backRightPosition.y + backSideExhaustLength);
    //        }
    //        context.strokeStyle = "yellow";
    //        context.stroke();

    //        //draw shield outline
    //        context.lineWidth = 1;
    //        var numShieldLayers = body.shieldHealth / 10;
    //        for (var layerNum = 1; layerNum <= numShieldLayers; layerNum++) {
    //            var shieldLayerOpacity = 1.0 - (layerNum / numShieldLayers);
    //            shieldLayerOpacity *= 0.5;
    //            context.strokeStyle = "rgba(220, 220, 255, " + shieldLayerOpacity + ")";
    //            context.beginPath();
    //            context.arc(body.position.x,
    //                body.position.y,
    //                body.radius * 2 + layerNum,
    //                0,
    //                2 * Math.PI);
    //            context.stroke();
    //        }

    //        //draw shield body
    //        var shieldBodyOpacity = 0.25 * body.shieldHealth / 100.0;
    //        context.fillStyle = "rgba(200, 200, 255, " + shieldBodyOpacity + ")";
    //        context.beginPath();
    //        context.arc(body.position.x,
    //            body.position.y,
    //            body.radius * 2,
    //            0,
    //            2 * Math.PI);
    //        context.fill();

    //        //draw ship body
    //        context.beginPath();
    //        context.moveTo(nosePosition.x, nosePosition.y);
    //        context.lineTo(backLeftPosition.x, backLeftPosition.y);
    //        context.lineTo(backRightPosition.x, backRightPosition.y);
    //        context.fillStyle = body.color;
    //        context.fill();

    //        //restore rotation
    //        context.restore();
    //    } else { //else if celestial body
    //        context.arc(body.position.x,
    //            body.position.y,
    //            body.radius,
    //            0,
    //            2 * Math.PI);
    //        context.fillStyle = body.color;
    //        context.fill();
    //    }
    //}
}
