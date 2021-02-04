using backend_api.Domain.Models;

namespace backend_api.Domain.Services.Communication
{
    public class DiaryEntryResponse : BaseResponse
    {
        public DiaryEntry DiaryEntry { get; private set; }

        private DiaryEntryResponse(bool success, string message, DiaryEntry diaryEntry) : base(success, message)
        {
            DiaryEntry = diaryEntry;
        }

        // Creates a success response
        public DiaryEntryResponse(DiaryEntry diaryEntry) : this(true, string.Empty, diaryEntry)
        {
        }

        // Creates an error response
        public DiaryEntryResponse(string message) : this(false, message, null)
        {
        }
    }
}