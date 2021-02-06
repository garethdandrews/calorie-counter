using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    /// <summary>
    /// The diary entry service.
    /// Handles retreiving, creating, updating and deleting diary entries.
    /// </summary>
    public class DiaryEntryService : IDiaryEntryService
    {
        private IDiaryEntryRepository _diaryEntryRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="diaryEntryRepository"></param>
        /// <param name="userService"></param>
        /// <param name="unitOfWork"></param>
        public DiaryEntryService(IDiaryEntryRepository diaryEntryRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _diaryEntryRepository = diaryEntryRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Validates and converts stringDate to DateTime to call the GetDiaryEntryAsync(int userId, DateTime date) method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stringDate"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the stringDate is not in the format dd-mm-yyyy;
        /// A call to the GetDiaryEntry method with the converted date object
        /// </returns>
        public async Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, string stringDate)
        {
            // conver the string date to a DateTime object
            DateTime date;
            try
            {
                date = StringDateHelper.ConverStringDateToDate(stringDate);
            }
            catch (ArgumentException e)
            {
                return new DiaryEntryResponse(e.Message);
            }

            // call the GetDiaryEntryAsync method with the new date object
            return await GetDiaryEntryAsync(userId, date);
        }

        /// <summary>
        /// Gets the users diary entry for a given date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryReponse if the user does not exist;
        /// Unsuccessful DiaryEntryResponse if the user does not have a diary entry for that date;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
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

        /// <summary>
        /// Adds a diary entry for the given user and date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the user does not exist;
        /// Unsuccessful DiaryEntryResponse if the user already has a diary entry for that date;
        /// Unsuccessful DiaryEntryResponse if there was an issue adding the diary entry to the database;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
        public async Task<DiaryEntryResponse> AddDiaryEntryAsync(int userId, DateTime date)
        {
            // validate userId
            var userResult = await _userService.GetUserAync(userId);
            if (!userResult.Success)
                return new DiaryEntryResponse(userResult.Message);
            
            // check if the user already has a diary entry for that date
            var diaryEntryResult = await GetUsersDiaryEntryForDateAsync(userId, date);
            if (diaryEntryResult.Success)
                return new DiaryEntryResponse($"User {userId} already has a diary for that day");
            
            // create a new diary entry
            var diaryEntry = new DiaryEntry
            {
                Date = date,
                User = userResult.User,
                CalorieTarget = userResult.User.CalorieTarget
            };

            // add to the database
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

        /// <summary>
        /// Gets the users diary entry for a given date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the user has no diary entry for the date;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
        public async Task<DiaryEntryResponse> GetUsersDiaryEntryForDateAsync(int userId, DateTime date)
        {
            var diaryEntry = await _diaryEntryRepository.GetUsersDiaryEntryForDate(userId, date);
            if (diaryEntry == null)
                return new DiaryEntryResponse($"User {userId} has no diary entry for that day");

            return new DiaryEntryResponse(diaryEntry);
        }

        /// <summary>
        /// Deletes a diary entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the diary does not exist;
        /// Unsuccessful DiaryEntryResponse if there was an issue removing the diary entry from the database;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
        public async Task<DiaryEntryResponse> DeleteDiaryEntryAsync(int id)
        {
            var existingDiaryEntry = await _diaryEntryRepository.GetAsync(id);
            if (existingDiaryEntry == null)
                return new DiaryEntryResponse($"Diary entry {id} not found");

            try
            {
                _diaryEntryRepository.Remove(existingDiaryEntry);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new DiaryEntryResponse($"An error occurred when deleting diary entry {id}: {e}");
            }

            return new DiaryEntryResponse(existingDiaryEntry);
        }
    }
}