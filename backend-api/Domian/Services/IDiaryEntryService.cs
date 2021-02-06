using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    /// <summary>
    /// The diary entry service.
    /// Handles retreiving, creating, updating and deleting diary entries.
    /// </summary>
    public interface IDiaryEntryService
    {
        /// <summary>
        /// Validates and converts stringDate to DateTime to call the GetDiaryEntryAsync(int userId, DateTime date) method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stringDate"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the stringDate is not in the format dd-mm-yyyy;
        /// A call to the GetDiaryEntry method with the converted date object
        /// </returns>
        Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, string stringDate);

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
        Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, DateTime date);

        /// <summary>
        /// Gets the users diary entry for a given date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the user has no diary entry for the date;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
        Task<DiaryEntryResponse> GetUsersDiaryEntryForDateAsync(int userId, DateTime date);

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
        Task<DiaryEntryResponse> AddDiaryEntryAsync(int userId, DateTime date);
        
        /// <summary>
        /// Deletes a diary entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful DiaryEntryResponse if the diary does not exist;
        /// Unsuccessful DiaryEntryResponse if there was an issue removing the diary entry from the database;
        /// Successful DiaryEntryResponse with the diary entry
        /// </returns>
        Task<DiaryEntryResponse> DeleteDiaryEntryAsync(int id);
    }
}