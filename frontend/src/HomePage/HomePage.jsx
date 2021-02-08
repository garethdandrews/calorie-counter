import React from 'react';

import { formatDate } from '@/_helpers';
import { diaryEntryService, authenticationService } from '@/_services';

class HomePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: authenticationService.currentUserValue,
            selectedDate: null,
            diaryEntry: null
        };
    }

    componentDidMount() {
        var selectedDate = new Date();
        this.setState({ selectedDate })
        // get the users diary entry for the day
        diaryEntryService.getDiaryEntry(selectedDate).then(diaryEntry => this.setState({ diaryEntry }));
    }

    render() {
        const { currentUser, selectedDate, diaryEntry } = this.state;
        return (
            <div>
                <h3>{formatDate(selectedDate)}</h3>
                {diaryEntry &&
                    <div>
                        <h5>
                            Total Calories: {diaryEntry.totalCalories}
                        </h5>
                        {diaryEntry.foodItems &&
                            <ul>
                                {diaryEntry.foodItems.map((item, index) =>
                                    <li key={index}>{item.name}: {item.calories}kcal</li>    
                                )}
                            </ul>
                        }
                    </div>
                }
                
            </div>
        );
    }
}

export { HomePage };