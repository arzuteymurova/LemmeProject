using AutoMapper;
using LemmeProject.Application.DTOs.Images;
using LemmeProject.Application.Helpers;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace LemmeProject.Application.Services.Concrete
{
    public class RoomImageService : IProductImageService
    {
        private readonly IProductImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly FileServerPath _fileServerPath;
        public RoomImageService(IProductImageRepository imageRepository, IMapper mapper, IOptionsSnapshot<FileServerPath> fileServerPath)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _fileServerPath = fileServerPath.Value;
        }

        public async Task AddAsync(ImageAddRequest imageAddRequest)
        {
            Image image = _mapper.Map<Image>(imageAddRequest);
            image.FileName = imageAddRequest.File.FileName;
            image.FilePath = _fileServerPath.Path;
            await _imageRepository.CreateAsync(image);
        }
    }
}
