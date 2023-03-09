using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Department - Get all departments from the database 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
    {
        return await _context.Department.ToListAsync();
    }

    // PUT: api/Department/5 - Update an existing department in the database 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment([FromRoute] int id, Department department)
    {
        if (id != department.DepartmentId)
        {
            return BadRequest();
        }
        // Update an existing department in the database
        _context.Entry(department).State = EntityState.Modified;

        // Save changes to the database if there are any problem with the update then return Return a 404 error if the department does not exist
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(id))
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

    // POST: api/Department - Add a new department to the database and return the newly created department
    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        // Adding new department to the database
        _context.Department.Add(department);
        await _context.SaveChangesAsync();

        // Return the newly created department
        return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
    }


    // DELETE: api/Department/5 - Delete a department from the database 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
    {
        // Created a variable to store the department that is being deleted
        var department = await _context.Department.FirstOrDefaultAsync(d => d.DepartmentId == id);

        // Return a 404 error if the department does not exist
        if (department == null)
        {
            return NotFound();
        }

        // Delete the department from the database
        _context.Department.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Created a private method to check if the department exists in the database
    private bool DepartmentExists(int id)
    {
        return _context.Department.Any(d => d.DepartmentId == id);
    }
}
