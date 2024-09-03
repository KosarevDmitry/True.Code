namespace True.Code.ToDoListAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ToDoItemController : ControllerBase, IToDoItemController
{
    private readonly ILogger<ToDoItemController> _logger;
    private readonly IToDoItemRepository _repository;


    public ToDoItemController(ILogger<ToDoItemController> logger, IToDoItemRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    [HttpGet]
    [Route("items/{id:int}")]
    [ProducesResponseType(typeof(ToDoItemCTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ToDoItemCTO?>> ItemByIdAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var item = await _repository.GetById(id);

        if (item != null)
        {
            return Ok(item);
        }

        return NotFound();
    }


    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreatedResult), (int)HttpStatusCode.Created)]
    public async Task<ActionResult> AddAsync(ToDoItemAddCTO toDoItemCTO)
    {
        var item = new ToDoItem
        {
            Title = toDoItemCTO?.Title,
            Description = toDoItemCTO?.Description,
            DueDate = toDoItemCTO?.DueDate,
            Level = toDoItemCTO?.Level,
            IsCompleted = toDoItemCTO?.IsCompleted,
            UserId = toDoItemCTO?.UserId
        };

        var result = await _repository.Add(item);

        return Created(new Uri(Request.Path, UriKind.Relative), item);
    }


    [HttpPut]
    [ProducesResponseType(typeof(ToDoItemCTO), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateAsync(ToDoItemCTO item)
    {
        ToDoItem response = await _repository.Update(item);


        var toDoItemCTO = new ToDoItemCTO()
        {
            Id = response.Id,
            Title = response.Title,
            Description = response.Description,
            UserId = response.UserId,
            IsCompleted = response.IsCompleted,
            Level = response.Level,
            DueDate = response.DueDate,
            Created = response.Created
        };
        return Accepted(toDoItemCTO);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> DeleteById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _repository.Delete(id);
        if (result) return NoContent();

        return NotFound();
    }
}
