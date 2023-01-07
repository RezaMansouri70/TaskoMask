﻿using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DbContext;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TaskoMask.Services.Owners.Read.Api.Consumers.Projects
{
    public class ProjectUpdatedConsumer : BaseConsumer<ProjectUpdated>
    {
        private readonly OwnerReadDbContext _ownerReadDbContext;


        public ProjectUpdatedConsumer(IInMemoryBus inMemoryBus, OwnerReadDbContext ownerReadDbContext) : base(inMemoryBus)
        {
            _ownerReadDbContext = ownerReadDbContext;
        }


        public override async Task ConsumeMessage(ConsumeContext<ProjectUpdated> context)
        {
            var project = await _ownerReadDbContext.Projects.Find(e => e.Id == context.Message.Id).FirstOrDefaultAsync();

            project.Name = context.Message.Name;
            project.Description = context.Message.Description;
            project.SetAsUpdated();

            await _ownerReadDbContext.Projects.ReplaceOneAsync(p => p.Id == project.Id, project, new ReplaceOptions() { IsUpsert = false });
        }
    }
}
