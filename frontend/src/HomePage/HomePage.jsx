import React from 'react';
import { formatDate } from '@/_helpers';
import { authenticationService, diaryEntryService, foodItemService, userService } from '@/_services';
import { AddForm } from '@/HomePage';

class HomePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: authenticationService.currentUserValue,
            selectedDate: new Date(),
            diaryEntry: null,
            calorieTarget: null
        };
    }

    componentDidMount() {
        // get the users diary entry for the day
        this.updateDiaryEntry(this.state.selectedDate);
        userService.getUser().then(user => this.setState({ calorieTarget: user.calorieTarget }));
    }

    back() {
        var newDate = new Date();
        newDate.setDate(this.state.selectedDate.getDate() - 1);
        this.updateDiaryEntry(newDate);
    }

    forward() {
        var newDate = new Date();
        newDate.setDate(this.state.selectedDate.getDate() + 1);
        this.updateDiaryEntry(newDate);
    }

    

    delete(id) {
        foodItemService.deleteFoodItem(id);
        this.updateDiaryEntry(this.state.selectedDate);
    }

    updateDiaryEntry(date) {
        this.setState({ selectedDate: date });
        diaryEntryService.getDiaryEntry(formatDate(date)).then(diaryEntry => this.setState({ diaryEntry }));
    }

    render() {
        const { selectedDate, diaryEntry, calorieTarget } = this.state;
        const formattedDate = formatDate(selectedDate);
        return (
            <div>
                <h1>{formattedDate}</h1>
                <h6><a onClick={() => this.back()}>Back</a></h6>
                <h6><a onClick={() => this.forward()}>Forward</a></h6>
                <hr />
                {diaryEntry &&
                    <div>
                        <h4>
                            Total Calories: {diaryEntry.totalCalories}
                        </h4>
                        <h4>
                            Target: {calorieTarget}
                        </h4>
                        <hr />
                        {diaryEntry.foodItems &&
                            <div>
                                {diaryEntry.foodItems.map((item, index) =>
                                    <div key={index}>
                                        <h4>{item.name}</h4>
                                        <h6>{item.calories}kcal</h6>
                                        <a onClick={() => this.delete(item.id)} className="">Delete</a>
                                    </div>

                                )}
                                
                            </div>
                        }

                    </div>
                }
                <hr />
                <AddForm formattedDate={formattedDate} updateDiaryEntry={this.updateDiaryEntry} />
            </div>
        );
    }
}

export { HomePage };