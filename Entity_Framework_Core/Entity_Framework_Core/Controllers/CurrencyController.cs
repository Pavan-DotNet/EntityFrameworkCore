using Entity_Framework_Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace Entity_Framework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDBContext appDBContext;

        public CurrencyController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet("")]
      //  public IActionResult GetAllCurrenices()
        public async Task<IActionResult> GetAllCurrenices()
        {
            /* For Currencies Table
           var result = appDBContext.Currencies.ToList(); // LINQ - one form
           var result = (from Currencies in appDBContext.Currencies select Currencies).ToList(); // LINQ - second form
           var result = await appDBContext.Currencies.ToListAsync(); // LINQ - third form 3
          var result = await (from Currencies in appDBContext.Currencies select Currencies).ToListAsync(); // LINQ - fourth form
            */

            /*var result = appDBContext.BookPrices.ToList();  //1
            //var result = await appDBContext.BookPrices.ToListAsync(); //2
           //var result = (from BookPrices in appDBContext.BookPrices select BookPrices).ToList();//3 */
            var result = await (from BookPrices in appDBContext.BookPrices select BookPrices).ToListAsync(); //4 
            // whenever you use getmethod or read method just use AsNoTracking before ToListAsync to enhance query getting from table
            return Ok(result);
        }

        [HttpGet("{id:int}")] //By Primary Key
        public async Task<IActionResult> GetcurrenciesbyId([FromRoute] int id)
        {
            var result = await appDBContext.Currencies.FindAsync(id);

            return Ok(result);
        }

        //[HttpGet("{name}")] //By any other parameter
        //public async Task<IActionResult> GetCurrenciesbyName([FromRoute]string name)
        //{
        //    //var result = await appDBContext.Currencies.Where(x => x.Title == name).FirstOrDefaultAsync();

        //    var result = await appDBContext.Currencies.FirstAsync(x => x.Title == name); // Efficient coding pattern
            
        //    /* SingleOrDefault : must have unique data ( means two same name having parameters retur in error)
        //     firstOrDefaultt : it can adjust any no of duplicate records gets record without exceptions
        //    FirstAsync : No error only takes first rows matching condition */
            
        //    return Ok(result);
        //} 

        [HttpGet("name")] // by two parameters
      //  [ HttpGet("{name}/{description}")] // by two parameters
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name, [FromQuery] string? description )
        {
            var result = await appDBContext.Currencies.FirstOrDefaultAsync(x=>x.Title == name && (string.IsNullOrEmpty(description)|| x.Description == description));
            return Ok(result);
        }

        [HttpGet("{name}/{description}")] // for getting all same rows
        public async Task<IActionResult> GetAllDuplicaterows([FromRoute] string description, [FromRoute] string name)
        {
            var result = await appDBContext.Currencies.Where(x => x.Title == name && (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync(); // this is the best for efficiency

          //  var result =  appDBContext.Currencies.ToList().Where(x => x.Title == name && (string.IsNullOrEmpty(description) || x.Description == description)); // what it does is, it brings all the rows first , from them getting row not a efficent process

            return Ok(result);
        }


       // [HttpPost("all")] // based on only inputs to get rowss
       //// [HttpGet]
       // public async Task<IActionResult> GetDBdataByNumbers([FromBody] List<int> ids )
       // {
       //   //  var ids = new List<int> { 1, 2, 3 };
       //     var result = await appDBContext.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync();

       //     return Ok(result);
       // }

        [HttpPost("all")] // to get selective columns
        public async Task<IActionResult> GetbyColumns([FromBody] List<int> ids )
        {
           var result = await appDBContext.Currencies.Where(x=>ids.Contains(x.Id) ).Select(x=> new Currencies() // Only two columns show data and remaining two null
            {
                Id = x.Id,
                Description = x.Description
            }).ToListAsync(); 

            return Ok(result);
        }


        //using foreign key properties to show data from multiple columns

        [HttpGet("GetDataUsingFk")]
        public async Task<IActionResult> GetBook()
        {
            var books = await appDBContext.Book.Select(x => new
            {
              id = x.Id,
              Title = x.Title,
              Author = x.Author != null ? x.Author.Name : "NA",
            }).ToListAsync();
            return Ok(books);
        }



    }
}
