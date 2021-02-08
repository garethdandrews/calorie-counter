import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, formatDate, handleResponse } from '@/_helpers';

export const diaryEntryService = {
    getDiaryEntry
};

function getDiaryEntry(date) {
    // authenticationService.refreshTokenIfInvalid();

    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    const username = authenticationService.currentUserValue.username;
    const stringDate = formatDate(date)

    return fetch(`${config.apiUrl}/diaryentry/${username}/${stringDate}`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}
