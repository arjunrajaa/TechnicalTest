using Common.Models;
using Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JsonPlaceholderApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumManager _dataManager;
    private readonly ILogger<AlbumController> _logger;
    public AlbumController(IAlbumManager manager, ILogger<AlbumController> logger)
    {
        _dataManager = manager;
        _logger = logger;
    }

    [Route("/api/AllAlbum")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAllAlbum()
    {
        _logger.LogInformation("Inside Get /api/AllAlbum");
        var albums = await _dataManager.GetCombinedData();
        _logger.LogInformation("Completed Request Get /api/AllAlbum");
        return Ok(albums);
    }

    [Route("/api/AllAlbumByUser")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsByUser(int userId)
    {
        _logger.LogInformation("Inside Get /api/AllAlbumByUser");
        var albums = await _dataManager.GetCombinedData(userId);
        if (!albums.Any())
        {
            return NoContent();
        }
        _logger.LogInformation("Completed Request Get /api/AllAlbum");
        return Ok(albums);
    }
}

