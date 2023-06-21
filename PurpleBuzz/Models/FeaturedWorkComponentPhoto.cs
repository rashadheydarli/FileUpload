﻿using System;
namespace PurpleBuzz.Models
{
	public class FeaturedWorkComponentPhoto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public int FeaturedWorkComponentId { get; set; }
		public DateTime CreatedAt { get; set; }
		public FeaturedWorkComponent FeaturedWorkComponent { get; set; }
	}
}

