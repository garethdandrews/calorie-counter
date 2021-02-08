import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, handleResponse } from '@/_helpers';

export const userService = {
    getUser,
    addUser
};

function getUser() {
    // authenticationService.refreshTokenIfInvalid();

    const requestOptions = {
        method: 'GET',
        headers: {'Authorization': authHeader()}
    };

    const username = authenticationService.currentUserValue.username;

    return fetch(`${config.apiUrl}/user/${username}`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}

function addUser(username, password, calorieTarget) {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", authHeader());
    myHeaders.append("Content-Type", "application/json");

    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: JSON.stringify({
            "Username":username,
            "Password":password,
            "CalorieTarget":parseInt(calorieTarget)})
    };

    return fetch(`${config.apiUrl}/user`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}
