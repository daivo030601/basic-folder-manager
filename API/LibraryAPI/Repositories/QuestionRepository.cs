using LibraryAPI.EfCore;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EF_DataContext _context;

        public QuestionRepository(EF_DataContext context)
        {
            _context = context;
        }
        public async Task AddQuestion(QuestionModel question)
        {
            Question dbTable = new Question();
            dbTable.Description = question.Description;
            dbTable.Answer = question.Answer;
            dbTable.FolderId = question.FolderId;
            _context.Questions.Add(dbTable);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _context.Questions.Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<QuestionModel> GetQuestionById(int id)
        {
            QuestionModel response = new QuestionModel();
            var row = await _context.Questions.Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
            return new QuestionModel()
            {
                FolderId = row.FolderId,
                Id = row.Id,
                Description = row.Description,
                Answer = row.Answer,
            };
        }

        public async Task<List<QuestionModel>> GetQuestions(int folderId)
        {
            List<QuestionModel> response = new List<QuestionModel>();
            var dataList = await _context.Questions
                .Where(q => q.FolderId == folderId)
                .ToListAsync();
            dataList.ForEach(row => response.Add(new QuestionModel()
            {
                Id = row.Id,
                FolderId = row.FolderId,
                Description = row.Description,
                Answer = row.Answer,
            }));
            return response;
        }

        public async Task<List<QuestionModel>> GetSearchQuestions(string keyword, int idFolder)
        {
            List<QuestionModel> response = new List<QuestionModel>();
            if (!string.IsNullOrEmpty(keyword))
            {
                var dataList = await _context.Questions.Where(q => q.FolderId.Equals(idFolder) && q.Description.Contains(keyword)
                                    || q.Answer.Contains(keyword)).ToListAsync();
                dataList.ForEach(row => response.Add(new QuestionModel()
                {
                    Id = row.Id,
                    FolderId = row.FolderId,
                    Description = row.Description,
                    Answer = row.Answer,
                }));
            }

            return response;
        }

        public async Task UpdateQuestion(QuestionModel question)
        {
            var dbTable = await _context.Questions.Where(q => q.Id.Equals(question.Id)).FirstOrDefaultAsync();
            if (dbTable != null)
            {
                dbTable.Description = String.IsNullOrEmpty(question.Description) ? dbTable.Description : question.Description;
                dbTable.Answer = String.IsNullOrEmpty(question.Answer) ? dbTable.Answer : question.Answer;
                dbTable.FolderId = question.FolderId == null ? dbTable.FolderId : question.FolderId;
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestions(ICollection<QuestionModel> questions)
        {
            foreach (QuestionModel question in questions)
            {
                Question dbQuestion = new Question();
                dbQuestion.Description = question.Description;
                dbQuestion.Answer = question.Answer;
                dbQuestion.FolderId = question.FolderId;
                _context.Questions.Add(dbQuestion);
            }
            await _context.SaveChangesAsync();
        }
    }
}
