using LibraryAPI.Data;
using LibraryAPI.EfCore;
using LibraryAPI.Model;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAPI.Controllers
{

    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IFolderRepository _folderRepo;

        public LibraryController(IQuestionRepository qRepo, IFolderRepository fRepo)
        {
            _questionRepo = qRepo;
            _folderRepo = fRepo;
        }


        // GET: api/<LibraryController>
        [HttpGet]
        [Route("api/[controller]/Questions/{folderId}")]
        public async Task<ActionResult<List<Question>>> Get(int folderId)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                List<QuestionModel> data = await _questionRepo.GetQuestions(folderId);
                if (data.Any())
                {
                    type = ResponseType.Success;
                }
                else
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }



        // GET api/<LibraryController>/Question/{id}
        [HttpGet]
        [Route("api/[controller]/Question/{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                QuestionModel data = await _questionRepo.GetQuestionById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        //GET QUESTION SEARCH
        [HttpGet()]
        [Route("api/[controller]/Question/{keyword}/{idFolder?}")]
        public async Task<ActionResult<Question>> GetSearchQuestion(string keyword, int idFolder)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                List<QuestionModel> data = await _questionRepo.GetSearchQuestions(keyword, idFolder);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/controller/Question
        [HttpPost]
        [Route("api/[controller]/Question")]

        public async Task<ActionResult> PostQuestion([FromBody] QuestionModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _questionRepo.AddQuestion(model);
                return Ok(ResponseHandler.GetApiResponse(type, model.Description));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/Questions")]

        public async Task<ActionResult> PostQuestions([FromBody] ICollection<QuestionModel> questions)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _questionRepo.AddQuestions(questions);
                return Ok(ResponseHandler.GetApiResponse(type, "adding success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<LibraryController>/Question/{id}
        [HttpPut]
        [Route("api/[controller]/Question")]

        public async Task<ActionResult> PutQuestion([FromBody] QuestionModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _questionRepo.UpdateQuestion(model);
                return Ok(ResponseHandler.GetApiResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<LibraryController>/Question/{id}
        [HttpDelete]
        [Route("api/[controller]/Question/{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _questionRepo.DeleteQuestion(id);
                return Ok(ResponseHandler.GetApiResponse(type, "Delete success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        //GET api/<LibraryController>/Folders
        [HttpGet]
        [Route("api/[controller]/Folders/{parentId}")]
        public async Task<ActionResult> GetFolder(int parentId)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                List<FolderModel> data = await _folderRepo.GetFolders(parentId);
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/AllFolders/")]
        public async Task<ActionResult> GetAllFolder()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                ICollection<FolderTreeModel> data = await _folderRepo.GetFolderTree();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetApiResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        // POST api/<LibraryController>
        [HttpPost]
        [Route("api/[controller]/Folder")]

        public async Task<ActionResult> Post([FromBody] FolderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _folderRepo.AddFolder(model.Name, model.ParentId);
                return Ok(ResponseHandler.GetApiResponse(type, model.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<LibraryController>/Folder/{id}
        [HttpPut]
        [Route("api/[controller]/Folder")]

        public async Task<ActionResult> Put([FromBody] FolderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _folderRepo.UpdateFolder(model);
                return Ok(ResponseHandler.GetApiResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete]
        [Route("api/[controller]/Folder/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _folderRepo.DeleteFolder(id);
                return Ok(ResponseHandler.GetApiResponse(type, "Delete success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
