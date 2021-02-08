import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, handleResponse } from '@/_helpers';

export const diaryEntryService = {
    getDiaryEntry
};

function getDiaryEntry(date) {
    // authenticationService.refreshTokenIfInvalid();

    const requestOptions = {
        method: 'GET',
        headers: {'Authorization': authHeader()}
    };

    const username = authenticationService.currentUserValue.username;

    return fetch(`${config.apiUrl}/diaryentry/${username}/${date}`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}
