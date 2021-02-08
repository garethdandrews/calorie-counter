import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { handleResponse } from '@/_helpers';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    refreshTokenIfInvalid,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: new Headers({'content-type': 'application/json'}),
        body: JSON.stringify({
            "Username":username,
            "Password":password
        })
    };

    return fetch(`${config.apiUrl}/login`, requestOptions) 
        .then(handleResponse)
        .then(user => updateUser(user))
        .catch(error => alert(error));
}

function logout() {
    const requestOptions = {
        method: 'POST',
        headers: new Headers({'content-type': 'application/json'}),
        body: JSON.stringify({
            "Token": currentUserSubject.value.refreshToken
        })
    };

    return fetch(`${config.apiUrl}/login/token/revoke`, requestOptions)
        .then(handleResponse)
        .then(() => {
            // remove user from local storage to log user out
            localStorage.removeItem('currentUser');
            currentUserSubject.next(null);
        })
        .catch(error => alert(error));
}

function refreshTokenIfInvalid() {
    if (!isAccessTokenValid(currentUserSubject.value.expiration)) {
        console.log("Access token is invalid. Refreshing...");
        refreshToken();
    }
}

function refreshToken() {
    const requestOptions = {
        method: 'POST',
        headers: new Headers({'content-type': 'application/json'}),
        body: JSON.stringify({
            "Token":currentUserSubject.value.refreshToken,
            "Username":currentUserSubject.value.username
        })
    };

    return fetch(`${config.apiUrl}/login/token/refresh`, requestOptions)
        .then(handleResponse)
        .then(user => updateUser(user))
        .catch(error => alert(error));
}

function updateUser(user) {
    // store user details and jwt token in local storage to keep user logged in between page refreshes
    localStorage.setItem('currentUser', JSON.stringify(user));
    currentUserSubject.next(user);

    console.log(user);
    console.log(user.refreshToken)

    return user;
}

function isAccessTokenValid(expiration) {
    // get the expiration date/time and the current date/time
    var expirationDate = convertTicksToDate(expiration);
    var currentDate = new Date()

    console.log("expiration, current");
    console.log(expirationDate);
    console.log(currentDate);

    // calculate the time difference in seconds
    var timeDifference = (expirationDate - currentDate)/1000;

    console.log("time difference");
    console.log(timeDifference);

    // token is invalid if the expiration date/time is in the past
    // or if there are only 5 seconds until the token expires
    if (timeDifference <= 0 || timeDifference < 5) {
        return false;
    }

    // otherwise the token is valid
    return true;
}

function convertTicksToDate(ticks) {
    // constants used for conversion
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000;

    return new Date((ticks - epochTicks) / ticksPerMillisecond)
}

// Backend API sends token expiration date/time represented by ticks
// e.g. 637483785803770000 = 2021-02-08​T10:56:20.376Z
// JavaScripts Date origin is the Unix epoch: midnight on 1st January 1970
// .NET DateTime origin is midnight on 1st January 0001
// Need to translate JavaScript Date to .NET ticks to compare the expiration dates
function getNumberOfTicksForNow() {
    // constants used for conversion
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000;

    // convert the current date/time to ticks
    var date = new Date();
    return ((date.getTime() * ticksPerMillisecond) + epochTicks);
}