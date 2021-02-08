import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, handleResponse } from '@/_helpers';

export const diaryEntryService = {
    getDiaryEntry
};

function getDiaryEntry(date) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/diaryentry/garyandy/2021-02-06`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}