import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, handleResponse } from '@/_helpers';

export const userService = {
    getUser
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
