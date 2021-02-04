using backend_api.Domain.Models;

namespace backend_api.Domain.Services.Communication
{
    public class GetDiaryEntryResponse : BaseResponse
    {
        public DiaryEntry DiaryEntry { get; private set; }

        private GetDiaryEntryResponse(bool success, string message, DiaryEntry diaryEntry) : base(success, message)
        {
            DiaryEntry = diaryEntry;
        }

        // Creates a success response
        public GetDiaryEntryResponse(DiaryEntry diaryEntry) : this(true, string.Empty, diaryEntry)
        {
        }

        // Creates an error response
        public GetDiaryEntryResponse(string message) : this(false, message, null)
        {
        }
    }
}