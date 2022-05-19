using Microsoft.AspNetCore.Mvc;
using RGMVC.Contracts.V1;
using RGMVC.Contracts.V1.Requests;
using RGMVC.Contracts.V1.Responses;
using RGMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGMVC.Controllers.V1
{
	public class PostsController : Controller
	{
		

		public PostsController()
		{
			
		}

		[HttpGet(ApiRoutes.Posts.GetAll)]
		public IActionResult GetAll()
		{
			return Ok(_posts);
		}

		[HttpGet(ApiRoutes.Posts.Get)]
		public IActionResult Get([FromRoute] Guid postId)
		{
			var post = _posts.SingleOrDefault(post => post.Id == postId);
			
			if (post == null)
			{
				return NotFound();
			}

			return Ok(post);
		}

		[HttpPost(ApiRoutes.Posts.Create)]
		public IActionResult Create([FromBody] CreatePostRequest postRequest)
		{
			Post post = new Post { Id = postRequest.Id};

			if (post.Id != Guid.Empty)
			{
				post.Id = Guid.NewGuid();
			}

			_posts.Add(post);

			string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

			string locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}",post.Id.ToString());

			PostRespons response = new PostRespons { Id = post.Id };

			return Created(locationUri, response);
		}






	}
}
