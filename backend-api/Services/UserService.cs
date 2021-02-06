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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        /*
         *  Get user by id
         */
        public async Task<UserResponse> GetUserAync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
                return new UserResponse($"User {id} not found");
            return new UserResponse(user);
        }

        /**
         *  Get user by name (users names are unique)
         */
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _userRepository.GetUserByNameAsync(name);
        }

        /*
         *  Add a new user to the database
         */
        public async Task<UserResponse> AddUserAsync(User user, params EApplicationRole[] userRoles)
        {
            // check if the user already exists
            var existingUser = await GetUserByNameAsync(user.Name);
            if (existingUser != null)
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

        /*
         *  Update a user
         */
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