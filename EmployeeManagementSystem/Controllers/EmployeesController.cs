using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    // Inject the AppDbContext into the controller using dependency injection 
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }
    // GET: api/Employee - Get all employees from the database 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        return await _context.Employee.Include(x => x.department).ToListAsync();
    }

    // GET: api/Employee/5 - Get an employee by id from the database 
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await _context.Employee.Include(e => e.department).FirstOrDefaultAsync(e => e.Id == id);

        // Return a 404 error if the employee does not exist
        if (employee == null)
        {
            return NotFound();
        }

        return employee;
    }

    // POST: api/Employee - Add a new employee to the database and return the newly created employee
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        employee.department = null;
        _context.Employee.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
    }

    // PUT: api/Employee/5 - Update an existing employee in the database
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee([FromRoute] int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }
        employee.department = null;
        // Update an existing employee in the database
        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
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

    // DELETE: api/Employee/5 - Delete a department from the database 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    {
        var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);
        // if employee is not found then return a 404 error
        if (employee == null)
        {
            return NotFound();
        }

        // Delete an employee from the database
        _context.Employee.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Created a private method to check if the employee exists in the database
    private bool EmployeeExists(int id)
    {
        return _context.Employee.Any(e => e.Id == id);
    }
}
