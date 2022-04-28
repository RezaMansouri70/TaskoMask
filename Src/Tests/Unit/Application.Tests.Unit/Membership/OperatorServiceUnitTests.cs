﻿using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskoMask.Application.Authorization.Users.Services;
using TaskoMask.Application.Membership.Operators.Services;
using TaskoMask.Application.Share.Helpers;
using TaskoMask.Application.Tests.Unit.Membership.TestData;
using TaskoMask.Application.Tests.Unit.TestData;
using TaskoMask.Domain.WriteModel.Membership.Data;
using TaskoMask.Domain.WriteModel.Membership.Entities;
using Xunit;

namespace TaskoMask.Application.Tests.Unit.Membership
{
    public class OperatorServiceUnitTests: TestsBase
    {
        #region Fields

        private IOperatorRepository _operatorRepository;
        private IRoleRepository _roleRepository;
        private IUserService _userService;
        private IOperatorService _operatorService;
        private List<Operator> _operators;

        #endregion

        #region Ctor

        //Run before each test method
        public OperatorServiceUnitTests()
        {
            FixtureSetup();
        }

        #endregion

        #region Test Methods



        [Fact]
        public async void Operator_Is_Created_Properly()
        {
            //Arrange
            var operatorDto = OperatorObjectMother.CreateNewOperatorUpsertDto();

            //Act
            var result = await _operatorService.CreateAsync(operatorDto);

            //Asserrt
            result.IsSuccess.Should().Be(true);
            var createdUser = _operators.FirstOrDefault(u => u.Id == result.Value.EntityId);
            createdUser.Email.Should().Be(operatorDto.Email);
        }



        [Fact]
        public async void Operator_Is_Updated_Properly( )
        {
            //Arrange
            var expectedEmail = "Test_2@TaskoMask.ir";
            var operatorToUpdate = _operators.First();
            var operatorDto = OperatorObjectMother.CreateNewOperatorUpsertDtoFromOperator(operatorToUpdate);

            //Act
            operatorDto.Email = expectedEmail;
            var result = await _operatorService.UpdateAsync(operatorDto);

            //Asserrt
            result.IsSuccess.Should().Be(true);
            operatorToUpdate.Email.Should().Be(expectedEmail);
        }



        #endregion

        #region Private Methods

        private void FixtureSetup()
        {

            _roleRepository = Substitute.For<IRoleRepository>();

            _userService = Substitute.For<IUserService>();
            _userService.CreateAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(Result.Success(new CommandResult(entityId: ObjectId.GenerateNewId().ToString())));
            _userService.UpdateUserNameAsync(Arg.Is<string>(arg => _operators.Select(o => o.Id).Any(id => id == arg)), Arg.Any<string>()).Returns(args => Result.Success(new CommandResult(entityId: (string)args[0])));

            _operators = GetOperatorsList();
            _operatorRepository = Substitute.For<IOperatorRepository>();
            _operatorRepository.CreateAsync(Arg.Any<Operator>()).Returns(args => AddNewOperator((Operator)args[0]));
            _operatorRepository.UpdateAsync(Arg.Is<Operator>(x => _operators.Any(u => u.Id == x.Id))).Returns(args => UpdateOperator((Operator)args[0]));
            _operatorRepository.GetByIdAsync(Arg.Is<string>(x => _operators.Any(u => u.Id == x))).Returns(args => _operators.First(u => u.Id == (string)args[0]));

            _operatorService = new OperatorService(_dummyInMemoryBus, _dummyIMapper, _dummyDomainNotificationHandler, _operatorRepository, _roleRepository, _userService);
        }



        private async Task AddNewOperator(Operator @operator)
        {
            _operators.Add(@operator);
        }



        private async Task UpdateOperator(Operator @operator)
        {
            var existOperator = _operators.FirstOrDefault(u => u.Id == @operator.Id);
            if (existOperator != null)
            {
                _operators.Remove(existOperator);
                _operators.Add(@operator);
            }
        }


        private List<Operator> GetOperatorsList()
        {
            return new List<Operator>
            {
                new Operator(ObjectId.GenerateNewId().ToString())
                {
                    Email="email_1@test.com",
                },
                 new Operator(ObjectId.GenerateNewId().ToString())
                {
                    Email="email_2@test.com",
                },
                  new Operator(ObjectId.GenerateNewId().ToString())
                {
                    Email="email_3@test.com",
                }
            };
        }



        #endregion
    }
}
