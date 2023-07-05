using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.FluentAPIs
{
	public class PostStatusConfiguration : IEntityTypeConfiguration<PostStatus>
	{
		public void Configure(EntityTypeBuilder<PostStatus> builder)
		{
			builder.HasData(new PostStatus
			{
				PostStatusId=1,
				PostStatusName=nameof(PostStatusEnum.Approved)
			},
			new PostStatus
			{
				PostStatusId=2,
				PostStatusName= nameof(PostStatusEnum.Pending)
			},
			new PostStatus
			{
				PostStatusId=3,
				PostStatusName=nameof(PostStatusEnum.Banned)
			}
			);

		}
	}
}
