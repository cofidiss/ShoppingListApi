using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Models;
using ShoppingListApi.Filters;
using ShoppingListApi.Dependencies;
namespace ShoppingListApi.Controllers
{
    [SampleActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class GroceriesController : ControllerBase
    {
        private readonly ShoppingListContext _context;
        private readonly IMyDependency1 _MyDependency1;
        public GroceriesController(ShoppingListContext context, IMyDependency1 myDependency1) 
        {
            _context = context;
            _MyDependency1= myDependency1;  
        }

        // GET: api/Groceries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grocery>>> GetGrocery()
        {
          if (_context.Grocery == null)
          {
              return NotFound();
          }
            return await _context.Grocery.ToListAsync();
        }
        [HttpGet("/getfile")]
        public async Task<IActionResult> GetFile()
        {
            var myfile = System.IO.File.ReadAllBytes("C:/Users/sb/Desktop/yazı.pdf");
            return File(myfile, "application/octet-stream", "File Result.pd"); 
        }

        [HttpPost("/uploadfile")]
        public async Task<IActionResult> UploadFile([FromForm] IList<IFormFile> files, [FromForm] string input)
        {
          

            var myfile = System.IO.File.ReadAllBytes("C:/Users/sb/Desktop/yazı.pdf");
            return File(myfile, "application/octet-stream", "File Result.pd");
        }
        /*
         <form action="https://localhost:7080/uploadfile" method="post">
  <label for="files">Select files:</label>
  <input type="file" id="files" name="files" multiple=""><br><br>
          <label for="name">name</label>
  <input type="text" id="name" name="name" ><br>
                  <label for="age">age</label>
  <input type="text" id="age" name="age" ><br>
                  <label for="name">Select files:</label>




  <input type="submit">
</form>
         function AJAXSubmit (oFormElement) {
        event.preventDefault();
    var oReq = new XMLHttpRequest();
    oReq.onload = function(e) { 
console.log(e);
    };
    oReq.open("post", oFormElement.action);
    oReq.send(new FormData(oFormElement));
  }
        */
        // GET: api/Groceries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grocery>> GetGrocery(int id)
        {
          if (_context.Grocery == null)
          {
              return NotFound();
          }
            var grocery = await _context.Grocery.FindAsync(id);

            if (grocery == null)
            {
                return NotFound();
            }

            return grocery;
        }

        // PUT: api/Groceries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrocery(int id, Grocery grocery)
        {
            if (id != grocery.Id)
            {
                return BadRequest();
            }

            _context.Entry(grocery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Groceries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grocery>> PostGrocery(Grocery grocery)
        {
          if (_context.Grocery == null)
          {
              return Problem("Entity set 'ShoppingListContext.Grocery'  is null.");
          }
            _context.Grocery.Add(grocery);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrocery), new { id = grocery.Id }, grocery);
        }

        // DELETE: api/Groceries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrocery(int id)
        {
            if (_context.Grocery == null)
            {
                return NotFound();
            }
            var grocery = await _context.Grocery.FindAsync(id);
            if (grocery == null)
            {
                return NotFound();
            }

            _context.Grocery.Remove(grocery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroceryExists(int id)
        {
            return (_context.Grocery?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
