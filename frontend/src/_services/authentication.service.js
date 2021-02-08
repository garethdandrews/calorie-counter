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
    var myHeaders = new Headers();
    myHeaders.append('content-type', 'application/json');

    var raw = JSON.stringify({"Name":username,"Password":password});

    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw
    };

    return fetch(`${config.apiUrl}/login`, requestOptions) //`${config.apiUrl}/login`
        .then(handleResponse)
        .then(updateUser)
        .catch(error => alert(error));
}

function logout() {
    var myHeaders = new Headers();
    myHeaders.append('content-type', 'application/json');

    var raw = JSON.stringify({"Token": ""})

    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}

function refreshToken() {
    var myHeaders = new Headers();
    myHeaders.append('content-type', 'application/json');

    var raw = JSON.stringify({"Token":currentUser.refreshToken,"Name":currentUser.name});

    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw
    };

    return fetch('http://localhost:8080/api/login/token/refresh', requestOptions) //`${config.apiUrl}/login`
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