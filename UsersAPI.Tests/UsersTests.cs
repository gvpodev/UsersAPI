using System.Net;
using Bogus;
using FluentAssertions;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;

namespace UsersAPI.Tests
{
    public class UsersTests
    {
        [Fact]
        public async Task Users_Post_Returns_Created()
        {
            var faker = new Faker("pt_BR");
            var request = new UserAddRequestDto
            {
                Name = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Password = "qhMWP{QoLf{^8&qeeHa<EqFk<",
                ConfirmationPassword = "qhMWP{QoLf{^8&qeeHa<EqFk<"
            };
            var content = TestHelper.CreateContent(request);

            var result = await TestHelper.CreateClient.PostAsync("api/users", content);

            result.StatusCode.Should().Be(HttpStatusCode.Created);
            var response = TestHelper.ReadResponse<UserResponseDto>(result);
            response.Id.Should().NotBeEmpty();
            response.Name.Should().Be(request.Name);
            response.Email.Should().Be(request.Email);
            response.CreatedAt.Should().NotBeNull();
        }

        [Fact(Skip = "Not implemented")]
        public void Users_Post_Returns_BadRequest()
        {
            //TODO
        }

        [Fact(Skip = "Not implemented")]
        public void Users_Put_Returns_Ok()
        {
            //TODO
        }

        [Fact(Skip = "Not implemented")]
        public void Users_Delete_Returns_Ok()
        {
            //TODO
        }

        [Fact(Skip = "Not implemented")]
        public void Users_Get_Returns_Ok()
        {
            //TODO
        }

    }
}
