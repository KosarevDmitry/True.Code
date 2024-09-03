namespace True.Code.ToDoListAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase, IUserController
{
    private readonly IUserRepository         _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository repository, ILogger<UserController> logger)
    {
        _logger     = logger;
        _repository = repository;
    }


    [HttpGet]
    [Route("items")]
    [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var items = await _repository.GetAsync();
        if (items.Any())
        {
            return Ok(items);
        }

        return NotFound();
    }


    [HttpGet]
    [Route("items/{id:int}")]
    [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IEnumerable<User>>> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item != null)
        {
            return Ok(item);
        }

        return NotFound();
    }


    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<ActionResult> AddAsync(string username)
    {
        var user = new User { Name = username };
        await _repository.AddAsync(user);

        var result = new UserRec(user.Id, user.Name);
        return Created(new Uri(Request.Path, UriKind.Relative), result);
    }

    private record UserRec(int id, string name);
}
