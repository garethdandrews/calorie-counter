import { authenticationService } from '@/_services';

export function authHeader() {
    // return authorization header with jwt token
    const currentUser = authenticationService.currentUserValue;
    if (currentUser && currentUser.accessToken) {
        var accessToken = null;
        // check if the access token is valid
        if (isAccessTokenValid) {
            // use the access token
            accessToken = currentUser.accessToken;
        } else {
            // refresh the token
            authenticationService.refreshToken()
                .then(token => accessToken = token.accessToken)
        }
        
        return { 'Authorization': `Bearer ${accessToken}` };
    } else {
        return {};
    }
}


function isAccessTokenValid(currentUser) {
    // get the expiration date/time and the current date/time
    var expirationDate = convertTicksToDate(currentUser.expiration);
    var currentDate = new Date()

    // calculate the time difference in seconds
    var timeDifference = (expirationDate - currentDate)/1000;

    // token is invalid if the expiration date/time is in the past
    // or if there are only 5 seconds until the token expires
    if (timeDifference <= 0 || timeDifference < 5) {
        return false;
    }

    // otherwise the token is valid
    return true;
}

function convertTicksToDate(ticks) {
    // constants used for conversion
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000;

    return new Date((ticks - epochTicks) / ticksPerMillisecond)
}

// Backend API sends token expiration date/time represented by ticks
// e.g. 637483785803770000 = 2021-02-08​T10:56:20.376Z
// JavaScripts Date origin is the Unix epoch: midnight on 1st January 1970
// .NET DateTime origin is midnight on 1st January 0001
// Need to translate JavaScript Date to .NET ticks to compare the expiration dates
function getNumberOfTicksForNow() {
    // constants used for conversion
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000;

    // convert the current date/time to ticks
    var date = new Date();
    return ((date.getTime() * ticksPerMillisecond) + epochTicks);
}