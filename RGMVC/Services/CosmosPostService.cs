using Cosmonaut;
using Cosmonaut.Extensions;
using Cosmonaut.Response;
using RGMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGMVC.Services
{
	public class CosmosPostService : IPostService
	{
		private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

		public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
		{
			_cosmosStore = cosmosStore;
		}

		public async Task<bool> CreatePostAsync(Post post)
		{
			CosmosPostDto cosmosPost = new CosmosPostDto
			{
				Id = Guid.NewGuid().ToString(),
				Name = post.Name
			};

			CosmosResponse<CosmosPostDto> response = await _cosmosStore.AddAsync(cosmosPost);

			post.Id = Guid.Parse(cosmosPost.Id);

			return response.IsSuccess;

		}

		public async Task<bool> DeletePostAsync(Guid postId)
		{
			CosmosResponse<CosmosPostDto> response = await _cosmosStore.RemoveByIdAsync(postId.ToString(), (postId.ToString()));

			return response.IsSuccess; 
		}

		public async Task<Post> GetPostByIdAsync(Guid postId)
		{
			var post = await _cosmosStore.FindAsync(postId.ToString());

			if (post == null)
			{
				return null;
			}

			return new Post { Id = Guid.Parse(post.Id), Name = post.Name };
		}

		public async Task<List<Post>> GetPostsAsync()
		{
			List<CosmosPostDto> posts = await _cosmosStore.Query().ToListAsync();

			return posts.Select(post => new Post { Id = Guid.Parse(post.Id), Name = post.Name}).ToList();
		}

		public async Task<bool> UpdatePostAsync(Post postToUpdate)
		{
			CosmosPostDto cosmosPost = new CosmosPostDto
			{
				Id = postToUpdate.Id.ToString(),
				Name = postToUpdate.Name
			};

			CosmosResponse<CosmosPostDto> response = await _cosmosStore.UpdateAsync(cosmosPost);

			return response.IsSuccess;
		}
	}
}
