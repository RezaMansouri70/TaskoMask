﻿using System.ComponentModel.DataAnnotations;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Resources;

namespace TaskoMask.BuildingBlocks.Contracts.Dtos.Tasks
{
    public class UpdateTaskDto
    {
        public string Id { get; set; }


        [Display(Name = nameof(ContractsMetadata.Title), ResourceType = typeof(ContractsMetadata))]
        [StringLength(DomainConstValues.Task_Title_Max_Length, MinimumLength = DomainConstValues.Task_Title_Min_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        [Required(ErrorMessageResourceName = nameof(ContractsMetadata.Required), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string Title { get; set; }


        [Display(Name = nameof(ContractsMetadata.Description), ResourceType = typeof(ContractsMetadata))]
        [MaxLength(DomainConstValues.Task_Description_Max_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Max_Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string Description { get; set; }
    }
}
