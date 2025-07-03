using Microsoft.AspNetCore.Mvc;

namespace server.controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var movies = new[] {
            new { Id = 1, Title = "Inception" },
            new { Id = 2, Title = "Interstellar" }
        };
        return Ok(movies);
    }
}