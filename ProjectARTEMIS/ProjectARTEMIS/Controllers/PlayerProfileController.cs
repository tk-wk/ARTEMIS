using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System.Security.Claims;
using SkiaSharp;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/v1/players")]
    [Authorize]
    public class PlayerProfileController : ControllerBase
    {
        private readonly PlayerProfileService _profileService;
        private readonly IWebHostEnvironment _environment;

        public PlayerProfileController(PlayerProfileService profileService, IWebHostEnvironment environment)
        {
            _profileService = profileService;
            _environment = environment;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<PlayerProfileDto>> GetProfile(string username)
        {
            try
            {
                var profile = await _profileService.GetPlayerProfileAsync(username);
                return Ok(profile);
            }
            catch (Exception ex) when (ex is NullReferenceException || ex is KeyNotFoundException)
            {
                return NotFound(new { Message = $"Player profile for user '{username}' was not found." });
            }
        }

        [HttpPut("details")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdatePlayerProfileRequest request)
        {
            var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(nameIdentifierClaim) || !Guid.TryParse(nameIdentifierClaim, out var authenticatedUserId))
                return Forbid();

            try
            {
                await _profileService.UpdateProfileDetails(request);
                return Ok(new { Message = "Profile details updated successfully." });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }


        [HttpPost("upload-picture")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfilePictureForm form)
        {
            var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(nameIdentifierClaim) || !Guid.TryParse(nameIdentifierClaim, out var authenticatedUserId))
                return Forbid();

            if (form.ProfilePicture == null || form.ProfilePicture.Length == 0)
                return BadRequest(new { Message = "No file stream was provided." });

            const long maxBytes = 8 * 1024 * 1024;
            if (form.ProfilePicture.Length > maxBytes)
                return BadRequest(new { Message = "Image is too large (max 8 MB)." });

            try
            {
                var folderName = Path.Combine(_environment.WebRootPath, "images", "profiles");
                if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);

                var uniqueFileName = $"{authenticatedUserId}_{DateTime.UtcNow.Ticks}.jpg";
                var targetDiskPath = Path.Combine(folderName, uniqueFileName);

                using var stream = form.ProfilePicture.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                ms.Position = 0;

                using var original = SKBitmap.Decode(ms);
                if (original == null)
                    return BadRequest(new { Message = "That file doesn't appear to be a valid image." });

                const int size = 512;
                var cropSize = Math.Min(original.Width, original.Height);
                var cropX = (original.Width - cropSize) / 2;
                var cropY = (original.Height - cropSize) / 2;

                using var cropped = new SKBitmap(cropSize, cropSize);
                using (var canvas = new SKCanvas(cropped))
                {
                    canvas.DrawBitmap(original,
                        SKRect.Create(cropX, cropY, cropSize, cropSize),
                        SKRect.Create(0, 0, cropSize, cropSize));
                }

                using var resized = cropped.Resize(new SKImageInfo(size, size), SKSamplingOptions.Default);
                if (resized == null)
                    return BadRequest(new { Message = "Could not process this image." });

                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 85);

                await using var fileStream = new FileStream(targetDiskPath, FileMode.Create);
                data.SaveTo(fileStream);

                var webRoutePath = $"/images/profiles/{uniqueFileName}";

                // Pass UserId — service does the profile lookup itself
                await _profileService.UpdateProfilePicture(authenticatedUserId, webRoutePath);

                return Ok(new { Message = "Profile picture updated successfully.", Path = webRoutePath });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}

public record UploadProfilePictureForm
{
    [FromForm(Name = "profilePicture")]
    public IFormFile ProfilePicture { get; set; } = null!;
}
