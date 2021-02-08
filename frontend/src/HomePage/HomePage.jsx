import React from 'react';

import { formatDate } from '@/_helpers';
import { diaryEntryService, authenticationService } from '@/_services';

class HomePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: authenticationService.currentUserValue,
            selectedDate: new Date()
            // users: null
        };
    }

    componentDidMount() {
        // get the users diary entry for the day
        diaryEntryService.getDiaryEntry(selectedDate)
    }

    render() {
        const { currentUser, users } = this.state;
        return (
            <div>
                <h1>Hi {currentUser.firstName}!</h1>
                <p>You're logged in with React & JWT!!</p>

                <h3>{formatDate(selectedDate)}</h3>
                {/* <h3>Users from secure api end point:</h3>
                {users &&
                    <ul>
                        {users.map(user =>
                            <li key={user.id}>{user.firstName} {user.lastName}</li>
                        )}
                    </ul>
                } */}
            </div>
        );
    }
}

export { HomePage };