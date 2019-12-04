using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Site.Application.Interfaces;
using Site.Application.Projects.Model;
using Site.Application.Skills.Model;
using Site.Application.Technologies.Models;
using Site.Domain.Entities;

namespace Site.Application.Projects.Queries
{
    public class GetUserProjectsQueryHandler : IRequestHandler<GetUserProjectsQuery, List<ProjectDto>>
    {
        private readonly IRepository<Project, int> _projectRepository;
        private readonly IRepository<ProjectSkill, int> _projectSkillsRepository;
        private readonly IRepository<ProjectTechnology, int> _projectTechRepository;
        private readonly IMapper _mapper;

        public GetUserProjectsQueryHandler(IRepository<Project, int> repository, IRepository<ProjectTechnology, int> techRepo, IRepository<ProjectSkill, int> skillsrepo, IMapper mapper)
        {
            _projectRepository = repository;
            _projectSkillsRepository = skillsrepo;
            _projectTechRepository = techRepo;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetIncluding(x => x.UserId.Equals(request.UserId),
                x => x.ProjectSkills,
                x => x.ProjectTechnologys);
            var projectIds = projects.Select(x => x.Id);
            var projectSkills = await _projectSkillsRepository.GetIncluding(x => projectIds.Contains(x.ProjectId), x => x.Skill);
            var projectTech = await _projectTechRepository.GetIncluding(x => projectIds.Contains(x.ProjectId), x => x.Technology);

            var projectDtos = projects.Select(x => _mapper.Map<ProjectDto>(x)).ToList();
            foreach (var projectDto in projectDtos)
            {
                projectDto.Skills = projectSkills.Where(x => x.ProjectId.Equals(projectDto.Id))
                    .Select(x => _mapper.Map<SkillDto>(x.Skill));
                projectDto.Technologies = projectTech.Where(x => x.ProjectId.Equals(projectDto.Id))
                    .Select(x => _mapper.Map<TechnologyDto>(x.Technology));
            }

            return projectDtos;


        }
    }
}
