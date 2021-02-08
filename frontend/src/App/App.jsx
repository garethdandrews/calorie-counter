import React from 'react';
import { Router, Route, Link } from 'react-router-dom';

import { history } from '@/_helpers';
import { authenticationService } from '@/_services';
import { PrivateRoute } from '@/_components';
import { LoginPage } from '@/LoginPage';
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
                            <div className="navbar-nav ml-auto">
                            <a onClick={this.logout} className="nav-item nav-link">Logout</a>
                            </div>
                        }
                    </nav>
                    <div className="jumbotron">
                        <div className="container">
                            <div className="row">
                                <div className="col-md-6 offset-md-3">
                                    <PrivateRoute exact path="/" component={HomePage} />
                                    <Route path="/login" component={LoginPage} />
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