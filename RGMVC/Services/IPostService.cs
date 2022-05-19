using RGMVC.Domain;
using System;
using System.Collections.Generic;

namespace RGMVC.Services
{
	public interface IPostService
	{
		List<Post> GetPosts();

		Post GetPostById(Guid postId);
	}
}
