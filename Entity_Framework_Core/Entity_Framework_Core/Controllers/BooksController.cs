using Entity_Framework_Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDBContext appDBContext) : ControllerBase
    {
        [HttpPost("SingleRowEnter")]

        public async Task<IActionResult> AddDataToBookTable([FromBody] Book model)
        {
        
            appDBContext.Book.Add(model); // this two lines only for adding book data single row
            await appDBContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("SingleRowEnterforbothtables")]
        public async Task<IActionResult> AddDataToBookTableandAuthor([FromBody] Book model)
        { 
            var author = new Author() // for author detail to fill through book class
            {
                Name = "Author 2",
                Email = "3test@gmail.com"

            };


            /*         for above author when you want to update author table and books table at same time use below input           
             *                   {
                                         "title": "Test Book",
                           "description": "Test Description",
                           "noOfPages": 100,
                           "isActive": true,
                           "createdOn": "2025-07-24T10:00:00",
                           "languageId": 1,
                           "authorId": 0,
                           "author": {
                                             "name": "Author Name",
                             "email": "author@example.com"
                           }
                                     } */
            model.Author = author; // for author detail to fill through book class
            appDBContext.Book.Add(model); // this two lines only for adding book data single row
            await appDBContext.SaveChangesAsync();
            return Ok(model);

        }

        [HttpPost("BulkPost")]
        public async Task<IActionResult> PushInBulk( [FromBody] List<Book> books)
        {
            appDBContext.Book.AddRange(books);
            await  appDBContext.SaveChangesAsync();
            return Ok(books);

        }


        [HttpPut("{bookId}")] //remember id is being passed from here API/Books/bookId
        public async Task<IActionResult> updatebook([FromRoute] int bookId,[FromBody] Book model)
        {
            var book = appDBContext.Book.FirstOrDefault(x => x.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;

            await appDBContext.SaveChangesAsync();

            return Ok(model);
        }


        [HttpPut("")] 
        public async Task<IActionResult> UpdateBookwithsingleQuery([FromBody] Book model)
        {
            appDBContext.Book.Update(model);
            await appDBContext.SaveChangesAsync();

            //otherway you can use below way to update one row completely
            //we just use
            // appDBContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //  await appDBContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("bulk")] // to update in bulk // if you mention {} in httpPut you must give input remeber it
        public async Task<IActionResult> updaterowsinbulk()
        {
          await  appDBContext.Book.Where(x=>x.NoOfPages > 1)  // we can remove where condition here to update all records ( by giving condition we update only thos satify condition)
                .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.Description, "Changed Descritpion") // see here value can be passed within ""
                .SetProperty(z=> z.Title, z=> z.AuthorId + "Changed Title")); // see here values can be passed with authorID + extra info

            // no update in state so no save changes   
            return Ok();
        }

        [HttpDelete("{BId}")]
        public async Task<IActionResult> RemoveRows( [FromRoute] int BId  )
        { 
            var record = appDBContext.Book.FirstOrDefault(x=>x.Id == BId);
            if(record == null)
            {
                return NotFound();
            }
            appDBContext.Book.Remove(record);
            await appDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Delete")] // multiple rows are used to delete <5 records
        public async Task<IActionResult> deleteinbulk()
        {
            var book = await appDBContext.Book.Where(x => x.Id < 5).ToListAsync();
            appDBContext.Book.RemoveRange(book);
            await appDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("deleteusignsingleQuery")]
        public async Task<IActionResult> DeleteBooksUsingSinglequery()
        {
            var books = await appDBContext.Book.Where(x => x.Id < 8).ExecuteDeleteAsync();
            return Ok();
        }

        
    }
}
