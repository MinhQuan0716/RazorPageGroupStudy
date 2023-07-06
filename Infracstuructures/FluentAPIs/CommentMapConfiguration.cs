using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.FluentAPIs
{
    public class CommentMapConfiguration : IEntityTypeConfiguration<CommentMap>
    {
        public void Configure(EntityTypeBuilder<CommentMap> builder)
        {
            builder.HasKey(x=>x.MapId);
            builder.HasOne(x=>x.ParentComment).WithMany(co=>co.ParentComment).HasForeignKey(x=>x.ParentCommentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x=>x.SubComment).WithMany(co=>co.ChildComment).HasForeignKey(x=>x.SubCommentId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
