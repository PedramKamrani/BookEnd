using BookEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Publisher = BookEnd.Models.Publisher;

namespace BookEnd.Mapping
{
    public class PublisherMap: IEntityTypeConfiguration<Publisher>
    {

        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            entity.HasKey(p => p.PublisherId);
            
        }
    }
}
