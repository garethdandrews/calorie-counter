import { authenticationService } from '@/_services';

export function handleResponse(response) {
    return response.text().then(text => {
        if (!response.ok) {
            if ([401, 403].indexOf(response.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                if (response.status === 401)
                {
                    console.log("unauthorised");
                }
                authenticationService.logout();
                text = "Session expired - you have been logged out"
            }
            console.log(text);
            return Promise.reject(text);
        } else {
            const data = text && JSON.parse(text);
            console.log(data);
            return data;
        }
    });
}