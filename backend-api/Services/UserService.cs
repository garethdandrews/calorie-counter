using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;

namespace backend_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> GetUserAync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
                return new UserResponse($"User {id} not found");
            return new UserResponse(user);
        }

        public async Task<UserResponse> AddUserAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error occurred when saving the user: {e}");
            }
        }

        public async Task<UserResponse> UpdateUserAsync(int id, User user)
        {
            var existingUser = await _userRepository.GetAsync(id);

            if (existingUser == null)
                return new UserResponse($"User {id} not found");

            existingUser.CalorieTarget = user.CalorieTarget;
            UpdateCalorieTargetForToday(existingUser.Diary, existingUser.CalorieTarget);

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
            var todaysDiaryEntry = diary.Find(x => x.Date.Date == DateTime.Now.Date);

            if (todaysDiaryEntry == null)
                return false;

            todaysDiaryEntry.CalorieTarget = calorieTarget;

            return true;
        } 
    }
}