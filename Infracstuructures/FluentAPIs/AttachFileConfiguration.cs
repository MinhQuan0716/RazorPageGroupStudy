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
    public class AttachFileConfiguration : IEntityTypeConfiguration<AttachFile>
    {
        public void Configure(EntityTypeBuilder<AttachFile> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}
