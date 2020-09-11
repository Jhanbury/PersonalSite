using System;
using AutoMapper;
using NUnit.Framework;
using Site.Application.CareerTimeLine.Models;
using Site.Application.Certifications.Models;
using Site.Application.Company.Models;
using Site.Application.Education.Model;
using Site.Application.Hobbies.Model;
using Site.Application.Infrastructure.AutoMapper;
using Site.Application.SocialMediaAccounts.Models;
using Site.Application.Users.Models;
using Site.Domain.Entities;

namespace Site.Application.Tests.AutoMapper
{
    [TestFixture]
    public class AutoMapperConfigTests
    {
        private IMapper _mapper;
        private const string _country = "Ireland";
        private const string _city = "Galway";
        private const string _platform = "Github";
        private const string _type = "Sport";
        private User _user = new User
        {
            Address = new Address
            {
                City = _city,
                Country = _country
            }
        };
        private SocialMediaAccount _account = new SocialMediaAccount()
        {
            SocialMediaPlatform = new SocialMediaPlatform
            {
                Name = _platform
            }
        };
        private Hobby _hobby = new Hobby()
        {
            HobbyType = new HobbyType
            {
                Type = _type
            }
        };

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = new Mapper(config);
        }

        [Test]
        public void Test_User_CurrentLocation_Mapping()
        {
            var actual = _mapper.Map<UserDto>(_user);
            var expected = $"{_city}, {_country}";

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.CurrentLocation);
        }

        [Test]
        public void Test_SocialAccount_Platform_Mapping()
        {
            var actual = _mapper.Map<SocialMediaAccountDto>(_account);
            var expected = _platform;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Platform);
        }

        [Test]
        public void Test_Hobby_Type_Mapping()
        {
            var actual = _mapper.Map<HobbyDto>(_hobby);
            var expected = _type;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Type);
        }


        [Test]
        [TestCase(typeof(SocialMediaAccount), typeof(SocialMediaAccountDto))]
        [TestCase(typeof(User), typeof(UserDto))]
        [TestCase(typeof(Hobby), typeof(HobbyDto))]
        [TestCase(typeof(UserDegree), typeof(UserDegreeDto))]
        [TestCase(typeof(Certification), typeof(CertificationDto))]
        [TestCase(typeof(UserDegree), typeof(CareerTimeLineDto))]
        [TestCase(typeof(UserWorkExperience), typeof(CareerTimeLineDto))]
        [TestCase(typeof(UserCertification), typeof(CareerTimeLineDto))]
        public void TestConfig(Type source, Type dest)
        {
          var instance = Activator.CreateInstance(source);

          _mapper.Map(instance, source, dest);
        }

    }
}
