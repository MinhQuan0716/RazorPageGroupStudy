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
    public class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
    {
        public void Configure(EntityTypeBuilder<GroupRole> builder)
        {
            builder.HasData(new GroupRole
            {
                Id= 1,
                Name=nameof(GroupRoleEnum.GroupAdmin)
            });
            builder.HasData(new GroupRole
            {
                Id=2,
                Name=nameof(GroupRoleEnum.GroupModerators)
            });
            builder.HasData(new GroupRole
            {
                Id=3,
                Name=nameof(GroupRoleEnum.GroupMember)
            });
        }
    }
}
