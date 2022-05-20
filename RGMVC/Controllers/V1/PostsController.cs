using Microsoft.AspNetCore.Mvc;
using RGMVC.Contracts.V1;
using RGMVC.Contracts.V1.Requests;
using RGMVC.Contracts.V1.Responses;
using RGMVC.Domain;
using RGMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGMVC.Controllers.V1
{
	public class PostsController : Controller
	{
		private readonly IPostService _postService;

		public PostsController(IPostService postService)
		{
			_postService = postService;
		}


		[HttpGet(ApiRoutes.Posts.GetAll)]
		public async Task<IActionResult> GetAll()
		{
			return Ok( await _postService.GetPostsAsync());
		}

		[HttpPut(ApiRoutes.Posts.Update)]
		public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody]UpdatePostRequest request)
		{

			Post post = new Post
			{
				Id = postId,
				Name = request.Name
			};

			bool updated = await _postService.UpdatePostAsync(post);

			if (updated)
			{
				return Ok(post);
			}

			return NotFound();
		}

		[HttpDelete(ApiRoutes.Posts.Delete)]
		public async Task<IActionResult> Delete([FromRoute] Guid postId)
		{
			bool deleted = await _postService.DeletePostAsync(postId);

			if (deleted)
			{
				return NoContent();
			}

			return NotFound();

		}


		[HttpGet(ApiRoutes.Posts.Get)]
		public async Task<IActionResult> Get([FromRoute] Guid postId)
		{
			Post post = await _postService.GetPostByIdAsync(postId);
			
			if (post == null)
			{
				return NotFound();
			}

			return Ok(post);
		}

		[HttpPost(ApiRoutes.Posts.Create)]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
		{
			Post post = new Post { Name  = postRequest.Name};

			if (post.Id != Guid.Empty)
			{
				post.Id = Guid.NewGuid();
			}

			await _postService.CreatePostAsync(post);

			string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

			string locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}",post.Id.ToString());

			PostRespons response = new PostRespons { Id = post.Id };

			return Created(locationUri, response);
		}






	}
}
