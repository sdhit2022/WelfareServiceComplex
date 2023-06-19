﻿using Application.BaseData.Dto;
using AutoMapper;
using Domain.ComplexModels;

namespace Application.BaseData
{
    internal class BaseDataMapping:Profile
    {
        public BaseDataMapping()
        {
            //unit
            this.CreateMap<CreateUnit, UnitOfMeasurement>()
                .ForMember(x => x.UomUid, opt => opt.MapFrom(x => x.Id == Guid.NewGuid()))
                .ForMember(x => x.UomName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.UomCode, opt => opt.MapFrom(x => x.Code))
                .ForMember(x => x.UomStatus, opt => opt.MapFrom(x => x.Status));
           
            this.CreateMap<UnitOfMeasurement, UnitDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.UomUid))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.UomName))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.UomCode))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.UomStatus));
           
            this.CreateMap<EditUnit, UnitOfMeasurement>()
                .ForMember(x => x.UomUid, opt => opt.MapFrom(x => x.Id == Guid.NewGuid()))
                .ForMember(x => x.UomName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.UomCode, opt => opt.MapFrom(x => x.Code))
                .ForMember(x => x.UomStatus, opt => opt.MapFrom(x => x.Status));

            //wareHouse
             this.CreateMap<CreateWareHouse, WareHouse>()
                .ForMember(x => x.WarHosUid, opt => opt.MapFrom(x => x.Id == Guid.NewGuid()))
                .ForMember(x => x.WarHosName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.WarHosCode, opt => opt.MapFrom(x => x.Code))
                .ForMember(x => x.WarHosStatus, opt => opt.MapFrom(x => x.Status));


        }
    }
}
