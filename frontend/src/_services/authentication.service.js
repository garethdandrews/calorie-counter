import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { handleResponse } from '@/_helpers';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(username, password) {
    const myHeaders = new Headers();
    myHeaders.append('content-type', 'application/json');

    var raw = JSON.stringify({"Name":username,"Password":password});

    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw
    };

    return fetch('http://localhost:8080/api/login', requestOptions) //`${config.apiUrl}/login`
        .then(handleResponse)
        .then(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            console.log(user)
            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}
