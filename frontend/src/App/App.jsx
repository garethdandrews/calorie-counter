import React from 'react';
import { Router, Route, Link } from 'react-router-dom';

import { history, authHeader } from '@/_helpers';
import { authenticationService } from '@/_services';
import { PrivateRoute } from '@/_components';
import { LoginPage } from '@/LoginPage';
import { RegisterPage } from '@/RegisterPage';
import { HomePage } from '@/HomePage';

class App extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: null
        };
    }

    componentDidMount() {
        authenticationService.currentUser.subscribe(x => this.setState({ currentUser: x }));
    }

    logout() {
        authenticationService.logout();
        history.push('/login');
    }

    register() {
        history.push('/register');
    }

    render() {
        const { currentUser } = this.state;
        return (
            <Router history={history}>
                <div>
                    <nav className="navbar navbar-expand-md navbar-dark bg-dark">
                        <div className="navbar-header ml-auto">
                            <a className="navbar-brand" href="/">Calorie Counter</a>
                        </div>

                        {currentUser &&
                            <div className="navbar-nav mr-auto">
                                <a className="nav-item nav-link" onClick={() => alert("Here you could change the calorie target")}>{currentUser.username}</a>
                            </div>
                        }

                        {currentUser &&
                            <div className="navbar-nav mr-auto">
                            <a onClick={this.logout} className="nav-item nav-link">Logout</a>
                            </div>
                        }
                        {!currentUser &&
                            <div className="navbar-nav mr-auto">
                            <a onClick={this.register} className="nav-item nav-link">Register</a>
                            </div>
                        }
                    </nav>
                    <div className="jumbotron">
                        <div className="container">
                            <div className="row">
                                <div className="col-md-6 offset-md-3">
                                    <PrivateRoute exact path="/" component={HomePage} />
                                    <Route path="/login" component={LoginPage} />
                                    <Route path="/register" component={RegisterPage} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Router>
        );
    }
}

export { App }; 
