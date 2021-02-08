using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Security.Hashing;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;

namespace backend_api.Services
{
    /// <summary>
    /// The user service.
    /// Handles retreiving, creating and updating users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="passwordHasher"></param>
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user not found;
        /// Successful UserResponse with the user
        /// </returns>
        public async Task<UserResponse> GetUserAync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
                return new UserResponse($"User {id} not found");
            return new UserResponse(user);
        }

        /// <summary>
        /// Get user by name (users names are unique)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user not found;
        /// Successful UserResponse with the user
        /// </returns>
        public async Task<UserResponse> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetUserByNameAsync(name);
            if (user == null)
                return new UserResponse("User not found"); 
            return new UserResponse(user);
        }

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRoles"></param>
        /// <returns>
        /// Unsuccessful UserResponse if the username is already in use;
        /// Unsuccessful UserReponse if there was an issue saving the user to the db;
        /// Successful UserResponse with the user
        /// </returns>
        public async Task<UserResponse> AddUserAsync(User user, params EApplicationRole[] userRoles)
        {
            // check if the user already exists
            var existingUserResult = await GetUserByNameAsync(user.Username);
            if (existingUserResult.Success)
                return new UserResponse("Username is already in use");

            // hash the password
            user.Password = _passwordHasher.HashPassword(user.Password);

            // save the user
            try
            {
                await _userRepository.AddAsync(user, userRoles);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error occurred when saving the user: {e}");
            }
        }

        /// <summary>
        /// Updates a users calorie target and updates the diary entry for today with the new target
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user could not be found;
        /// Unsuccessful UserResponse if there was an issue updating the user in the db;
        /// Successful UserResponse with the user
        /// </returns>
        public async Task<UserResponse> UpdateUserAsync(int id, User user)
        {
            // get the existing user for the given ID
            var existingUser = await _userRepository.GetAsync(id);
            if (existingUser == null)
                return new UserResponse($"User {id} not found");

            // update the users calorie target and update the calorie target for todays diary
            existingUser.CalorieTarget = user.CalorieTarget;
            UpdateCalorieTargetForToday(existingUser.Diary, existingUser.CalorieTarget);

            // update the user
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error occurred when updating the user: {e}");
            }
        }

        /// <summary>
        /// Updates the users diary entry for today (if it exists) with the new calorie target
        /// </summary>
        /// <param name="diary"></param>
        /// <param name="calorieTarget"></param>
        /// <returns></returns>
        private bool UpdateCalorieTargetForToday(List<DiaryEntry> diary, int calorieTarget)
        {
            // get the users diary entry for today
            var todaysDiaryEntry = diary.Find(x => x.Date.Date == DateTime.Now.Date);
            if (todaysDiaryEntry == null) // if no entry exists for today, then do nothing
                return false;

            todaysDiaryEntry.CalorieTarget = calorieTarget;
            return true;
        } 
    }
}