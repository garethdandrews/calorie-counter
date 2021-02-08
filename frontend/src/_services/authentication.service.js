import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { handleResponse } from '@/_helpers';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    refreshToken,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: new Headers({'content-type': 'application/json'}),
        body: JSON.stringify({
            "Name":username,
            "Password":password
        })
    };

    return fetch(`${config.apiUrl}/login`, requestOptions) 
        .then(handleResponse)
        .then(updateUser)
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

function refreshToken() {
    const requestOptions = {
        method: 'POST',
        headers: new Headers({'content-type': 'application/json'}),
        body: JSON.stringify({
            "Token":currentUserSubject.value.refreshToken,
            "Name":currentUserSubject.value.name
        })
    };

    return fetch(`${config.apiUrl}/token/refresh`, requestOptions) //`${config.apiUrl}/login`
        .then(handleResponse)
        .then(updateUser)
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