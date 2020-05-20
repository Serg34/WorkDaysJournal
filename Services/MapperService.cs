using AutoMapper;
using SC.Common.Model;

namespace SC.Common.Services
{
	public class MapperService
	{
		private static readonly MapperConfiguration _config;
		static MapperService()
		{
			_config = new MapperConfiguration(
				cfg =>
				{
					cfg.CreateMap<ResOPViewModel, CResOPDto>()
						.ForMember(d => d.ID,
							o => o.MapFrom(s => s.ResOP_ID));

					cfg.CreateMap<ResOPViewModel, CResourceDto>()
						.ForMember(d => d.ID,
							o => o.MapFrom(s => s.Resource_ID))
						.ForMember(d => d.Manager_ID,
							o => o.MapFrom(s => s.IsStaff ? s.ManagerID : null));

					cfg.CreateMap<CUserDto, CResourceDto>()
						.ForMember(d => d.ID,
							o => o.MapFrom(s => 0))
						.ForMember(d => d.User_ID,
							o => o.MapFrom(s => s.ID));

					cfg.CreateMap<CUserDto, CUser>();
					cfg.CreateMap<CUser, CUserDto>();

					cfg.CreateMap<CRequestOp, CRequestOpDto>();

					cfg.CreateMap<CPostupleniya, CNakladLine>()
						.ForMember(d => d.Postup_ID,
							o => o.MapFrom(s => s.ID))
						.ForMember(d => d.Quantity,
							o => o.MapFrom(s => s.QuantityOnSklad));

					cfg.CreateMap<CNakladnayaDto, CNakladnaya>();
					cfg.CreateMap<CNakladLineDto, CNakladLine>();
					cfg.CreateMap<CNakladLine, CPostupleniyaDto>()
						.ForMember(d => d.ID,
							o => o.MapFrom(s => s.Postup_ID));

					cfg.CreateMap<CSchetViewModel, CSchetDto>();
					cfg.CreateMap<COperation, COperationDto>();

					cfg.CreateMap<COperationDto, COperationHistoryDto>()
						.ForMember(d => d.ID,
							o => o.MapFrom(s => 0))
						.ForMember(d => d.Op_ID,
						o => o.MapFrom(s => s.ID))
						.ForMember(d => d.Op_dtc,
							o => o.MapFrom(s => s.dtc));

					cfg.CreateMap<COplata, COplataDto>();
					
					cfg.CreateMap<CNoteDto, CNote>();
					cfg.CreateMap<CNote, CNoteDto>();

					cfg.CreateMap<CStRash, CStRashDto>();
					cfg.CreateMap<CStRashDto, CStRash>();

					cfg.CreateMap<CDetail, CDetailDto>();
					cfg.CreateMap<CDetailDto, CDetail>();
				});
		}
		public static T Map<T>(object obj) => _config.CreateMapper().Map<T>(obj);
	}
}
