﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RGMVC.Domain
{

	public class Post
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
