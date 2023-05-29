using LibraryAPI.Model;

namespace LibraryAPI.Repositories
{
    public interface IQuestionRepository
    {
        public Task<List<QuestionModel>> GetQuestions(int folderId);
        public Task<QuestionModel> GetQuestionById(int id);
        public Task AddQuestion(QuestionModel question);
        public Task AddQuestions(ICollection<QuestionModel> questions);
        public Task UpdateQuestion(QuestionModel question);
        public Task DeleteQuestion(int id);
        public Task<List<QuestionModel>> GetSearchQuestions(string keyword, int idFolder);
    }
}
