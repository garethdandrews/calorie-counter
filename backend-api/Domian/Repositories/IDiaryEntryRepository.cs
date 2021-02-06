using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    /// <summary>
    /// The diary entry repository.
    /// Handles CRUD operations
    /// </summary>
    public interface IDiaryEntryRepository
    {

        /// <summary></summary>
        /// <returns>
        /// A list of diary entries
        /// </returns>
        Task<IEnumerable<DiaryEntry>> ListAsync();

        /// <summary></summary>
        /// <param name="id"></param>
        /// <returns>
        /// A diary entry for the given ID, null if no diary entry is found
        /// </returns>
        Task<DiaryEntry> GetAsync(int id);

        /// <summary>
        /// Add a diary entry to the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        /// <returns></returns>
        Task AddAsync(DiaryEntry diaryEntry);

        /// <summary>
        /// Updates a diary entry in the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        void Update(DiaryEntry diaryEntry);

        /// <summary>
        /// Removes a diary entry from the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        void Remove(DiaryEntry diaryEntry);

        /// <summary>
        /// Get the users diary entry for a given date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// The users diary entry for a given date
        /// </returns>
        Task<DiaryEntry> GetUsersDiaryEntryForDate(int userId, DateTime date);
    }
}