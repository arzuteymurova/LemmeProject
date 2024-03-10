using LemmeProject.Application.DTOs.Questions;
using LemmeProject.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LemmeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Bearer")]

    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("AddQuestion")]
        public async Task<IActionResult> AddQuestion(QuestionAddRequest questionAddRequest)
        {
            var result = await _questionService.AddAsync(questionAddRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpPut("EditQuestion")]
        public async Task<IActionResult> EditQuestion(QuestionUpdateRequest questionUpdateRequest)
        {
            var result = await _questionService.EditAsync(questionUpdateRequest);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpDelete("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _questionService.DeleteByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetAllQuestions")]
        public async Task<IActionResult> GetQuestions()
        {
            var result = await _questionService.GetTableAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetQuestionById/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var result = await _questionService.GetByIdAsync(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}
