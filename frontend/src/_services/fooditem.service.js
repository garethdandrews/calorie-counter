import config from 'config';
import { authenticationService } from '@/_services';
import { authHeader, formatDate, handleResponse } from '@/_helpers';

export const foodItemService = {
    addFoodItem,
    deleteFoodItem
};

function addFoodItem(name, calories, date) {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", authHeader());
    myHeaders.append("Content-Type", "application/json");

    const username = authenticationService.currentUserValue.username;

    const requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: JSON.stringify({
            "Username":username,
            "StringDate":date,
            "Name":name,
            "Calories":parseInt(calories)})
    };

    return fetch(`${config.apiUrl}/fooditem`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}

function deleteFoodItem(id) {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", authHeader());
    myHeaders.append("Content-Type", "application/json");

    const requestOptions = {
        method: 'DELETE',
        headers: myHeaders
    };

    return fetch(`${config.apiUrl}/fooditem/${id}`, requestOptions)
        .then(handleResponse)
        .catch(error => alert(error));
}