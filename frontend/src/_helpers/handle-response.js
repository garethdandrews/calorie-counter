import { authenticationService } from '@/_services';

export function handleResponse(response) {
    return response.text().then(text => {
        
        if (!response.ok) {
            if ([401, 403].indexOf(response.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                authenticationService.logout();
            }
            location.reload();
            return Promise.reject(text);
        }
        else 
        {
            const data = text && JSON.parse(text);
            console.log(data);
            return data;
        }
        
    });
}