using AutoMapper;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Models;

namespace MyCompany.FileSharingApp.MVC.Mapping.AutoMapper
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<FolderModel, Folder>().ReverseMap();
            CreateMap<FileModel, MyCompany.FileSharingApp.Entities.Concrete.File>().ReverseMap();
        }
    }
}
