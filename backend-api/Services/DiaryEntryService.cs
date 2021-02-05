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

        public async Task<DiaryEntryResponse> GetDiaryEntry(int userId, string stringDate)
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

            return await GetDiaryEntry(userId, date);
        }

        public async Task<DiaryEntryResponse> GetDiaryEntry(int userId, DateTime date)
        {
            // validate userId
            var userResult = await _userService.GetUserAync(userId);

            if (!userResult.Success)
                return new DiaryEntryResponse(userResult.Message);

            var diaryEntry = await _diaryEntryRepository.GetUsersDiaryEntryForDate(userId, date);

            if (diaryEntry == null)
                return new DiaryEntryResponse($"User {userResult.User.Id} has no diary for that day");

            var diaryEntryWithFoodItems = await _diaryEntryRepository.GetAsync(diaryEntry.Id);

            return new DiaryEntryResponse(diaryEntryWithFoodItems);
        }

        public async Task<DiaryEntryResponse> AddDiaryEntry(int userId, DateTime date)
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

        public async Task<DiaryEntryResponse>

    }
}