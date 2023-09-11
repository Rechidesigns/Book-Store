using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Project2.Models;
using Test_Project2.Repository;

namespace Test_Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IBook_Repository book_Repository;
        private readonly IMapper mapper;
        public BookStoreController(IBook_Repository book_Repository, IMapper mapper)
        {
            this.book_Repository = book_Repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await book_Repository.GetAllAsync();
            var booksDto = mapper.Map<List<Books_ModelDto>>(books);
            return Ok(booksDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetBooksAsync")]
        public async Task<IActionResult> GetBooksAsync(Guid id)
        {
            var books = await book_Repository.GetAsync(id);
            if (books == null)
            {
                return BadRequest("Books Does not Exist");
            }
            else
            {
                var booksDto = mapper.Map<Books_ModelDto>(books);
                return Ok(booksDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBooksAsync(AddBook request)
        {
            var books = new Books_Model()
            {
                Authors_Name = request.Authors_Name,
                Books_Title = request.Books_Title,
                Cover_Image_Url = request.Cover_Image_Url,
                Books_File = request.Books_File,
                Published_Date = request.Published_Date,
               //ated_On = DateTime.Now,

            };
            books = await book_Repository.AddAsync(books);
            var booksDto = new Books_ModelDto()
            {
                Id = books.Id,
                Authors_Name = books.Authors_Name,
                Books_Title = books.Books_Title,
                Cover_Image_Url = books.Cover_Image_Url,
                Books_File = books.Books_File,
                Published_Date = books.Published_Date,
               //reated_On = books.Created_On,
            };

            return CreatedAtAction(nameof(GetBooksAsync), new { id = booksDto.Id }, booksDto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBooksAsync(Guid id, AddBook request)
        {
            var books = new Books_Model()
            {
                Authors_Name = request.Authors_Name,
                Books_Title = request.Books_Title,
                Cover_Image_Url = request.Cover_Image_Url,
                Books_File = request.Books_File,
                Published_Date = request.Published_Date,
               //reated_On = DateTime.UtcNow,

            };
            var bookss =await book_Repository.UpdateAsync(id, books);
            if (bookss==null)
            {
                return BadRequest("Books Does Not Exist");
            }
            else
            {
                var booksDto = new Books_ModelDto()
                {
                    Id = books.Id,
                    Authors_Name = books.Authors_Name,
                    Books_Title = books.Books_Title,
                    Cover_Image_Url = books.Cover_Image_Url,
                    Books_File = books.Books_File,
                    Published_Date = books.Published_Date,
                   //reated_On = books.Created_On,
                };

                return CreatedAtAction(nameof(GetBooksAsync), new { id = booksDto.Id }, booksDto);
            }
        }
    }
}
