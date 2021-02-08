import { authenticationService } from '@/_services';

export function authHeader() {
    // return authorization header with jwt token
    const currentUser = authenticationService.currentUserValue;
    if (currentUser && currentUser.accessToken) {
        // check if the access token has expired
        var date = new Date();
        
        
        console.log(ticks);
        return { Authorization: `Bearer ${currentUser.token}` };
    } else {
        return {};
    }
}


function isAccessTokenValid(currentUser) {
    return currentUser.expiration < getNumberOfTicksForNow();
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

export function convertTicksToDate(ticks) {
    // constants used for conversion
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000;

    var date = new Date();
    return date.toTimeString((ticks - epochTicks) / ticksPerMillisecond)
}