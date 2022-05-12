using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Test.Controllers;

[TestClass]
public class TokenControllerTest
{
  private static readonly Guid TEST_USER_ID = Guid.NewGuid();
  
  private readonly Mock<ITokenService> _tokenServiceMock = new();
  private readonly Token _token = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Token #1",
    UserId = TEST_USER_ID,
    LastEdited = DateTime.Now,
    Value = "TestToken"
  };
  private readonly ClaimsPrincipal _user = new(new ClaimsIdentity(new[]
  {
    new Claim(ClaimTypes.NameIdentifier, TEST_USER_ID.ToString())
  }, "mock"));
  
  [TestMethod]
  public void CanCreateTokenController()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object);
    Assert.IsNotNull(tokenController);
  }
  
  [TestMethod]
  public async Task CanGetTokenNamesByUser_Zero()
  {
    var tokenNames = new List<string>(); 
    var actionResult = new ActionResult<IEnumerable<string>>(tokenNames);
    _tokenServiceMock.Setup(service => service.GetTokenNamesByUserId(TEST_USER_ID)).ReturnsAsync(actionResult);
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    Assert.AreSame(actionResult, actionResultReceived);
    Assert.IsNull(actionResultReceived.Value?.GetEnumerator().Current);
  }

  [TestMethod]
  public async Task CanGetTokenNamesByUser_One()
  {
    var tokenNames = new List<string> {_token.Name!};
    var actionResult = new ActionResult<IEnumerable<string>>(tokenNames);
    _tokenServiceMock.Setup(service => service.GetTokenNamesByUserId(TEST_USER_ID)).ReturnsAsync(actionResult);
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetTokenNamesByUser_Multiple()
  {
    Token token2 = new()
    {
      Id = Guid.NewGuid(),
      Name = "Demo Token #2",
      UserId = TEST_USER_ID,
      LastEdited = DateTime.Now,
      Value = "TestToken"
    };
    var tokenNames = new List<string> {_token.Name!, token2.Name!};
    var actionResult = new ActionResult<IEnumerable<string>>(tokenNames);
    _tokenServiceMock.Setup(service => service.GetTokenNamesByUserId(TEST_USER_ID)).ReturnsAsync(actionResult);
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResultReceived = await tokenController.GetToken(_token.Id);
    var tokenReceived = actionResultReceived.Value;
    Assert.AreSame(_token, tokenReceived);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingToken()
  {
    var newId = Guid.NewGuid();
    _tokenServiceMock.Setup(service => service.GetTokenById(newId)).ReturnsAsync(new ActionResult<Token?>((Token?) null));
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResult = await tokenController.GetToken(newId);
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPutToken()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(true);
    _tokenServiceMock.Setup(service => service.PutToken(_token)).Returns(new ActionResult<Token>(_token));
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };
    
    var actionResult = await tokenController.PutToken(_token.Id, _token);
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Token>));

    var token = actionResult.Value;
    Assert.AreSame(_token, token);
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(Guid.NewGuid(), _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }
  
  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdIsNull()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(null, _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsUnauthorizedIfTokenIsNotOwnedByUser()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(false);
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(_token.Id, _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(UnauthorizedResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateConcurrencyException))]
  public async Task Put_ThrowsExceptionIfAMatchingTokenExistsButCouldNotBeOverriden()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(true);
    _tokenServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _tokenServiceMock.Setup(service => service.TokenExists(_token.Id)).Returns(true);
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    await tokenController.PutToken(_token.Id, _token);
  }


  [TestMethod]
  public async Task CanPostToken()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PostToken(_token);
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = actionResult.Result as CreatedAtActionResult;

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_token, createdAtAction.Value);
    Assert.AreEqual(_token.UserId, TEST_USER_ID);
  }

  [TestMethod]
  public async Task Post_ReturnsBadRequestIfTokenAlreadyExists()
  {
    _tokenServiceMock.Setup(service => service.TokenExists(_token.Id)).Returns(true);
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResult = await tokenController.PostToken(_token);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }
  
  [TestMethod]
  public async Task Post_ReturnsBadRequestIfUserHasEquallyNamedToken()
  {
    _tokenServiceMock.Setup(service => service.HasUserEquallyNamedToken(_token.UserId, _token.Name!)).Returns(true);
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResult = await tokenController.PostToken(_token);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task CanDeleteToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResult = await tokenController.DeleteToken(_token.Id);
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Delete_ReturnsNotFoundIfThereIsNoMatchingToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object);

    var actionResult = await tokenController.DeleteToken(Guid.NewGuid());
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
  }
}