import React from 'react';
import { Router, Route, Link } from 'react-router-dom';

import { history } from '@/_helpers';

class App extends React.Component {
  constructor(props) {
      super(props);

      this.state = {
          currentUser: null
      };
  }

  componentDidMount() {
      
  }

  render() {
      const { currentUser } = this.state;
      return (
          <Router history={history}>
              <div>
                  {currentUser &&
                      <nav className="navbar navbar-expand navbar-dark bg-dark">
                          <div className="navbar-nav">
                              <Link to="/" className="nav-item nav-link">Home</Link>
                          </div>
                      </nav>
                  }
                  <div className="jumbotron">
                      <div className="container">
                          <div className="row">
                              <div className="col-md-6 offset-md-3">
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