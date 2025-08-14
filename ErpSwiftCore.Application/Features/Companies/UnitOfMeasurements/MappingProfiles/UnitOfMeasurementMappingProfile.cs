using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for UnitOfMeasurement entity and related DTOs.
    /// </summary>
    public class UnitOfMeasurementMappingProfile : Profile
    {
        public UnitOfMeasurementMappingProfile()
        {
            // ───────── UnitOfMeasurementCreateDto ↔ UnitOfMeasurement ─────────
            CreateMap<UnitOfMeasurementCreateDto, UnitOfMeasurement>().ReverseMap();

            // ───────── UnitOfMeasurementUpdateDto ↔ UnitOfMeasurement ─────────
            CreateMap<UnitOfMeasurementUpdateDto, UnitOfMeasurement>().ReverseMap();

            // ───────── UnitOfMeasurement ↔ UnitOfMeasurementDto (Read) ─────────
            CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>().ReverseMap();

            // ───────── Additional reverse mappings (if needed) ─────────
            CreateMap<UnitOfMeasurementDto, UnitOfMeasurementCreateDto>().ReverseMap();
            CreateMap<UnitOfMeasurementDto, UnitOfMeasurementUpdateDto>().ReverseMap();
        }
    }
}