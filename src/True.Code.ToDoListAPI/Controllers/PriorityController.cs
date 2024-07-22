using Microsoft.Extensions.DependencyModel;

namespace True.Code.ToDoListAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PriorityController : ControllerBase
{
    private readonly IPriorityRepository _repository;
    private readonly ILogger<PriorityController> _logger;

    public PriorityController(ILogger<PriorityController> logger, IPriorityRepository repository)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GePriority()
    {
        var priorities = await _repository.Get();
        var result = priorities.Select(x => x.Level);
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> AddPriority(Priority priority)
    {
        await _repository.Add(priority);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("range")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> AddRange(int[] levels)
    {
        var priorities = levels.Select(l => new Priority { Level = l });
        await _repository.AddRange(priorities);
        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpDelete("{level}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> DeletePriority(int level)
    {
        var priority = new Priority { Level = level };
        var result = await _repository.Delete(priority);
        if (result) return NoContent();

        return NotFound();
    }
}