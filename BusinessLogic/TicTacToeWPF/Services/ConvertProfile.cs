using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.BLL.Structures;
using TicTacToeWPF.Models;
using XOGame3D.Interfaces;

namespace TicTacToeWPF.Services
{
    class ConvertProfile : Profile
    {
        public ConvertProfile()
        {
            CreateMap<IArea, BigAreaModel>()
                 .ForMember(x => x.AreaState,
                    opt => opt.MapFrom(x => x.State));

            CreateMap<ICell, CellModel>()
                .ForMember(x => x.Coordinates, 
                    opt=> opt.MapFrom(x=> new Coordinates {
                        CoordX = x.Column,
                        CoordY = x.Row
                    }) )
                .ForMember(x => x.CellState,
                    opt => opt.MapFrom(x => x.State));

            CreateMap<IArea, MiniAreaModel>()
                .ForMember(x => x.Coordinates,
                    opt => opt.MapFrom(x => new Coordinates
                    {
                        CoordX = (x as ICell).Column,
                        CoordY = (x as ICell).Row
                    } ))
                .ForMember(x => x.AreaState,
                    opt => opt.MapFrom(x => x.State))
                .ForMember(x => x.CellState, 
                    opt => opt.MapFrom(x => x.State));
        }
    }
}
