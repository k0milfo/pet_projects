using MessageService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
	private readonly MessageDbContext _db;

	public MessagesController(MessageDbContext db)
	{
		_db = db;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
	{
		return await _db.Messages.ToListAsync();
	}

	[HttpPost]
	public async Task<ActionResult<Message>> SendMessage([FromBody] string text)
	{
		Console.WriteLine("Работа метода SendMessage");
		var message = new Message { 
			text = text,
			dateTime = DateTime.UtcNow
		};

		_db.Messages.Add(message);
		await _db.SaveChangesAsync();
		return Ok(message);
	}
}

