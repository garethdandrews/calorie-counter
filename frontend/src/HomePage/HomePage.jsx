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

        this.add = this.add.bind(this);
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

    add() {
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
                <h1 className="text-center">{formattedDate}</h1>
                <div className="row">
                	<div className="col-md-6">
                		<button type="button" className="btn btn-primary" onClick={() => this.back()}>Prev</button>
                	</div>
                	<div className="col-md-6 text-right">
                	<button type="button" className="btn btn-primary" onClick={() => this.forward()}>Next</button>		</div>
                </div>
                <hr />
                {diaryEntry &&
                    <div>
                        <h4>
                            Calories: {diaryEntry.totalCalories}
                        </h4>
                        <h5 className="text-right">
                            Target: {calorieTarget}
                        </h5>
                        <hr />
                        
                        {diaryEntry.foodItems &&
                            <div>
                                {diaryEntry.foodItems.map((item, index) =>
                                <div key={index} >
                                    <div className="row border p-3">
                                        <h4 className="col-md-6">{item.name}</h4>
                                        <h6 className="col-md-6 text-right">{item.calories}kcal</h6>
                                        <div className="col-md-12 text-right" >
                                        	<button type="button" className="btn btn-outline-danger btn-sm" onClick={() => this.delete(item.id)}>Delete</button>
                                        </div>
                                    </div>
                                    <br />
				</div>
                                )}
                                
                            </div>
                        }

                    </div>
                }
                <hr />
                <AddForm formattedDate={formattedDate} add={this.add} />
            </div>
        );
    }
}

export { HomePage };
