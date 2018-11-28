import React, { Component } from 'react';
import $ from 'jquery';

export class Home extends Component {
    displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            email: "",
            name: "",
            message: "",
            feedback: ""
        };
    }

    onHandleChange = event => {
        this.setState({
            [event.target.name]: event.target.value
        });
    }

    onSubmit() {
        console.log(this.state);
        $.ajax({
            url: '/api/contactmessages',
            contentType: 'application/json',
            dataType: 'json',
            method: 'POST',
            data: JSON.stringify({
                "email": this.state.email,
                "name": this.state.name,
                "message": this.state.message
            }),
            success: function (result) { 
                this.setState({ feedback: "New entity created with guid " + result.id });
                console.log(this.state);

            }.bind(this)
        });
    }

    render() {
        return (
            <div className="container-fluid"
                style={{ padding: '20px', background: 'white', marginTop: '20px' }}>
                <div className="row"
                    style={{ padding: "10px" }}>
                    <div className="col-xs-3">
                        <label htmlFor="name"
                            className="pull-right">
                            Name
                            </label>
                    </div>
                    <div className="col-xs-9">
                        <input type="text"
                            style={{ width: "100%" }}
                            value={this.state.name}
                            name="name"
                            onChange={this.onHandleChange}
                        />
                    </div>
                </div>
                <div className="row"
                    style={{ padding: "10px" }}>
                    <div className="col-xs-3">
                        <label htmlFor="email"
                            className="pull-right">
                            Email
                            </label>
                    </div>
                    <div className="col-xs-9">
                        <input type="text"
                            style={{ width: "100%" }}
                            name="email"
                            value={this.state.email}
                            onChange={this.onHandleChange} />
                    </div>
                </div>
                <div className="row"
                    style={{ padding: "10px" }}>
                    <div className="col-xs-3">
                        <label htmlFor="message"
                            className="pull-right">
                            Message
                            </label>
                    </div>
                    <div className="col-xs-9">
                        <textarea name="message"
                            style={{ width: "100%" }}
                            name="message"
                            value={this.state.message}
                            onChange={this.onHandleChange} />
                    </div>
                </div>
                <div className="row"
                    style={{ padding: "10px" }}>
                    <div className="col-xs-3">
                    </div>
                    <div className="col-xs-9">
                        <button className="btn btn-success"
                            onClick={(event) => this.onSubmit()}>
                            Submit
                            </button>
                    </div>
                </div>
                <div className="row"
                    style={{ padding: "10px" }}>
                    <div className="col-xs-12">
                        {this.state.feedback} 
                    </div>
                </div>
            </div>
        );
    }
}