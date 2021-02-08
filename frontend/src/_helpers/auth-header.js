import { authenticationService } from '@/_services';

export function authHeader() {
    // return authorization header with jwt token
    const currentUser = authenticationService.currentUserValue;
    if (currentUser && currentUser.accessToken) {
        return `Bearer ${currentUser.accessToken}`;
    } else {
        return "";
    }
}