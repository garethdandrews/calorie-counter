using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    public class DiaryEntryService : IDiaryEntryService
    {
        private IDiaryEntryRepository _diaryEntryRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public DiaryEntryService(IDiaryEntryRepository diaryEntryRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _diaryEntryRepository = diaryEntryRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DiaryEntry>> ListAsync()
        {
            return await _diaryEntryRepository.ListAsync();
        }

        // validates and converts stringDate to DateTime
        public async Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, string stringDate)
        {
            DateTime date;
            try
            {
                date = StringDateHelper.ConverStringDateToDate(stringDate);
            }
            catch (ArgumentException e)
            {
                return new DiaryEntryResponse(e.Message);
            }

            return await GetDiaryEntryAsync(userId, date);
        }

        public async Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, DateTime date)
        {
            // validate userId
            var userResult = await _userService.GetUserAync(userId);

            if (!userResult.Success)
                return new DiaryEntryResponse(userResult.Message);

            var diaryEntryResult = await GetUsersDiaryEntryForDateAsync(userId, date);

            if (!diaryEntryResult.Success)
                return diaryEntryResult;

            var diaryEntryWithFoodItems = await _diaryEntryRepository.GetAsync(diaryEntryResult.DiaryEntry.Id);

            return new DiaryEntryResponse(diaryEntryWithFoodItems);
        }

        public async Task<DiaryEntryResponse> GetUsersDiaryEntryForDateAsync(int userId, DateTime date)
        {
            var diaryEntry = await _diaryEntryRepository.GetUsersDiaryEntryForDate(userId, date);

            if (diaryEntry == null)
                return new DiaryEntryResponse($"User {userId} has no diary for that day");

            return new DiaryEntryResponse(diaryEntry);
        }

        public async Task<DiaryEntryResponse> AddDiaryEntryAsync(int userId, DateTime date)
        {
            // validate userId
            var userResult = await _userService.GetUserAync(userId);

            if (!userResult.Success)
                return new DiaryEntryResponse(userResult.Message);
            
            // check if the user already has a diary entry for that date
            var diaryEntry = await _diaryEntryRepository.GetUsersDiaryEntryForDate(userId, date);

            if (diaryEntry != null)
                return new DiaryEntryResponse($"User {userResult.User.Id} already has a diary entry for that day");
            
            // create a new diary entry
            diaryEntry = new DiaryEntry
            {
                Date = date,
                User = userResult.User
            };

            try
            {
                await _diaryEntryRepository.AddAsync(diaryEntry);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new DiaryEntryResponse($"An error occurred when saving the diary entry: {e}");
            }

            return new DiaryEntryResponse(diaryEntry);
        }

        public async Task<DiaryEntryResponse> UpdateDiaryEntryAsync(int id, DiaryEntry diaryEntry)
        {
            var existingDiaryEntry = await _diaryEntryRepository.GetAsync(id);

            if (existingDiaryEntry == null)
                return new DiaryEntryResponse($"Diary entry {id} not found");

            existingDiaryEntry.TotalCalories = diaryEntry.TotalCalories;

            try
            {
                _diaryEntryRepository.Update(existingDiaryEntry);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new DiaryEntryResponse($"An error occurred when updating diary entry {id}: {e}");
            }

            return new DiaryEntryResponse(existingDiaryEntry);
        }

        public async Task<DiaryEntryResponse> DeleteDiaryEntryAsync(int id)
        {
            var existingDiaryEntry = await _diaryEntryRepository.GetAsync(id);

            if (existingDiaryEntry == null)
                return new DiaryEntryResponse($"Diary entry {id} not found");

            try
            {
                _diaryEntryRepository.Remove(existingDiaryEntry);
                await _unitOfWork.CompleteAsync();

                return new DiaryEntryResponse(existingDiaryEntry);
            }
            catch (Exception e)
            {
                return new DiaryEntryResponse($"An error occurred when deleting diary entry {id}: {e}");
            }
        }
    }
}