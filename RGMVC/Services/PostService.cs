using Microsoft.EntityFrameworkCore;
using RGMVC.Data;
using RGMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGMVC.Services
{
	public class PostService : IPostService
	{
		private readonly DataContext _dataContext;

		public PostService(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<bool> DeletePostAsync(Guid postId)
		{
			Post post = await GetPostByIdAsync(postId);

			if (post == null)
			{
				return false;
			}

			 _dataContext.Posts.Remove(post);

			int deleted = await _dataContext.SaveChangesAsync();

			return deleted > 0;
		}

		public async Task<bool> CreatePostAsync(Post post)
		{
			await _dataContext.Posts.AddAsync(post);
			
			int created = await _dataContext.SaveChangesAsync();

			return created > 0;
		}

		public async Task< Post> GetPostByIdAsync(Guid postId)
		{
			return await _dataContext.Posts.SingleOrDefaultAsync(post => post.Id == postId);
		}

		public async Task< List<Post>> GetPostsAsync()
		{
			return await _dataContext.Posts.ToListAsync();
		}

		public async Task<bool> UpdatePostAsync(Post postToUpdate)
		{
			 _dataContext.Posts.Update(postToUpdate);

			int updated = await _dataContext.SaveChangesAsync();

			return updated > 0;

		}
	}
}
